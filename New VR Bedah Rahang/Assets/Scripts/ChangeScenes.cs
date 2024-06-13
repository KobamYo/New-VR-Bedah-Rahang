using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour
{
    public void Scene1()
    {
        SceneManager.LoadScene("Interaction 1 (Two Points)");
    }

    public void Scene2()
    {
        SceneManager.LoadScene("Interaction 2 (Single Point)");
    }

    public void Scene3()
    {
        SceneManager.LoadScene("Interaction 3 (Transparent Plane)");
    }

    public void Scene4()
    {
        SceneManager.LoadScene("Interaction 4 (Normal Slice)");
    }
}
