using System;
using System.Collections.Generic;

namespace Assets.Features.UIElementFeature.Scripts.Interfaces
{
    public interface IUIElementStorage<TView> : IDisposable
        where TView : IUIElement
    {
        public event EventHandler<int> ItemAdded;
        public event EventHandler<int> ItemRemoved;
        public IReadOnlyDictionary<int, TView> Items { get; }
        public void Add(int key, TView view);
        public bool TryAdd(int key, TView view);
        public void Remove(int key);
        public void Clear();
    }
}