using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField]
    public static GameStateManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            initState();
        }
    }

    public enum PlayerState
    {
        Idle,
        Moving,
        Attacking,
        Dead
    }

    public static int score = 0;
    public static int kills = 0;


    private void initState()
    {
        score = 0;
        kills = 0;
    }
    public static void AddScore(int points)
    {
        score += points;
    }
    public static void AddKill()
    {
        kills += 1;
    }
}
