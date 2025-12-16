using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    public Transform leftDoor;
    public Transform rightDoor;

    public Transform leftOpenPoint;
    public Transform rightOpenPoint;

    public float speed = 3f;

    private bool opening = false;
    public AudioClip openDoorSfx;

    void Update()
    {
        if (!opening) return;

        if (leftDoor != null && leftOpenPoint != null)
        {
            leftDoor.position = Vector3.MoveTowards(
                leftDoor.position,
                leftOpenPoint.position,
                speed * Time.deltaTime
            );
        }

        if (rightDoor != null && rightOpenPoint != null)
        {
            rightDoor.position = Vector3.MoveTowards(
                rightDoor.position,
                rightOpenPoint.position,
                speed * Time.deltaTime
            );
        }
    }
    public void OpenDoor()
    {
        if (opening) return;

        opening = true;

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX(openDoorSfx);
        }
    }
}
