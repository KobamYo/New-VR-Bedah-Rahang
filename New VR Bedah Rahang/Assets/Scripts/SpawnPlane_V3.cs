using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlane_V3 : MonoBehaviour
{
    public GameObject target;
    public GameObject planePrefab;

    [HideInInspector]
    public List<GameObject> spawnedPlanes = new List<GameObject>();
    private List<PlaneSlice_EzySlice> slicingPlanes = new List<PlaneSlice_EzySlice>();

    private bool hasCollided = false;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Mandible"))
        {
            Debug.Log("Collide Stay");
            hasCollided = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Mandible"))
        {
            hasCollided = false;
            Debug.Log("Exit");
        }
    }

    public void SpawnPlane()
    {
        GameObject planeObject = Instantiate(planePrefab, this.transform.position, this.transform.rotation);
        PlaneSlice_EzySlice slicingPlane = planeObject.GetComponent<PlaneSlice_EzySlice>();

        if (spawnedPlanes.Count > 0)
        {
            // Flip the rotation of the subsequent planes
            planeObject.transform.Rotate(180, 0, 0);
            slicingPlane.firstPlane = slicingPlanes[0].firstPlane;
            slicingPlane.secondPlane = planeObject.transform;
            slicingPlanes[0].secondPlane = planeObject.transform;
        }
        else
        {
            slicingPlane.firstPlane = planeObject.transform;
        }

        planeObject.transform.SetParent(target.transform);
        slicingPlane.target = target;

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

            Debug.Log("Plane destroyed.");
        }
    }
}
