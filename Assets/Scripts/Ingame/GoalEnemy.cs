using UnityEngine;

public class GoalEnemy : MonoBehaviour
{
    public AudioClip destroySfx;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlaySFX(destroySfx);
            }
            EnemyCounter.instance.EnemyKilled();
            Destroy(gameObject);
        }
    }
}
