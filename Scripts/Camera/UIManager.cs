using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI healthText;
    [SerializeField]
    TextMeshProUGUI scoreText;
    [SerializeField]
    TextMeshProUGUI killsText;

    private void Update()
    {
        healthText.text = $"Health: {Player.health}";
        scoreText.text = $"Score: {GameStateManager.score}";
        killsText.text = $"Kills: {GameStateManager.kills}";
    }
}
