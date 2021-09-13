using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text scoreText;
    public Text bestText;
    public GameObject pausePanel;
    public GameManager gameManager;

    void Awake()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        bestText = GameObject.Find("BestText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = gameManager.score.ToString();
        bestText.text = "BEST: " + HighScore.highScore.ToString();
        pausePanel.SetActive(gameManager.paused);
    }
}
