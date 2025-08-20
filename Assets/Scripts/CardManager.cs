using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField]
    GameObject PlayerFaceDown0;
    [SerializeField]
    GameObject PlayerFaceDown1;
    [SerializeField]
    GameObject PlayerFaceDown2;
    void OnEnable()
    {
        GameManager.OnGameStateChanged += FaceUpFreeze;
    }

    void OnDisable()
    {
        GameManager.OnGameStateChanged -= FaceUpFreeze;
    }

    void FaceUpFreeze(GameState state)
    {
        // player cards are frozen until endgame
        if (state == GameState.Ready)
        {
            PlayerFaceDown0.transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
            PlayerFaceDown1.transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
            PlayerFaceDown2.transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
