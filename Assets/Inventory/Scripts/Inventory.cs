using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Inventories
{
    public sealed class Inventory : IEnumerable<Item>
    {
        public event Action<Item, Vector2Int> OnAdded;
        public event Action<Item, Vector2Int> OnRemoved;
        public event Action<Item, Vector2Int> OnMoved;
        public event Action OnCleared;

        private readonly int width;
        private readonly int height;

        private readonly Item[,] inventoryCells;

        public int Width => width;
        public int Height => height;
        public int Count => inventoryItems.Count;

        private readonly Dictionary<Item, Vector2Int> inventoryItems = new Dictionary<Item, Vector2Int>();

        public Inventory(in int width, in int height)
        {
            if(width < 0 || height < 0 || width == 0 && height == 0)
                throw new ArgumentException();
            
            this.width = width;
            this.height = height;

            inventoryCells = new Item[width, height];
        }

        public Inventory(
            in int width,
            in int height,
            params KeyValuePair<Item, Vector2Int>[] items
        ) : this(width, height)
        {
            if (items == null)
                throw new ArgumentException(nameof(items));

            foreach (KeyValuePair<Item, Vector2Int> item in items)
                AddItem(item.Key, item.Value);
        }

        public Inventory(
            in int width,
            in int height,
            params Item[] items
        ) : this(width, height)
        {
            if (items == null)
                throw new ArgumentException();
        }

        public Inventory(
            in int width,
            in int height,
            in IEnumerable<KeyValuePair<Item, Vector2Int>> items
        ) : this(width, height)
        {
            if (items == null)
                throw new ArgumentException();
        }

        public Inventory(
            in int width,
            in int height,
            in IEnumerable<Item> items
        ) : this(width, height)
        {
            if (items == null)
                throw new ArgumentException();
        }

        private void FillInventoryCells(int startX, int startY, int sizeX, int sizeY, Item item)
        {
            for (int x = startX; x < startX + sizeX; x++)
            for (int y = startY; y < startY + sizeY; y++)
                inventoryCells[x, y] = item;

        }

        private bool CheckBounds(in int posX, in int posY, int sizeX, in int sizeY)
        {
            if (posX >= width || posY >= height || posX < 0 || posY < 0)
                return false;

            if (posX + sizeX > width || posY + sizeY > height)
                return false;

            return true;
        }

        /// <summary>
        /// Checks for adding an item on a specified position
        /// </summary>
        public bool CanAddItem(in Item item, in Vector2Int position)
        {
            return CanAddItem(item, position.x, position.y);
        }

        private bool CanAddItem(in Item item, in int posX, in int posY)
        {
            if (item == null)
                return false;
            
            if(item.Size.x <= 0 || item.Size.y <= 0)
                throw new ArgumentException();

            if (!CheckBounds(posX, posY, item.Size.x, item.Size.y))
                return false;

            if (Contains(item))
                return false;

            for (int x = posX; x < posX + item.Size.x; x++)
            for (int y = posY; y < posY + item.Size.y; y++)
            {
                if (!IsOccupied(x, y)) continue;
                return false;
            }

            return true;
        }

        /// <summary>
        /// Adds an item on a specified position if not exists
        /// </summary>
        public bool AddItem(in Item item, in Vector2Int position)
        {
            return AddItem(item, position.x, position.y);
        }

        public bool AddItem(in Item item, in int posX, in int posY)
        {
            if (!CanAddItem(item, new Vector2Int(posX, posY)))
                return false;

            inventoryItems.Add(item, new Vector2Int(posX, posY));
            
            FillInventoryCells(posX, posY, item.Size.x, item.Size.y, item);

            OnAdded?.Invoke(item, new Vector2Int(posX, posY));
            return true;
        }

        /// <summary>
        /// Checks for adding an item on a free position
        /// </summary>
        public bool CanAddItem(in Item item)
        {
            if (item == null)
                return false;

            return FindFreePosition(item, out Vector2Int position) && CanAddItem(item, position);
        }

        /// <summary>
        /// Adds an item on a free position
        /// </summary>
        public bool AddItem(in Item item)
        {
            if (!CanAddItem(item))
                return false;

            FindFreePosition(item, out Vector2Int position);

            inventoryItems.Add(item, position);
            
            FillInventoryCells(position.x, position.y, item.Size.x, item.Size.y, item);

            OnAdded?.Invoke(item, new Vector2Int(position.x, position.y));

            return true;
        }

        /// <summary>
        /// Returns a free position for a specified item
        /// </summary>
        private bool FindFreePosition(in Item item, out Vector2Int freePosition)
        {
            return FindFreePosition(item.Size, out freePosition);
        }

        public bool FindFreePosition(Vector2Int size, out Vector2Int freePosition)
        {
            return FindFreePosition(size.x, size.y, out freePosition);
        }
        
        private bool FindFreePosition(in int sizeX, int sizeY, out Vector2Int freePosition)
        {
            freePosition = new Vector2Int(0, 0);

            if (sizeX <= 0 || sizeY <= 0)
                throw new ArgumentException("Размер предмета должен быть положительным.");

            if (sizeX > Width || sizeY > Height)
                return false;

            for (int y = 0; y <= Height - sizeY; y++)
            {
                for (int x = 0; x <= Width - sizeX; x++)
                {
                    if (!IsAreaFree(new Vector2Int(x, y), new Vector2Int(sizeX, sizeY))) continue;
                    
                    freePosition = new Vector2Int(x, y);
                    return true;
                }
            }

            return false;
        }
        
        private bool IsAreaFree(Vector2Int startPosition, Vector2Int size)
        {
            for (int y = 0; y < size.y; y++)
            {
                for (int x = 0; x < size.x; x++)
                {
                    if (IsOccupied(new Vector2Int(startPosition.x + x, startPosition.y + y)))
                        return false;
                }
            }
            return true;
        }
 

        /// <summary>
        /// Checks if a specified item exists
        /// </summary>
        public bool Contains(in Item item)
        {
            return item != null && inventoryItems.ContainsKey(item);
        }

        /// <summary>
        /// Checks if a specified position is occupied
        /// </summary>
        private bool IsOccupied(in Vector2Int position)
        {
            return IsOccupied(position.x, position.y);
        }

        public bool IsOccupied(in int x, in int y)
        {
            return inventoryCells[x, y] != null;
        }

        /// <summary>
        /// Checks if the A position is free
        /// </summary>
        private bool IsFree(in Vector2Int position)
        {
            if(inventoryCells[position.x, position.y] != null)
                return false;

            return true;
        }

        public bool IsFree(in int x, in int y)
        {
            return IsFree(new Vector2Int(x, y));
        }

        /// <summary>
        /// Removes a specified item if exists
        /// </summary>
        private bool RemoveItem(in Item item)
        {
            return RemoveItem(item, out var position);
        }

        public bool RemoveItem(in Item item, out Vector2Int position)
        {
            position = new Vector2Int(0, 0);
            
            if (!Contains(item))
                return false;
            
            var positions = GetPositions(item);

            if (positions == null)
                return false;

            foreach (var itemPos in positions)
                inventoryCells[itemPos.x, itemPos.y] = null;

            position = positions[0];
            inventoryItems.Remove(item);
            
            OnRemoved?.Invoke(item, position);

            return true;
        }

        /// <summary>
        /// Returns an item at specified position 
        /// </summary>
        public Item GetItem(in Vector2Int position)
        {
            return GetItem(position.x, position.y);
        }

        public Item GetItem(in int x, in int y)
        {
            if (x < 0 || y < 0 || x >= width || y >= height)
                throw new IndexOutOfRangeException();

            if (!TryGetItem(new Vector2Int(x, y), out Item item))
                throw new NullReferenceException();
            
            return item;
        }

        public bool TryGetItem(in Vector2Int position, out Item item)
        {
            return TryGetItem(position.x, position.y, out item);
        }

        public bool TryGetItem(in int x, in int y, out Item item)
        {
            item = null;

            if (x < 0 || y < 0 || x >= width || y >= height)
                return false;

            item = inventoryCells[x, y];
            
            return item != null;
        }

        /// <summary>
        /// Returns matrix positions of a specified item 
        /// </summary>
        public Vector2Int[] GetPositions(in Item item)
        {
            if (TryGetPositions(item, out Vector2Int[] position)) return position;
            
            if (item == null && position == null)
                throw new NullReferenceException();
            
            if(!Contains(item) && position == null)
                throw new KeyNotFoundException();

            return position;
        }

        public bool TryGetPositions(in Item item, out Vector2Int[] positions)
        {
            positions = null;

            if (item == null)
                return false;

            if (!inventoryItems.TryGetValue(item, out var position))
                return false;
            
            var positionsList = new List<Vector2Int>();
            
            for(int x = position.x; x < position.x + item.Size.x; x++)
            for (int y = position.y; y < position.y + item.Size.y; y++)
            {
                if(inventoryCells[x, y] == null)
                    continue;
                
                if (!inventoryCells[x, y].Equals(item)) continue;

                positionsList.Add(new Vector2Int(x, y));
            }
            
            positions = positionsList.Count == 0 ? null : positionsList.ToArray();

            return positions != null;
        }

        /// <summary>
        /// Clears all inventory items
        /// </summary>
        public void Clear()
        {
            if(inventoryItems is { Count: 0 })
                return;
            
            Array.Clear(inventoryCells, 0, inventoryCells.Length);
            
            inventoryItems.Clear();
            OnCleared?.Invoke();
        }

        /// <summary>
        /// Returns a count of items with a specified name
        /// </summary>
        public int GetItemCount(string name)
        {
            var count = 0;
            
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems.Keys.ElementAt(i).Name == name)
                    count++;
            }
            
            return count;
        }

        /// <summary>
        /// Moves a specified item at target position if exists
        /// </summary>
        public bool MoveItem(in Item item, in Vector2Int position)
        {
            if(item == null)
                throw new ArgumentNullException(nameof(item));
            
            if (!Contains(item))
                return false;

            var positions = GetPositions(item);
            
            if (positions == null)
                return false;

            if (!CheckBounds(position.x, position.y, item.Size.x, item.Size.y))
                return false;

            for (int x = position.x; x < position.x + item.Size.x; x++)
            for (int y = position.y; y < position.y + item.Size.y; y++)
            {
                if (!IsOccupied(x, y)) continue;
                
                if(inventoryCells[x, y].Equals(item))
                    continue;

                return false;
            }
            
            foreach (var itemPoses in positions)
                inventoryCells[itemPoses.x, itemPoses.y] = null;
            
            FillInventoryCells(position.x, position.y, item.Size.x, item.Size.y, item);
            inventoryItems[item] = new Vector2Int(position.x, position.y);
            
            OnMoved?.Invoke(item, position);
            return true;
        }

        /// <summary>
        /// Reorganizes an inventory space so that the free area is uniform
        /// </summary>
        public void ReorganizeSpace()
        {
            FillInventoryCells(0,0, width,height,null);

            var sortedBySize = inventoryItems.OrderByDescending(x => x.Key.Size.y * x.Key.Size.x).ToList();
            
            foreach (var itemPair in sortedBySize)
            {
                if(!FindFreePosition(itemPair.Key, out Vector2Int freePosition))
                    return;
                
                FillInventoryCells(freePosition.x, freePosition.y, itemPair.Key.Size.x, itemPair.Key.Size.y, itemPair.Key);
            }
        }

        /// <summary>
        /// Copies inventory items to a specified matrix
        /// </summary>
        public void CopyTo(in Item[,] matrix)
        {
            Array.Copy(inventoryCells, matrix, inventoryCells.Length);
        }

        public IEnumerator<Item> GetEnumerator()
        {
            return this.inventoryItems.Select(it => it.Key).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}