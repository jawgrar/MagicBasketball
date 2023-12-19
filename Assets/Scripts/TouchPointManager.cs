using UnityEngine;

public class TouchPointManager : MonoBehaviour
{
    public GameObject circlePrefab;

    void Start()
    {
        circlePrefab.SetActive(false);
    }

    void Update()
    {
        // add mobile touch support
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
            {
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                circlePrefab.SetActive(true);
                circlePrefab.transform.position = touchPosition;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                circlePrefab.SetActive(false);
            }
        }
        // add mouse support
        else if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            circlePrefab.SetActive(true);
            circlePrefab.transform.position = mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            circlePrefab.SetActive(false);
        }
    }
}
