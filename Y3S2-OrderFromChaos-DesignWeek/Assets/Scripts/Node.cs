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

    public AudioClip pickupSound;
    public AudioClip dropoffSound;
    
    AudioSource audioSrc;
    Animator an;
    CanvasGroup group;
    RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        group = GetComponent<CanvasGroup>();
        audioSrc = GetComponent<AudioSource>();
        an = GetComponent<Animator>();
    }

    private void Update()
    {
        if (good)
        {
            audioSrc.clip = goodSound;
        } else
        {
            audioSrc.clip = badSound;
        }

        an.SetBool("IsPlaying", audioSrc.isPlaying);
    }

    #region begin
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (canDrag) {
            group.blocksRaycasts = false;
            audioSrc.PlayOneShot(pickupSound);
        }
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
        if (canDrag)
        {
            good = !SlotMap.instance.PlaceNote(id, transform.localPosition);
            if (AccessibilityOption.accessibilityMode) canDrag = good;
            group.blocksRaycasts = true;
            audioSrc.PlayOneShot(dropoffSound);
        }
    }
    #endregion

    #region on click
    public void OnPointerDown(PointerEventData eventData)
    {

    }
    #endregion



}
