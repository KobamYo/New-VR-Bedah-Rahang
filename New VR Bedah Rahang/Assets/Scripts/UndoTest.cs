using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndoTest : MonoBehaviour
{
    public GameObject originalCube;

    void Update()
    {
        // Check if the 'Z' key is pressed
        if (Input.GetKeyDown(KeyCode.Z))
        {
            CloneCube();
        }
    }
    
    void CloneCube()
    {
        // Ensure there is an original cube assigned
        if (originalCube != null)
        {
            // Find the child sphere
            Transform childSphere = originalCube.transform.Find("Sphere");

            // Create a new GameObject
            GameObject newCube = new GameObject("ClonedCube");

            // Get the original mesh
            MeshFilter originalMeshFilter = originalCube.GetComponent<MeshFilter>();
            Mesh originalMesh = originalMeshFilter.sharedMesh;

            // Clone the mesh
            Mesh clonedMesh = Instantiate(originalMesh);

            // Assign the cloned mesh to the new GameObject
            MeshFilter newMeshFilter = newCube.AddComponent<MeshFilter>();
            newMeshFilter.mesh = clonedMesh;

            MeshRenderer newMeshRenderer = newCube.AddComponent<MeshRenderer>();
            newMeshRenderer.material = originalMeshFilter.GetComponent<MeshRenderer>().material;

            // Optionally, set position, rotation, and scale of the new cube
            newCube.transform.position = originalCube.transform.position + new Vector3(2, 0, 0); // Offset to the right of the original
            newCube.transform.rotation = originalCube.transform.rotation;
            newCube.transform.localScale = originalCube.transform.localScale;
        }
        else
        {
            Debug.LogWarning("Original cube is not assigned.");
        }
    }
}
