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

    void Update()
    {
        GameObject[] points = GameObject.FindGameObjectsWithTag("Point");
        
        if (points.Length >= 2)
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
        PlaneSlice_EzySlice slicingPlane = planeObject.GetComponent<PlaneSlice_EzySlice>();
        Transform planeTransform = planeObject.gameObject.transform;

        planeTransform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        planeTransform.SetParent(target.transform);
        slicingPlane.target = target;

        if (spawnedPlanes.Count == 0)
        {
            // First plane
            slicingPlane.firstPlane = planeTransform;
        }
        else if (spawnedPlanes.Count == 1)
        {
            // Second plane
            planeObject.transform.Rotate(180, 0, 0);
            slicingPlane.firstPlane = slicingPlanes[0].firstPlane;
            slicingPlane.secondPlane = planeTransform;
            slicingPlanes[0].secondPlane = planeTransform;
        }

        spawnedPlanes.Add(planeObject);
        slicingPlanes.Add(slicingPlane);
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

            // Update the references in the remaining planes
            if (slicingPlanes.Count > 0)
            {
                slicingPlanes[0].secondPlane = slicingPlanes.Count > 1 ? slicingPlanes[1].transform : null;
            }
        }
    }
}
