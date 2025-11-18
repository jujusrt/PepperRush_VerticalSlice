using UnityEngine;

public class CameraFollowZ : MonoBehaviour
{
    public Transform player;
    public float height = 5f;
    public float distance = 5f;

    void LateUpdate()
    {
        if (player == null) return;

        // La cámara se coloca detrás y arriba, solo ajustando su posición Z a la del jugador.
        Vector3 newPos = new Vector3(
            transform.position.x,      // no se mueve en X
            player.position.y + height, // sigue la altura del player + offset
            player.position.z - distance // detrás del jugador
        );

        transform.position = newPos;

        // Mantener rotación fija (mirando hacia delante)
        // Puedes ajustar esto según tu escena:
        transform.rotation = Quaternion.Euler(40f, 0f, 0f);
    }
}
