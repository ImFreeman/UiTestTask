using Assets.Features.UIElementFeature.Scripts.Interfaces;
using System;

namespace Assets.Features.StickyItem.Scripts.Interfaces
{
    public interface IListItemProcessingStrategy : IDisposable
    {
        public void Apply(IUIList list, IListView listView, IUIElement element, int position);
        public void Remove(IUIElement element);
    }
}