#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class AutoNeighbor2D : MonoBehaviour
{
    [ContextMenu("Auto-Assign Neighbors (4-Directional Raycast)")]
    void AutoAssignNeighbors()
    {
        Territory[] territories = FindObjectsOfType<Territory>();

        foreach (Territory t in territories)
        {
            t.neighbors.Clear(); // Clear existing neighbors

            Vector2 origin = t.transform.position;
            float rayDistance = 0.5f;
            Vector2[] directions = new Vector2[] {
                Vector2.up, Vector2.down, Vector2.left, Vector2.right
            };

            foreach (Vector2 dir in directions)
            {
                RaycastHit2D hit = Physics2D.Raycast(origin, dir, rayDistance);

                if (hit.collider != null && hit.collider.gameObject != t.gameObject)
                {
                    Territory neighbor = hit.collider.GetComponent<Territory>();
                    if (neighbor != null && !t.neighbors.Contains(neighbor))
                    {
                        t.neighbors.Add(neighbor);
                        EditorUtility.SetDirty(t); // Mark as changed for saving
                    }
                }
            }
        }

        Debug.Log("Territory neighbors assigned via directional raycasting.");
    }
}
#endif
