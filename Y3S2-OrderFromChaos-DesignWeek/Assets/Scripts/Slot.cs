using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    //private DragDrop dragDrop;
   //public int num;
   //public int matchNum;
   /*public void Match(int num)
    {
        OnDrop();
    }
   */
   public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("dropping");
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }
    }
}
