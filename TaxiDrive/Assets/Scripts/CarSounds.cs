using UnityEngine;

public class CarSounds : MonoBehaviour
{
    public float minPitch = 1f;
    public float maxPitch = 3f;
    public float minVolume = 0.2f;
    public float maxVolume = 1f;

    private float currentPitch;
    private float carSpeed;
    private float currentVolume;
    private AudioSource carSounds;
    private Rigidbody carRb;

    // Start is called before the first frame update
    void Start()
    {
        carSounds = GetComponent<AudioSource>();
        carRb = GetComponent<Rigidbody>();
        carSounds.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        EngineSound();
    }

    void EngineSound()
    {
        carSpeed = carRb.velocity.magnitude;

        // Adjust pitch based on speed
        currentPitch = Mathf.Lerp(minPitch, maxPitch, carSpeed / 100);
        carSounds.pitch = currentPitch;

        // Adjust volume based on speed
        currentVolume = Mathf.Lerp(minVolume, maxVolume, carSpeed / 100);
        carSounds.volume = currentVolume;

        // Stop playing sound if speed is zero, otherwise play
        if (carSpeed < 0.1f)
        {
            if (carSounds.isPlaying)
            {
                carSounds.Stop();
            }
        }
        else
        {
            if (!carSounds.isPlaying)
            {
                carSounds.Play();
            }
        }
    }
}
