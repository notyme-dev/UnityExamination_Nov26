using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialog : MonoBehaviour
{
    [SerializeField] public string lose_text;
    [SerializeField] public string win_text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private Image speech;
    void Start()
    {
        speech = GetComponentInChildren<Image>();
        if (speech != null)
        {
            speech.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
             if (speech != null)
            {
                speech.enabled = true;
            }
            DialogueHandler dh = other.GetComponent<DialogueHandler>();
            if (dh != null)
            {
                dh.current_dialogue = this;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (speech != null)
        {
            speech.enabled = false;
        }
        if (other.tag == "Player")
        {
            DialogueHandler dh = other.GetComponent<DialogueHandler>();
            if (dh != null)
            {
                dh.current_dialogue = null;
                dh.ForceClose();
            }
        }
    }
}
