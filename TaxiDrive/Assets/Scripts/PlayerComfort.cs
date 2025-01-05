using Unity.VisualScripting;
using UnityEngine;

public class PlayerComfort : MonoBehaviour
{
    public float maxComfort = 100f;
    public ComfortBarFill comfortBar;

    private float currentComfort;
    private bool eventActive = false;

    private void Start()
    {
        comfortBar.gameObject.SetActive(false);
    }

    private void StartEvent()
    {
        currentComfort = maxComfort;
        comfortBar.UpdateComfortBar(currentComfort, maxComfort);
        comfortBar.gameObject.SetActive(true);
    }

    private void EndEvent()
    {
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
        else if (collision.gameObject.CompareTag("Clock"))
        {
            Debug.Log("Collision Detected with: " + collision.gameObject.name);
            float healAmount = 20f;
            TakeDamage(healAmount);
        }
    }
}
