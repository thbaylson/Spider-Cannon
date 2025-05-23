using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject leaderBoardPanel;
    [SerializeField] private GameObject timeTextObject;

    [SerializeField] private TMP_Text gameOverText;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private SpiderLauncher player;
    public LeaderBoard leaderboard;
    public PlayerManager playerManager;
    



    private void Awake()
    {
        player.OnRunEnded += Show;
        gameOverPanel.SetActive(false);
        leaderBoardPanel.SetActive(false);
    }

    private void Show(float distance, bool reachedFinish)
    {
        
        gameOverText.text = $"GAME OVER\nDistance Traveled: {Math.Round(distance, 1)}cm!";
        if (reachedFinish)
        {
            gameOverPanel.SetActive(true);
            leaderBoardPanel.SetActive(true);
            timeTextObject.SetActive(true);
            timeText.text = "Time:\n " + ConvertGameTimeToString();
            StartCoroutine(LeaderboardShit());

        }
        
        gameOverPanel.SetActive(true);  // TODO: PlayerPerfs to track best? 

    }
    IEnumerator LeaderboardShit()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(1f);
        yield return leaderboard.SubmitScoreRoutine(playerManager.GetTime());
        Time.timeScale = 1f;
    }
    private string ConvertGameTimeToString()
    {
        int timeToConvert = playerManager.GetTime();
        int minutes = timeToConvert / 60;
        int seconds = timeToConvert % 60;
        string timeString = string.Format("{0:00}:{1:00}", minutes, seconds);
        return timeString;
    }
    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
