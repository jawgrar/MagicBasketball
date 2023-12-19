// 2023-12-13 AI-Tag 
// This was created with assistance from Muse, a Unity Artificial Intelligence product

using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private Rigidbody2D playerRigidbody;
    private Vector2 originalPosition;
    private bool isPlayerBack;
    public float forceMagnitude = 700f;
    public GameObject explosionEffect;

    void Start()
    {
        originalPosition = transform.position;
        playerRigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckPlayerPosition();
        ProcessInput();        
    }

    // Check if the player is back or out of bounds
    // 2023-12-13 AI-Tag 
    void CheckPlayerPosition()
    {
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
        if (viewportPosition.x < 0 || viewportPosition.x > 1 || viewportPosition.y < 0)
        {
            Debug.Log("Player is out of bounds, resetting position");
            transform.position = originalPosition;
            isPlayerBack = true;

        }
        else if (isPlayerBack)
        {
            Debug.Log("Player is back, setting gravity to 0");
            playerRigidbody.gravityScale = 0;
            playerRigidbody.velocity = Vector2.zero;
        }
    }


    // Process input (touch or mouse click)
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
        Debug.Log("Input ended, setting gravity to 1");
        playerRigidbody.gravityScale = 1;
        isPlayerBack = false;

        Vector2 direction = (touchPos - (Vector2)transform.position).normalized;
        Debug.Log("Input ended, adding force to player");
        playerRigidbody.AddForce(direction * forceMagnitude);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Square"))
        {
            ScoreManager.score += 1;
            ExplodePlayer();
        }
    }
    
    void ExplodePlayer()
    {
        GameObject explosionInstance = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        explosionInstance.AddComponent<AutoDestroyParticleSystem>();
    }
}
