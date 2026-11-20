using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GlobalPlayerInfo : MonoBehaviour
{
    public static GlobalPlayerInfo Instance { get; private set; }

    public int health = 10;
    public bool dead = false;
    public Vector3 position = Vector3.zero;
    public bool hasKey = false;
    public bool collectedAllPoints = false;

    public int points = 0;
    public int maxPoints = 5;

    public UnityEvent PlayerDied;

    private float diedTime = 0;
    
    private void ReopenCurrentScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
    
    public void AddPoints(int amount)
    {
        points += amount;
        if (points >= maxPoints)
        {
            collectedAllPoints = true;
        }
    }

    public void TakeDamage(int amount)
    {
        health = Mathf.Max(health-amount,0);
        if (health <= 0)
        {
            dead = true;
            PlayerDied.Invoke();
            diedTime = Time.time;
        }
    }

    private void Update()
    {
        if (dead && diedTime + 3f < Time.time)
        {
            ReopenCurrentScene();
        }
    }

    private void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
        
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    public static GlobalPlayerInfo GetInstance()
    {
        return Instance; 
    }
    

}
