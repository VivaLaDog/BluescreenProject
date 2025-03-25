using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Portal linkedPortal;
    public MeshRenderer screen;
    Camera playerCam;
    Camera portalCam;
    RenderTexture viewTexture;

    private void Awake()
    {
        playerCam = Camera.main;
        portalCam = GetComponentInChildren<Camera>();
        portalCam.enabled = false;
    }

    void CreateViewTexture()
    {
        if (viewTexture == null || viewTexture.height != Screen.height || viewTexture.width != Screen.width)
        {
            if(viewTexture != null)
            {
                viewTexture.Release();
            }
            viewTexture = new RenderTexture(Screen.width, Screen.height, 0);
            portalCam.targetTexture = viewTexture;

            linkedPortal.screen.material.SetTexture("_MainTex", viewTexture);
        }
    }
    public void Render()
    {
        screen.enabled = false;

        CreateViewTexture();
        var m = transform.localToWorldMatrix * linkedPortal.transform.localToWorldMatrix * playerCam.transform.localToWorldMatrix;
        portalCam.transform.SetPositionAndRotation(m.GetColumn(3), m.rotation);

        portalCam.Render();

        screen.enabled = true;
    }
}