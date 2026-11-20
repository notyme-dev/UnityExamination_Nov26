using TMPro;
using UnityEngine;

public class YouDiedTracker : MonoBehaviour
{
    private TextMeshProUGUI tmp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tmp =  GetComponent<TextMeshProUGUI>();
        tmp.enabled = false;
        GlobalPlayerInfo gpi = GlobalPlayerInfo.GetInstance();
        gpi.PlayerDied.AddListener(Show);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Show()
    {
        tmp.enabled = true;
    }
}
