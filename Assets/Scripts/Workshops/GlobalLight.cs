using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GlobalLight : MonoBehaviour
{
    private float start_int = 0;

    private Light2D light;

    private float speed = 1.0f;
    private float target_int = 1.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        light = GetComponent<Light2D>();
        start_int = light.intensity;
        target_int = start_int;
    }

    // Update is called once per frame
    void Update()
    {
        light.intensity = Mathf.MoveTowards(light.intensity, target_int, Time.deltaTime * speed);
    }
    
    public void SetTargetIntensity(float intensity, float new_speed) 
    {
        target_int = intensity;
        speed = new_speed;
    }
    
    public float GetStartInt()
    {
        return start_int;
    }
}
