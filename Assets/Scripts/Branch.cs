using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : MonoBehaviour
{
    TreeScript soil;
    GameObject leaves;
    Transform soilTransform;
    float rate;
    LineRenderer lineRenderer;
    public Vector2 root, end;

    public Branch parent;
    public List<Branch> children;

    public Vector2 cRoot, cEnd;
    public float rootW, endW;

    public bool hasLeaves = false;
    public bool growing = false;

    public float angle;

    public void SetUpRender(Transform soilT)
    {
        soilTransform = soilT;
        lineRenderer = GetComponent<LineRenderer>(); 
        lineRenderer.startColor = Color.black; //pick a brown hue
        lineRenderer.endColor = Color.black;
        lineRenderer.useWorldSpace = true;

        //Debug.Log(end + " " + root);
        lineRenderer.positionCount = 2;
        soil = GetComponentInParent<TreeScript>();

        if (hasLeaves)
        { 
            leaves = Instantiate(soil.leaves);
            leaves.transform.SetParent(soilTransform, true);
        }

        if (parent != null)
        {
            Debug.Log(parent ? "parent" : "no parent");
            cRoot = parent.cEnd;
        }
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
            rootW += rate/100;
        }
        if (Vector2.Distance(cEnd, end) >= 0.1)
        {
            cEnd = Vector2.MoveTowards(cEnd, end, Vector2.Distance(cEnd, end) * rate*2);
            endW += rate/100;
            foreach (Branch child in children)
            {
                child.cRoot = cEnd;
                child.cEnd = cEnd;
            }
        }
        else { growing = false; }
        //increment root and end positions towards the final
        //update the children's roots too(exponentially?)

        if (Vector2.Distance(cEnd, end) <= 10)
        {
            foreach (Branch child in children)
            {
                child.growing = true;
            }
        }
    }

 
    void DrawLeaves()
    {
        leaves.transform.position = cEnd;
        if (growing) { leaves.transform.localScale *= 1.002f; }
        //change hue
    }

    void Update()
    {
        rate = TreeManager.growRate;
        if (growing) { Grow(); }
        if (hasLeaves) { DrawLeaves(); }
        RenderBranch();
    }
}
