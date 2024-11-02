using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Inventories
{
    public sealed class Inventory : IEnumerable<Item>
    {
        public event Action<Item, Vector2Int> OnAdded;
        public event Action<Item, Vector2Int> OnRemoved;
        public event Action<Item, Vector2Int> OnMoved;
        public event Action OnCleared;

        private int width;
        private int height;
        
        private Item[,] inventoryCells;
        
        public int Width => width;
        public int Height => height;
        public int Count => inventoryItems.Count;
        
        private List<KeyValuePair<Item, Vector2Int>> inventoryItems = new();

        public Inventory(in int width, in int height)
        {
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
            if(items == null)
                throw new ArgumentException(nameof(items));

            foreach (KeyValuePair<Item, Vector2Int> item in items)
            {
                AddItem(item.Key, item.Value);
            }
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
            if(items == null)
                throw new ArgumentException(); 
        }

        private void FillInventoryCells(int startX, int startY, int sizeX, int sizeY, Item item)
        {
            for (int x = startX; x < startX + sizeX; x++)
            {
                for (int y = startY; y < startY + sizeY; y++)
                {
                    inventoryCells[x, y] = item;
                }
            }
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
            if(item == null)
                return false;

            if (!CheckBounds(posX, posY, item.Size.x, item.Size.y))
                return false;

            if (Contains(item))
                return false;

            for(int x = posX; x < posX + item.Size.x; x++)
            for (int y = posY; y < posY + item.Size.y; y++)
            {
                if (IsOccupied(x, y))
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
            if(!CanAddItem(item, new Vector2Int(posX, posY)))
                return false;

            inventoryItems.Add(new KeyValuePair<Item, Vector2Int>(item, new Vector2Int(posX, posY)));
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

            if (!FindFreePosition(item, out Vector2Int position))
                return false;
            
            return CanAddItem(item, position);
        }

        /// <summary>
        /// Adds an item on a free position
        /// </summary>
        public bool AddItem(in Item item)
        {
            if (!CanAddItem(item))
                return false;

            FindFreePosition(item, out Vector2Int position);

            inventoryItems.Add(new KeyValuePair<Item, Vector2Int>(item, position));
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

        public bool FindFreePosition(in Vector2Int size, out Vector2Int freePosition)
        {
            return FindFreePosition(size.x, size.y, out freePosition);
        }

        private bool FindFreePosition(in int sizeX, int sizeY, out Vector2Int freePosition)
        {
            freePosition = new Vector2Int(0, 0);
            
            if (!CheckBounds(0, 0, sizeX, sizeY))
                return false;
            
            if (inventoryItems.Count == 0)
                return true;
            
            //new KeyValuePair<Item, Vector2Int>(new Item("X", 1, 1), new Vector2Int(1, 1)) exist
            //new Vector2Int(3, 3), size
            //new Vector2Int(2, 0) exp

            for (var i = 0; i < inventoryCells.GetLength(0); i++)
            {
                for (var j = 0; j < inventoryCells.GetLength(1); j++)
                {
                    freePosition.x = i;
                    freePosition.y = j;

                    for (int w = i; w < i + (sizeX - 1); w++)
                    {
                        if(w > inventoryCells.GetLength(0) - 1)
                            break;
                        
                        for (int h = j; h < j + (sizeY - 1); h++)
                        {
                            if(h > inventoryCells.GetLength(1) - 1)
                                break;
                            
                            if(inventoryCells[w, h] != null)
                                break;

                            if (w == sizeX - 1 && h == sizeY - 1)
                                return true;
                        }
                    }
                }
            }
            
            //freePosition = new Vector2Int(0, 0);
            return false;


            for (int h = 0; h < height; h++)
            {
                if(h + 1 >= height)
                    continue;
                
                freePosition.y = h;
                
                for (int w = 0; w < width; w++)
                {
                    if(w + 1 > width)
                        continue;

                    if(IsOccupied(w, h))
                        continue;
                    
                    freePosition.x = w;

                    if(w + sizeX >= width || h + sizeY >= height)
                        break;

                    for (int xSize = w + 1; xSize < w + sizeX; xSize++)
                    {
                        if(xSize >= width)
                            break;
                        
                        if (IsOccupied(xSize, h))
                            break;
                        
                        for (int ySize = h + 1; ySize < h + sizeY; ySize++)
                        {
                            if(ySize >= height)
                                break;
                            
                            if(IsOccupied(w, ySize))
                                break;
                            
                            if (IsOccupied(xSize, ySize))
                                break;
                            
                            return true;
                        }
                    }
                }

                freePosition = new Vector2Int(0, 0);
                return false;
            }
            
            freePosition = new Vector2Int(0, 0);
            return false;
        }

        /// <summary>
        /// Checks if a specified item exists
        /// </summary>
        public bool Contains(in Item item)
        {
            return inventoryItems.Select(it => it.Key).ToHashSet().TryGetValue(item, out var value);
        }

        /// <summary>
        /// Checks if a specified position is occupied
        /// </summary>
        public bool IsOccupied(in Vector2Int position)
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
        public bool IsFree(in Vector2Int position)
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
        public bool RemoveItem(in Item item)
        {
            return RemoveItem(item, out var position);
        }

        public bool RemoveItem(in Item item, out Vector2Int position)
        {
            position = new Vector2Int(0, 0);
            
            if (!Contains(item))
                return false;

            var itemToRemove = item;
            var listItem = inventoryItems.Find(x => x.Key.Equals(itemToRemove));
            inventoryItems.Remove(listItem);
            
            position = new Vector2Int(listItem.Value.x, listItem.Value.y);
            
            FillInventoryCells(position.x, position.y, listItem.Key.Size.x, listItem.Key.Size.y, null);
            
            OnRemoved?.Invoke(itemToRemove, position);

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
            if (!TryGetItem(new Vector2Int(x, y), out Item item))
                return null;
            
            return item;
        }

        public bool TryGetItem(in Vector2Int position, out Item item)
        {
            return TryGetItem(position.x, position.y, out item);
        }

        public bool TryGetItem(in int x, in int y, out Item item)
        {
            if(x < 0 || y < 0 || x >= width || y >= height)
                throw new IndexOutOfRangeException();

            item = inventoryCells[x, y];
            
            return item != null;
        }

        /// <summary>
        /// Returns matrix positions of a specified item 
        /// </summary>
        public Vector2Int[] GetPositions(in Item item)
        {
            return !TryGetPositions(item, out Vector2Int[] position) ? null : position;
        }

        public bool TryGetPositions(in Item item, out Vector2Int[] positions)
        {
            positions = null;
            
            if (item == null)
                return false;
            
            var positionsList = new List<Vector2Int>();
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if(inventoryCells[x, y] == null)
                        continue;
                    
                    if (inventoryCells[x, y].Equals(item))
                        positionsList.Add(new Vector2Int(x, y));
                }
            }

            positions = positionsList.Count == 0 ? null : positionsList.ToArray();
             
            if (positions == null)
                return false;

            return true;
        }

        /// <summary>
        /// Clears all inventory items
        /// </summary>
        public void Clear()
        {
            inventoryItems.Clear();
            
            for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
                inventoryCells[i, j] = null;
            
            OnCleared?.Invoke();
        }

        /// <summary>
        /// Returns a count of items with a specified name
        /// </summary>
        public int GetItemCount(string name)
        {
            return inventoryItems.Count(it => it.Key.Name == name);
        }

        /// <summary>
        /// Moves a specified item at target position if exists
        /// </summary>
        public bool MoveItem(in Item item, in Vector2Int position) =>
            throw new NotImplementedException();

        /// <summary>
        /// Reorganizes a inventory space so that the free area is uniform
        /// </summary>
        public void ReorganizeSpace() =>
            throw new NotImplementedException();

        /// <summary>
        /// Copies inventory items to a specified matrix
        /// </summary>
        public void CopyTo(in Item[,] matrix)
        {
            StringBuilder stringBuilder = new StringBuilder();
            
            for (var i = 0; i < inventoryCells.GetLength(0); i++)
            {
                for (var j = 0; j < inventoryCells.GetLength(1); j++)
                {
                    stringBuilder.Append(inventoryCells[i, j] == null ? "null" : inventoryCells[i, j]);
                    matrix.SetValue(inventoryCells[i, j], i, j);
                }
                stringBuilder.Append("\n");
            }
            
            Debug.Log(stringBuilder.ToString());
        }

        public IEnumerator<Item> GetEnumerator()
        {
            return this.inventoryItems.Select(it => it.Key).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            Debug.Log("Падаем тут");
            throw new NotImplementedException();
        }
    }
}