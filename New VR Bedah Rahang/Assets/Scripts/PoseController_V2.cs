using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PoseController_V2 : MonoBehaviour
{
    public GameObject leftHandPointer;
    public GameObject rightHandPointer;

    private PlaneSlice_EzySlice planeSliceScript;
    private SpawnPlane_V2 spawnPlaneScript;

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
            spawnPlaneScript = FindObjectOfType<SpawnPlane_V2>();

            if (planeSliceScript != null && spawnPlaneScript != null)
            {
                isPlaneSpawned = true;
            }
        }

        if (isPlaneSpawned)
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
        if (planeSliceScript.currentState == SliceState.Original)
        {
            planeSliceScript.Slice(planeSliceScript.target);
        }
    }

    public void UndoPlanePose()
    {
        if (spawnPlaneScript.planeObject2 != null)
        {
            spawnPlaneScript.UndoSpawnPlane();
        }
        else if (spawnPlaneScript.planeObject1 != null)
        {
            spawnPlaneScript.UndoSpawnPlane();
        }
    }

    public void UndoSlicePose()
    {
        if (planeSliceScript.currentState == SliceState.Sliced)
        {
            planeSliceScript.RevertSlice();
        }
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
