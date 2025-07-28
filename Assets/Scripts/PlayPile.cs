using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class PlayPile : MonoBehaviour, IDropHandler
{
    int playPileSize = 0;
    public void OnDrop(PointerEventData eventData)
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
