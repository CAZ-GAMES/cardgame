using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Splines;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;

public class HandManager : MonoBehaviour
{
    [SerializeField] private int maxHandSize;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private SplineContainer splineContainer;
    [SerializeField] private float dealSpeed = 0.25f;
    [SerializeField] private float splineRange = 0.6f; // How much of the spline to use (0.2 to 0.8)

    public List<GameObject> handCards = new();

    public void DrawCard(Vector3 spawnPosition, Sprite sprite)
    {
        if (handCards.Count >= maxHandSize) return;
        
        var newCard = Instantiate(cardPrefab, spawnPosition, Quaternion.identity);
        newCard.GetComponent<SpriteRenderer>().sprite = sprite;
        newCard.GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("FaceUp");
        newCard.name = sprite.name;
        
        // Set proper parent - all cards should be children of HandManager
        newCard.transform.SetParent(this.transform);
        
        handCards.Add(newCard);
        
        // Disable collider during animation if needed
        var collider = newCard.GetComponent<BoxCollider2D>();
        if (collider != null)
            collider.enabled = false;
        
        UpdateCardPositions();
        
        // Re-enable collider after animation
        if (collider != null)
        {
            DOVirtual.DelayedCall(dealSpeed, () => {
                if (newCard != null) collider.enabled = true;
            });
        }
    }

    public void UpdateCardPositions()
    {
        if (handCards.Count == 0 || splineContainer?.Spline == null) return;

        Spline spline = splineContainer.Spline;
        
        for (int i = 0; i < handCards.Count; i++)
        {
            if (handCards[i] == null) continue;
            
            // Calculate position along spline (0 to 1)
            float t = handCards.Count == 1 ? 0.5f : 
                     (0.5f - splineRange/2f) + (i / (float)(handCards.Count - 1)) * splineRange;
            
            // Clamp t to valid range
            t = Mathf.Clamp01(t);
            
            // Get spline data
            float3 splinePos = spline.EvaluatePosition(t);
            float3 splineTangent = spline.EvaluateTangent(t);
            
            // Convert to Vector3 and normalize
            Vector3 splinePosition = splinePos;
            Vector3 tangent = math.normalize(splineTangent);
            
            // For 2D card games, usually rotate around Z axis
            float angle = Mathf.Atan2(tangent.y, tangent.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0, 0, angle); // Try without the -90f first
            
            // Animate to new position and rotation
            handCards[i].transform.DOMove(splinePosition, dealSpeed).SetEase(Ease.OutCubic);
            handCards[i].transform.DORotateQuaternion(rotation, dealSpeed).SetEase(Ease.OutCubic);
            
            // Update sorting order so cards fan properly
            var renderer = handCards[i].GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.sortingOrder = i;
            }
        }
    }

    public void RemoveCard(GameObject card)
    {
        if (handCards.Contains(card))
        {
            handCards.Remove(card);
            Destroy(card);
            UpdateCardPositions();
        }
    }

    public void RemoveCardAt(int index)
    {
        if (index >= 0 && index < handCards.Count)
        {
            GameObject cardToRemove = handCards[index];
            handCards.RemoveAt(index);
            Destroy(cardToRemove);
            UpdateCardPositions();
        }
    }

    private void OnValidate()
    {
        // Clamp values in inspector
        maxHandSize = Mathf.Max(1, maxHandSize);
        splineRange = Mathf.Clamp01(splineRange);
    }
}