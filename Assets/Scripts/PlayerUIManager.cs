using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    public List<GameObject> PlayerUI;
    private int numberOfPlayers;
    [SerializeField] public List<Player> players = new List<Player>();
    public GameManager GameManagerScript;
    private int CurrentPlayerindex;
    private float time = 0f;
    void Start()
    {
        CurrentPlayerindex = GameManagerScript.currentPlayerIndex;
        numberOfPlayers = PlayerPrefs.GetInt("numberOfPlayers");
        for (int i = numberOfPlayers; i < 5; i++)
        {
            PlayerUI[i].SetActive(false);
        }
        for (int i = 0; i < numberOfPlayers; i++)
        {
            TextMeshProUGUI name = PlayerUI[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            name.text = players[i].playerName;
            TextMeshProUGUI blocks = PlayerUI[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            blocks.text = players[i].ownedTerritories.Count.ToString();
            TextMeshProUGUI troops = PlayerUI[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            troops.text = players[i].playertroops.ToString();
            if (i == CurrentPlayerindex)
                {
                    PlayerUI[i].transform.GetChild(3).gameObject.SetActive(true);
                }
            else
                {
                    PlayerUI[i].transform.GetChild(3).gameObject.SetActive(false);
                }
        }
    }
    void Update()//Logic for updating Player UI
    {   
        CurrentPlayerindex = GameManagerScript.currentPlayerIndex;
        time += Time.deltaTime;//Giving a buffer for 1 sec as its update is not that important
        if (time > 1)
        {
            for (int i = 0; i < numberOfPlayers; i++)
            {
                TextMeshProUGUI name = PlayerUI[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                name.text = players[i].playerName;
                TextMeshProUGUI blocks = PlayerUI[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>();
                blocks.text = players[i].ownedTerritories.Count.ToString();
                TextMeshProUGUI troops = PlayerUI[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>();
                troops.text = players[i].playertroops.ToString();
                if (i == CurrentPlayerindex)
                {
                    PlayerUI[i].transform.GetChild(3).gameObject.SetActive(true);
                }
                else
                {
                    PlayerUI[i].transform.GetChild(3).gameObject.SetActive(false);
                }
            }
            time = 0;
        }
    }
    
}


