using UnityEngine;

public class SinePosition : MonoBehaviour
{
    [SerializeField] private float amplitude = 1f;
    [SerializeField] private float frequency = 1f;

    [SerializeField] private Vector3 axis = Vector3.up;

    private Vector3 startPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = startPosition + axis * amplitude * Mathf.Sin(frequency * Time.time);
    }
}
