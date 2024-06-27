using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlane_V2 : MonoBehaviour
{
    public GameObject target;
    public GameObject planePrefab;

    [HideInInspector]
    public List<GameObject> spawnedPlanes = new List<GameObject>();
    private List<PlaneSlice_EzySlice> slicingPlanes = new List<PlaneSlice_EzySlice>();

    private List<GameObject> spawnedPoints = new List<GameObject>();

    void Update()
    {
        GameObject[] points = GameObject.FindGameObjectsWithTag("Point");

        foreach (GameObject point in points)
        {
            if (!spawnedPoints.Contains(point))
            {
                SpawnPlaneAtPoint(point.transform.position);
                spawnedPoints.Add(point);
                Destroy(point);
            }
        }
    }

    private void SpawnPlaneAtPoint(Vector3 pointPosition)
    {
        Quaternion planeRotation = Quaternion.identity; // Default rotation

        GameObject planeObject = Instantiate(planePrefab, pointPosition, planeRotation);
        PlaneSlice_EzySlice slicingPlane = planeObject.GetComponent<PlaneSlice_EzySlice>();
        Transform planeTransform = planeObject.gameObject.transform;

        planeTransform.rotation = Quaternion.identity; // Adjust as needed
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
