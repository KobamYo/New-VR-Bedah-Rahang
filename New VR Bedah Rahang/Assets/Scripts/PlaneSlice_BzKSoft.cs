using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
// using BzKovSoft.ObjectSlicer;

public class PlaneSlice_BzKSoft : MonoBehaviour
{
    public GameObject target;

    private GameObject plane;

    void Start()
    {
        plane = gameObject;
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            // Slice();
        }
    }

    //async void Slice()
    //{
    //    var sliceable = target.GetComponent<IBzMeshSlicer>();

    //    if (sliceable != null)
    //    {
    //        Vector3 planeNormal = plane.transform.up;
    //        Vector3 planePosition = plane.transform.position;
            
    //        Plane slicingPlane = new Plane(planeNormal, planePosition);

    //        // Perform the slice operation using the manually positioned plane prefab
    //        await sliceable.SliceAsync(slicingPlane); 
    //    }
    //}
}
