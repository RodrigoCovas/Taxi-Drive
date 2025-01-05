using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerPoints : MonoBehaviour
{
    public Text pointsText;
    public SystemMessages messages;

    private int points = 0;
    private bool flag = false;

    void Start()
    {
        pointsText.text = points.ToString() + " POINTS";
    }

    public void AddPoints(float remainingTime, float remainingComfort)
    {
        if (flag)
        {
            int time = (int)Math.Round(remainingTime);
            int comfort = (int)remainingComfort;
            int pointsEarned = time + comfort;
            points += pointsEarned;

            pointsText.text = points.ToString() + " POINTS";
            string message = "You earned: " + pointsEarned + "points" + "\n"
                            + "Remaining Time: " + time + "s" + "\n"
                            + "Passengers Comfort: " + comfort;
            messages.WriteMessage(message);
        }
        flag = !flag;
    }
}
