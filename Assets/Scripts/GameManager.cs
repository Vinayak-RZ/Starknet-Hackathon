using UnityEngine;
using System.Collections.Generic;

public enum GamePhase
{
    Draft,
    Attack,
    Fortify
}

public class GameManager : MonoBehaviour
{
    
    public List<Player> players;  
    public List<int> playerOrder; // Order of players for turns
    public int currentPlayerIndex = 0;
    public GamePhase currentPhase = GamePhase.Draft;

    public Attack attackScript;
    public Fortify fortifyScript;
    public Draft draftScript;

    private Player CurrentPlayer => players[currentPlayerIndex];

    private void Start()
    {
        int numberOfPlayers = PlayerPrefs.GetInt("numberOfPlayers");
        // TODO: initiate players list
        SetActivePlayerComponents();
        Debug.Log($"Game Start: {CurrentPlayer.playerName}'s turn begins. Phase: {currentPhase}");
    }

    private void Update()
    {
        // For testing purposes, press space to move to next phase.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextPhase();
        }
    }

    public void NextPhase()
    {
        if (currentPhase == GamePhase.Draft)
        {
            currentPhase = GamePhase.Attack;
        }
        else if (currentPhase == GamePhase.Attack)
        {
            currentPhase = GamePhase.Fortify;
        }
        else
        {
            EndTurn();
        }

        Debug.Log($"Phase changed to: {currentPhase}");
        SetActivePlayerComponents();
    }

    private void EndTurn()
    {
        currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
        currentPhase = GamePhase.Draft;
        Debug.Log($"Turn ended. Next player: {CurrentPlayer.playerName}");
        SetActivePlayerComponents();
    }

    private void SetActivePlayerComponents()
    {
        if (attackScript != null)
        {
            attackScript.enabled = currentPhase == GamePhase.Attack;
            attackScript.player = CurrentPlayer;
        }

        if (fortifyScript != null)
        {
            fortifyScript.enabled = currentPhase == GamePhase.Fortify;
            fortifyScript.player = CurrentPlayer;
        }

        if (draftScript != null)
        {
            draftScript.enabled = currentPhase == GamePhase.Draft;
            draftScript.player = CurrentPlayer;
        }
    }
}
