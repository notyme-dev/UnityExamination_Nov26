using UnityEngine;

public class GlobalPlayerInfo : MonoBehaviour
{
    public static GlobalPlayerInfo Instance { get; private set; }

    public int health = 100;
    public Vector3 position = Vector3.zero;
    public bool hasKey = false;
    public bool collectedAllPoints = false;

    public int points = 0;
    public int maxPoints = 5;
    


    public void AddPoints(int amount)
    {
        points += amount;
        if (points >= maxPoints)
        {
            collectedAllPoints = true;
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
