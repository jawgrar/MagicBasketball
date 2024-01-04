using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private Rigidbody2D playerRigidbody;
    private Vector2 originalPosition;
    private Vector2 originalTouchPosition;
    private bool isPlayerBack = true;
    private bool isPlayerMoved = false;

    public float forceMagnitude = 700f;
    public GameObject explosionEffect;
    public LineRenderer lineRenderer;
    public GameObject touchPoint;

    void Start()
    {
        originalPosition = transform.position;
        playerRigidbody = gameObject.GetComponent<Rigidbody2D>();
        originalTouchPosition = touchPoint.transform.position;
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
        if (other.gameObject.CompareTag("Basket"))
        {
            ScoreManager.Update();
            ExplodePlayer();
        }
    }

    void ExplodePlayer()
    {
        GameObject explosionInstance = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        explosionInstance.AddComponent<AutoDestroyFireworks>();
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
