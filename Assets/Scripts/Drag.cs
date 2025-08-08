using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Vector3 posToReturnTo;
    public Vector3 originalPosition;
    public Transform originalParent;

    public Sprite faceSprite;
    public bool faceDown;
    void Start()
    {
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        this.gameObject.GetComponent<Renderer>().sortingOrder = 99;

        posToReturnTo = transform.position;
        originalPosition = transform.position;
        originalParent = transform.parent;
        if (faceDown)
        {
            this.GetComponent<SpriteRenderer>().sprite = faceSprite;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Get the current position of the pointer
        Vector3 newPosition = eventData.position;
        
        newPosition = Camera.main.ScreenToWorldPoint(new Vector3(newPosition.x, newPosition.y, 10)); // Adjust 10 for your desired distance
        
        // Update the GameObject's position
        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
    }

    public void PlaceCardPerfectlyInSlot()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;

        var myTween = transform.DOMove(posToReturnTo, 0.3f);
        myTween.OnComplete(() =>
        {
            //gameObject.GetComponent<BoxCollider2D>().enabled = true;
            // transform.DOPunchPosition(transform.position, .25f, 1, 0f, false);
        });
    }

    public void ReturnCardToOriginSlot()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;

        var myTween = transform.DOMove(originalPosition, 0.3f);
        myTween.OnComplete(() =>
        {
            //gameObject.GetComponent<BoxCollider2D>().enabled = true;
            // transform.DOPunchPosition(transform.position, .25f, 1, 0f, false);
        });
    }

    public void OnlyPlacePlayerHandCardInPlayPile(PointerEventData eventData)
    {
        print("Original Parent: " + eventData.pointerDrag.gameObject.GetComponent<Drag>().originalParent.name);

        if (
            (eventData.pointerDrag.gameObject.GetComponent<Drag>().originalParent.name == "Player Hand 0")
            || (eventData.pointerDrag.gameObject.GetComponent<Drag>().originalParent.name == "Player Hand 1")
            || (eventData.pointerDrag.gameObject.GetComponent<Drag>().originalParent.name == "Player Hand 2")
        )
        {
            PlaceCardPerfectlyInSlot();
        }
        else
        {
            ReturnCardToOriginSlot();
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        OnlyPlacePlayerHandCardInPlayPile(eventData);
    }

}
