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

        public void Add(IUIList list, IListView listView, IUIElement element)
        {
            element.RectTransform.SetParent(listView.ContentContainer);
            var lastListItem = list.Get(list.Count - 1).RectTransform;
            element.RectTransform.anchoredPosition = lastListItem.anchoredPosition
                - new Vector2(0, element.RectTransform.rect.height);

            element.RectTransform.offsetMin = new Vector2(0.0f, element.RectTransform.offsetMin.y);
            element.RectTransform.offsetMax = new Vector2(0.0f, element.RectTransform.offsetMax.y);
            listView.ContentContainer.sizeDelta = new Vector2(
                listView.ContentContainer.sizeDelta.x,
                listView.ContentContainer.sizeDelta.y + element.RectTransform.sizeDelta.y
                );
        }

        public void Insert(IUIList list, IListView listView, IUIElement element, int position)
        {
            element.RectTransform.SetParent(listView.ContentContainer);
            element.RectTransform.anchoredPosition = list.GetItemPosition(position, element);                

            element.RectTransform.offsetMin = new Vector2(0.0f, element.RectTransform.offsetMin.y);
            element.RectTransform.offsetMax = new Vector2(0.0f, element.RectTransform.offsetMax.y);
            listView.ContentContainer.sizeDelta = new Vector2(listView.ContentContainer.sizeDelta.x, listView.ContentContainer.sizeDelta.y + element.RectTransform.sizeDelta.y);
        }


        public void Remove(IUIElement element, IListView listView)
        {
            element.RectTransform.SetParent(null);
            listView.ContentContainer.sizeDelta = new Vector2(listView.ContentContainer.sizeDelta.x, listView.ContentContainer.sizeDelta.y - element.RectTransform.sizeDelta.y);
        }
    }
}