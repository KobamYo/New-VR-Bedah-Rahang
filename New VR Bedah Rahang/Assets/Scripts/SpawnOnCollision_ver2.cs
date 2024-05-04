using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class SpawnOnCollision_ver2 : MonoBehaviour
{
    public GameObject pointPrefab;
    public LineRenderer lineRenderer;

    private GameObject startPoint;
    private GameObject endPoint;
    private Vector3 contactPoint;
    
    private bool hasCollided = false;


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
        if (hasCollided && collision.gameObject.CompareTag("Mandible"))
        {
            
            if (startPoint == null)
            {
                startPoint = Instantiate(pointPrefab, contactPoint, Quaternion.identity);
                startPoint.transform.SetParent(collision.gameObject.transform);

            }
            else if (startPoint != null)
            {
                endPoint = Instantiate(pointPrefab, contactPoint, Quaternion.identity);
                endPoint.transform.SetParent(collision.gameObject.transform);

                DrawLineBetweenPoints();

                startPoint = null;
                endPoint = null;
                hasCollided = false;
            }
        }
    }

    private void DrawLineBetweenPoints()
    {
        if (startPoint && endPoint != null && lineRenderer != null)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, startPoint.transform.position);
            lineRenderer.SetPosition(1, endPoint.transform.position);
        }
    }
}
