using Assets.Features.UIElementFeature.Scripts.Interfaces;
using System;
using UnityEngine;

namespace Assets.Features.StickyItem.Scripts.Interfaces
{
    public interface IUIList : IDisposable
    {
        public int Count { get; }
        public void RemoveItem(int place);
        public void Clear();
        public Vector2 GetItemPosition(int place);
    }
    public interface IUIList<TView> : IUIList
        where TView : IUIElement
    {
        public event EventHandler<TView> ItemAdded;
        public event EventHandler<TView> ItemRemoved;
        
        public int AddItem(TView element);
        public TView Get(int place);
        public void AddItem(TView element, int place);
        
    }
}