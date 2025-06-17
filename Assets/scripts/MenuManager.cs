using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject gamePanel; // Игровая панель
    public MonoBehaviour[] scriptsToEnableAfterStart; // Скрипты, которые нужно включить после старта

    private void Start()
    {
        // Отключаем все игровые скрипты при старте
        foreach (var script in scriptsToEnableAfterStart)
        {
            script.enabled = false;
        }
    }

    public void StartGame()
    {
        if (gamePanel == null)
        {
            Debug.LogError("Ссылка на игровую панель не установлена!");
            return;
        }

        Debug.Log("Игра началась!");
        gamePanel.SetActive(true); // Включаем игровую панель
        
        // Включаем все игровые скрипты
        foreach (var script in scriptsToEnableAfterStart)
        {
            script.enabled = true;
        }
        
        gameObject.SetActive(false); // Скрываем меню
    }
}