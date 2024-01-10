
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // Singleton instance
    public bool isGameStarted = false;
    public bool isGameOver = false;
    public float timerDuration = 10f;
    public AudioClip musicClip;
    private AudioSource musicSource;
    private TMP_Text scoreText;
    private TMP_Text timerText;
    private GameObject player;
    private GameObject target;
    private GameObject gameOverPanel;
    private bool _gameActive = false;

    private void Start()
    {
        SetDefaultPlayerPrefs();
    }

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
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameScene")
        {
            FindGameObjects();
            InitiateGame();
            PlayMusic();
        }
    }

    void SetDefaultPlayerPrefs()
    {
        if (!PlayerPrefs.HasKey("Volume"))
        {
            PlayerPrefs.SetFloat("Volume", 1.0f);
        }

        if (!PlayerPrefs.HasKey("Music"))
        {
            PlayerPrefs.SetInt("Music", 1);
        }
    }

    void PlayMusic()
    {
        musicSource.clip = musicClip;

        if (PlayerPrefs.GetInt("Music") == 1)
        {
            musicSource.volume = PlayerPrefs.GetFloat("Volume", 1);
            musicSource.Play();
        }
        else
        {
            musicSource.Stop();
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
        musicSource = GameObject.FindGameObjectWithTag("Music1").GetComponent<AudioSource>();
    }

    private void InitiateGame()
    {
        // reset flags
        _gameActive = true;
        isGameStarted = false;
        isGameOver = false;

        // reset UI
        ScoreManager.Reset();
        scoreText.text = ScoreManager.GetScore().ToString("0000");
        TimerManager.Reset(timerDuration);
        timerText.text = TimerManager.GetTimer().ToString("00");

        // reset game objects
        player.GetComponent<PlayerController>().ResetFirstMove();
        player.SetActive(true);
        target.SetActive(true);
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    private void FixedUpdate()
    {
        // gams started then ended. waiting for player to restart
        if (_gameActive == false) return;

        // initial game state
        if (isGameStarted == false && isGameOver == false)
        {
            isGameStarted = player.GetComponent<PlayerController>().MadeFirstMove();
        }
        // game started
        if (isGameStarted == true && isGameOver == false)
        {
            timerText.text = TimerManager.GetTimer().ToString("00");
            scoreText.text = ScoreManager.GetScore().ToString("0000");

            if (TimerManager.IsTimeUp())
            {
                isGameOver = true;
            }
        }
        // game over
        if (isGameStarted == true && isGameOver == true)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        player.SetActive(false);
        target.SetActive(false);
        gameOverPanel.SetActive(true);
        _gameActive = false;
    }
}
