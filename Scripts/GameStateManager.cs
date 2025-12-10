using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
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

    public static bool inGame = false;

    public static int score = 0;
    public static int kills = 0;
    public static int enemyCounter = 0;
    public static int maxEnemies = 100;


    private void initState()
    {
        score = 0;
        kills = 0;
    }
    public static void AddScore(float points)
    {
        score += (int)points;
    }
    public static void AddKill()
    {
        kills += 1;
    }

    public static void IncreaseEnemyCounter() 
    {
        enemyCounter += 1;
    }

    public static void DecreaseEnemyCounter()
    {
        enemyCounter -= 1;
    }

    public static bool HasReachedEnemyCap()
    {
        if (enemyCounter >= maxEnemies)
        {
            return true;
        }
        return false;
    }

    public static void GoToGame()
    {
        inGame = true;
        SceneManager.LoadScene("Gameplay");
    }

    public static void GoToMenu()
    {
        inGame = false;
        SceneManager.LoadScene("Main Menu");
    }

    public static void QuitGame()
    {
        Application.Quit();
    }
}
