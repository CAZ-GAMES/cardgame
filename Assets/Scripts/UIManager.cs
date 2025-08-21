using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{

    [SerializeField]
    GameObject readyUP;
    void OnEnable()
    {
        // subscribe to action that triggers once all cards are dealt
        GameManager.OnGameStateChanged += ShowButton;
    }

    void OnDisable()
    {
        // unsubscribe to avoid issues
        GameManager.OnGameStateChanged -= ShowButton;
    }

    void ShowButton(GameState state)
    {
        // activate UI Button to start game when player is ready
        readyUP.SetActive(state == GameState.PlayerReadyUp);
    }

    public void PlayerReady()
    {
        readyUP.SetActive(false);
        GameManager.Instance.UpdateGameState(GameState.Ready);
        // make faceup cards not interactable

    }
}