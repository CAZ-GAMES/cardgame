using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] PlayerFaceUpCards;
    [SerializeField]
    GameObject[] PlayerFaceDownCards;
    [SerializeField]
    GameObject[] PlayerHandCards;
    [SerializeField]
    GameObject[] CompFaceUpCards;
    [SerializeField]
    GameObject[] CompFaceDownCards;
    [SerializeField]
    GameObject[] CompHandCards;
    [SerializeField]
    GameObject Deck;

    public static GameObject PlayPileTopCard;


    void OnEnable()
    {
        GameManager.OnGameStateChanged += FaceUpFreeze;
        GameManager.OnGameStateChanged += UnlockPlayerCards;
    }

    void OnDisable()
    {
        GameManager.OnGameStateChanged -= FaceUpFreeze;
        GameManager.OnGameStateChanged -= UnlockPlayerCards;
    }

    private void UnlockPlayerCards(GameState state)
    {
        if (state == GameState.PlayerReadyUp)
        {
            // Unlock Player Face Up Cards
            for (int i = 0; i < PlayerFaceUpCards.Length; i++)
            {
                PlayerFaceUpCards[i].transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = true;
            }
            // Unlock Player Hand Cards
            for (int i = 0; i < PlayerHandCards.Length; i++)
            {
                PlayerHandCards[i].transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = true;
            }
            // Unlock deck for now might need to use a different call for unlocking deck
            // TODO: Make another way to call function for unlocking deck
            for (int i = 0; i < Deck.transform.childCount; i++)
            {
                Deck.transform.GetChild(i).GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }

    void FaceUpFreeze(GameState state)
    {
        // player cards are frozen until endgame
        if (state == GameState.Ready)
        {
            for (int i = 0; i < PlayerFaceUpCards.Length; i++)
            {
                PlayerFaceUpCards[i].transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }

    public static bool ValidateCard(GameObject cardToBePlayed, GameObject CardOnTop)
    {
        int CTBP = int.Parse(cardToBePlayed.name[..2]);
        // check to see if we have special cards first
        if (CTBP == 2)
        {
            // reset pile
            print("Reset Pile");
            return true;
        }
        else if (CTBP == 10)
        {
            // blow up pile
            print("Blow Up Pile");
            return true;
        }
        if (CTBP >= int.Parse(CardOnTop.name[..2]))
        {
            print("True");
            return true;
        }
        else
        {
            print("False");
            return false;
        }
    }
}
