using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnCollision : MonoBehaviour
{
    public GameObject pointPrefab;
    public GameObject planePrefab;
    public LineRenderer lineRenderer;

    private GameObject startPoint;
    private GameObject endPoint;

    private Vector3 contactPoint;

    private bool hasCollided = false;
    private bool canSpawn = true;
    private float cooldownDuration = 0.5f;

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasCollided && collision.gameObject.CompareTag("Skull"))
        {
            ContactPoint firstContact = collision.contacts[0];
            contactPoint = firstContact.point;
            hasCollided = true;
            Debug.Log("Collision Entered with Cube");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (hasCollided && canSpawn && collision.gameObject.CompareTag("Skull"))
        {
            if (startPoint == null)
            {
                startPoint = Instantiate(pointPrefab, contactPoint, Quaternion.identity);
                startPoint.transform.SetParent(collision.gameObject.transform);
                Debug.Log("Sphere Spawned on Exit as First Point");
            }
            else if (endPoint == null)
            {
                endPoint = Instantiate(pointPrefab, contactPoint, Quaternion.identity);
                endPoint.transform.SetParent(collision.gameObject.transform);
                Debug.Log("Second Point Spawned on Exit as End Point");

                CreateLineBetweenPoints();
                SpawnPlaneAlongLine();

                startPoint = null;
                endPoint = null;
                hasCollided = false;
                canSpawn = false;
                Invoke("ResetCooldown", cooldownDuration);
            }
        }
    }
    private void CreateLineBetweenPoints()
    {
        if (startPoint != null && endPoint != null && lineRenderer != null)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, startPoint.transform.position);
            lineRenderer.SetPosition(1, endPoint.transform.position);
        }
    }

    private void SpawnPlaneAlongLine()
    {
        if (startPoint != null && endPoint != null && planePrefab != null)
        {
            Vector3 planePosition = (startPoint.transform.position + endPoint.transform.position) / 2f;
            Quaternion planeRotation = Quaternion.LookRotation(endPoint.transform.position - startPoint.transform.position);

            GameObject plane = Instantiate(planePrefab, planePosition, planeRotation);
            Debug.Log("Plane Spawned along Line");
        }
    }
    private void ResetCooldown()
    {
        canSpawn = true;
    }
}
