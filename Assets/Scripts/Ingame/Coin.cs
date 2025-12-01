using UnityEngine;

public class Coin : MonoBehaviour
{
    public float rotationSpeed = 120f;

    void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CoinManager.instance.AddCoin();
            Destroy(gameObject);
        }
    }
}
