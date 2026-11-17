using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Target & Success Event")]
    [SerializeField] public RectTransform targetDropZone;
    public UnityEvent onSuccessfulDrop;

    [Header("Settings")]
    [SerializeField] private float overlapThreshold = 0.5f; // Needs 50% overlap to succeed

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 originalPosition;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        
        // Add a CanvasGroup if missing (temporarily disable Raycasts during dragging)
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null) canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        // Starting local position to snap back later
        originalPosition = rectTransform.localPosition;
        
        // Block raycasts from hitting object while dragging, EventSystem can see through it
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f; // Visual feedback
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        // Track cursor
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform.parent as RectTransform, 
            eventData.position, 
            eventData.pressEventCamera, 
            out Vector2 localPoint))
        {
            rectTransform.localPosition = localPoint;
        }
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1.0f;

        // Check if the dragged image overlaps with target
        if (targetDropZone != null && IsOverlapping(rectTransform, targetDropZone))
        {
            // Optional: snap exactly to the target's center
            //rectTransform.localPosition = targetDropZone.localPosition; 
            
            // Trigger logic/event
            onSuccessfulDrop?.Invoke();

            LevelHandler levelHandler = Object.FindAnyObjectByType<LevelHandler>();
            if( levelHandler.GetCurrentStationName() == "Counter") Object.FindAnyObjectByType<CounterStation>().OnSuccessfulDrop();
            else if( levelHandler.GetCurrentStationName() == "Pot") Object.FindAnyObjectByType<PotStation>().OnSuccessfulDrop();
        }
        else
        {
            // failed; snap to original pos
            rectTransform.localPosition = originalPosition;
        }
    }

    // Overlap calculation for UI RectTransforms
    private bool IsOverlapping(RectTransform rectA, RectTransform rectB)
    {
        Rect rA = GetWorldRect(rectA);
        Rect rB = GetWorldRect(rectB);

        return rA.Overlaps(rB);
    }

    private Rect GetWorldRect(RectTransform rt)
    {
        Vector3[] corners = new Vector3[4];
        rt.GetWorldCorners(corners);
        // corners[0] is bottom-left, corners[2] is top-right
        return new Rect(corners[0].x, corners[0].y, corners[2].x - corners[0].x, corners[2].y - corners[0].y);
    }
}
