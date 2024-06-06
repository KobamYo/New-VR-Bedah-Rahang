using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Oculus.Interaction;

public class PoseController_V2 : MonoBehaviour
{
    public GameObject leftHandPlane;
    public GameObject rightHandPlane;

    private PlaneSlice_EzySlice planeSliceScript;
    private bool isPlaneSpawned = false;

    void Start()
    {
        if (leftHandPlane != null)
        {
            leftHandPlane.SetActive(false);
        }

        if (rightHandPlane != null)
        {
            rightHandPlane.SetActive(false);
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

    public void UndoPose()
    {
        planeSliceScript.RevertSlice();
    }

    public void ToggleRightPlane()
    {
        if (rightHandPlane != null)
        {
            rightHandPlane.SetActive(!rightHandPlane.activeSelf);
            Debug.Log(rightHandPlane);
        }
    }

    public void ToggleLeftPlane()
    {
        if (leftHandPlane != null)
        {
            leftHandPlane.SetActive(!leftHandPlane.activeSelf);
        }
    }
}
