using UnityEngine;

public class BikeSmoke : MonoBehaviour
{
    [SerializeField] private ParticleSystem smoke;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float minSpeedToSmoke = 2f;

    private void Awake()
    {
        if (rb == null) rb = GetComponentInParent<Rigidbody>();
        if (smoke == null) smoke = GetComponentInChildren<ParticleSystem>(true);

        if (smoke == null)
            Debug.LogError("[BikeSmoke] No encuentro ParticleSystem (smoke). Asignalo en el Inspector o colócalo como hijo.", this);

        if (rb == null)
            Debug.LogError("[BikeSmoke] No encuentro Rigidbody (rb). Asignalo en el Inspector o colócalo en el padre.", this);
    }

    private void Update()
    {
        if (smoke == null || rb == null) return;

        float speed = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z).magnitude;

        var emission = smoke.emission; // SIEMPRE obtenido del ParticleSystem
        if (speed > minSpeedToSmoke)
        {
            emission.rateOverTime = Mathf.Lerp(5f, 30f, speed / 20f);
            if (!smoke.isPlaying) smoke.Play();
        }
        else
        {
            emission.rateOverTime = 0f;
            if (smoke.isPlaying) smoke.Stop();
        }
    }
}
