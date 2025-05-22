#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public class TerritoryAutoNamer : MonoBehaviour
{
    [ContextMenu("Auto-Assign Territory Names")]
    void AutoAssignNames()
    {
        Territory[] territories = FindObjectsOfType<Territory>();
        for (int i = 0; i < territories.Length; i++)
        {
            territories[i].territoryName = "Territory_" + i;
            EditorUtility.SetDirty(territories[i]);
        }
    }
}
#endif

