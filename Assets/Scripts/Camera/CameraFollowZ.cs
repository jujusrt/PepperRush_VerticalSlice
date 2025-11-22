using UnityEngine;

public class CameraFollowZ : MonoBehaviour
{
    public Transform player;
    public float height = 5f;
    public float distance = 5f;
    public float camRotation = 50f;
    void LateUpdate()
    {
        if (player == null) return;

        Vector3 newPos = new Vector3(transform.position.x, player.position.y + height, player.position.z - distance);

        transform.position = newPos;
        transform.rotation = Quaternion.Euler(camRotation, 0f, 0f);
    }
}
