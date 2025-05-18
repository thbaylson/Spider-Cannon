using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text gameOverText;
    [SerializeField] private DistanceTracker distanceTracker;

    private void Awake()
    {
        distanceTracker.OnRunEnded += Show;
        gameOverPanel.SetActive(false);
    }

    private void Show(float distance)
    {
        gameOverText.text = $"GAME OVER\nDistance Traveled: {Math.Round(distance, 1)}cm!";
        gameOverPanel.SetActive(true);
        // TODO: PlayerPerfs to track best?
    }

    public void RestartButton()
    {
        distanceTracker.OnRunEnded -= Show;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
