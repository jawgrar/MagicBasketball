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

    void CheckPlayerPosition()
    {
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
        if (viewportPosition.x < 0 || viewportPosition.x > 1 || viewportPosition.y < 0)
        {
            transform.position = originalPosition;
            isPlayerBack = true;

        }
        else if (isPlayerBack)
        {
            playerRigidbody.gravityScale = 0;
            playerRigidbody.velocity = Vector2.zero;
        }
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
        playerRigidbody.gravityScale = 1;
        isPlayerBack = false;

        Vector2 direction = (touchPos - (Vector2)transform.position).normalized;
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
