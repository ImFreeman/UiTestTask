using Assets.Features.UIElementFeature.Scripts.Interfaces;
using System;

namespace Assets.Features.StickyItem.Scripts.Interfaces
{
    public interface IUIListStickness : IDisposable
    {
        public void SetSticky(IUIElement element, int place);
        public void Release(int place);
    }
}