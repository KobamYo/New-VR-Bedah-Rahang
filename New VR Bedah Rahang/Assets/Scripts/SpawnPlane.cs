using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlane : MonoBehaviour
{
    public GameObject planePrefab;
    public LineRenderer lineRenderer;

    private GameObject startPoint;
    private GameObject endPoint;
    private bool lineDrawn = false;

    void Update()
    {
        GameObject[] points = GameObject.FindGameObjectsWithTag("Point");
        if (points.Length >= 2 && !lineDrawn)
        {
            startPoint = points[0];
            endPoint = points[1];

            DrawLineBetweenPoints(startPoint.transform.position, endPoint.transform.position);
            lineDrawn = true;
        }

        if (lineDrawn)
        {
            SpawnPlaneAlongLine();
            Destroy(startPoint);
            Destroy(endPoint);
            lineDrawn = false;
        }
    }

    private void DrawLineBetweenPoints(Vector3 start, Vector3 end)
    {
        if (lineRenderer != null)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, start);
            lineRenderer.SetPosition(1, end);
        }
    }

    private void SpawnPlaneAlongLine()
    {
        Vector3 planePosition = (startPoint.transform.position + endPoint.transform.position) / 2f;
        Vector3 direction = endPoint.transform.position - startPoint.transform.position;
        Quaternion planeRotation = Quaternion.LookRotation(direction);

        GameObject plane = Instantiate(planePrefab, planePosition, planeRotation);
        plane.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);

        lineRenderer.positionCount = 0; // Clear the line
    }
}
