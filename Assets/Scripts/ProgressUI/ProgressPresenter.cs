using System;
using Modules;
using TMPro;

namespace ProgressUI
{
    public class ProgressPresenter : IDisposable
    {
        private TextMeshPro _levelText;
        private IDifficulty _difficulty;
        
        public ProgressPresenter(TextMeshPro levelText, IDifficulty difficulty)
        {
            _levelText = levelText;
            _difficulty = difficulty;
            
            _difficulty.OnStateChanged += DifficultyOnStateChanged;
        }

        private void DifficultyOnStateChanged()
        {
            _levelText.text = $"Level {_difficulty.Current}/{_difficulty.Max}";
        }

        public void Dispose()
        {
            _difficulty.OnStateChanged -= DifficultyOnStateChanged;
        }
    }
}