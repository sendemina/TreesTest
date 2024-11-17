using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : MonoBehaviour
{
    LineRenderer lineRenderer;
    public Vector2 root, end;

    private void Start()
    {
    }
    public void RenderBranch()
    {
        lineRenderer = GetComponent<LineRenderer>(); 
        lineRenderer.startColor = Color.black;
        lineRenderer.endColor = Color.black;
        lineRenderer.startWidth = 0.2f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.useWorldSpace = true;

        Debug.Log(end + " " + root);
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, root);
        lineRenderer.SetPosition(1, end);
    }

    void Update()
    {
        
    }
}
