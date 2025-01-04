using UnityEngine;
using UnityEngine.UI;
public class ComfortBarFill : MonoBehaviour
{
    public Image comfortBarFill;

    void Start()
    {
        comfortBarFill.fillAmount = 1f;
    }
    public void UpdateComfortBar(float currentComfort, float maxComfort)
    {
        float fillAmount = currentComfort / maxComfort;
        comfortBarFill.fillAmount = fillAmount;
    }
}
