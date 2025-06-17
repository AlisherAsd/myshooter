using UnityEngine;

public class MiniMapFollow : MonoBehaviour
{
    public Transform player; // Перетащите сюда игрока
    public float height = 10f; // Высота камеры над игроком

    void LateUpdate()
    {
        // Камера следует за игроком, но остается сверху
        transform.position = player.position + Vector3.up * height;
        // Поворот камеры совпадает с поворотом игрока по оси Y
        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0);
    }
}