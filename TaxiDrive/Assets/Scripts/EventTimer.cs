using UnityEngine;
using UnityEngine.UI;

public class EventTimer : MonoBehaviour
{
    public Text timerText;
    public SystemMessages messages;

    private float maxTime = 9999;
    private float currentTime;
    private bool eventActive = false;

    void Update()
    {
        if (eventActive)
        {
            currentTime -= Time.deltaTime;
            currentTime = Mathf.Clamp(currentTime, 0f, maxTime);
            timerText.text = currentTime.ToString("F2") + "s";
        }
    }

    public float GetCurrentTime()
    { return currentTime; }

    public void StartEvent(float time)
    {
        eventActive = true;
        currentTime = time;
        timerText.gameObject.SetActive(true);
    }

    public void EndEvent()
    {
        eventActive = false;
        timerText.gameObject.SetActive(false);
    }

    public void AddTime(float additionalTime)
    {
        if (eventActive)
        {
            currentTime += additionalTime;
        }
    }

    public void ChangeState(float time)
    {
        Debug.Log("Timer State Has Changed");
        if (eventActive)
        {
            EndEvent();
        }
        else
        {
            StartEvent(time);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Clock"))
        {
            Debug.Log("Clock object triggered");
            if (eventActive)
            {
                float addedTime = 30f;
                AddTime(addedTime);
                string message = "You found a CLOCK" + "\n" +
                                 "You got " + addedTime + " more seconds!";
                messages.WriteMessage(message, 2f);
            }
        }
    }
}
