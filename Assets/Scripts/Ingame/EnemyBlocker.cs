using UnityEngine;

public class EnemyBlocker : MonoBehaviour
{
    public AudioClip destroySfx;

    public void TakeDamage()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX(destroySfx);
        }

        Destroy(gameObject);
    }
}
