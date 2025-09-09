using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Splines;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;

public class HandManager : MonoBehaviour
{
    [SerializeField] private int maxHandSize;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private SplineContainer splineContainer;

    public List<GameObject> handCards = new();


    public void DrawCard(Vector3 spawnPosition, Sprite sprite)
    {
        if (handCards.Count >= maxHandSize) return;
        var pre = Instantiate(cardPrefab, spawnPosition, Quaternion.identity);
        pre.GetComponent<SpriteRenderer>().sprite = sprite;
        pre.GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("FaceUp");
        pre.name = sprite.name;
        handCards.Add(pre);
        pre.transform.SetParent(handCards.Last().transform);
        // pre.transform.DOMove(playerHand[i].transform.position, dealSpeed);
        // pre.GetComponent<BoxCollider2D>().enabled = false;
        UpdateCardPosition();
    }

    public void UpdateCardPosition()
    {
        if (handCards.Count == 0) return;

        float cardSpacing = 1f / maxHandSize;
        float firstCardPosition = 0.5f - (handCards.Count - 1) * cardSpacing / 2;
        Spline spline = splineContainer.Spline;
        for (int i = 0; i < handCards.Count; i++)
        {
            float p = firstCardPosition + i * cardSpacing;
            Vector3 splinePosition = spline.EvaluatePosition(p);
            Vector3 forward = spline.EvaluateTangent(p);
            Vector3 up = spline.EvaluateUpVector(p);
            Quaternion rotation = Quaternion.LookRotation(-up, Vector3.Cross(up, forward).normalized);
            handCards[i].transform.DOMove(splinePosition, 0.25f);
            handCards[i].transform.DOLocalRotateQuaternion(rotation, 0.25f);
        }
    }
}
