using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance;

    private int currentScore = 0;

    [Header("Gestion des cibles")]
    public int maxTargets = 5;
    public int currentTargets = 0;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    // ------------------ SCORE ------------------

    public void AddScore(int amount)
    {
        currentScore += amount;
    }

    public int GetScore()
    {
        return currentScore;
    }

    // ------------------ TARGETS ------------------

    public void RegisterTargetSpawn()
    {
        currentTargets++;
    }

    public void RegisterTargetDespawn()
    {
        currentTargets--;

        if (currentTargets < 0)
            currentTargets = 0;
    }
}
