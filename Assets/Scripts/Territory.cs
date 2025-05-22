using UnityEngine;
using System;
using System.Collections.Generic;
public class Territory : MonoBehaviour
{
    [SerializeField] public string territoryName;
    [SerializeField] public Player owner;
    [SerializeField] public int initialTroops;
    [SerializeField] public List<Territory> neighbors = new List<Territory>();  // Reference to the other territories
}


