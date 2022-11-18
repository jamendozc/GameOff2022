using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public Node fromNode;
    public Node toNode;
    private LineRenderer lr;
    public bool fromNodeWasChosen = false;

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
            /*
            Gradient g = new Gradient();
            g.SetKeys(
                new GradientColorKey[] { new GradientColorKey(c, 0f), new GradientColorKey(c, 1f)},
                new GradientAlphaKey[] { new GradientAlphaKey(1f, 0f), new GradientAlphaKey(1f, 1f)});
            lr.colorGradient = g;*/
            lr.startColor = c;
            lr.endColor = c;
        }
            
    }
}
