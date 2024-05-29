using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using UnityEngine.InputSystem;
using Oculus.Interaction;

public class PlaneSlice_EzySlice : MonoBehaviour
{
    public Transform firstPlane;
    public Transform secondPlane;
    public GameObject target;
    public Material crossSectionMaterial;

    private GameObject clone;
    private GameObject skullParent;
    private List<GameObject> slicedParts = new List<GameObject>();

    void Start()
    {
        // Clone the original target to restore later
        clone = Instantiate(target);
        clone.transform.SetParent(clone.transform);
        clone.SetActive(false); // Hide the clone
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (firstPlane == null || secondPlane == null)
            {
                return;
            }

            Slice(target);
        }

        if (Keyboard.current.zKey.wasPressedThisFrame)
        {
            UndoSlice();
        }

    }

    public void Slice(GameObject target)
    {
        // Store references to the parent GameObject and the sliced parts
        target.transform.parent = null;
        SlicedHull firstSlice = target.Slice(firstPlane.position, firstPlane.up);

        if (firstSlice != null)
        {
            GameObject upperHull = firstSlice.CreateUpperHull(target, crossSectionMaterial);
            GameObject lowerHull = firstSlice.CreateLowerHull(target, crossSectionMaterial);

            GameObject skullParent = GameObject.FindGameObjectWithTag("Skull");
            upperHull.transform.SetParent(skullParent.transform);
            slicedParts.Add(upperHull);
            slicedParts.Add(lowerHull);

            SlicedHull secondSlice = lowerHull.Slice(secondPlane.position, secondPlane.up);

            if (secondSlice != null)
            {
                GameObject finalLowerHull = secondSlice.CreateUpperHull(target, crossSectionMaterial);
                GameObject middleHull = secondSlice.CreateLowerHull(target, crossSectionMaterial);
                
                finalLowerHull.transform.SetParent(skullParent.transform);
                slicedParts.Add(finalLowerHull);
                slicedParts.Add(middleHull);

                Destroy(lowerHull);
                middleHull.SetActive(false);
            }

            target.SetActive(false);
        }
    }

    public void UndoSlice()
    {
        // Destroy all sliced parts
        foreach (var part in slicedParts)
        {
            Destroy(part);
        }

        slicedParts.Clear();

        // Reactivate the original target
        target.SetActive(true);

        // Set the target back as a child of the "Skull" GameObject
        GameObject skullParent = GameObject.FindGameObjectWithTag("Skull");
        target.transform.SetParent(skullParent.transform);

        // Optionally, you can reset the position and rotation if needed
        target.transform.position = clone.transform.position;
        target.transform.rotation = clone.transform.rotation;

        // Set the original clone back to inactive
        clone.SetActive(false);
    }

    public void SetupSlicedComponent(GameObject slicedObject)
    {
        slicedObject.tag = "Mandible";
        Collider collider = slicedObject.AddComponent<MeshCollider>();
        Rigidbody rigidbody = slicedObject.AddComponent<Rigidbody>();
    }
}
