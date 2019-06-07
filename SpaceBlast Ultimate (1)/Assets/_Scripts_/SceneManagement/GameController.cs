using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour //Bogdan C. SU17A - 2019©\\
{
    #region Variables
    public GameObject gameoverUI, gameGUI, Video;
    public TextMeshProUGUI scoreText;
    public static bool isDead = false;
    public int gameScore;
    #endregion

    private void Start()
    {
        Cursor.visible = false;
        isDead = false;
        Time.timeScale = 1f;
        gameoverUI.SetActive(false);
        FindObjectOfType<AudioManager>().Play("Theme");
        StartCoroutine(LoadGameStart());
    }

    void Update()
    {
        if (isDead == true)
        {
            Video.SetActive(true);
            gameScore = ScoreSystem.Instance.Internalscore;
            scoreText.text = gameScore.ToString();
            StartCoroutine(LoadGameOver());
            gameGUI.SetActive(false);
        }

        else
        {
            Time.timeScale = 1f;
        }
    }

    IEnumerator LoadGameOver()
    {
        yield return new WaitForSeconds(2);
        Time.timeScale = 0f;
        gameoverUI.SetActive(true);
        FindObjectOfType<AudioManager>().Play("GO-Theme");
    }

    IEnumerator LoadGameStart()
    {
        yield return new WaitForSeconds(1);
        FindObjectOfType<AudioManager>().Play("Begin");
        yield return new WaitForSeconds(2);
        FindObjectOfType<AudioManager>().Play("Startup");
    }

    public static void GameOver()
    {
        isDead = true;
    }

    public void LoadMenu(string MainMenu)
    {
        SceneManager.LoadScene(MainMenu);
        Debug.Log("sceneName to load: " + MainMenu);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
