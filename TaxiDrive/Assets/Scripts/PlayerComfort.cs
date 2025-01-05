using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;

public class PlayerComfort : MonoBehaviour
{
    public float maxComfort = 100f;
    public ComfortBarFill comfortBar;
    public SystemMessages messages;

    private float currentComfort;
    private bool eventActive = false;

    private void Start()
    {
        comfortBar.gameObject.SetActive(false);
    }

    public float GetCurrentComfort()
    { return currentComfort; }

    private void StartEvent()
    {
        eventActive = true;
        currentComfort = maxComfort;
        comfortBar.UpdateComfortBar(currentComfort, maxComfort);
        comfortBar.gameObject.SetActive(true);
    }

    private void EndEvent()
    {
        eventActive = false;
        comfortBar.gameObject.SetActive(false);
    }

    public void ChangeState()
    {
        if (eventActive)
        {
            EndEvent();
        }
        else
        {
            StartEvent();
        }
    }

    public void TakeDamage(float damage)
    {
        currentComfort -= damage;
        currentComfort = Mathf.Clamp(currentComfort, 0, maxComfort);
        comfortBar.UpdateComfortBar(currentComfort, maxComfort);
    }

    public void Heal(float healAmount)
    {
        currentComfort += healAmount;
        currentComfort = Mathf.Clamp(currentComfort, 0, maxComfort);
        comfortBar.UpdateComfortBar(currentComfort, maxComfort);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PoliceCar"))
        {
            Debug.Log("Collision Detected with: " + collision.gameObject.name);
            float damageAmount = 10f;
            TakeDamage(damageAmount);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Drink"))
        {
            Debug.Log("Drink object triggered");
            if (eventActive)
            {
                float healAmount = 20f;
                Heal(healAmount);
                string message = "You found a COLA" + "\n" +
                                 "Your passenger feels more COMFORTABLE";
                messages.WriteMessage(message, 2f);
            }
        }
    }
}
