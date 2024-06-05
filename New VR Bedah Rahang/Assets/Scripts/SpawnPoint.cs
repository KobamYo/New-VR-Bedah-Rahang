using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject pointPrefab;
    private Vector3 contactPoint;
    private GameObject storedPoint;
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
            storedPoint = Instantiate(pointPrefab, contactPoint, Quaternion.identity);
            storedPoint.transform.SetParent(collision.gameObject.transform);
            hasCollided = false;
        }
    }
}
