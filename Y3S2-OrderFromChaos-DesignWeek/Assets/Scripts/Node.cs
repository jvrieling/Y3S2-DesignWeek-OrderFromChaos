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
    public bool good = false;

    public AudioClip badSound;
    public AudioClip goodSound;
    AudioSource audioSrc;

    private CanvasGroup group;
    private RectTransform rect;
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        group = GetComponent<CanvasGroup>();
        audioSrc = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!canDrag)
        {
            audioSrc.clip = goodSound;
        } else
        {
            audioSrc.clip = badSound;
        }
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

    }
    #endregion



}
