using UnityEngine;

public class TimerSphereTrigger : MonoBehaviour
{
    public string PlayerTag = "Player";
    private float timeInside = 0.0f;
    private bool isInside = false;
    public bool triggered = false;

    public GameObject sphereToControl; // Reference to the sphere this script controls
    public float destructionTime = 3.0f; // Time required inside the trigger to destroy the sphere

    private EventTimer timer;
    private float challengeTime = 60f;
    private PlayerComfort comfortBar;

    public void AssignTimerComfort(EventTimer ogTimer, float ogChallengeTime, PlayerComfort ogComfortBar)
    {
        timer = ogTimer;
        challengeTime = ogChallengeTime;
        comfortBar = ogComfortBar;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PlayerTag))
        {
            Debug.Log("Player Entered Sphere!");
            isInside = true;
            timeInside = 0.0f;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(PlayerTag) && isInside)
        {
            timeInside += Time.deltaTime;

            if (timeInside >= destructionTime)
            {
                triggered = true;
                
                // Destroy the sphere this trigger controls
                if (sphereToControl != null)
                {
                    timer.ChangeState(challengeTime);
                    comfortBar.ChangeState();
                    Destroy(sphereToControl);
                    Debug.Log($"Sphere {sphereToControl.name} destroyed!");
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(PlayerTag))
        {
            isInside = false;
            timeInside = 0.0f;
        }
    }
}
