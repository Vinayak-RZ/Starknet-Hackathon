using Unity.VisualScripting;
using UnityEngine;

public class FloatEffect : MonoBehaviour
{
    private GameObject obj;
    private RectTransform button;

    private Vector2 startPos;

    // Randomized params
    private float ampX, ampY;
    private float freqX, freqY;
    private float phaseX, phaseY;
    private float noiseAmp;

    void Start() {
        obj = transform.GameObject();
        button = obj.GetComponent<RectTransform>();
        startPos = button.anchoredPosition; 
        //startPos = transform.localPosition;

        ampX = Random.Range(1.5f, 3f);
        ampY = Random.Range(1.5f, 3f);
        freqX = Random.Range(0.8f, 1.5f);
        freqY = Random.Range(0.6f, 1.2f);
        phaseX = Random.Range(0f, Mathf.PI * 2);
        phaseY = Random.Range(0f, Mathf.PI * 2);
        noiseAmp = Random.Range(0.5f, 1f);
    }

    void Update()
    {
        float t = Time.time;
        float offsetX = Mathf.Sin(t * freqX + phaseX) * (ampX/2) + Mathf.Sin(t * 2.3f + phaseX) * (noiseAmp/2);
        float offsetY = Mathf.Cos(t * freqY + phaseY) * (ampY/2) + Mathf.Sin(t * 1.8f + phaseY) * (noiseAmp/2);
        button.anchoredPosition = startPos + new Vector2(offsetX, offsetY);
        //transform.localPosition = startPos + new Vector2(offsetX, offsetY);
    }

}
