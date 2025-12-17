using UnityEngine;

public class GoalEnemy : MonoBehaviour
{
    public AudioClip destroySfx;
    [SerializeField]  GameObject destroyParticles;
    [SerializeField] GameObject boomParticles;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlaySFX(destroySfx);
            }

            GameObject fx = Instantiate(
                destroyParticles,
                transform.position,
                Quaternion.identity
            );
            GameObject fx2 = Instantiate(
                boomParticles,
                transform.position,
                Quaternion.identity
            );
            EnemyCounter.instance.EnemyKilled();
            Destroy(gameObject);
        }
    }
}
