using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Tetris.View
{
    public class NextBlockUI : MonoBehaviour
    {
        [SerializeField] private RectTransform _minoImagesParent;
        [SerializeField] private Image _minoImage;

        private Queue<Image> _hideImageQueue = new();

        public void ChangeImage(Sprite sprite, Color color)
        {
            var image = _hideImageQueue.Count == 0 ? Instantiate(_minoImage, _minoImagesParent) : _hideImageQueue.Dequeue();
            image.sprite = sprite;
            image.color = color;
            image.gameObject.SetActive(true);
        }

        public void HideTopImage()
        {
            var hideImage = _minoImagesParent.GetChild(0);
            hideImage.SetAsLastSibling();
            hideImage.gameObject.SetActive(false);
            _hideImageQueue.Enqueue(hideImage.GetComponent<Image>());
        }
    }
}