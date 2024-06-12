using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlane_V3 : MonoBehaviour
{
    public GameObject target;
    public GameObject planePrefab;

    private GameObject planeObject1;
    private GameObject planeObject2;
    private PlaneSlice_EzySlice slicingPlane1;
    private PlaneSlice_EzySlice slicingPlane2;

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

    private void Update()
    {
        if (hasCollided && Input.GetKeyDown(KeyCode.Space))
        {
            SpawnPlane();
        }
        //if ((planeObject2 != null || planeObject1 != null) && Input.GetKeyDown(KeyCode.Z))
        //{
        //    UndoSpawnPlane();
        //}
    }

    public void SpawnPlane()
    {
        if (planeObject1 == null)
        {
            planeObject1 = Instantiate(planePrefab, this.transform.position, this.transform.rotation);
            slicingPlane1 = planeObject1.GetComponent<PlaneSlice_EzySlice>();
            planeObject1.transform.SetParent(target.transform);

            slicingPlane1.firstPlane = planeObject1.transform;
            slicingPlane1.target = target;
        }
        else if (planeObject2 == null)
        {
            
            planeObject2 = Instantiate(planePrefab, this.transform.position, this.transform.rotation);
            planeObject2.transform.Rotate(180, 0, 0); // Flip the rotation of the second plane opposite of this finger plane
            slicingPlane2 = planeObject2.GetComponent<PlaneSlice_EzySlice>();
            planeObject2.transform.SetParent(target.transform);

            slicingPlane1.secondPlane = planeObject2.transform;
            slicingPlane2.firstPlane = slicingPlane1.firstPlane;
            slicingPlane2.secondPlane = planeObject2.transform;
            slicingPlane2.target = target;
        }
    }

    public void UndoSpawnPlane()
    {
        if (planeObject2 != null)
        {
            Destroy(planeObject2);
            planeObject2 = null;
            slicingPlane2 = null;
            Debug.Log("Second Plane destroyed.");
        }
        else if (planeObject1 != null)
        {
            Destroy(planeObject1);
            planeObject1 = null;
            slicingPlane1 = null;
            Debug.Log("First Plane destroyed.");
        }
    }

    public bool IsFirstPlaneSpawned()
    {
        return planeObject1 != null;
    }

    public bool IsSecondPlaneSpawned()
    {
        return planeObject2 != null;
    }
}
