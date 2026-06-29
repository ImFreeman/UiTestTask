using System;

namespace Assets.Features.UIElementFeature.Scripts.Interfaces
{
    public interface IUIElementsSpawner<TView, TProtocol> : IDisposable
        where TView : IUIElement
        where TProtocol : struct
    {        
        public TView Spawn(TProtocol protocol);
        public void Despawn(TView view);
    }
}
