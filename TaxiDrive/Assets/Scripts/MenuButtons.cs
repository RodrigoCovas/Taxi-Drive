using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    private AudioManager audioManager;
    public AudioClip clickButton;

    public void Awake()
    {
        audioManager = AudioManager.Instance;
    }
    public void PlayGame()
    {
        audioManager.PlaySFX(clickButton);
        StartCoroutine(LoadSceneDelay());
    }

    public void MyRide()
    {
        audioManager.PlaySFX(clickButton);
        SceneManager.LoadScene("MyRide");
    }

    public void Settings()
    {
        audioManager.PlaySFX(clickButton);
        SceneManager.LoadScene("Settings");
    }

    public void QuitGame()
    {
        audioManager.PlaySFX(clickButton);
        StartCoroutine(QuitGameDelay());

    }

    public IEnumerator LoadSceneDelay()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private IEnumerator QuitGameDelay()
    {
        yield return new WaitForSeconds(0.5f);
        Application.Quit();
    }
}
