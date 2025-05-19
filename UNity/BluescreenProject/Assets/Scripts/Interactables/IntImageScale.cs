using UnityEngine;

public class IntImageScale : MonoBehaviour
{
    Vector3 ogScale;
    BLPlayerMovement bl;
    bool scale = false;
    Interactable inter;

    internal void HlBool(bool v)
    {
        scale = v;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bl = transform.root.GetComponentInChildren<BLPlayerMovement>();
        ogScale = transform.localScale;

        if (GetComponentInParent<Items>() != null)
            inter = GetComponentInParent<Items>();

        if (GetComponentInParent<LoreTexts>() != null)
            inter = GetComponentInParent<LoreTexts>();

        if (GetComponentInParent<LeverPull>() != null)
            inter = GetComponentInParent<LeverPull>();

        if (GetComponentInParent<PuzzleButton>() != null)
            inter = GetComponentInParent<PuzzleButton>();

        if(GetComponentInParent<Poster>() != null)
            inter = GetComponentInParent<Poster >();

        if(GetComponentInParent<Doors>() != null)
            inter = GetComponentInParent<Doors >();

        if(GetComponentInParent<NumericCode>() != null)
            inter = GetComponentInParent<NumericCode>();

        if(GetComponentInParent<Padlock>() != null)
            inter = GetComponentInParent<Padlock>();

        if(GetComponentInParent<NumericCode2>() != null)
            inter = GetComponentInParent<NumericCode2>();

        if(GetComponentInParent<WeakerDoor>() != null)
            inter = GetComponentInParent<WeakerDoor>();

        if(GetComponentInParent<FacButton>() != null)
            inter = GetComponentInParent<FacButton>();

        if(GetComponentInParent<Photo>() != null)
            inter = GetComponentInParent<Photo>();
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
        {//different items have different scale, which makes some small and some big??
            var distance = Vector3.Distance(pos, transform.position);
            var parentscale = transform.parent.localScale;

            float a = ogScale.x / distance;
            float b = ogScale.y / distance;

            float divider = inter.imageScalerDivider;
            if (divider == 0)
                divider = 1;

            float psx = parentscale.x / divider;
            float psy = parentscale.y / divider;

            if (a > psx)
                a = psx;
            if (b > psy)
                b = psy;

            if (a < b)
                a = b;
            else
                b = a;

            Vector3 newScale = new Vector3(a, b, 0);
            if (distance < 4)
                transform.localScale = newScale;
        }
        else
        {
            transform.localScale = Vector3.zero;
        }
    }


}
