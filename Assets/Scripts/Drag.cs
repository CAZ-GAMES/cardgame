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
        // GameObject[] objectsToDisable = GameObject.FindGameObjectsWithTag("Card");

        // foreach (GameObject objectToDisable in objectsToDisable)
        // {
        //     objectToDisable.GetComponent<BoxCollider2D>().isTrigger = false;
        // }

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

    public void OnEndDrag(PointerEventData eventData)
    {
        // this.transform.position = posToReturnTo;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        //gameObject.GetComponent<BoxCollider2D>().enabled = false;
        var myTween = transform.DOMove(posToReturnTo, 0.3f);
        myTween.OnComplete(() =>
        {
            //gameObject.GetComponent<BoxCollider2D>().enabled = true;
            // transform.DOPunchPosition(transform.position, .25f, 1, 0f, false);
        });
    }

}
