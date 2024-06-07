using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ruler : MonoBehaviour
{
    public Transform startSphere;
    public Transform endSphere;
    public LineRenderer lineRenderer;
    public TextMeshProUGUI distanceText;

    void Start()
    {
        lineRenderer.positionCount = 2;
    }

    void Update()
    {
        lineRenderer.SetPosition(0, startSphere.position);
        lineRenderer.SetPosition(1, endSphere.position);

        float distance = Vector3.Distance(startSphere.position, endSphere.position) * 100;
        distanceText.text = distance.ToString("F2") + " cm";

        Vector3 midPoint = (startSphere.position + endSphere.position) / 2;
        distanceText.transform.position = midPoint;

        // Optionally, make the text face the camera
        // distanceText.transform.rotation = Quaternion.LookRotation(distanceText.transform.position - Camera.main.transform.position);
    }
}
