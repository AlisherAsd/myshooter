using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int score = 0;

    void Start()
    {
        Debug.Log("Текущий счёт: " + score);
    }
}