using Assets.Features.StickyItem.Scripts.Interfaces;
using Assets.Features.UIElementFeature.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Features.StickyItem.Scripts.Realization
{
    public class UIList<TView> : IUIList<TView>
        where TView : IUIElement
    {
        public int Count => _items.Count;

        public event EventHandler<TView> ItemAdded;
        public event EventHandler<TView> ItemRemoved;

        private readonly IList<TView> _items;

        private IListView _listView;
        private IListItemProcessingStrategy _applyingStrategy;
       
        public UIList(IListView listView, IListItemProcessingStrategy applyingStrategy, int listInitCapacity = 0)
        {
            _listView = listView;
            _applyingStrategy = applyingStrategy;
            _items = new List<TView>() { Capacity = listInitCapacity };
        }

        public void Dispose()
        {
            Clear();

            _applyingStrategy = null;
            _listView = null;
        }

        public Vector2 GetItemPosition(int place)
        {
            var posY = 0.0f;
            for (int i = 0; i < place; i++)
            {
                posY -= Get(i).RectTransform.rect.height;
            }

            posY -= Get(place).RectTransform.rect.height * (1 - Get(place).RectTransform.pivot.y);

            return new Vector2(0.0f, posY);
        }

        public int AddItem(TView element)
        {
            var count = _items.Count;
            _items.Add(element);
            _applyingStrategy.Apply(this, _listView, element, count);
            ItemAdded?.Invoke(this, element);
            return count;
        }

        public void AddItem(TView element, int place)
        {
            _items.Insert(place, element);
            _applyingStrategy.Apply(this, _listView, element, place);
            ItemAdded?.Invoke(this, element);
        }
        public TView Get(int place)
        {
            try
            {
                return _items[place];
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                throw e;
            }
        }

        public void RemoveItem(int place)
        {
            var item = _items[place];
            if(item != null)
            {
                _applyingStrategy.Remove(item);                
            }
            _items.RemoveAt(place);
            ItemRemoved?.Invoke(this, item);
        }

        public void Clear()
        {
            int count = 0;
            while (_items.Count > 0)
            {
                RemoveItem(count);
            }
        }
    }
}