using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Oculus.Interaction;

public class PoseController : MonoBehaviour
{
    public GameObject leftHandPointer;
    public GameObject rightHandPointer;

    private PlaneSlice_EzySlice planeSliceScript;
    private bool isPlaneSpawned = false;

    void Start()
    {
        if (leftHandPointer != null)
        {
            leftHandPointer.SetActive(false);
        }

        if (rightHandPointer != null)
        {
            rightHandPointer.SetActive(false);
        }
    }

    void Update()
    {
        if (!isPlaneSpawned)
        {
            planeSliceScript = FindObjectOfType<PlaneSlice_EzySlice>();

            if (planeSliceScript != null)
            {
                isPlaneSpawned = true;
                Debug.Log("PlaneSlice_EzySlice script found in the scene.");
            }
        }

        if (isPlaneSpawned)
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                Debug.Log("Space key pressed");
                if (planeSliceScript.firstPlane == null || planeSliceScript.secondPlane == null)
                {
                    Debug.LogWarning("Planes not assigned");
                    return;
                }

                Debug.Log("Calling Slice method");
                planeSliceScript.Slice(planeSliceScript.target);
            }

            if (Keyboard.current.zKey.wasPressedThisFrame)
            {
                Debug.Log("Z key pressed");
                planeSliceScript.RevertSlice();
            }
        }
    }

    public void SlicePose()
    {
        planeSliceScript.Slice(planeSliceScript.target);
    }

    public void UndoPose()
    {
        planeSliceScript.RevertSlice();
    }

    public void ToggleRightPointer()
    {
        if (rightHandPointer != null)
        {
            rightHandPointer.SetActive(!rightHandPointer.activeSelf);
        }
    }

    public void ToggleLeftPointer()
    {
        if (leftHandPointer != null)
        {
            leftHandPointer.SetActive(!leftHandPointer.activeSelf);
        }
    }
}
