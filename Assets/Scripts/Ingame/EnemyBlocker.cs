using UnityEngine;

public class EnemyBlocker : MonoBehaviour
{
    public void TakeDamage()
    {
        Destroy(gameObject);
    }
}
