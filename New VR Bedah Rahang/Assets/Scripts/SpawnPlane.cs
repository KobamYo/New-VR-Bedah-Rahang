using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlane : MonoBehaviour
{
    [SerializeField]
    private GameObject target;

    private PlaneSlice_EzySlice planeSlice1;
    private PlaneSlice_EzySlice planeSlice2;

    public GameObject planePrefab;
    public GameObject planeObject1;
    public GameObject planeObject2;

    private GameObject startPoint;
    private GameObject endPoint;
    private LineRenderer lineRenderer;
    
    private bool lineDrawn = false;

    void Start()
    {
        // Initialize LineRenderer if not already assigned
        lineRenderer = GetComponent<LineRenderer>();

        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.positionCount = 2;
            lineRenderer.enabled = false;
        }
    }

    void Update()
    {
        GameObject[] points = GameObject.FindGameObjectsWithTag("Point");
        
        if (points.Length >= 2 && !lineDrawn)
        {
            startPoint = points[0];
            endPoint = points[1];

            lineRenderer.enabled = true;
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

        if (planeObject1 == null)
        {
            planeObject1 = Instantiate(planePrefab, planePosition, planeRotation);
            planeSlice1 = planeObject1.GetComponent<PlaneSlice_EzySlice>();

            Transform planeTransform1 = planeObject1.gameObject.transform;
            planeTransform1.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
            planeTransform1.transform.SetParent(target.transform);

            planeSlice1.firstPlane = planeTransform1;
            planeSlice1.target = target;
        }
        else
        {
            planeObject2 = Instantiate(planePrefab, planePosition, planeRotation);
            planeSlice2 = planeObject2.GetComponent<PlaneSlice_EzySlice>();

            Transform planeTransform2 = planeObject2.gameObject.transform;
            planeTransform2.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
            planeTransform2.transform.SetParent(target.transform);

            planeSlice1.secondPlane = planeTransform2;
            planeSlice2.secondPlane = planeTransform2;
            planeSlice2.firstPlane = planeSlice1.transform;
            planeSlice2.target = target;
        }

        lineRenderer.positionCount = 0;
    }
}
