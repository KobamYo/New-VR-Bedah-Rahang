using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using UnityEngine.InputSystem;

public class PlaneSlice : MonoBehaviour
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
}
