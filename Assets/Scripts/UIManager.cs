using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{

    [SerializeField]
    GameObject readyUP;
    void OnEnable()
    {
        // subscribe to action that triggers once all cards are dealt
        Setup.OnCardsDealt += ShowButton;
    }

    void OnDisable()
    {
        // unsubscribe to avoid issues
        Setup.OnCardsDealt -= ShowButton;
    }

    void ShowButton()
    {
        // activate UI Button to start game when player is ready
        readyUP.SetActive(true);
    }
}