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
    private PlayerPoints playerPoints;
    private SystemMessages messages;

    public void AssignTimerComfortPoints(EventTimer ogTimer, float ogChallengeTime, PlayerComfort ogComfortBar, PlayerPoints ogPlayerPoints, SystemMessages ogMessages)
    {
        timer = ogTimer;
        challengeTime = ogChallengeTime;
        comfortBar = ogComfortBar;
        playerPoints = ogPlayerPoints;
        messages = ogMessages; 
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PlayerTag))
        {
            Debug.Log("Player Entered Sphere!");
            isInside = true;
            timeInside = 0.0f;

            string message = "Wait " + destructionTime + " seconds inside the area";
            messages.WriteMessage(message, 2f);
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

                float remainingTime = timer.GetCurrentTime();
                float remainingComfort = comfortBar.GetCurrentComfort();

                timer.ChangeState(challengeTime);
                comfortBar.ChangeState();
                playerPoints.AddPoints(remainingTime, remainingComfort);

                // Destroy the sphere this trigger controls
                if (sphereToControl != null)
                {
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
