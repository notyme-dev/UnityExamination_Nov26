using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class button : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private AudioSource hoverSound;
    [SerializeField] private AudioSource clickSound;

    [SerializeField] private int sceneIndex;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        SceneManager.LoadScene(sceneIndex);
        if (clickSound != null)
        {
            clickSound.Play();
        }
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        transform.localScale = new Vector3(1.05f, 1.05f, 1.05f);
        if (hoverSound != null)
        {
            hoverSound.Play();
        }
    }
    
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        transform.localScale = new Vector3(1, 1, 1);
    }
}
