using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour, IDragHandler
{
    [SerializeField] AudioMixer masterMixer;
    private Slider slider;
    private void SetVolume(float volume)
    {
        float dB = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20;
        masterMixer.SetFloat("MasterVolume", dB);
    }

    private float ConvertDBtoScalar(float db)
    {
        return Mathf.Pow(10,db/20);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slider = GetComponent<Slider>();
        float db = 0;
        bool success = masterMixer.GetFloat("MasterVolume",out db);
        if (success)
        {
            slider.value = ConvertDBtoScalar(db);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        SetVolume(slider.value);
    }
}
