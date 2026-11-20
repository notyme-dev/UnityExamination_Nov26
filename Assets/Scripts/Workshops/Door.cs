using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private TextFade promptText;
    [SerializeField] private GlobalLight gl;
    
    private float endTriggered = 0;
    private bool ended = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ended & Time.time > endTriggered + 3)
        {
            SceneManager.LoadScene(0);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            BooController bc = other.GetComponent<BooController>();
            if (bc == null) return;
            if (bc.hasKey)
            {
                Debug.Log("You escaped!");
                promptText.PromptText("You escaped!", 0.6f);
                gl.SetTargetIntensity(0, 0.5f);
                ended = true;
                endTriggered = Time.time;
            }
            else
            {
                Debug.Log("You need the key!");
                promptText.PromptText("It's locked...", 0.6f);
            }
        }
    }
}
