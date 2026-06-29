using Assets.Features.UIElementFeature.Scripts.Realization;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Features.StickyItem.Demo.Scripts
{
    public class SampleItemView : UIElement
    {
        public event EventHandler ButtonClicked;
        public Image Image => _image;

        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Button _button;

        protected void Init(Protocol protocol)
        {
            _image.sprite = protocol.Sprite;
            _text.text = protocol.Text;
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        private void OnDestroy()
        {
            ButtonClicked = null;
        }

        private void OnClick()
        {
            ButtonClicked?.Invoke(this, EventArgs.Empty);
        }

        public readonly struct Protocol
        {
            public readonly Sprite Sprite;
            public readonly string Text;
            public Protocol(Sprite sprite, string text)
            {
                Sprite = sprite;
                Text = text;
            }
        }

        public class Pool : UIElementPool<SampleItemView, Protocol>
        {
            public Pool(SampleItemView prefab, int initCapacity = 0) : base(prefab, initCapacity)
            {
            }

            protected override void InitView(SampleItemView view, Protocol protocol)
            {
                view.Init(protocol);
            }
        }
    }
}