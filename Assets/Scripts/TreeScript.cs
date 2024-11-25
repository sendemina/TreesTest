using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    [SerializeField] GameObject branch;
    [SerializeField] public GameObject leaves;
    public Transform soilTransform;
    LineRenderer lineRenderer;

    //variables based on seed
    float minAngle = Mathf.PI*3/4;
    float maxAngle = Mathf.PI*1/4;
    int maxSteps;
    //branchThickness

    void Start()
    {

        //For creating line renderer object
        lineRenderer = new GameObject("Line").AddComponent<LineRenderer>();

        maxSteps = Random.Range(3,6);
        soilTransform = GetComponent<Transform>();
        NewTree();

    }


    void GrowNewBranch(Vector2 root, int step, Branch parent)
    {
        if (step >= maxSteps)
        {
            //GrowLeaves(root);

        }
        else
        {
            float angle = Random.Range(minAngle, maxAngle);
            float length = 4/(step+1); //make a more organic formula for length

            
            int children = Random.Range(2, 5);
            for (int i = 0; i < children; i++)
            {
                Vector2 end = new Vector2(root.x+Mathf.Cos(angle) * length, root.y+Mathf.Sin(angle) * length);
                Branch newBranch = Instantiate(branch).GetComponent<Branch>();
                newBranch.gameObject.transform.SetParent(soilTransform, true);
                newBranch.root = root;
                newBranch.end = end;
                newBranch.parent = parent;
                newBranch.rootW = 0.6f/(step+1); 
                newBranch.endW = 0.3f/(step+1);
                if (parent != null)
                {
                    parent.children.Add(newBranch);
                }
                else 
                {
                    newBranch.cRoot = soilTransform.position;
                }

                newBranch.SetUpRender();
                if (step >= maxSteps) { newBranch.hasLeaves = true; }
                else { GrowNewBranch(end, step + 1, newBranch); }
            }
        }
    }


    public void NewTree()
    {
        foreach (Transform child in soilTransform)
        {
            Object.Destroy(child.gameObject);
        }
        int trunksN = Random.Range(1, 3);
        for (int i = 0; i < trunksN; i++)
        {
            GrowNewBranch(soilTransform.position, 0, null);
        }
    }

}
