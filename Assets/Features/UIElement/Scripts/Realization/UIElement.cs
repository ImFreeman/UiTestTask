using Assets.Features.UIElementFeature.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Features.UIElementFeature.Scripts.Realization
{
    public class UIElement : MonoBehaviour, IUIElement
    {
        [SerializeField] protected RectTransform _rectTransform;
        public RectTransform RectTransform => _rectTransform;
    }
}