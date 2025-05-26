using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AssignTerritoryAndTroops : MonoBehaviour
{
    private Territory[] territories;
    [SerializeField] private int playerinitialtroops;

    [SerializeField] public List<Player> players = new List<Player>();
    [SerializeField] private int initialTroopsPerTerritory = 1;
    [SerializeField] private int numberOfPlayers;
    void Start()
    {
        numberOfPlayers= Mathf.Max(PlayerPrefs.GetInt("numberOfPlayers"),2);
        territories = FindObjectsOfType<Territory>();
        if (numberOfPlayers == 2)
        {
            playerinitialtroops = 50;
        }
        else if (numberOfPlayers == 3)
        {
            playerinitialtroops = 35;
        }
        else if (numberOfPlayers == 4)
        {
            playerinitialtroops = 30;
        }
        else
        {
            playerinitialtroops = 25;
        }
        for (int i = 0; i < numberOfPlayers; i++)
        {
            players[i].ownedTerritories.Clear();
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
            SpriteRenderer sr = territory.transform.GetComponent<SpriteRenderer>();
            SpriteRenderer sr_child = child.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.color = currentPlayer.playerColor * 2f;
                sr_child.color = currentPlayer.playerColor;
                // sr.color = Color.blue;
            }

                playerIndex = (playerIndex + 1) % numberOfPlayers;
        }
    }
    void InitializeTroops() {
    for (int i = 0;i < numberOfPlayers; i++) {
        int numTerritories = players[i].ownedTerritories.Count;
            players[i].playertroops = playerinitialtroops;//assigning total initial troops to players for UI
        //Randomly assign remaining troops
            int remaining = playerinitialtroops- numTerritories;    

        while (remaining > 0) {
            Territory randomTerritory = players[i].ownedTerritories[UnityEngine.Random.Range(0, numTerritories)];
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
