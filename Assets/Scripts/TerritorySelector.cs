using UnityEngine;
using System.Collections.Generic;

public class TerritorySelector : MonoBehaviour
{
    private Territory selectedTerritory;
    private Color originalColor;
    private SpriteRenderer selectedSpriteRenderer;
    public Material pulseLineMaterial;
    public GameObject drawingPrefab;
    private List<LineRenderer> lineRenderers = new List<LineRenderer>();

    void Update()
    {
        if (Input.GetMouseButtonDown(0))  
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit2D = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit2D.collider != null)
            {
                Territory territory = hit2D.collider.GetComponent<Territory>();
                if (territory != null)
                {
                    SelectTerritory(territory);
                }
            }
        }
    }

    void SelectTerritory(Territory territory)
    {
        if (selectedTerritory == territory)
        {
            DeselectTerritory();
            return;
        }

        DeselectTerritory();

        selectedTerritory = territory;
        Transform childofterr = selectedTerritory.gameObject.transform.GetChild(0);
        selectedSpriteRenderer = childofterr.GetComponent<SpriteRenderer>();

        if (selectedSpriteRenderer != null)
        {
            originalColor = selectedSpriteRenderer.color;
            selectedSpriteRenderer.color = originalColor*1.5f; 
        }

        DrawNeighborLines(selectedTerritory);
    }

    void DeselectTerritory()
    {
        if (selectedSpriteRenderer != null)
        {
            selectedSpriteRenderer.color = originalColor;
        }

        ClearNeighborLines();

        selectedTerritory = null;
        selectedSpriteRenderer = null;
    }

    void DrawNeighborLines(Territory territory)
        {
            foreach (Territory neighbor in territory.neighbors)
            {
                GameObject lineObj = Instantiate(drawingPrefab);
                lineObj.transform.parent = territory.transform;

                LineRenderer lr = lineObj.GetComponent<LineRenderer>();
                lr.positionCount = 5;  // More points = smoother curvesss
                lr.startWidth = 0.1f;
                lr.endWidth = 0.1f;
                lr.startColor = Color.cyan;
                lr.endColor = Color.cyan;

                Vector3 startPos = territory.transform.position;
                Vector3 endPos = neighbor.transform.position;
                startPos.z = 0f;
                endPos.z = 0f;

                Vector3 midPoint = (startPos + endPos) / 2f + Vector3.up * 0.5f;

                // Using Bezier curve
                for (int i = 0; i < lr.positionCount; i++)
                {
                    float t = i / (lr.positionCount - 1f);
                    Vector3 pointOnCurve = Mathf.Pow(1 - t, 2) * startPos +
                                        2 * (1 - t) * t * midPoint +
                                        Mathf.Pow(t, 2) * endPos;
                    lr.SetPosition(i, pointOnCurve);
                }

                lineRenderers.Add(lr);
            }
        }

    void ClearNeighborLines()
    {
        foreach (var lr in lineRenderers)
        {
            if (lr != null)
                Destroy(lr.gameObject);
        }
        lineRenderers.Clear();
    }
}
