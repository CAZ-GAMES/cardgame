using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.Timeline;
using NUnit.Framework;
using Unity.VisualScripting;
public class Setup : MonoBehaviour
{
    public GameObject prefab;
    public static List<Sprite> restOfDeck = new List<Sprite>();

    [Header("Computer Setup")]
    [SerializeField] GameObject[] compFaceDown = new GameObject[3];
    [SerializeField] GameObject[] compFaceUp = new GameObject[3];
    [SerializeField] GameObject[] compHand = new GameObject[3];
    [SerializeField] GameObject[] playerFaceDown = new GameObject[3];
    [SerializeField] GameObject[] playerFaceUp = new GameObject[3];
    [SerializeField] GameObject[] playerHand = new GameObject[3];
    [SerializeField] List<GameObject> deck = new List<GameObject>();

    [SerializeField] GameObject deckCover;

    [SerializeField] Sprite[] sprites;
    [SerializeField] Sprite cardBack;
    Sprite[] spritesShuffled;
    Stack<Sprite> spritesShuffledStack = new Stack<Sprite>();
    int cardsDealt = 0;

    void OnEnable()
    {
        GameManager.OnGameStateChanged += RunCoroutine;
    }

    void OnDisable()
    {
        GameManager.OnGameStateChanged -= RunCoroutine;
    }
    //awake needs to be called so that everything is shuffled before anything else is called
    void Awake()
    {
        spritesShuffled = Shuffle<Sprite>(sprites);
        for (int i = 0; i < spritesShuffled.Length; i++)
        {
            spritesShuffledStack.Push(spritesShuffled[i]);
        }
    }
    T[] Shuffle<T>(T[] arr)
    {
        System.Random random = new System.Random();
        return arr.OrderBy(x => random.Next()).ToArray();
    }

    // runs the coroutine to get the cycle going to setup all cards on screen and in deck
    private void RunCoroutine(GameState state)
    {
        if (state == GameState.ShuffleUpAndDeal)
        {
            StartCoroutine(SetCompDownCards());
        }
    }


    IEnumerator SetCompDownCards()
    {
        for (int i = 0; i < compFaceDown.Length; i++)
        {
            var pre = Instantiate(prefab, transform.position, Quaternion.identity);
            pre.GetComponent<Drag>().faceDown = true;
            pre.GetComponent<Drag>().faceSprite = spritesShuffled[cardsDealt];
            pre.GetComponent<SpriteRenderer>().sprite = cardBack;
            // pre.name = spritesShuffled[cardsDealt].name;
            pre.name = spritesShuffledStack.Pop().name;
            pre.transform.SetParent(compFaceDown[i].transform);
            pre.transform.DOMove(compFaceDown[i].transform.position, 0.35f);
            pre.GetComponent<BoxCollider2D>().enabled = false;
            cardsDealt++;
            yield return new WaitForSeconds(0.5f);
        }
        StartCoroutine(SetCompUpCards());
    }
    IEnumerator SetCompUpCards()
    {
        for (int i = 0; i < compFaceUp.Length; i++)
        {
            var pre = Instantiate(prefab, transform.position, Quaternion.identity);
            pre.GetComponent<SpriteRenderer>().sprite = spritesShuffled[cardsDealt];
            pre.GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("FaceUp");
            pre.name = spritesShuffled[cardsDealt].name;
            pre.transform.SetParent(compFaceUp[i].transform);
            pre.transform.DOMove(compFaceUp[i].transform.position, 0.35f);
            pre.GetComponent<BoxCollider2D>().enabled = false;
            cardsDealt++;
            yield return new WaitForSeconds(0.5f);
        }
        StartCoroutine(SetCompHand());
    }
    IEnumerator SetCompHand()
    {
        for (int i = 0; i < compHand.Length; i++)
        {
            var pre = Instantiate(prefab, transform.position, Quaternion.identity);
            pre.GetComponent<SpriteRenderer>().sprite = spritesShuffled[cardsDealt];
            pre.GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("FaceUp");
            pre.name = spritesShuffled[cardsDealt].name;
            pre.transform.SetParent(compHand[i].transform);
            pre.transform.DOMove(compHand[i].transform.position, 0.35f);
            pre.GetComponent<BoxCollider2D>().enabled = false;
            cardsDealt++;
            yield return new WaitForSeconds(0.5f);
        }
        StartCoroutine(SetPlayerDownCards());
    }

