using UnityEngine;

public class SpawnPlaneMovement : MonoBehaviour
{
    public float MoveSpeed = 2f;

    void Update()
    {
        // ������� ����� ������ ������ �� ��� Z
        transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime);
    }
}
