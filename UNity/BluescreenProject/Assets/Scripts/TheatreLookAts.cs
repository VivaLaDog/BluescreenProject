using UnityEngine;

public class TheatreLookAts : MonoBehaviour
{
    Transform targ;
    Vector3 targetPos;
    Vector3 oldPos;
    private void Start()
    {
        targ = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        targetPos = new Vector3(targ.position.x, transform.position.y, targ.position.z);
        if (oldPos != targetPos)
        {
            transform.LookAt(targetPos);
            transform.Rotate(new Vector3(transform.rotation.x, transform.rotation.y + 180, transform.rotation.z));
            oldPos = targetPos;
        }
    }
}
