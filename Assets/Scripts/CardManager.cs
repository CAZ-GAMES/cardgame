using System;
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
}
