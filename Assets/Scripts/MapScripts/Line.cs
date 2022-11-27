using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public Node fromNode;
    public Node toNode;
    private LineRenderer lr;
    private bool fromNodeWasChosen = false;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    public void SetNodes(Node from, Node to)
    {
        fromNode = from;
        toNode = to;
        transform.position = Vector3.zero;
        Vector3[] linePos = { from.transform.position, to.transform.position };
        lr.SetPositions(linePos);
        fromNode.outgoingLines.Add(this);
        toNode.incomingLines.Add(this);
    }

    public void Resolve()
    {
        if(fromNodeWasChosen)
        {
            Debug.Log("Resolving...");
            Color c = new Color(190f / 255f, 148f / 255f, 55f / 255f);
            lr.startColor = c;
            lr.endColor = c;
        }
            
    }

    public void Deactivate()
    {
        Color c = new Color(103f / 255f, 79f / 255f, 58f / 255f);
        lr.startColor = c;
        lr.endColor = c;
    }
    public void FromNodeBecomesChosen()
    {
        fromNodeWasChosen = true;
        Color c = new Color(255f / 255f, 203f / 255f, 158f / 255f);
        lr.startColor = c;
        lr.endColor = c;
    }
}
