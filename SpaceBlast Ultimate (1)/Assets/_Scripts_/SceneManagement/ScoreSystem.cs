
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour //Bogdan C. SU17A - 2019©\\
{
    #region Variables
    public TextMeshProUGUI[] scoreText;
    public  TextMeshProUGUI highScoreText;

    public int Internalscore;

    public  int oldHighscore;
    public  int newHighscore;

    private static ScoreSystem instance;
    private float timer;
    #endregion

    public static ScoreSystem Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ScoreSystem>();
            }
            return instance;
        }
    }

    void Start()
    {
        highScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (Internalscore > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", Internalscore);
            highScoreText.text = Internalscore.ToString();
        }

        if (timer > 1f)
        {
            Internalscore += 10;

            //Reset the timer to 0.
            timer = 0;

            scoreText[0].text = Internalscore.ToString();
        }

        if (Internalscore < 0)
        {
            PlayerHealth.playerDied = true;
        }
    }
}
