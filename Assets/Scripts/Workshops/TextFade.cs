using TMPro;
using UnityEngine;

public class TextFade : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float speed = 1.0f;
    private float s = 1.0f;
    void Start()
    {
        s = speed;
    }

    // Update is called once per frame
    void Update()
    {
        TextMeshProUGUI tmp = GetComponent<TextMeshProUGUI>();
        Color c = tmp.color;
        tmp.color  = new Color(c.r, c.g, c.b, 
            Mathf.Max(0,c.a - Time.deltaTime * s));
    }

    public void PromptText(string text, float speed = 1)
    {
        TextMeshProUGUI tmp = GetComponent<TextMeshProUGUI>();
        tmp.text = text;
        Color c = tmp.color;
        c.a = 1;
        tmp.color = c;
        s = speed;
    }
}
