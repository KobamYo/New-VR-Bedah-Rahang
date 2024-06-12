using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlane_V3 : MonoBehaviour
{
    public GameObject target;
    public GameObject planePrefab;
    public GameObject fingerPlane;
    
    private GameObject planeObject1;
    private GameObject planeObject2;
    private PlaneSlice_EzySlice slicingPlane1;
    private PlaneSlice_EzySlice slicingPlane2;

    private bool hasCollided = false;
    private Vector3 planePosition;
    private Quaternion planeRotation;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Mandible"))
        {
            Debug.Log("Collide");
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
    }

    public void SpawnPlane()
    {
        if (planeObject1 == null && hasCollided)
        {
            planeObject1 = Instantiate(planePrefab, fingerPlane.transform.position, fingerPlane.transform.rotation);
            slicingPlane1 = planeObject1.GetComponent<PlaneSlice_EzySlice>();

            planeObject1.transform.SetParent(target.transform);

            slicingPlane1.firstPlane = planeObject1.transform;
            slicingPlane1.target = target;

            Debug.Log("First Plane spawned successfully.");
        }
        else if (planeObject2 == null && hasCollided)
        {
            planeObject2 = Instantiate(planePrefab, fingerPlane.transform.position, fingerPlane.transform.rotation);
            slicingPlane2 = planeObject2.GetComponent<PlaneSlice_EzySlice>();

            planeObject2.transform.SetParent(target.transform);

            slicingPlane2.firstPlane = slicingPlane1.firstPlane;
            slicingPlane2.secondPlane = planeObject2.transform;
            slicingPlane2.target = target;

            Debug.Log("Second Plane spawned successfully.");
        }
    }
}
