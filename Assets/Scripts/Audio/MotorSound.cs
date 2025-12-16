using UnityEngine;

public class MotorSound : MonoBehaviour
{
    public PlayerMovement movement;

    [Header("Clips")]
    public AudioClip engineLoop;
    public AudioClip turnSfx;

    [Header("Engine")]
    public float minPitch = 0.8f;
    public float maxPitch = 1.6f;
    public float minVolume = 0.15f;
    public float maxVolume = 0.8f;

    void Reset()
    {
        movement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (AudioManager.Instance == null) return;

        if (Time.timeScale == 0f)
        {
            AudioManager.Instance.StopEngine();
            return;
        }

        if (movement == null || movement.rb == null) return;

        Vector3 flatVel = new Vector3(movement.rb.linearVelocity.x, 0f, movement.rb.linearVelocity.z);
        float speed = flatVel.magnitude;

        float t = (movement.moveSpeed <= 0.01f) ? 0f : Mathf.Clamp01(speed / movement.moveSpeed);

        float pitch = Mathf.Lerp(minPitch, maxPitch, t);
        float vol = Mathf.Lerp(minVolume, maxVolume, t);

        AudioManager.Instance.SetEngine(engineLoop, vol, pitch);
    }


    void OnDisable()
    {
        // Si desactivas el player, paras motor
        if (AudioManager.Instance != null) AudioManager.Instance.StopEngine();
    }
}
