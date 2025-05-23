using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AssignTerritoryAndTroops : MonoBehaviour
{
    private Territory[] territories;
    [SerializeField] private int total_initial_troop_each = 40;

    [SerializeField] public List<Player> players = new List<Player>();
    [SerializeField] private int initialTroopsPerTerritory = 1;

    void Start()
    {
        territories = FindObjectsOfType<Territory>();
        foreach (Player player in players) {
            player.ownedTerritories.Clear();
        }
        AssignTerritories();
        InitializeTroops();
    }

    private void AssignTerritories()
    {
        List<Territory> shuffledTerritories = new List<Territory>(territories);
        Shuffle(shuffledTerritories);

        int playerIndex = 0;

        foreach (Territory territory in shuffledTerritories)
        {
            Player currentPlayer = players[playerIndex];

            territory.owner = currentPlayer;
            if (!currentPlayer.ownedTerritories.Contains(territory))
            {
                currentPlayer.ownedTerritories.Add(territory);
            }
            territory.Troopscount = initialTroopsPerTerritory;//Giving 1 troop to each territory initially
            Transform child = territory.transform.GetChild(0);
            SpriteRenderer sr = child.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.color = currentPlayer.playerColor;
                // sr.color = Color.blue;
            }

                playerIndex = (playerIndex + 1) % players.Count;
        }
    }
    void InitializeTroops() {
    foreach (Player player in players) {
        int numTerritories = player.ownedTerritories.Count;
        //Randomly assign remaining troops
        int remaining = total_initial_troop_each - numTerritories;

        while (remaining > 0) {
            Territory randomTerritory = player.ownedTerritories[UnityEngine.Random.Range(0, numTerritories)];
            if (randomTerritory == null) continue;

            randomTerritory.Troopscount += 1;

            Transform child = randomTerritory.transform.Find("Text (TMP)");
            if (child == null) {
                Debug.LogWarning($"Missing 'Text (TMP)' child in {randomTerritory.name}");
                continue;
            }

            TextMeshPro tmp = child.GetComponent<TextMeshPro>();
            if (tmp != null) {
                tmp.text = randomTerritory.Troopscount.ToString();
            }

            remaining--;
        }
    }
}

    // Fisher–Yates shuffle
    private void Shuffle<T>(List<T> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            (list[n], list[k]) = (list[k], list[n]);
        }
    }
}
