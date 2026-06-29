using Assets.Features.UIElementFeature.Scripts.Interfaces;
using System;
using UnityEngine;

namespace Assets.Features.StickyItem.Scripts.Interfaces
{
    public interface IListView : IUIElement
    {
        public event EventHandler<Vector2> ValueChanged;
        public RectTransform ContentContainer { get; }
        public RectTransform Viewport { get; }
    }
}