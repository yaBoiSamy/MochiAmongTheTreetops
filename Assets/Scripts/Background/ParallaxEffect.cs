using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Vector2 parallaxMultiplier;
    public GameObject mainCamera;
    private Vector3 lastCameraPosition;

    void Start()
    {
        lastCameraPosition = mainCamera.transform.position;
    }

    void LateUpdate()
    {
        Vector2 deltaMovement = mainCamera.transform.position - lastCameraPosition;
        transform.position = new Vector2(deltaMovement.x * parallaxMultiplier.x + transform.position.x, deltaMovement.y * parallaxMultiplier.y + transform.position.y);
        lastCameraPosition = mainCamera.transform.position;
    }
}
