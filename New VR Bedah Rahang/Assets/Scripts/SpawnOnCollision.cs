using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnCollision : MonoBehaviour
{
    public GameObject spherePrefab;
    private Vector3 contactPoint;
    private bool hasCollided = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasCollided)
        {
            ContactPoint firstContact = collision.contacts[0];
            contactPoint = firstContact.point;
            hasCollided = true;
            Debug.Log("Collision Entered");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (hasCollided)
        {
            Instantiate(spherePrefab, contactPoint, Quaternion.identity);
            Debug.Log("Sphere Spawned on Exit");

            hasCollided = false; // Reset the flag
        }
    }
}
