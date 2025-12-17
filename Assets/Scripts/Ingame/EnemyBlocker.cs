using UnityEngine;

public class EnemyBlocker : MonoBehaviour
{
    public AudioClip destroySfx;
    [SerializeField] private GameObject destroyParticles;
    [SerializeField] private GameObject boomParticles;
    [SerializeField] private Transform fxPoint;

    public void TakeDamage()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX(destroySfx);
        }
        GameObject fx = Instantiate(
                destroyParticles,
                fxPoint.position,
                Quaternion.identity
        );
        GameObject fx2 = Instantiate(
                boomParticles,
                fxPoint.position,
                Quaternion.identity
            );

        Destroy(gameObject);
    }
}
