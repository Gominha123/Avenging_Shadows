using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera)), ExecuteInEditMode]
[AddComponentMenu("Effects/Crepuscular Rays", -1)]
public class Crepuscular : MonoBehaviour
{
    public Material material;
    public GameObject light;
    public string excludedTag = "ExcludeCrepuscular";

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Camera camera = GetComponent<Camera>();
        Vector3 lightPosViewport = camera.WorldToViewportPoint(transform.position - light.transform.forward);

        if (!IsInExcludedTag(lightPosViewport))
        {
            material.SetVector("_LightPos", lightPosViewport);
            Graphics.Blit(source, destination, material);
        }
        else
        {
            // If the light is behind an object with the excluded tag, just copy the source to the destination.
            Graphics.Blit(source, destination);
        }
    }

    bool IsInExcludedTag(Vector3 viewportPos)
    {
        Vector3 origin = GetComponent<Camera>().ViewportToWorldPoint(viewportPos);
        Vector3 direction = (light.transform.position - origin).normalized;

        RaycastHit hit;
        if (Physics.Linecast(origin, light.transform.position, out hit))
        {
            return hit.collider.CompareTag(excludedTag);
        }

        return false;
    }
}
