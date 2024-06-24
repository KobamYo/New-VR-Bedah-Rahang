using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlane : MonoBehaviour
{
    public GameObject target;
    public GameObject planePrefab;

    [HideInInspector]
    public List<GameObject> spawnedPlanes = new List<GameObject>();
    private List<PlaneSlice_EzySlice> slicingPlanes = new List<PlaneSlice_EzySlice>();

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

            SpawnPlaneBetweenPoints();
            Destroy(startPoint);
            Destroy(endPoint);
        }

        
    }

    private void SpawnPlaneBetweenPoints()
    {
        Vector3 planePosition = (startPoint.transform.position + endPoint.transform.position) / 2f;
        Vector3 direction = endPoint.transform.position - startPoint.transform.position;
        Quaternion planeRotation = Quaternion.LookRotation(direction);

        GameObject planeObject = Instantiate(planePrefab, planePosition, planeRotation);
        PlaneSlice_EzySlice planeSlice = planeObject.GetComponent<PlaneSlice_EzySlice>();

        Transform planeTransform = planeObject.gameObject.transform;
        planeTransform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        planeTransform.SetParent(target.transform);

        planeSlice.target = target;

        if (spawnedPlanes.Count > 0)
        {
            planeSlice.firstPlane = slicingPlanes[0].firstPlane;
            planeSlice.secondPlane = planeTransform;
            slicingPlanes[0].secondPlane = planeTransform;
        }
        else
        {
            planeSlice.firstPlane = planeTransform;
        }

        spawnedPlanes.Add(planeObject);
        slicingPlanes.Add(planeSlice);
    }

    public void UndoSpawnPlane()
    {
        if (spawnedPlanes.Count > 0)
        {
            GameObject lastPlane = spawnedPlanes[spawnedPlanes.Count - 1];
            PlaneSlice_EzySlice lastSlicingPlane = slicingPlanes[slicingPlanes.Count - 1];

            Destroy(lastPlane);

            spawnedPlanes.RemoveAt(spawnedPlanes.Count - 1);
            slicingPlanes.RemoveAt(slicingPlanes.Count - 1);

            Debug.Log("Plane destroyed.");
        }
    }
}
