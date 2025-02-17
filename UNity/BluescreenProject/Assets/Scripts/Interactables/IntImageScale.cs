using UnityEngine;

public class IntImageScale : MonoBehaviour
{
    Vector3 ogScale;
    BLPlayerMovement bl;
    bool scale = false;

    internal void HlBool(bool v)
    {
        scale = v;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bl = transform.root.GetComponentInChildren<BLPlayerMovement>();
        ogScale = transform.localScale;
    }

    void Update()
    {
        //scale the canvas in accordance of BLs position, look at the player's camera
        var pos = Vector3.zero;
        if (bl.GetComponentInChildren<Camera>())
        {
            pos = bl.GetComponentInChildren<Camera>().transform.position;
        }

        transform.LookAt(transform.position - (pos - transform.position));


        if (scale)
        {
            var distance = Vector3.Distance(pos, transform.position);

            float a = ogScale.x / distance;
            float b = ogScale.y / distance;
            /*if (a > 0.15)
                a = .15f;
            if (b > 0.15)
                b = .15f;*/

            Vector3 newScale = new Vector3(a, b, 0);

            transform.localScale = newScale;
        }
        else
        {
            transform.localScale = Vector3.zero;
        }
    }


}
