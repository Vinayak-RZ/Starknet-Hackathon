using System.Collections.Generic;
using UnityEngine;

public class Draft : MonoBehaviour
{
    [SerializeField] public Player player;
    private Territory targetTerritory;
    public int troopsToDraft = 3;

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
         if (territory.owner == player && troopsToDraft > 0)
        {
            Debug.Log("Clicked on own territory.");
            troopsToDraft -= 1;
            territory.Troopscount += 1; 
            territory.UpdateTerritoryCountText();
        }
    }
}
