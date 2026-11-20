using TMPro;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    private TextMeshProUGUI tmp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tmp =  GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        GlobalPlayerInfo gpi = GlobalPlayerInfo.GetInstance();
        tmp.text = gpi.points.ToString() + " / " + gpi.maxPoints.ToString();
    }
}
