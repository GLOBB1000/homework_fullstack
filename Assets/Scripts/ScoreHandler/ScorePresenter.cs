using System;
using Modules;
using TMPro;

namespace ScoreHandler
{
    public class ScorePresenter : IDisposable
    {
        private TextMeshPro _scoreText;
        private IScore _score;

        public ScorePresenter(TextMeshPro scoreText, IScore score)
        {
            _scoreText = scoreText;
            _score = score;
            
            _score.OnStateChanged += ScoreOnOnStateChanged;
            
            _scoreText.text = $"Score: 0";
        }

        private void ScoreOnOnStateChanged(int obj)
        {
            _scoreText.text = $"Score: {obj}";
        }

        public void Dispose()
        {
            _score.OnStateChanged -= ScoreOnOnStateChanged;
        }
    }
}