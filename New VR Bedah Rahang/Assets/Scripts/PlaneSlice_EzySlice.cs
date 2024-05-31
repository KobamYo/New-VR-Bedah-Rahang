using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using UnityEngine.InputSystem;

public class PlaneSlice_EzySlice : MonoBehaviour
{
    public Transform firstPlane;
    public Transform secondPlane;
    public GameObject target;
    public Material crossSectionMaterial;

    private GameObject skullParent;
    private Stack<GameObject[]> slicedPartsStack = new Stack<GameObject[]>();
    private GameObject originalTargetState;

    void Start()
    {
        originalTargetState = Instantiate(target);
        originalTargetState.SetActive(false);

        //skullParent = GameObject.FindGameObjectWithTag("Skull");

        //if (UndoManager.Instance != null)
        //{
        //    UndoManager.Instance.Undo(target);
        //}
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
            RevertSlice();
        }

    }

    public void Slice(GameObject target)
    {
        target.transform.parent = null; // Detach target from parent

        // Perform the first slice
        // It creates upper hull and lower hull
        SlicedHull firstSlice = target.Slice(firstPlane.position, firstPlane.up);

        if (firstSlice != null)
        {
            GameObject upperHull = firstSlice.CreateUpperHull(target, crossSectionMaterial);
            GameObject lowerHull = firstSlice.CreateLowerHull(target, crossSectionMaterial);

            skullParent = GameObject.FindGameObjectWithTag("Skull");
            upperHull.transform.SetParent(skullParent.transform);
            // slicedParts.Add(upperHull);

            // Perform the second slice
            // It slice the first lower hull. From the first lower hull, it creates another upper hull and lower hull
            // This second lower hull will be destroyed. So it left only first upper hull and second upper hull
            SlicedHull secondSlice = lowerHull.Slice(secondPlane.position, secondPlane.up);

            if (secondSlice != null)
            {
                GameObject secondUpperHull = secondSlice.CreateUpperHull(target, crossSectionMaterial);
                GameObject middleHull = secondSlice.CreateLowerHull(target, crossSectionMaterial);
                
                secondUpperHull.transform.SetParent(skullParent.transform);
                // slicedParts.Add(seconUpperHull);
                
                // Store the sliced parts in the stack
                slicedPartsStack.Push(new GameObject[] { upperHull, secondUpperHull });

                Destroy(lowerHull);
                Destroy(middleHull);
            }

            target.SetActive(false); // Use this to set the target to be inactive
            // Destroy(target); // Use this to destroy the target
            // But apparently when I destroy the target, GameObject "Skull" can't be moved.
            originalTargetState = target;
        }
    }

    public void RevertSlice()
    {
        // Check if there are sliced parts to revert
        if (slicedPartsStack.Count > 0)
        {
            // Destroy all the sliced parts
            foreach (GameObject[] slicedParts in slicedPartsStack)
            {
                foreach (GameObject part in slicedParts)
                {
                    Destroy(part);
                }
            }
            // Clear the stack
            slicedPartsStack.Clear();

            // Reactivate the original target and reset its state
            target.SetActive(true);
            originalTargetState.SetActive(false);
        }
    }

    //public void UndoSlice()
    //{   
    //    foreach (var part in slicedParts)
    //    {
    //        Destroy(part);
    //    }

    //    slicedParts.Clear();

    //    UndoManager.Instance.Undo(target);
    //    Debug.Log("Undo");
    //}

    public void SetupSlicedComponent(GameObject slicedObject)
    {
        slicedObject.tag = "Mandible";
        Collider collider = slicedObject.AddComponent<MeshCollider>();
        Rigidbody rigidbody = slicedObject.AddComponent<Rigidbody>();
    }
}
