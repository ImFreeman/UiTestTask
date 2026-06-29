using Assets.Features.StickyItem.Scripts.Interfaces;
using Assets.Features.StickyItem.Scripts.Realization;
using Assets.Features.UIElementFeature.Scripts.Interfaces;
using System;
using UnityEngine;

namespace Assets.Features.StickyItem.Demo.Scripts
{
    public class DemoLauncher : MonoBehaviour
    {
        [SerializeField] private ListView _listView;
        [SerializeField] private SampleItemView _itemPrefab;
        [SerializeField] private TestData _data;
        [SerializeField] private int _count;

        private IUIList<SampleItemView> _list;
        private IListItemProcessingStrategy _strategy;
        private IUIListStickness _stickness;

        private int _currentPressedButton = -1;

        private IUIElementsSpawner<SampleItemView, SampleItemView.Protocol> _itemSpawner;
        private void Start()
        {
            _itemSpawner = new SampleItemView.Pool(_itemPrefab, _count);

            _strategy = new GenericListItemProcessingStrategy();
            _list = new UIList<SampleItemView>(_listView, _strategy, _count);

            _stickness = new UIListSingleItemStickness<SampleItemView>(_list, _listView);
            for (int i = 0; i < _count; i++)
            {
                var place = i;
                var itemView = _itemSpawner.Spawn(new SampleItemView.Protocol(_data.Sprite, _data.Text + $"#{i}"));
                _list.AddItem(itemView);
                itemView.ButtonClicked += (sender, e) =>
                {
                    if (_currentPressedButton != -1)
                    {
                        _list.Get(_currentPressedButton).Image.color = Color.white;
                    }
                    _stickness.SetSticky(itemView, place);
                    itemView.Image.color = Color.green;
                    _currentPressedButton = place;
                };
            }
        }

        private void OnDestroy()
        {
            _stickness.Dispose();
            _stickness = null;

            _strategy.Dispose();
            _strategy = null;
            

            _list.Dispose();
            _list = null;                        

            _itemSpawner.Dispose();
            _itemSpawner = null;
        }

        [Serializable]
        public struct TestData
        {
            public Sprite Sprite;
            public string Text;
        }
    }
}