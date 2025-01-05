using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SystemMessages : MonoBehaviour
{
    public Text messagesText;

    public void WriteMessage(string message, float time = 5f)
    {
        messagesText.text = message;
        StartCoroutine(HideMessageAfterDelay(time));
    }
    private IEnumerator HideMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        messagesText.text = "";
    }

}
