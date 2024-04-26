using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerCollisionDebug : MonoBehaviour
{
    private void OnColliderEnter(Collider other)
    {
        Debug.Log("Collider Enter");
    }

    private void OnColliderStay(Collider other)
    {
        Debug.Log("Collider Stay");
    }

    private void OnColliderExit(Collider other)
    {
        Debug.Log("Collider Exit");
    }
}
