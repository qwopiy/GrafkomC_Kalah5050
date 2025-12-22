using TMPro;
using UnityEngine;

public class HighScoreText : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;
    void Start()
    {
        highScoreText = gameObject.GetComponent<TextMeshProUGUI>();
        highScoreText.text = "High Score : " + GameStateManager.highScore;
    }

}
