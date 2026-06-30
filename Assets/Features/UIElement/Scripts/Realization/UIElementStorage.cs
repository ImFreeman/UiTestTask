using Assets.Features.UIElementFeature.Scripts.Interfaces;
using System;
using System.Collections.Generic;

namespace Assets.Features.UIElementFeature.Scripts.Realization
{
    public class UIElementStorage<TView> : IUIElementStorage<TView>
        where TView : IUIElement
    {
        public IReadOnlyDictionary<int, TView> Items => _data;

        public event EventHandler<int> ItemAdded;
        public event EventHandler<int> ItemRemoved;

        private Dictionary<int, TView> _data = new Dictionary<int, TView>();

        public void Add(int key, TView view)
        {
            if (_data.TryAdd(key, view))
            {
                ItemAdded?.Invoke(this, key);
            }
        }

        public bool TryAdd(int key, TView view)
        {
            return _data.TryAdd(key, view);
        }

        public void Remove(int key)
        {
            if (_data.Remove(key))
            {
                ItemRemoved?.Invoke(this, key);
            }
        }

        public void Clear()
        {
            foreach (var item in _data.Keys)
            {
                ItemRemoved?.Invoke(this, item);
            }
            _data.Clear();
        }

        public void Dispose()
        {
            _data.Clear();
            _data = null;
        }
    }
}