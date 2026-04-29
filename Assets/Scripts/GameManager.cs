using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public AudioSource source;
    public AudioClip buttonClickSoundEffect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void StartGame()
    {
        source.PlayOneShot(buttonClickSoundEffect);
        SceneManager.LoadScene("MainLevel", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        source.PlayOneShot(buttonClickSoundEffect);
        Application.Quit();
    }

    public void PauseGame()
    {
        if(Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }
        else
        {
            Time.timeScale = 0f;
            SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("PauseMenu"));
        }
    }

    public void UnPauseGame()
    {
        source.PlayOneShot(buttonClickSoundEffect);
        SceneManager.UnloadSceneAsync("PauseMenu");
        Time.timeScale = 1f;
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("MainLevel"));
    }

    public void MainMenu()
    {
        source.PlayOneShot(buttonClickSoundEffect);
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
