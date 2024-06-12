using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerPlane : MonoBehaviour
{
    public GameObject cubePrefab;
    private bool isColliding = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Mandible"))
        {
            Debug.Log("Collide");
            isColliding = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Mandible"))
        {
            isColliding = false;
            Debug.Log("Exit");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isColliding)
        {
            Instantiate(cubePrefab, transform.position, transform.rotation);
        }
    }
}
