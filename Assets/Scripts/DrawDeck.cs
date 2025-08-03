using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using Unity.VisualScripting;
using System.Collections.Generic;

public class DrawDeck : MonoBehaviour
{
    int pileCount;
    [SerializeField]
    // cover to fill in blank space of deck to make sure it look like cards are in there
    private GameObject deckCover;
    void Update()
    {
        // check to see if card has been pulled off of drawable deck
        if (pileCount != transform.childCount && transform.childCount > 0)
        {
            Reactivate();
        }
    }

    // set all cards inactive only first card to be active
    public void SetCardsActive()
    {
        pileCount = transform.childCount;



        while (pileCount > 0)
        {
            transform.GetChild(pileCount - 1).gameObject.SetActive(false);
            pileCount--;
        }
        // set pileCount back to original number to be used in update method
        pileCount = transform.childCount;
        // activate the top card on deck to be pulled out
        transform.GetChild(0).gameObject.SetActive(true);
    }

    void Reactivate()
    {
        // active next top card
        transform.GetChild(0).gameObject.SetActive(true);
        // if the last card in deck then the deck cover should go away to show an empty deck
        if (transform.childCount == 1)
        {
            deckCover.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
