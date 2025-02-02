using Oculus.Interaction;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasCollided && collision.gameObject.CompareTag("Mandible"))
        {
            ContactPoint contact = collision.contacts[0];
            contactPoint = contact.point;
            hasCollided = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (hasCollided && canSpawn && collision.gameObject.CompareTag("Mandible"))
        {
            if (startPoint == null)
            {
                startPoint = Instantiate(pointPrefab, contactPoint, Quaternion.identity);
                startPoint.transform.SetParent(collision.gameObject.transform);
            }
            else if (endPoint == null)
            {
                endPoint = Instantiate(pointPrefab, contactPoint, Quaternion.identity);
                endPoint.transform.SetParent(collision.gameObject.transform);

                DrawLineBetweenPoints();
                SpawnPlane();

                if (startPoint != null) Destroy(startPoint);

                if (endPoint != null) Destroy(endPoint);

                startPoint = null;
                endPoint = null;
                hasCollided = false;
                canSpawn = false;

                Invoke("ResetCooldown", 1.5f);
            }
        }
    }
    private void DrawLineBetweenPoints()
    {
        if (startPoint != null && endPoint != null && lineRenderer != null)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, startPoint.transform.position);
            lineRenderer.SetPosition(1, endPoint.transform.position);
        }
    }

    private void SpawnPlane()
    {
        if (startPoint != null && endPoint != null && planePrefab != null)
        {
            Vector3 planePosition = (startPoint.transform.position + endPoint.transform.position) / 2f;
            Vector3 lineDirection = endPoint.transform.position - startPoint.transform.position;
            Quaternion planeRotation = Quaternion.LookRotation(lineDirection);

            GameObject plane = Instantiate(planePrefab, planePosition, planeRotation);
            plane.transform.rotation = Quaternion.LookRotation(endPoint.transform.position - startPoint.transform.position, Vector3.up);
        }
    }

    private void ResetCooldown()
    {
        canSpawn = true;
    }
}
