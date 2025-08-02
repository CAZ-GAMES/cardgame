using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using Unity.VisualScripting;

public class DrawDeck : MonoBehaviour
{
    //     public void OnDrop(PointerEventData eventData)
    //     {
    //         print("What was dropped: " + eventData.pointerDrag);
    //         if (this.transform.childCount > 0)
    //         {
    //             this.transform.GetChild(0).gameObject.SetActive(false);
    //             eventData.pointerDrag.transform.GetComponent<Renderer>().sortingOrder = 1;
    //             eventData.pointerDrag.transform.SetParent(this.transform);
    //             eventData.pointerDrag.gameObject.GetComponent<Drag>().posToReturnTo = this.transform.position;
    //         }
    //         else
    //         {
    //             eventData.pointerDrag.transform.GetComponent<Renderer>().sortingOrder = 1;
    //             eventData.pointerDrag.transform.SetParent(this.transform);
    //             eventData.pointerDrag.gameObject.GetComponent<Drag>().posToReturnTo = this.transform.position;

    //         }
    //     }
    void Start()
    {
        print(this.transform.childCount);
    }
}
