using Assets.Features.UIElementFeature.Scripts.Interfaces;
using Assets.Features.UIElementFeature.Scripts.Realization;
using System;
using UnityEngine;

namespace Assets.Features.StickyItem.Scripts.Interfaces
{
    public interface IUIList : IDisposable
    {
        public event EventHandler<IUIElement> ItemAdded;
        public event EventHandler<IUIElement> ItemRemoved;
        public int Count { get; }
        public void RemoveItem(int place);
        public void Clear();
        public Vector2 GetItemPosition(int place, IUIElement element);
        public int AddItem(IUIElement element);
        public IUIElement Get(int place);
        public void AddItem(IUIElement element, int place);
    }    
}