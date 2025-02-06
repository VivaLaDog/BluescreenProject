using UnityEngine;

public class HighLightChanger : MonoBehaviour
{
    float intensity = 0;
    float baseIntensity = 0;
    BLPlayerMovement bl;
    new Light light;
    float distance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        bl = transform.root.GetComponentInChildren<BLPlayerMovement>();
        light = GetComponent<Light>();
        baseIntensity = light.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(bl.transform.position, transform.position);
        intensity = baseIntensity / distance;

        light.intensity = intensity;
    }
}
