using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class PlayPile : MonoBehaviour, IDropHandler
{
    int playPileSize = 0;
    public void OnDrop(PointerEventData eventData)
    {
        OnlyPlacePlayerHandCardInPlayPile(eventData);
    }

    public void OnlyPlacePlayerHandCardInPlayPile(PointerEventData eventData)
    {
        if (
            (eventData.pointerDrag.gameObject.GetComponent<Drag>().originalParent.name == "Player Hand 0")
            || (eventData.pointerDrag.gameObject.GetComponent<Drag>().originalParent.name == "Player Hand 1")
            || (eventData.pointerDrag.gameObject.GetComponent<Drag>().originalParent.name == "Player Hand 2")
        )
        {
            if (this.transform.childCount > 0)
            {
                //print(this.transform.GetChild(playPileSize - 1).gameObject.name);
                if (CardManager.ValidateCard(eventData.pointerDrag.gameObject, CardManager.PlayPileTopCard))
                {
                    this.transform.GetChild(playPileSize - 1).gameObject.SetActive(false);
                    eventData.pointerDrag.transform.GetComponent<Renderer>().sortingOrder = 1;
                    eventData.pointerDrag.transform.SetParent(this.transform);
                    eventData.pointerDrag.gameObject.GetComponent<Drag>().posToReturnTo = this.transform.position;
                    CardManager.PlayPileTopCard = eventData.pointerDrag.gameObject;
                    playPileSize++;
                }
            }
                else
                {
                    eventData.pointerDrag.transform.GetComponent<Renderer>().sortingOrder = 1;
                    eventData.pointerDrag.transform.SetParent(this.transform);
                    eventData.pointerDrag.gameObject.GetComponent<Drag>().posToReturnTo = this.transform.position;
                    CardManager.PlayPileTopCard = eventData.pointerDrag.gameObject;
                    playPileSize++;
                }
        }
    }
}
