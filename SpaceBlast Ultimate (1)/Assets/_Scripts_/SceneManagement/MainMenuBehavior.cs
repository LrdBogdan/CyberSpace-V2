using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenuBehavior : MonoBehaviour //Bogdan C. SU17A\\
{
    #region Variables
    public GameObject mainMenu;
    public GameObject playVideo;
    private float timer;
    #endregion

    void Start()
    {
        Cursor.visible = false;
        FindObjectOfType<AudioManager>().Play("Theme");
    }

    public void LoadScene(string MainScene)
    {
        FindObjectOfType<AudioManager>().Play("Click");
        Debug.Log("sceneName to load: " + MainScene);
        SceneManager.LoadScene(MainScene);
        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        Debug.Log("UUUUUUUUT!!!!");
        Application.Quit();
    }
}