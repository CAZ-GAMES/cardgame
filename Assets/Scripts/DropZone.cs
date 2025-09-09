using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IPointerUpHandler, IPointerClickHandler, IDropHandler
{
    [SerializeField] HandManager playerHand = new HandManager();
    public void DropCardOnlyOnPlayerPile(PointerEventData eventData)
    {
        print("Dropped in card only function: " + eventData.pointerDrag);
        if (this.transform.childCount > 0)
        {
            print(this.transform.GetChild(0).gameObject.name);
            Vector3 v3 = new Vector3(0, 0, 0);
            this.transform.GetChild(0).DOMove(eventData.pointerDrag.gameObject.GetComponent<Drag>().originalPosition, 0.3f);
            this.transform.GetChild(0).SetParent(eventData.pointerDrag.gameObject.GetComponent<Drag>().originalParent);
            eventData.pointerDrag.transform.SetParent(this.transform);
            eventData.pointerDrag.gameObject.GetComponent<Drag>().posToReturnTo = this.transform.position;
        }
        else
        {
            eventData.pointerDrag.transform.SetParent(this.transform);
            eventData.pointerDrag.gameObject.GetComponent<Drag>().posToReturnTo = this.transform.position;
        }
    }

    // use this to set the position for the card 
    public void OnDrop(PointerEventData eventData)
    {
        OnlyPlaceDeckCardsInPlayerHand(eventData);
    }
    public void OnPointerClick(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        print("Mouse up on: " + gameObject.name);
    }
    private void OnMouseUp()
    {

        print("UP");
    }

    public void OnlyPlaceDeckCardsInPlayerHand(PointerEventData eventData)
    {
        print("DROPZONE!");
        print(this.transform.GetChild(0).gameObject.name);
        // playerHand.handCards.Add()
    }

}
