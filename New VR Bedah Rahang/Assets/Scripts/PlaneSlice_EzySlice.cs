using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class PlaneSlice_EzySlice : MonoBehaviour
{
    public Transform firstPlane;
    public Transform secondPlane;
    public GameObject target;
    public Material crossSectionMaterial;

    private GameObject skullParent; 
    private List<GameObject> slicedParts = new List<GameObject>();
    private SliceState currentState = SliceState.Original;

    void Start()
    {
        skullParent = GameObject.FindGameObjectWithTag("Skull");
    }

    public void Slice(GameObject target)
    {
        Debug.Log("Slicing started");

        target.transform.parent = null; // Detach target from parent

        // Perform the first slice
        // It creates upper hull and lower hull
        SlicedHull firstSlice = target.Slice(firstPlane.position, firstPlane.up);

        if (firstSlice != null)
        {
            GameObject upperHull = firstSlice.CreateUpperHull(target, crossSectionMaterial);
            GameObject lowerHull = firstSlice.CreateLowerHull(target, crossSectionMaterial);

            upperHull.transform.SetParent(skullParent.transform);

            // Perform the second slice
            // It slice the first lower hull. From the first lower hull, it creates another upper hull and lower hull
            // This second lower hull will be destroyed. So it left only first upper hull and second upper hull
            SlicedHull secondSlice = lowerHull.Slice(secondPlane.position, secondPlane.up);

            if (secondSlice != null)
            {
                GameObject secondUpperHull = secondSlice.CreateUpperHull(target, crossSectionMaterial);
                GameObject middleHull = secondSlice.CreateLowerHull(target, crossSectionMaterial);
                
                secondUpperHull.transform.SetParent(skullParent.transform);

                // Store the sliced parts in the list
                slicedParts.Add(upperHull);
                slicedParts.Add(secondUpperHull);

                Destroy(lowerHull);
                Destroy(middleHull);
            }

            target.SetActive(false); // Use this to set the target to be inactive
            // Destroy(target); // Use this to destroy the target
            // But apparently when I destroy the target, GameObject "Skull" can't be moved.
            currentState = SliceState.Sliced;
            Debug.Log("Slicing completed");
        }
    }

    public void RevertSlice()
    {
        Debug.Log("Reverting slice");
        if (currentState == SliceState.Sliced)
        {
            foreach (GameObject part in slicedParts)
            {
                Destroy(part);
            }
            slicedParts.Clear();

            target.SetActive(true);

            // Synchronize transform properties with the parent (skullParent)
            target.transform.position = skullParent.transform.position;
            target.transform.rotation = skullParent.transform.rotation;
            target.transform.localScale = skullParent.transform.localScale;
            target.transform.SetParent(skullParent.transform);

            currentState = SliceState.Original;
            Debug.Log("Revert completed");
        }
    }

    public void SetupSlicedComponent(GameObject slicedObject)
    {
        slicedObject.tag = "Mandible";
        Collider collider = slicedObject.AddComponent<MeshCollider>();
        Rigidbody rigidbody = slicedObject.AddComponent<Rigidbody>();
    }
}
