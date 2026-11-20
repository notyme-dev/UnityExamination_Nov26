using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour
{
    [SerializeField] private GameObject keyIcon;
    [SerializeField] private TextFade promptText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            BooController bc = other.GetComponent<BooController>();
            if (bc == null) return;
            bc.hasKey = true;
            Destroy(gameObject);
            keyIcon.GetComponent<RawImage>().enabled = true;
        }
    }
}
