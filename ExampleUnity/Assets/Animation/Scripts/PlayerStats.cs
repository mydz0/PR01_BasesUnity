using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    int currentPoints = 0;

    void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int AddTotalPoints(int points)
    {
        currentPoints += points;
        return currentPoints;
    }

    public int GetTotalPoints()
    {
        return currentPoints;
    }

    public void ResetPoints()
    {
        currentPoints = 0;
    }
}
