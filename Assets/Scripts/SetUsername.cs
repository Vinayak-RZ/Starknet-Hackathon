using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
public class SetUsername : MonoBehaviour
{
    public Player player;
    public void SetUsernamee(TMP_InputField input)
    {
        player.playerName = input.text;
    }
}
