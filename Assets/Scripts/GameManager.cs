using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverCanvas;
    public TextMeshProUGUI finalScoreText;
    public WormMovement worm;

    private bool isGameOver = false;

    // when worm 'dies'
    public void GameOver()
    {
        Debug.Log("Worm died, game over triggered");
        isGameOver = true;

        Time.timeScale = 0f;

        gameOverCanvas.SetActive(true);

        int finalScore = Mathf.RoundToInt(worm.currentWormThickness * 100); //reused from WormMovement.UpdateScore() , make a GetScore() function
        finalScoreText.text = "Game over! Your score is: " + finalScore.ToString();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
        isGameOver = false;
    }

    public void QuitGame()
    {
        Debug.Log("Game quit requested");
        Application.Quit();
    }
}
