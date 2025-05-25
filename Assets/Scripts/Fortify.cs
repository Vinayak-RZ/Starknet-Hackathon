using System.Collections.Generic;
using UnityEngine;

public class Fortify : MonoBehaviour
{
    [SerializeField] public Player player;
    private Territory sourceTerritory;
    private Territory targetTerritory;
    private List<Territory> possibleTargetTerritories = new List<Territory>();

    void Update()
    {
        // return;
        if (Input.GetMouseButtonDown(0))  
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit2D = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit2D.collider != null)
            {
                Territory territory = hit2D.collider.GetComponent<Territory>();
                if (territory != null)
                {
                    TerritoryClicked(territory);
                }
            }
        }
    }

    void TerritoryClicked(Territory territory)
    {
        Debug.Log($"Clicked on territory: {territory.territoryName}");
        if (territory == sourceTerritory)
        {
            Debug.Log("Clicked on the same territory, deselecting.");
            DeselectSourceTerritory();
            sourceTerritory = null;
        }
        else if (territory.owner == player)
        {
            Debug.Log("Clicked on own territory.");
            if (possibleTargetTerritories.Contains(territory))
            {
                Debug.Log("Clicked on a valid target territory for fortification.");
                targetTerritory = territory;
                FortifyTroops();
                DeselectSourceTerritory();
                sourceTerritory = null;
                targetTerritory = null;
            }
            else if (sourceTerritory == null && territory.Troopscount > 1)
            {
                Debug.Log("Selected source territory for fortification.");
                SelectSourceTerritory(territory);
                sourceTerritory = territory;
            }
        }
    }

    void SelectSourceTerritory(Territory territory)
    {
        // TODO: Highlight the selected territory and all possible target territories
        possibleTargetTerritories.Clear();
        FindPossibleTargetTerritories(territory, territory);
        territory.UpdateTerritoryColor(2.5f); 
        foreach (Territory target in possibleTargetTerritories)
        {
            target.UpdateTerritoryColor(1.5f); 
        }
    }

    void DeselectSourceTerritory()
    {
        // TODO: Remove highlight from the selected territory and target territories
        sourceTerritory.UpdateTerritoryColor();
        foreach (Territory target in possibleTargetTerritories)
        {
            target.UpdateTerritoryColor(); 
        }
        possibleTargetTerritories.Clear();
    }

    void FortifyTroops()
    {
        int troopsToMove = Mathf.Max(1, sourceTerritory.Troopscount - 1); // Move at least 1 troop
        sourceTerritory.Troopscount -= troopsToMove;
        targetTerritory.Troopscount += troopsToMove;

        sourceTerritory.UpdateTerritoryCountText();
        targetTerritory.UpdateTerritoryCountText();
    }


    void FindPossibleTargetTerritories(Territory territory, Territory sourceTerritory)
    {
        foreach (Territory neighbor in territory.neighbors)
        {
            if (neighbor.owner == player && !possibleTargetTerritories.Contains(neighbor) && neighbor != sourceTerritory)
            {
                possibleTargetTerritories.Add(neighbor);
                FindPossibleTargetTerritories(neighbor, sourceTerritory);
            }
        }
    }
}
