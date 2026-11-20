using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogWindow : MonoBehaviour
{
    private TextMeshProUGUI tmp;

    private Image img;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        img = GetComponent<Image>();
        tmp = GetComponentInChildren<TextMeshProUGUI>();
        img.enabled = false;
        tmp.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowDialog(bool show)
    {
        img.enabled = show;
        tmp.enabled = show;
    }

    public void SetText(string txt)
    {
        if (tmp == null) return;
        tmp.text = txt;
    }
}
