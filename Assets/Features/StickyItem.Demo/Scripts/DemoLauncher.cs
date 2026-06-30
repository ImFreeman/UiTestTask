using Assets.Features.StickyItem.Scripts.Interfaces;
using Assets.Features.StickyItem.Scripts.Realization;
using Assets.Features.UIElementFeature.Scripts.Interfaces;
using Assets.Features.UIElementFeature.Scripts.Realization;
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

        private IUIList _list;
        private IListItemProcessingStrategy _strategy;
        private IUIListStickness _stickness;

        private bool _isQuitting;

        private int _currentPressedButton = -1;

        private IUIElementsSpawner<SampleItemView, SampleItemView.Protocol> _itemSpawner;
        private IUIElementStorage<SampleItemView> _itemStorage;
        private void Start()
        {
            _itemSpawner = new SampleItemView.Pool(_itemPrefab, _count);
            _itemStorage = new UIElementStorage<SampleItemView>();

            _strategy = new GenericListItemProcessingStrategy();
            _list = new UIList(_listView, _strategy, _count);

            _stickness = new UIListSingleItemStickness(_list, _listView);
            for (int i = 0; i < _count; i++)
            {
                var place = i;
                var itemView = _itemSpawner.Spawn(new SampleItemView.Protocol(_data.Sprite, _data.Text + $"#{i}"));
                _itemStorage.Add(itemView.gameObject.GetInstanceID(), itemView);
                _list.AddItem(itemView);
                itemView.ButtonClicked += (sender, e) =>
                {
                    if (_currentPressedButton != -1)
                    {
                        if(_itemStorage.Items.TryGetValue(_currentPressedButton, out var item))
                        {
                            item.Image.color = Color.white;
                        }                         
                    }
                    _stickness.SetSticky(itemView, place);
                    itemView.Image.color = Color.green;
                    _currentPressedButton = itemView.gameObject.GetInstanceID();
                };
            }
        }       

        private void OnApplicationQuit()
        {
            _isQuitting = true;
        }

        private void OnDestroy()
        {
            if (_isQuitting)
                return;

            _stickness.Dispose();
            _stickness = null;

            _strategy.Dispose();
            _strategy = null;
            
            _list.Dispose();
            _list = null;

            _itemStorage.Dispose();
            _itemStorage = null;

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