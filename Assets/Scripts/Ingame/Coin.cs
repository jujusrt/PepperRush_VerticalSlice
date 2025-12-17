using UnityEngine;

public class Coin : MonoBehaviour
{
    public float rotationSpeed = 120f;
    public AudioClip coinSound;

    [SerializeField] private GameObject pickupParticles;

    void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject fx = Instantiate(
                pickupParticles,
                transform.position,
                Quaternion.identity
            );
            CoinManager.instance.AddCoin();
            AudioManager.Instance.PlaySFX(coinSound);
            Destroy(gameObject);
        }
    }
}
