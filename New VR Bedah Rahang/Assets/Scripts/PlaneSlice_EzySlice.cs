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

    private GameObject skullParent;
    private List<GameObject> slicedParts = new List<GameObject>();

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
        target.transform.parent = null; // Detach target from parent
        SlicedHull firstSlice = target.Slice(firstPlane.position, firstPlane.up);

        if (firstSlice != null)
        {
            GameObject upperHull = firstSlice.CreateUpperHull(target, crossSectionMaterial);
            GameObject lowerHull = firstSlice.CreateLowerHull(target, crossSectionMaterial);

            skullParent = GameObject.FindGameObjectWithTag("Skull");
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
        foreach (var part in slicedParts)
        {
            Destroy(part);
        }

        slicedParts.Clear();

        UndoManager.Instance.Undo();
        Debug.Log("Undo");
    }

    public void SetupSlicedComponent(GameObject slicedObject)
    {
        slicedObject.tag = "Mandible";
        Collider collider = slicedObject.AddComponent<MeshCollider>();
        Rigidbody rigidbody = slicedObject.AddComponent<Rigidbody>();
    }
}
