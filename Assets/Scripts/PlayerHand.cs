using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class PlayerHand : MonoBehaviour, IDropHandler
{
    int playPileSize = 0;
    public void OnDrop(PointerEventData eventData)
    {
        OnlyPlaceDeckCardsInPlayerHand(eventData);
    }

    public void OnlyPlaceDeckCardsInPlayerHand(PointerEventData eventData)
    {
        if (eventData.pointerDrag.gameObject.GetComponent<Drag>().originalParent.name == "Deck")
        {
            print("What was dropped: " + eventData.pointerDrag);
            if (this.transform.childCount > 0)
            {
                print(this.transform.GetChild(playPileSize - 1).gameObject.name);
                this.transform.GetChild(playPileSize - 1).gameObject.SetActive(false);
                eventData.pointerDrag.transform.GetComponent<Renderer>().sortingOrder = 1;
                eventData.pointerDrag.transform.SetParent(this.transform);
                eventData.pointerDrag.gameObject.GetComponent<Drag>().posToReturnTo = this.transform.position;
                playPileSize++;
            }
            else
            {
                eventData.pointerDrag.transform.GetComponent<Renderer>().sortingOrder = 1;
                eventData.pointerDrag.transform.SetParent(this.transform);
                eventData.pointerDrag.gameObject.GetComponent<Drag>().posToReturnTo = this.transform.position;
                playPileSize++;
            }
        }
    }
}
