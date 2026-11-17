using UnityEngine;

public class JitterEffect : MonoBehaviour
{
    [Header("Jitter Settings")]
    [Tooltip("Maximum distance the object can move from its origin.")]
    public float intensity = 0.05f;

    [Tooltip("How frequently the position updates (in seconds). Set to 0 for every frame.")]
    public float updateInterval = 0.02f;

    [Tooltip("Enable or disable the jitter effect.")]
    public bool isJittering = true;

    private Vector3 initialLocalPosition;
    private float timer;

    void Start()
    {
        // Save the starting local position so the object stays anchored in place
        initialLocalPosition = transform.localPosition;
    }

    void Update()
    {
        if (!isJittering)
        {
            transform.localPosition = initialLocalPosition;
            return;
        }

        timer += Time.deltaTime;

        if (timer >= updateInterval)
        {
            // Generate a random offset inside a sphere of radius 1, scaled by intensity
            Vector3 randomOffset = Random.insideUnitSphere * intensity;
            transform.localPosition = initialLocalPosition + randomOffset;
            
            timer = 0f;
        }
    }

    // Call if object is intentionally moved & want to update anchor point
    public void ResetOrigin()
    {
        initialLocalPosition = transform.localPosition;
    }
}
