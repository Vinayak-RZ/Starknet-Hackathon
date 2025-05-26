using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Scriptable Objects/Player")]
public class Player :ScriptableObject {
    public string playerName;
    public int playertroops;
    public Color playerColor;
    public List<Territory> ownedTerritories;
}

