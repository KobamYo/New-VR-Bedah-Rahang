using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlane_V3 : MonoBehaviour
{
    public GameObject target;
    public GameObject fingerPlane;
    public GameObject planePrefab;
    public GameObject planeObject1;
    public GameObject planeObject2;

    private PlaneSlice_EzySlice slicingPlane1;
    private PlaneSlice_EzySlice slicingPlane2;

    private Vector3 planePosition;
    private Quaternion planeRotation;
    private Vector3 planeScale;
    private bool hasCollided = false;

    private void Update()
    {
        if (hasCollided && Input.GetKeyDown(KeyCode.Space))
        {
            // Save the transform properties of the finger's plane
            planePosition = fingerPlane.transform.position;
            planeRotation = fingerPlane.transform.rotation;
            planeScale = fingerPlane.transform.localScale;

            SpawnPlane();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Mandible"))
        {
            if (collision.contacts[0].thisCollider.gameObject == fingerPlane)
            {
                Debug.Log("Finger Plane Entered Collision with Mandible");
                hasCollided = true;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (hasCollided && collision.gameObject.CompareTag("Mandible"))
        {
            if (collision.contacts[0].thisCollider.gameObject == fingerPlane)
            {
                Debug.Log("Finger Plane Staying in Collision with Mandible");
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Mandible"))
        {
            if (collision.contacts[0].thisCollider.gameObject == fingerPlane)
            {
                Debug.Log("Finger Plane Exited Collision with Mandible");
                hasCollided = false;
            }
        }
    }

    public void SpawnPlane()
    {
        if (planeObject1 == null)
        {
            planeObject1 = Instantiate(planePrefab, planePosition, planeRotation);
            planeObject1.transform.localScale = planeScale;
            slicingPlane1 = planeObject1.GetComponent<PlaneSlice_EzySlice>();

            planeObject1.transform.SetParent(target.transform);

            slicingPlane1.firstPlane = planeObject1.transform;
            slicingPlane1.target = target;
        }
        else
        {
            planeObject2 = Instantiate(planePrefab, planePosition, planeRotation);
            planeObject2.transform.localScale = planeScale;
            slicingPlane2 = planeObject2.GetComponent<PlaneSlice_EzySlice>();

            planeObject2.transform.SetParent(target.transform);

            slicingPlane2.firstPlane = slicingPlane1.firstPlane;
            slicingPlane2.secondPlane = planeObject2.transform;
            slicingPlane2.target = target;
        }

        Debug.Log("Plane spawned successfully.");
    }
}
