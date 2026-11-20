using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PitchControl : MonoBehaviour, IDragHandler
{
    [SerializeField] AudioMixer masterMixer;
    private Slider slider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
   
    private void SetPitch(float pitch)
    {
        masterMixer.SetFloat("MasterPitch", pitch);
    }
   
    void Start()
    {
        slider = GetComponent<Slider>();
        float pitch = 0;
        bool success = masterMixer.GetFloat("MasterPitch",out pitch);
        if (success)
        {
            slider.value = pitch;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        SetPitch(slider.value);
    }
}
