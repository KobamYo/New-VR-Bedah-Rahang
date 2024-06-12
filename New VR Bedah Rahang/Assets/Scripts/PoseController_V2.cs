using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PoseController_V2 : MonoBehaviour
{
    public GameObject leftHandPlane;
    public GameObject rightHandPlane;

    private PlaneSlice_EzySlice planeSliceScript;
    private SpawnPlane_V3 spawnPlaneScript;
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

        if (planeSliceScript.currentState == SliceState.Sliced)
        {
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
        if (planeSliceScript.currentState == SliceState.Sliced)
        {
            planeSliceScript.RevertSlice();
        }
        else if (isPlaneSpawned)
        {
            spawnPlaneScript.UndoSpawnPlane();
        }
    }

    public void ToggleRightPlane()
    {
        if (rightHandPlane != null)
        {
            rightHandPlane.SetActive(!rightHandPlane.activeSelf);
        }
    }

    public void ToggleLeftPlane()
    {
        if (leftHandPlane != null)
        {
            leftHandPlane.SetActive(!leftHandPlane.activeSelf);
        }
    }

    public void InstantiatePlane()
    {
        spawnPlaneScript.SpawnPlane();
    }
}
