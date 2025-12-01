using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 3f;
    public float rotationSpeed = 120f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime, 0f, 0f);
    }

    private void OnCollisionEnter(Collision other)
    {
        EnemyBlocker enemy = other.gameObject.GetComponent<EnemyBlocker>();

        if (enemy != null)
            enemy.TakeDamage();

        Destroy(gameObject);
    }

}
