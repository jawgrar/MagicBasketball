using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // Singleton instance
    public bool isGameStarted = false;
    public bool isGameOver = false;
    public float timerDuration = 10f;

    // TODO: implement a TimerManager class
    private float timer;
    private TMP_Text scoreText;
    private TMP_Text timerText;
    private GameObject player;
    private GameObject target;
    private GameObject gameOverPanel;

    private bool _gameEnded = false;

    private void Awake()
    {
        // Singleton pattern implementation
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // Unsubscribe when the object is destroyed
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameScene")
        {
            FindGameObjects();
            Reset();
        }
    }

    private void FindGameObjects()
    {
        // find game objects
        player = GameObject.FindGameObjectWithTag("Player");
        target = GameObject.FindGameObjectWithTag("Basket");
        gameOverPanel = GameObject.FindGameObjectWithTag("GameOverPanel");
        scoreText = GameObject.FindGameObjectWithTag("Score").GetComponent<TMP_Text>();
        timerText = GameObject.FindGameObjectWithTag("Timer").GetComponent<TMP_Text>();
    }

    private void Reset()
    {
        Debug.Log("GameManager Reset");

        // reset flags
        _gameEnded = false;
        isGameStarted = false;
        isGameOver = false;
        timer = timerDuration;

        // reset UI
        ScoreManager.Reset();
        scoreText.text = ScoreManager.GetScore().ToString("0000");
        timerText.text = timer.ToString("00");

        // reset game objects
        player.GetComponent<PlayerManager>().ResetFirstMove();
        player.SetActive(true);
        target.SetActive(true);
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    private void FixedUpdate()
    {
        // initial game state
        if (isGameStarted == false && isGameOver == false && _gameEnded == false)
        {
            isGameStarted = player.GetComponent<PlayerManager>().MadeFirstMove();
        }
        // game started
        if (isGameStarted == true && isGameOver == false && _gameEnded == false)
        {
            timer -= Time.deltaTime;
            timerText.text = timer.ToString("00");
            scoreText.text = ScoreManager.GetScore().ToString("0000");

            if (timer <= 0)
            {
                isGameOver = true;
            }
        }
        // game over
        if (isGameStarted == true && isGameOver == true && _gameEnded == false)
        {
            EndGame();
            _gameEnded = true;
        }
    }

    private void EndGame()
    {
        player.SetActive(false);
        target.SetActive(false);
        gameOverPanel.SetActive(true);
    }
}
