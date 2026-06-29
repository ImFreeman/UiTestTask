using Assets.Features.StickyItem.Scripts.Interfaces;
using Assets.Features.UIElementFeature.Scripts.Realization;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Features.StickyItem.Scripts.Realization
{
    public class ListView : UIElement, IListView
    {
        public event EventHandler<Vector2> ValueChanged;
        public RectTransform ContentContainer => _contentContainer;

        public RectTransform Viewport => _viewport;

        [SerializeField] private RectTransform _contentContainer;
        [SerializeField] private ScrollRect _scroll;
        [SerializeField] private RectTransform _viewport;

        private void OnEnable()
        {
            _scroll.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnDisable()
        {
            _scroll.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged(Vector2 position)
        {
            ValueChanged?.Invoke(this, position);
        }
    }
}