    IEnumerator SetPlayerDownCards()
    {
        for (int i = 0; i < playerFaceDown.Length; i++)
        {
            var pre = Instantiate(prefab, transform.position, Quaternion.identity);
            pre.GetComponent<Drag>().faceDown = true;
            pre.GetComponent<Drag>().faceSprite = spritesShuffled[cardsDealt];
            pre.GetComponent<SpriteRenderer>().sprite = cardBack;
            pre.name = spritesShuffled[cardsDealt].name;
            pre.transform.SetParent(playerFaceDown[i].transform);
            pre.transform.DOMove(playerFaceDown[i].transform.position, 0.35f);
            pre.GetComponent<BoxCollider2D>().enabled = false;
            cardsDealt++;
            yield return new WaitForSeconds(0.5f);
        }
        StartCoroutine(SetPlayerUpCards());
    }

    IEnumerator SetPlayerUpCards()
    {
        for (int i = 0; i < playerFaceUp.Length; i++)
        {
            var pre = Instantiate(prefab, transform.position, Quaternion.identity);
            pre.GetComponent<SpriteRenderer>().sprite = spritesShuffled[cardsDealt];
            pre.GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("FaceUp");
            pre.name = spritesShuffled[cardsDealt].name;
            pre.transform.SetParent(playerFaceUp[i].transform);
            pre.transform.DOMove(playerFaceUp[i].transform.position, 0.35f);
            pre.GetComponent<BoxCollider2D>().enabled = false;
            cardsDealt++;
            yield return new WaitForSeconds(0.5f);
        }
        StartCoroutine(SetPlayerHand());
    }

    IEnumerator SetPlayerHand()
    {
        for (int i = 0; i < playerHand.Length; i++)
        {
            var pre = Instantiate(prefab, transform.position, Quaternion.identity);
            pre.GetComponent<SpriteRenderer>().sprite = spritesShuffled[cardsDealt];
            pre.GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("FaceUp");
            pre.name = spritesShuffled[cardsDealt].name;
            pre.transform.SetParent(playerHand[i].transform);
            pre.transform.DOMove(playerHand[i].transform.position, 0.35f);
            pre.GetComponent<BoxCollider2D>().enabled = false;
            cardsDealt++;
            yield return new WaitForSeconds(0.5f);
        }
        DrawableDeck();
    }

    void DrawableDeck()
    {
        for (int i = cardsDealt; i < spritesShuffled.Length; i++)
        {
            restOfDeck.Add(spritesShuffled[i]);
        }
        for (int i = 0; i < restOfDeck.Count; i++)
        {
            var pre = Instantiate(prefab, transform.position, Quaternion.identity);
            pre.GetComponent<Drag>().faceDown = true;
            pre.GetComponent<SpriteRenderer>().sprite = cardBack;
            pre.GetComponent<Drag>().faceSprite = spritesShuffled[cardsDealt];
            pre.name = spritesShuffled[cardsDealt].name;
            pre.transform.SetParent(deck[0].transform);
            pre.transform.DOMove(deck[0].transform.position, 0.35f);
            pre.GetComponent<BoxCollider2D>().enabled = false;
            cardsDealt++;
        }
        // call DrawDeck function in the deck script to get started
        var drawDeck = GameObject.Find("Deck").GetComponent<DrawDeck>();
        drawDeck.SetCardsActive();
        // activate deckCover to have between card pick ups
        deckCover.GetComponent<SpriteRenderer>().enabled = true;

        // change gamestate to player ready up
        GameManager.Instance.UpdateGameState(GameState.PlayerReadyUp);
    }
}
