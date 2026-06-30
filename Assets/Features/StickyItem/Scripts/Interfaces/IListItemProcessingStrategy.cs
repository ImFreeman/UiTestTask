using Assets.Features.UIElementFeature.Scripts.Interfaces;
using System;

namespace Assets.Features.StickyItem.Scripts.Interfaces
{
    public interface IListItemProcessingStrategy : IDisposable
    {
        public void Insert(IUIList list, IListView listView, IUIElement element, int position);
        public void Add(IUIList list, IListView listView, IUIElement element);
        public void Remove(IUIElement element, IListView listView);
    }
}