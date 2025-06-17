using UnityEngine;

public class ShotBotController : MonoBehaviour
{
    public int health = 3; // Здоровье бота

    public void OnHit()
    {
        Debug.Log("Попали в бота: " + name);

        health--;
        Debug.Log(name + " получил урон. Осталось здоровья: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(name + " уничтожен!");

        PlayerStats.score++; // Увеличиваем счёт игрока
        Destroy(gameObject); // Удаляем бота
    }
}