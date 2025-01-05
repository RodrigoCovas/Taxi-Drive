using UnityEngine;
using UnityEngine.UI;

public class EventTimer : MonoBehaviour
{
    public Text timerText;

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
}
