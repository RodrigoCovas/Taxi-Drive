using UnityEngine;

public class TimerSphereTrigger : MonoBehaviour
{
    public string PlayerTag = "Player";
    private float timeInside = 0.0f;
    private bool isInside = false;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Entered!");
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

            if (timeInside >= 3.0f)
            {
                Debug.Log("Sphere Triggered!");
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
