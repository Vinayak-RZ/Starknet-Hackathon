using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class Attack : MonoBehaviour
{
    private Territory attackingTerritory;
    private Territory defenderTerritory;
    [SerializeField] public Player player;
    private Color originalAttackingColor;
    public GameObject drawingPrefab;
    public GameObject damageIndicatorPrefab;
    private List<LineRenderer> lineRenderers = new List<LineRenderer>();

    void Update()
    {
        return;
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
        Debug.Log($"Owner: {territory.owner.playerName}, Troops: {territory.Troopscount}");
        if (territory == attackingTerritory)
        {
            Debug.Log("Clicked on the same territory, deselecting.");
            DeselectAttackingTerritory();
            attackingTerritory = null;
        }
        else if (territory.owner == player)
        {
            Debug.Log("Clicked on own territory.");
            if (attackingTerritory != null)
            {
                DeselectAttackingTerritory();
                attackingTerritory = null;
            }

            if (territory.Troopscount > 1)
            {
                attackingTerritory = territory;
                // TODO: Highlight the selected territory
                // TODO: Show neighbour enemy territories
                SelectAttackingTerritory(attackingTerritory);
            }
            
        }
        else if(attackingTerritory != null)
        {
            Debug.Log("Clicked on enemy territory.");
            if (territory.owner != player && attackingTerritory.neighbors.Contains(territory))
            {
                defenderTerritory = territory;
                PerformAttack();
                DeselectAttackingTerritory();
                attackingTerritory = null; 
                defenderTerritory = null;
            }
        }
    }

    void SelectAttackingTerritory(Territory territory)
    {
        Transform childofterr = territory.gameObject.transform.GetChild(0);
        SpriteRenderer selectedSpriteRenderer = childofterr.GetComponent<SpriteRenderer>();

        if (selectedSpriteRenderer != null)
        {
            originalAttackingColor = selectedSpriteRenderer.color;
            selectedSpriteRenderer.color = originalAttackingColor*1.5f; 
        }

        DrawNeighborLines(attackingTerritory);
    }

    void DeselectAttackingTerritory()
    {
        if (attackingTerritory != null)
        {
            Transform childofterr = attackingTerritory.gameObject.transform.GetChild(0);
            SpriteRenderer selectedSpriteRenderer = childofterr.GetComponent<SpriteRenderer>();
            selectedSpriteRenderer.color = originalAttackingColor;
            ClearNeighborLines();
        }
    }

    void PerformAttack()
    {
        Debug.Log($"Attacking {defenderTerritory.territoryName} from {attackingTerritory.territoryName}");
        int attackerArmies = attackingTerritory.Troopscount; // Leave 1 troop behind
        int defenderArmies = defenderTerritory.Troopscount;

        int attackDamage = 0;
        int defenceDamage = 0;

        while (attackerArmies > 1 && defenderArmies > 0)
        {
            int attackDice = Mathf.Min(3, attackerArmies - 1);
            int defendDice = Mathf.Min(2, defenderArmies);

        

            List<int> attackerRolls = RollDice(attackDice);
            List<int> defenderRolls = RollDice(defendDice);

            attackerRolls.Sort((a, b) => b.CompareTo(a)); 
            defenderRolls.Sort((a, b) => b.CompareTo(a)); 

            Debug.Log($"Attacker rolls: {string.Join(", ", attackerRolls)}");
            Debug.Log($"Defender rolls: {string.Join(", ", defenderRolls)}");

            int comparisons = Mathf.Min(attackerRolls.Count, defenderRolls.Count);
            for (int i = 0; i < comparisons; i++)
            {
                if (attackerRolls[i] > defenderRolls[i])
                {
                    defenderArmies--;
                    defenceDamage++;
                }
                else
                {
                    attackerArmies--;
                    attackDamage++;
                }
            }

            Debug.Log($"Remaining - Attacker: {attackerArmies}, Defender: {defenderArmies}");
        }
        
        ShowDamage(attackingTerritory.gameObject, attackDamage);
        ShowDamage(defenderTerritory.gameObject, defenceDamage);

        Debug.Log(attackerArmies > 1 ? "Attacker wins!" : "Defender holds!");
        if (attackerArmies > 1)
        {
            defenderTerritory.owner = attackingTerritory.owner;
            defenderTerritory.Troopscount = attackerArmies - 1;
            attackingTerritory.Troopscount = 1; 
            defenderTerritory.UpdateTerritoryOwner(attackingTerritory.owner);
        }
        else
        {
            defenderTerritory.Troopscount = defenderArmies;
            attackingTerritory.Troopscount = attackerArmies;
        }

        attackingTerritory.UpdateTerritoryCountText();
        defenderTerritory.UpdateTerritoryCountText();
    }

    List<int> RollDice(int count)
    {
        List<int> results = new List<int>();
        for (int i = 0; i < count; i++)
        {
            results.Add(Random.Range(1, 7)); // 1 to 6 inclusive
        }
        return results;
    }

    void DrawNeighborLines(Territory territory)
    {
        foreach (Territory neighbor in territory.neighbors)
        {
            GameObject lineObj = Instantiate(drawingPrefab);
            lineObj.transform.parent = territory.transform;

            LineRenderer lr = lineObj.GetComponent<LineRenderer>();
            lr.positionCount = 2;
            lr.startWidth = 0.1f;
            lr.endWidth = 0.1f;
            lr.startColor = Color.cyan;
            lr.endColor = Color.cyan;

            Vector3 startPos = territory.transform.position;
            Vector3 endPos = neighbor.transform.position;
            startPos.z = 0f;  // keep lines in 2D plane
            endPos.z = 0f;

            lr.SetPosition(0, startPos);
            lr.SetPosition(1, endPos);

            lineRenderers.Add(lr);
        }
    }

    void ClearNeighborLines()
    {
        foreach (var lr in lineRenderers)
        {
            if (lr != null)
                Destroy(lr.gameObject);
        }
        lineRenderers.Clear();
    }

    void ShowDamage(GameObject target, int damage)
    {
        if (damage <= 0) return;

        if (damageIndicatorPrefab != null && target != null)
        {
            Vector3 spawnPos = target.transform.position + Vector3.up * 2;
            Debug.Log($"Spawning damage indicator at {spawnPos}");
            GameObject damageIndicator = Instantiate(damageIndicatorPrefab, spawnPos, Quaternion.identity);
            
            TextMeshPro tmp = damageIndicator.GetComponentInChildren<TextMeshPro>();
            // tmp.gameObject.transform.position = spawnPos;
            if (tmp != null) {
                tmp.text = $"-{damage}";
            }
        }
    }
    
}


