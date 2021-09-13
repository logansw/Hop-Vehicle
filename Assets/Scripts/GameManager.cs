using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int floorsPassed;
    public FloorSpawner floorSpawner;
    public GameObject player;
    public bool gameOver;
    public int score;
    public bool paused;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
        if (score > HighScore.highScore)
            HighScore.highScore = score;
    }

    // Start is called before the first frame update
    void Start()
    {
        floorsPassed = 1;
    }

    public void EndGame()
    {
        Cloud.cloudCount = 0;
        StartCoroutine(holdUpThenReload());
    }

    public void PauseGame()
    {
        if (paused)
        {
            paused = false;
            // Enable Pause Panel
            Time.timeScale = 1f;
        } else
        {
            paused = true;
            // Disable Pause Panel
            Time.timeScale = 0f;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator holdUpThenReload()
    {
        yield return new WaitForSeconds(1f);
        gameOver = false;
        SceneManager.LoadScene(0);
    }
}