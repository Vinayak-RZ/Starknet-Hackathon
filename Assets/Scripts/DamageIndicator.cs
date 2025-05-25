using UnityEngine;

public class DamageIndicator : MonoBehaviour
{
    public float floatSpeed = 1f;
    public float lifetime = 1f;

    void Update()
    {
        transform.Translate(Vector3.up * floatSpeed * Time.deltaTime);
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
            Destroy(gameObject);
    }
}
