using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Oculus.Interaction;

public class SliceController : MonoBehaviour
{
    private PlaneSlice_EzySlice planeSliceScript;
    private bool isPlaneSpawned = false;
    private GameObject originalTargetState;
    private SliceState currentState = SliceState.Original;

    void Start()
    {
        originalTargetState = Instantiate(planeSliceScript.target);
    }

    void Update()
    {
        if (!isPlaneSpawned)
        {
            // Try to find the PlaneSlice_EzySlice component in the scene
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

            // Check for the key press to revert the slice
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
}
