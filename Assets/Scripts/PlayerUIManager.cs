using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    public List<GameObject> PlayerUI;

    [SerializeField] private int numberOfPlayers; 
    [SerializeField] public List<Player> players = new List<Player>();
    private float time;
    void Start()
    {
        numberOfPlayers = PlayerPrefs.GetInt("numberOfPlayers");
        for (int i = numberOfPlayers; i < 5; i++)
        {
            PlayerUI[i].SetActive(false);
        }
    }
    void Update()//Logic for updating Player UI
    {
        time = Time.deltaTime;//Giving a buffer for 1 sec as its update is not that important
        if (time > 1)
        {
            for (int i = 0; i < numberOfPlayers; i++)
            {
                TextMeshProUGUI name = PlayerUI[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                name.text = players[i].playerName;
                
            }
            time = 0;
        }
    }
}


