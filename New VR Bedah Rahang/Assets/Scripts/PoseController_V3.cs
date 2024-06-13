using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PoseController_V3 : MonoBehaviour
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
            spawnPlaneScript = FindObjectOfType<SpawnPlane_V3>();

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
