using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IPointerUpHandler, IPointerClickHandler, IDropHandler
{
    // use this to set the position for the card 
    public void OnDrop(PointerEventData eventData)
    {
        print("What was dropped: " + eventData.pointerDrag);
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

    // void OnTriggerEnter2D(Collider2D collision)
    // {
    //     collision.gameObject.GetComponent<Drag>().posToReturnTo = this.transform.position;
    // }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (transform.childCount > 0 && transform.GetChild(0).name != collision.name)
        {
            
            // swap objects here
            //print(collision);
            //print(collision.transform.parent.name);

            //transform.GetChild(0).DOMove(collision.transform.GetComponent<Drag>().posToReturnTo, 0.1f);
            //transform.GetChild(0).transform.SetParent(collision.transform.parent, true);

        }
        else
        {
            //collision.gameObject.GetComponent<Drag>().posToReturnTo = this.transform.position;
            //collision.gameObject.transform.SetParent(this.transform, false);
        }

    }
    void OnTriggerStay2D(Collider2D collision)
    {
        // if (transform.childCount > 0 && transform.GetChild(0).name != collision.name)
        // {
        //     // swap objects here
        //     //print(collision);
        //     print(collision.transform.parent.name);

        //     transform.GetChild(0).DOMove(collision.transform.GetComponent<Drag>().posToReturnTo, 0.1f);
        //     transform.GetChild(0).transform.SetParent(collision.transform.parent, true);

        // }
        // else
        // {
        //     collision.gameObject.GetComponent<Drag>().posToReturnTo = this.transform.position;
        //     collision.gameObject.transform.SetParent(this.transform, false);
        // }
    }


}
