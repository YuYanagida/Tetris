using UnityEngine;

namespace Game.Tetris.View
{
    public class Block : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private SpriteRenderer _simulateRenderer;
        [SerializeField] private Sprite _sprite;

        private void Awake()
        {
            _spriteRenderer.sprite = _sprite;
            _simulateRenderer.sprite = _sprite;
            _simulateRenderer.enabled = false;
        }

        public void SetImage(Color color)
        {
            _spriteRenderer.color = color;
        }

        public void SetSimulate(bool isSimulate)
        {
            _simulateRenderer.enabled = isSimulate;
        }

        public void SetSimulateColor(Color color)
        {
            _simulateRenderer.color = color;
        }
    }
}