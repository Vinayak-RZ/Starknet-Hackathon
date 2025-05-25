#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

public class TerritoryAutoNamer : MonoBehaviour
{
    [ContextMenu("Auto-Assign Territory Names")]
    void AutoAssignNames()
    {
        Territory[] territories = FindObjectsOfType<Territory>();
        for (int i = 0; i < territories.Length; i++)
        {
            // var a = territories[i].GetComponent<FloatEffect>();
            // DestroyImmediate(a);
            territories[i].territoryName = "Territory_" + i;
            EditorUtility.SetDirty(territories[i]);
        }
    }
}
#endif

