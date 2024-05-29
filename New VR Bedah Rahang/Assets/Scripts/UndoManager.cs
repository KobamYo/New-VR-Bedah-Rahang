using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndoManager : MonoBehaviour
{
    private GameObject clone;
    private GameObject skullParent;

    [SerializeField]
    private GameObject target;

    private static UndoManager instance;

    // Property to get the instance
    public static UndoManager Instance
    {
        get
        {
            if (instance == null)
            {
                // Check if an instance already exists in the scene
                instance = FindObjectOfType<UndoManager>();

                // If it doesn't, create a new one
                if (instance != null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<UndoManager>();
                    singletonObject.name = typeof(UndoManager).Name;

                    // Make the instance persistent
                    DontDestroyOnLoad(singletonObject);
                }
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }


    void Start()
    {
        // Clone the original target to restore later
        clone = Instantiate(target, target.transform.parent);
        skullParent = GameObject.FindGameObjectWithTag("Skull");
        clone.transform.SetParent(skullParent.transform);
        clone.SetActive(false); // Hide the clone
    }

    public void Undo()
    {
        if (clone != null)
        {
            skullParent = GameObject.FindGameObjectWithTag("Skull");
            target.SetActive(false);
            clone.SetActive(true);
            clone.transform.SetParent(skullParent.transform);
            Debug.Log("Undo performed, clone activated");
        }
        else
        {
            Debug.LogWarning("Clone is null, cannot undo");
        }
    }
}
