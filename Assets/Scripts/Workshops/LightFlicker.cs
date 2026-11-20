using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFlicker : MonoBehaviour
{
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float add = 0.0f;

    private float seedOffset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        seedOffset = Random.value * 1000f;
    }

    // Update is called once per frame
    void Update()
    {
        float t = (Time.time + seedOffset) * speed;
        float intensity = curve.Evaluate(t);
        Light2D light = GetComponent<Light2D>();
        if (light != null)
        {
            light.intensity = intensity + add;
        }
    }
}
