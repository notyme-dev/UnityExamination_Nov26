using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private ParticleSystem ps;
    private AudioSource asrc;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ps = GetComponentInChildren<ParticleSystem>();
        asrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (ps != null)
            {
                ps.Play();
            }
            if (asrc != null)
            {
                asrc.enabled = false;
            }
            Collider2D col = GetComponent<Collider2D>();
            if (col != null)
            {
                col.enabled = false;
            }
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.enabled = false;
            }
            if (GlobalPlayerInfo.Instance != null)
            {
                GlobalPlayerInfo.Instance.AddPoints(1);
            }
        }
        //Destroy(gameObject);
    }
}
