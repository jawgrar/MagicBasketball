using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // Singleton instance

    public int score = 0;
    public TMP_Text scoreText;
    public float timer = 20f;
    public TMP_Text timerText;
    public bool isGameStarted = false;
    public bool isGameOver = false;
    public GameObject player;
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
    }

    private void Start() {
        scoreText.text = score.ToString("0000");
        timerText.text = timer.ToString("00");;
    }

    private void FixedUpdate()
    {
        if(timer <= 0) {
            isGameOver = true;
        }

        if(isGameStarted == false && isGameOver == false) {
            StartGame();
        }
        else if(isGameStarted == true && isGameOver == false)
        {
            timer -= Time.deltaTime;
            timerText.text = timer.ToString("00");
            scoreText.text = ScoreManager.score.ToString("0000");
        }
        else if (isGameOver == true)
        {
            EndGame();
        }
    }

    // Use this method to begin the game
    public void StartGame()
    {
        if(Input.touchCount > 0 || Input.GetMouseButton(0))
        {
            isGameStarted = true;
            ScoreManager.Start();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void EndGame()
    {
        isGameStarted = false;   
        player.SetActive(false);
    }
}
