using UnityEngine;

public class SpawnPlaneMovement : MonoBehaviour
{
    public float MoveSpeed = 2f;

    void Update()
    {
        // Двигаем точку спавна вперед по оси Z
        transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime);
    }
}
