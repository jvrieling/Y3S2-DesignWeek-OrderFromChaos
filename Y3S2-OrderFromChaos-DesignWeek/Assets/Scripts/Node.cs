using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Node : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    //private Slot slot;
    //public int num;
    [SerializeField] private Canvas canvas;

    public int id;
    public bool canDrag = true;

    public AudioClip badSound;
    public AudioClip goodSound;

    private CanvasGroup group;
    private RectTransform rect;
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        group = GetComponent<CanvasGroup>();
    }
    #region begin
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(canDrag) group.blocksRaycasts = false;
    }
    #endregion

    #region on drag
    public void OnDrag(PointerEventData eventData)
    {
        if (canDrag) rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
    #endregion

    #region end
    public void OnEndDrag(PointerEventData eventData)
    {
        if (canDrag) canDrag = !SlotMap.instance.PlaceNote(id, transform.localPosition);

        if (canDrag) group.blocksRaycasts = true;
    }
    #endregion

    #region on click
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("pointerDown");
    }
    #endregion



}
