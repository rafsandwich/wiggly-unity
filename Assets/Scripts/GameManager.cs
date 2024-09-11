using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverCanvas;
    public TextMeshProUGUI finalScoreText;
    public WormMovement worm;

    public Image fadeBlackImage;

    private bool isGameOver = false;

    // when worm 'dies'
    public void GameOver()
    {
        Debug.Log("Worm died, game over triggered");
        isGameOver = true;

        //StartCoroutine(FadeToBlack());
        Time.timeScale = 0f;

        gameOverCanvas.SetActive(true);

        int finalScore = Mathf.RoundToInt((worm.currentWormThickness * 100) - 50); //reused from WormMovement.UpdateScore() , make a GetScore() function
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

    //IEnumerator FadeToBlack()
    //{
    //    for (float alpha = 0; alpha <= 1; alpha += Time.deltaTime)
    //    {
    //        Color colour = fadeBlackImage.color;
    //        colour.a = alpha;
    //        fadeBlackImage.color = colour;
    //        yield return null;
    //    }
    //}
}
