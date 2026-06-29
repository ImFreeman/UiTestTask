using Assets.Features.UIElementFeature.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Features.UIElementFeature.Scripts.Interfaces
{
    public interface IUIElement
    {
        public RectTransform RectTransform { get; }
    }
}
