using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    
    private AudioManager audioManager;
    private static string previousSceneName;

    public void Awake()
    {
        audioManager = AudioManager.Instance;

        if (string.IsNullOrEmpty(previousSceneName))
        {
            previousSceneName = SceneManager.GetActiveScene().name;
        }
    }

    public void PlayGame()
    {
        Time.timeScale = 1f;
        audioManager.PlaySFX(audioManager.clickButton);

        UpdatePreviousScene();
        SceneManager.LoadScene("SampleScene");
    }

    public void MyRide()
    {
        audioManager.PlaySFX(audioManager.clickButton);

        UpdatePreviousScene();
        SceneManager.LoadScene("MyRide");
    }

    public void Settings()
    {
        audioManager.PlaySFX(audioManager.clickButton);

        UpdatePreviousScene();
        SceneManager.LoadScene("Settings");
    }

    public void QuitGame()
    {
        audioManager.PlaySFX(audioManager.clickButton);
        Application.Quit();
    }

    public void ResumeGame()
    {
        audioManager.PlaySFX(audioManager.clickButton);

    }

    public void MainMenu()
    {
        audioManager.PlaySFX(audioManager.clickButton);

        UpdatePreviousScene();
        SceneManager.LoadScene("TitleScreen");
    }

    public void GoBack()
    {
        Time.timeScale = 1f;
        if (!string.IsNullOrEmpty(previousSceneName))
        {
            audioManager.PlaySFX(audioManager.clickButton);
            SceneManager.LoadScene(previousSceneName);
        }
        else
        {
            Debug.LogWarning("No previous scene to go back to.");
        }
    }

    private void UpdatePreviousScene()
    {
        previousSceneName = SceneManager.GetActiveScene().name;
    }
}
