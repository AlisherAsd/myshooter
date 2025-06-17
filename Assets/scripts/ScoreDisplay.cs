using UnityEngine;
using TMPro; // Импортируем пространство имен TextMeshPro

public class ScoreDisplay : MonoBehaviour
{
    private TMP_Text scoreText; // Используем TMP_Text вместо UnityEngine.UI.Text

    void Start()
    {
        GameObject textObject = GameObject.Find("ScoreText");

        if (textObject != null)
        {
            scoreText = textObject.GetComponent<TMP_Text>(); // Используем GetComponent<TMP_Text>
        }
        else
        {
            Debug.LogError("Объект 'ScoreText' не найден на сцене!");
        }
    }

    void Update()
    {
        if (scoreText != null)
        {
            scoreText.text = "Очки: " + PlayerStats.score;
        }
    }
}