using UnityEngine;

public class PlayerPoliceSpawner : MonoBehaviour
{
    public PoliceCarSpawner policeCarSpawner;
    public SystemMessages messages;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Alarm"))
        {
            Debug.Log("Alarm object triggered");
            for (int i = 0; i < 3; i++)
            {
                policeCarSpawner.SpawnPoliceCar();
            }
            string message = "You were seen by a CAMERA" + "\n" +
                             "More POLICE will be coming after you";
            messages.WriteMessage(message, 2f);
        }
    }
}
