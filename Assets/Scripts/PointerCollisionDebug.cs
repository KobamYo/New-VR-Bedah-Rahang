using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerCollisionDebug : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.CompareTag("Mandible");
        Debug.Log("Collider Enter");
    }

    private void OnCollisionStay(Collision collision)
    {
        collision.gameObject.CompareTag("Mandible");
        Debug.Log("Collider Stay");
    }

    private void OnCollisionExit(Collision collision)
    {
        collision.gameObject.CompareTag("Mandible");
        Debug.Log("Collider Exit");
    }
}
