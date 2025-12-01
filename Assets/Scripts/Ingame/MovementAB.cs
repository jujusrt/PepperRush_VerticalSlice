using UnityEngine;

public class MovementAB : MonoBehaviour
{
    public Transform[] points;
    public float vel = 3f;
    private int index = 0;

    void Update()
    {
        if (points == null || points.Length == 0)
        {
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, points[index].position, vel * Time.deltaTime);

        if (Vector3.Distance(transform.position, points[index].position) < 0.1f)
        {
            index = (index + 1) % points.Length;
        }
    }
}
