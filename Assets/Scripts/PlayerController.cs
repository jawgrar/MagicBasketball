using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRigidbody;
    private Vector2 originalPosition;
    private Vector2 originalTouchPosition;
    private bool isPlayerBack = true;
    private bool isPlayerMoved = false;
    private float forceMagnitude = 700f;
    private GameObject currentBall;
    public LineRenderer lineRenderer;
    public GameObject touchPoint;
    public GameObject explosionEffect;
    public AudioSource audioSource;
    public AudioClip explosionSound;
    public GameObject ball1Prefab;
    public GameObject ball2Prefab;


    void Start()
    {
        AssignBall();

        originalPosition = transform.position;
        playerRigidbody = gameObject.GetComponent<Rigidbody2D>();
        originalTouchPosition = touchPoint.transform.position;
    }

    private void AssignBall()
    {
        // Assign the correct prefab based on PlayerPrefs
        string ballName = PlayerPrefs.GetString("Ball", "Ball1");

        // Deactivate the default ball
        ball1Prefab.SetActive(false);

        switch (ballName)
        {
            case "Ball1":
                currentBall = ball1Prefab;
                break;
            case "Ball2":
                currentBall = ball2Prefab;
                break;
            default:
                currentBall = ball1Prefab;
                break;
        }

        currentBall.SetActive(true);

        // Instantiate the chosen prefab
        // currentBall = Instantiate(ballPrefab, transform.position, Quaternion.identity);
    }

    void Update()
    {
        CheckPlayerPosition();
        ProcessInput();
        DrawLineToTrajectory();
    }

    void CheckPlayerPosition()
    {
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
        if (viewportPosition.x < 0 || viewportPosition.x > 1 || viewportPosition.y < 0)
        {
            transform.position = originalPosition;
            touchPoint.transform.position = originalTouchPosition;
            isPlayerBack = true;

        }
        else if (isPlayerBack)
        {
            playerRigidbody.gravityScale = 0;
            playerRigidbody.velocity = Vector2.zero;
        }

        touchPoint.SetActive(isPlayerBack);
    }

    void ProcessInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended)
            {
                ApplyForce(touch.position);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            ApplyForce(Input.mousePosition);
        }
    }

    // Apply force to player
    void ApplyForce(Vector2 touchPos)
    {
        if (!isPlayerMoved)
        {
            isPlayerMoved = true;
        }

        playerRigidbody.gravityScale = 1;
        isPlayerBack = false;

        Vector2 direction = (touchPos - (Vector2)transform.position).normalized;
        playerRigidbody.AddForce(direction * forceMagnitude);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ScoreCollider"))
        {
            ScoreManager.Update();
            ExplodePlayer();
        }
    }

    void ExplodePlayer()
    {
        GameObject explosionInstance = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        explosionInstance.AddComponent<AutoDestroyFireworks>();
        // play explosion sound
        audioSource.volume = PlayerPrefs.GetFloat("Volume");
        audioSource.PlayOneShot(explosionSound);
    }

    void DrawLineToTrajectory()
    {
        if (touchPoint.activeInHierarchy)
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, touchPoint.transform.position);
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    public bool MadeFirstMove()
    {
        return isPlayerMoved;
    }

    public void ResetFirstMove()
    {
        isPlayerMoved = false;
    }
}