using UnityEngine;

public class GoalEnemy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            EnemyCounter.instance.EnemyKilled();
            Destroy(gameObject);
        }
    }
}
