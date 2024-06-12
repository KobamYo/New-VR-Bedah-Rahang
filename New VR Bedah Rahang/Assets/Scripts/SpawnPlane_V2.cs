using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlane_V2 : MonoBehaviour
{
    [SerializeField]
    private GameObject target;

    private PlaneSlice_EzySlice planeSlice1;
    private PlaneSlice_EzySlice planeSlice2;

    public GameObject planePrefab;
    public GameObject planeObject1;
    public GameObject planeObject2;

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

        if (planeObject1 == null)
        {
            planeObject1 = Instantiate(planePrefab, pointPosition, planeRotation);
            planeSlice1 = planeObject1.GetComponent<PlaneSlice_EzySlice>();

            Transform planeTransform1 = planeObject1.gameObject.transform;
            planeTransform1.transform.rotation = Quaternion.identity; // Adjust as needed
            planeTransform1.transform.SetParent(target.transform);

            planeSlice1.firstPlane = planeTransform1;
            planeSlice1.target = target;
        }
        else
        {
            planeObject2 = Instantiate(planePrefab, pointPosition, planeRotation);
            planeSlice2 = planeObject2.GetComponent<PlaneSlice_EzySlice>();

            Transform planeTransform2 = planeObject2.gameObject.transform;
            planeTransform2.transform.rotation = Quaternion.identity; // Adjust as needed
            planeTransform2.transform.SetParent(target.transform);

            planeSlice1.secondPlane = planeTransform2;
            planeSlice2.secondPlane = planeTransform2;
            planeSlice2.firstPlane = planeSlice1.transform;
            planeSlice2.target = target;
        }
    }
}
