using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DirectionIndicator : MonoBehaviour
{
    public Camera mainCamera;
    public float lineDisplayTime = 0.4f; // Time in seconds the line will be visible
    public float originOffset = 0.3f; // Depends on your model

    private LineRenderer lineRenderer;
    private Vector3 targetPosition;
    private bool shouldRender;


    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        mainCamera=Camera.main;
    }
    private void Update()
    {
        if (shouldRender)
        {
            lineRenderer.positionCount = 2;
            Vector3 startPosition = new Vector3(transform.position.x, transform.position.y + originOffset, transform.position.z);
            lineRenderer.SetPosition(0, startPosition);
            lineRenderer.SetPosition(1, targetPosition);
        }
    }
    public void DrawLine(RaycastHit hit)
    {
        shouldRender = true;
        targetPosition = hit.point;
    }

    public void ChangeIndicatorVisibility(bool trigger)
    {
        lineRenderer.enabled=trigger;
    }
}
