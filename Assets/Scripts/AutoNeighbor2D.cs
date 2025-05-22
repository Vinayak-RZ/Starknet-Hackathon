#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class AutoNeighbor2D : MonoBehaviour
{
    [ContextMenu("Auto-Assign Neighbors (2D Overlap)")]
    void AutoAssignNeighbors()
    {
        Territory[] territories = FindObjectsOfType<Territory>();

        foreach (Territory t in territories)
        {
            t.neighbors.Clear(); // Clear existing neighbors

            Collider2D thisCollider = t.GetComponent<Collider2D>();
            if (thisCollider == null)
            {
                Debug.LogWarning($"{t.name} has no 2D collider!");
                continue;
            }

            // Get all overlapping colliders
            ContactFilter2D filter = new ContactFilter2D();
            filter.useTriggers = false;
            Collider2D[] results = new Collider2D[50];

            int count = thisCollider.Overlap(filter, results);

            for (int i = 0; i < count; i++)
            {
                Collider2D col = results[i];
                if (col == null || col == thisCollider) continue;

                Territory neighbor = col.GetComponent<Territory>();
                if (neighbor != null && !t.neighbors.Contains(neighbor))
                {
                    t.neighbors.Add(neighbor);
                    EditorUtility.SetDirty(t); // Mark as changed
                }
            }
        }

        Debug.Log("Territory neighbors assigned.");
    }
}
#endif

