using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public Node fromNode;
    public Node toNode;
    private LineRenderer lr;

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
}
