using UnityEngine;

public class Spotlight : MonoBehaviour
{
    [SerializeField] Transform lookAt;

    Vector3 stillPos;
    void Update()
    {
        if (lookAt.position != stillPos)
        {
            transform.LookAt(lookAt);
            stillPos = lookAt.position;
        }
    }
}
