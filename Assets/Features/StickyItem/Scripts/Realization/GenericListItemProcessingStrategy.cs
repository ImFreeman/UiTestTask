using Assets.Features.StickyItem.Scripts.Interfaces;
using Assets.Features.UIElementFeature.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Features.StickyItem.Scripts.Realization
{
    public class GenericListItemProcessingStrategy : IListItemProcessingStrategy
    {
        public void Dispose()
        {
        }

        public void Apply(IUIList list, IListView listView, IUIElement element, int position)
        {
            element.RectTransform.SetParent(listView.ContentContainer);
            element.RectTransform.anchoredPosition = list.GetItemPosition(position);

            element.RectTransform.offsetMin = new Vector2(0.0f, element.RectTransform.offsetMin.y);
            element.RectTransform.offsetMax = new Vector2(0.0f, element.RectTransform.offsetMax.y);
            listView.ContentContainer.sizeDelta = new Vector2(listView.ContentContainer.sizeDelta.x, listView.ContentContainer.sizeDelta.y + element.RectTransform.sizeDelta.y);
        }


        public void Remove(IUIElement element)
        {
            element.RectTransform.SetParent(null);
        }
    }
}