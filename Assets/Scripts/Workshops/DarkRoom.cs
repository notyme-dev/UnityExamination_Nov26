using System;
using TMPro;
using UnityEngine;

public class DarkRoom : MonoBehaviour
{
    [SerializeField] private GlobalLight gl;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            gl.SetTargetIntensity(0.33f, 1);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            gl.SetTargetIntensity(gl.GetStartInt(), 1);
        }
    }
}
