using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CanvasManager canvasManager;

    public static GameManager Instance;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void HitCoin(int count = 1)
    {
        int points = PlayerStats.Instance.AddTotalPoints(count);
        canvasManager.UpdateCoinPoints(points);
    }
}
