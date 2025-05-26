
using System.Collections.Generic;
using UnityEngine;

public class SimpleAI : MonoBehaviour
{
    public Player aiPlayer;
    public List<Territory> allTerritories;
    public List<Territory> aiTerritories;

    void UpdateAI(Player player)
    {
        aiPlayer = player;
        UpdateTerritories();
    }

    void UpdateTerritories()
    {
        allTerritories = new List<Territory>(FindObjectsOfType<Territory>());
        foreach (var terr in allTerritories)
        {
            if (terr.owner == aiPlayer)
                aiTerritories.Add(terr);
        }
    }

    public List<(Territory territory, int troopsToAdd)> AiDraft(int count)
    {
        Dictionary<Territory, int> threatScores = new Dictionary<Territory, int>();
        
        foreach (Territory terr in aiTerritories)
        {
            int threat = 0;
            foreach (Territory neighbor in terr.neighbors)
            {
                if (neighbor.owner != aiPlayer)
                    threat += 1 + neighbor.Troopscount;
            }
            threatScores[terr] = threat;
        }

        List<Territory> sorted = new List<Territory>(aiTerritories);
        sorted.Sort((a, b) => threatScores[b].CompareTo(threatScores[a]));

        Dictionary<Territory, int> troopsAssigned = new Dictionary<Territory, int>();
        int i = 0;
        while (count > 0 && sorted.Count > 0)
        {
            Territory target = sorted[i % sorted.Count];

            if (!troopsAssigned.ContainsKey(target))
                troopsAssigned[target] = 0;

            troopsAssigned[target]++;
            count--;
            i++;
        }

        foreach (var kvp in troopsAssigned)
        {
            result.Add((kvp.Key, kvp.Value));
        }

        return result;
    }

    public (Territory attacker, Territory defender) AiAttack(int attemptNumber)
    {
        List<(Territory attacker, Territory defender, int score)> candidates = new();

        foreach (Territory terr in allTerritories)
        {
            if (terr.owner != aiPlayer || terr.Troopscount <= 1)
                continue;

            foreach (Territory neighbor in terr.neighbors)
            {
                if (neighbor.owner == aiPlayer)
                    continue;

                int score = terr.Troopscount - neighbor.Troopscount;

                if (score > 0 || attemptNumber == 0)
                {
                    candidates.Add((terr, neighbor, score));
                }
            }
        }

        if (candidates.Count == 0)
            return (null, null);

        var best = candidates.OrderByDescending(c => c.score).First();
        return (best.attacker, best.defender);
    }

    public (Territory from, Territory to, int troopsToMove) PlanFortify()
    {
        List<Territory> aiTerritories = new();
        foreach (var terr in allTerritories)
        {
            if (terr.owner == aiPlayer)
                aiTerritories.Add(terr);
        }

        Territory bestFrom = null;
        Territory bestTo = null;
        int maxThreatDifference = int.MinValue;
        int troopsToMove = 0;

        foreach (Territory from in aiTerritories)
        {
            if (from.Troopscount <= 1)
                continue;

            foreach (Territory to in from.neighbors)
            {
                if (to.owner != aiPlayer)
                    continue;

                int fromThreat = ComputeThreatScore(from);
                int toThreat = ComputeThreatScore(to);
                int threatDiff = toThreat - fromThreat;

                int movable = from.Troopscount - 1;
                if (threatDiff > maxThreatDifference && movable > 0)
                {
                    maxThreatDifference = threatDiff;
                    bestFrom = from;
                    bestTo = to;
                    troopsToMove = movable;
                }
            }
        }

        if (bestFrom == null || bestTo == null || troopsToMove <= 0)
            return (null, null, 0);

        return (bestFrom, bestTo, troopsToMove);
    }
}