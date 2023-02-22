using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Core.ScriptableObjects;
using Core.Player;

public class NoteButtonManager : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public bool canBeDragged;
    public GameObject noteToSpawn;
    //public DamageType damageType;
    public float delayTime = 2.0f; // set the delay time in seconds

    private bool canInstantiate = true;
    private Button instantiateButton;

    private BeatScroller bs;
    private bool isDragging;

    public RectTransform centreObject;
    private RectTransform imageRectTransform;
    private Vector3 originalPosition;
    

    void Start()
    {
        bs = FindObjectOfType<BeatScroller>();
        instantiateButton = GetComponent<Button>();
        instantiateButton.onClick.AddListener(InstantiatePrefab);

        imageRectTransform = GetComponent<RectTransform>();
        originalPosition = imageRectTransform.anchoredPosition;
    }

    private void Update()
    {

    }

    void InstantiatePrefab()
    {
        if (canInstantiate && !isDragging)
        {
            canInstantiate = false;
            bs.SpawnNote(noteToSpawn);
            StartCoroutine(EnableInstantiate());
        }
    }

    IEnumerator EnableInstantiate()
    {
        yield return new WaitForSeconds(delayTime);
        canInstantiate = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!canBeDragged) return;
        imageRectTransform.anchoredPosition += eventData.delta;
        isDragging = true;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = false;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isDragging)
        {
            // The button was clicked, not dragged
            //Debug.Log("Button clicked");
        }
        else
        {
            // The button was dragged, not clicked
            //Debug.Log("Button dragged");

            Vector2 mousePos = Input.mousePosition;
            Vector2 localPoint;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(centreObject, mousePos, null, out localPoint))
            {
                if (centreObject.rect.Contains(localPoint))
                {
                    // The mouse button was released over the UI element
                    CombineElements.instance.AddElement(gameObject.GetComponent<NoteButtonManager>().noteToSpawn.GetComponent<NoteController>().noteDamageType);
                }
            }

            // Move the button back to its original position
            imageRectTransform.anchoredPosition = originalPosition;

        }
    }
}
