using UnityEngine;

public class MouseParallax : MonoBehaviour
{
    Vector2 startPos;
    [SerializeField] int moveBy;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        Vector2 pz = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        float posX = Mathf.Lerp(transform.position.x, startPos.x + (pz.x * moveBy), Time.deltaTime);
        float posY = Mathf.Lerp(transform.position.y, startPos.y + (pz.y * moveBy), Time.deltaTime);

        transform.position = new Vector3(posX, posY, 0);
    }
}
