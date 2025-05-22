using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Scriptable Objects/Player")]
public class Player {
    public string playerName;
    public Color playerColor;
    public List<Territory> ownedTerritories;
}

