using Assets.Features.StickyItem.Scripts.Interfaces;
using Assets.Features.UIElementFeature.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Features.StickyItem.Scripts.Realization
{
    public class UIListSingleItemStickness : IUIListStickness        
    {
        private IUIList _list;
        private IListView _listView;

        private int _currentItemPlace = -1;
        private IUIElement _currentItem;

        private float _upBorder;
        private float _downBorder;
        private float _fullPath;
        private bool _isSticked;

        public UIListSingleItemStickness(IUIList list, IListView listView)
        {
            _list = list;
            _listView = listView;

            _list.ItemAdded += OnListChangedAdded;
            _list.ItemRemoved += OnListChangedAdded;
        }        

        public void Dispose()
        {
            _list.ItemAdded -= OnListChangedAdded;
            _list.ItemRemoved -= OnListChangedAdded;

            if (_currentItem != null)
            {
                if (_currentItemPlace != -1)
                {
                    Release(_currentItemPlace);
                }
                if (_isSticked)
                {
                    Unstick();
                }
            }
            
            _currentItem = null;
            _list = null;
            _listView = null;
        }

        public void Release(int place)
        {
            if (_currentItemPlace != place)
            {
                return;
            }
            if (_isSticked)
            {
                Unstick();
            }
            _listView.ValueChanged -= OnValueChanged;
            _currentItemPlace = -1;
            _currentItem = null;
        }

        public void SetSticky(IUIElement element, int place)
        {
            if (_currentItemPlace != -1)
            {
                Release(_currentItemPlace);
            }
            _currentItem = element;
            _currentItemPlace = place;
            CalculateBorders();
            _listView.ValueChanged += OnValueChanged;
        }

        private void OnValueChanged(object sender, Vector2 e)
        {
            var currentDelta = (1 - e.y) * _fullPath;

            if (currentDelta > _upBorder)
            {
                StickUp();
            }
            else if (currentDelta < _downBorder)
            {
                StickDown();
            }
            else
            {
                Unstick();
            }
        }

        private void StickUp()
        {
            if (_isSticked)
            {
                return;
            }
            _isSticked = true;
            _currentItem.RectTransform.SetParent(_listView.Viewport);
            _currentItem.RectTransform.anchoredPosition = new Vector2(
                0.0f,
                -_currentItem.RectTransform.rect.height * (1 - _currentItem.RectTransform.pivot.y)
                );
        }

        private void StickDown()
        {
            if (_isSticked)
            {
                return;
            }
            _isSticked = true;
            _currentItem.RectTransform.SetParent(_listView.Viewport);
            _currentItem.RectTransform.anchoredPosition = new Vector2(
                0.0f,
                -_listView.Viewport.rect.height
                + _currentItem.RectTransform.rect.height * _currentItem.RectTransform.pivot.y);
        }

        private void Unstick()
        {
            if (!_isSticked)
            {
                return;
            }
            _isSticked = false;

            _currentItem.RectTransform.SetParent(_listView.ContentContainer);
            _currentItem.RectTransform.anchoredPosition = _list.GetItemPosition(_currentItemPlace, _currentItem);

            _currentItem.RectTransform.offsetMin = new Vector2(0.0f, _currentItem.RectTransform.offsetMin.y);
            _currentItem.RectTransform.offsetMax = new Vector2(0.0f, _currentItem.RectTransform.offsetMax.y);
        }

        private void CalculateBorders()
        {            
            var originPosition = _list.GetItemPosition(_currentItemPlace, _currentItem);

            _upBorder = Mathf.Abs(
                originPosition.y
                + _currentItem.RectTransform.rect.height * (1 - _currentItem.RectTransform.pivot.y)
                );

            var lastItem = _list.Get(_list.Count - 1);

            _downBorder = -originPosition.y
                - _listView.Viewport.rect.height
               + _currentItem.RectTransform.rect.height * (1 - _currentItem.RectTransform.pivot.y);

            _fullPath = Mathf.Abs(
                _list.GetItemPosition(_list.Count - 1, lastItem).y
                + _listView.Viewport.rect.height
                - lastItem.RectTransform.rect.height * (1 - lastItem.RectTransform.pivot.y)
                );
        }

        private void OnListChangedAdded(object sender, IUIElement e)
        {
            if(_currentItemPlace != -1)
            {
                CalculateBorders();
            }
        }
    }
}