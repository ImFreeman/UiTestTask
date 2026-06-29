using Assets.Features.UIElementFeature.Scripts.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Features.UIElementFeature.Scripts.Realization
{
    public abstract class UIElementPool<TView, TProtocol> : IUIElementsSpawner<TView, TProtocol>
        where TView : UIElement
        where TProtocol : struct
    {
        private const string DeactiveContainerName = "UIGraphicElementPoolDeactiveContainer";

        private TView _prefab;
        private Transform _deactiveContainer;
        private Stack<TView> _pool;

        public UIElementPool(TView prefab, int initCapacity = 0)
        {
            _prefab = prefab;
            _deactiveContainer = new GameObject(DeactiveContainerName).transform;
            _deactiveContainer.gameObject.SetActive(false);
            _pool = new Stack<TView>();
            for (int i = 0; i < initCapacity; i++)
            {
                _pool.Push(CreateNew());
            }
        }

        public void Dispose()
        {
            _prefab = null;

            foreach (var view in _pool)
            {
                Object.Destroy(view.gameObject);
            }
            _pool.Clear();
            _pool = null;

            if( _deactiveContainer != null )
            {
                Object.Destroy(_deactiveContainer.gameObject);
            }            
            _deactiveContainer = null;
        }

        public TView Spawn(TProtocol protocol)
        {
            TView instance;
            if (_pool.Count > 0)
            {
                instance = _pool.Pop();
            }
            else
            {
                instance = CreateNew();
            }

            instance.RectTransform.SetParent(null);
            InitView(instance, protocol);

            return instance;
        }

        public void Despawn(TView view)
        {
            if (view == null)
            {
                throw new System.Exception("You trying return null to a poll");
            }

            view.RectTransform.SetParent(_deactiveContainer);
            _pool.Push(view);
        }

        protected abstract void InitView(TView view, TProtocol protocol);

        private TView CreateNew()
        {
            return Object.Instantiate(_prefab, _deactiveContainer);
        }


    }
}