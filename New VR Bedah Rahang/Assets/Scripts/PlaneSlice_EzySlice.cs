using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using UnityEngine.InputSystem;
using Oculus.Interaction;

public class PlaneSlice_EzySlice : MonoBehaviour
{
    public Transform plane;
    public GameObject target;
    public Material crossSectionMaterial;

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Slice(target);
        }
    }

    public void Slice(GameObject target)
    {
        SlicedHull hull = target.Slice(plane.position, plane.up);

        if (hull != null)
        {
            GameObject upperHull = hull.CreateUpperHull(target, crossSectionMaterial);
            GameObject lowerHull = hull.CreateLowerHull(target, crossSectionMaterial);

            Destroy(target);
        }
    }

    public void SetupSlicedComponent(GameObject slicedObject)
    {
        Rigidbody rigidbody = slicedObject.AddComponent<Rigidbody>();
        OVRGrabbable grabbable = slicedObject.AddComponent<OVRGrabbable>();
    }
}
