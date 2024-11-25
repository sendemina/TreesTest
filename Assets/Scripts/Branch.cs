using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : MonoBehaviour
{
    TreeScript soil;
    GameObject leaves;
    float rate;
    LineRenderer lineRenderer;
    public Vector2 root, end;

    public Branch parent;
    public List<Branch> children;

    public Vector2 cRoot, cEnd;
    public float rootW, endW;

    public bool hasLeaves = false;

    public void SetUpRender()
    {
        lineRenderer = GetComponent<LineRenderer>(); 
        lineRenderer.startColor = Color.black; //pick a brown hue
        lineRenderer.endColor = Color.black;
        lineRenderer.useWorldSpace = true;

        Debug.Log(end + " " + root);
        lineRenderer.positionCount = 2;

        if (hasLeaves)
        { 
            leaves = Instantiate(soil.leaves);
            //leaves.transform.SetParent(soilTransform, true);
        }

            if (parent != null)
        { cRoot = parent.cEnd; }
        cEnd = cRoot;
    }

    private void RenderBranch()
    {
        lineRenderer.startWidth = rootW;
        lineRenderer.endWidth = endW;
        lineRenderer.SetPosition(0, cRoot);
        lineRenderer.SetPosition(1, cEnd);
    }

    private void Grow()
    {
        //implement timer to pace the increments
        if (Vector2.Distance(cRoot, root) >= 0.1)
        {
            cRoot = Vector2.MoveTowards(cRoot, root, Vector2.Distance(cRoot, root) * rate);
            rootW += rate;
        }
        if (Vector2.Distance(cEnd, end) >= 0.1)
        {
            cEnd = Vector2.MoveTowards(cEnd, end, Vector2.Distance(cEnd, end) * rate);
            endW += rate;
            foreach (Branch child in children)
            {
                child.cRoot = cEnd;
            }
        }
        //increment root and end positions towards the final
        //update the children's roots too(exponentially?)

        if (hasLeaves) { DrawLeaves(); }
    }

 
    void DrawLeaves()
    {
        leaves.transform.position = cEnd;
        leaves.transform.localScale *= 0.1f;
        //change hue
    }
    void Update()
    {
        rate = TreeManager.growRate;
        Grow();
        RenderBranch();
    }
}
