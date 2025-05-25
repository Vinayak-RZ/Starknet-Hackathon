using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;

public class Territory : MonoBehaviour
{
    [SerializeField] public string territoryName;
    [SerializeField] public Player owner;
    [SerializeField] public int Troopscount;
    [SerializeField] public List<Territory> neighbors = new List<Territory>();  // Reference to the other territories

    public void UpdateTerritoryCountText()
    {
        GameObject parentObj = gameObject;
        Transform child = parentObj.transform.Find("Text (TMP)");
        if (child == null) {
            Debug.LogWarning($"Missing 'Text (TMP)' child in {territoryName}");
            return;
        }

        TextMeshPro tmp = child.GetComponent<TextMeshPro>();
        if (tmp != null) {
            tmp.text = Troopscount.ToString();
        }
    }

    public void UpdateTerritoryOwner(Player newOwner)
    {
        owner = newOwner;
        Transform child = gameObject.transform.GetChild(0);
        SpriteRenderer sr = child.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.color = owner.playerColor;
        }
    }

    public void UpdateTerritoryColor(bool isHighlighted = false)
    {
        Transform child = gameObject.transform.GetChild(0);
        SpriteRenderer sr = child.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.color = owner.playerColor * (isHighlighted ? 1.5f : 1f);
        }
    }
}

