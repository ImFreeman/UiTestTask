using Assets.Features.StickyItem.Scripts.Interfaces;
using Assets.Features.UIElementFeature.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Features.StickyItem.Scripts.Realization
{
    public class UIList : IUIList        
    {
        public int Count => _items.Count;

        public event EventHandler<IUIElement> ItemAdded;
        public event EventHandler<IUIElement> ItemRemoved;

        private readonly IList<IUIElement> _items;

        private IListView _listView;
        private IListItemProcessingStrategy _applyingStrategy;
       
        public UIList(IListView listView, IListItemProcessingStrategy applyingStrategy, int listInitCapacity = 0)
        {
            _listView = listView;
            _applyingStrategy = applyingStrategy;
            _items = new List<IUIElement>() { Capacity = listInitCapacity };
        }

        public void Dispose()
        {
            Clear();

            _applyingStrategy = null;
            _listView = null;
        }

        public Vector2 GetItemPosition(int place, IUIElement element)
        {
            var posY = 0.0f;
            for (int i = 0; i < place; i++)
            {
                posY -= Get(i).RectTransform.rect.height;
            }

            posY -= element.RectTransform.rect.height * (1 - element.RectTransform.pivot.y);

            return new Vector2(0.0f, posY);
        }

        public int AddItem(IUIElement element)
        {            
            var count = _items.Count;            
            if (count > 0)
            {
                _applyingStrategy.Add(this, _listView, element);                
            }
            else
            {
                _applyingStrategy.Insert(this, _listView, element, 0);
            }
            _items.Add(element);

            ItemAdded?.Invoke(this, element);
            return count;
        }

        public void AddItem(IUIElement element, int place)
        {
            _items.Insert(place, element);
            _applyingStrategy.Insert(this, _listView, element, place);
            ItemAdded?.Invoke(this, element);
        }
        public IUIElement Get(int place)
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
            if(item != null && _listView != null)
            {
                _applyingStrategy.Remove(item, _listView);                
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