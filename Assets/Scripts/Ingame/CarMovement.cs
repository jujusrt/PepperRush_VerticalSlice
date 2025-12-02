using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 5f;

    void Update()
    {
        if (pointA == null || pointB == null)
            return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            pointB.position,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, pointB.position) < 0.05f)
        {
            transform.position = pointA.position;
        }
    }
}
