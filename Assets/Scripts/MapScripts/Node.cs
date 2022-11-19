using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public enum NodeState
    {
        ACTIVE,
        INACTIVE,
        PASSED
    }
    public List<Line> incomingLines;
    public List<Line> outgoingLines;

    private bool isClickable = false;
    private NodeData nodeData;
    private SpriteRenderer sr;

    public delegate void NodeClicked(Node node);
    public static event NodeClicked OnNodeClicked;

    public delegate void NodeHovered(Node node, bool isHovering);
    public static event NodeHovered OnNodeHovered;

    public void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    public void AssignNodeData(NodeData data)
    {
        nodeData = data;
        UpdateSprite();
    }

    public void AssingRandomNodeData(NodePool dataList)
    {
        nodeData = dataList.nodePool[Random.Range(0, dataList.nodePool.Count)];
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        sr.sprite = nodeData.eventImage;
    }

    public NodeData GetNodeData()
    {
        return nodeData;
    }

    public void SetNodeState(NodeState nodeState)
    {
        if(nodeState == NodeState.ACTIVE)
            isClickable = true;
        else
            isClickable = false;

        ColorChange(nodeState);
    }

    public void OnMouseDown()
    {
        if(isClickable)
        {
            Debug.Log("Pressed Node");
            OnNodeClicked?.Invoke(this);
        }
        
    }

    public void OnMouseEnter()
    {
        if(isClickable)
            OnNodeHovered?.Invoke(this, true);
    }

    public void OnMouseExit()
    {
        if(isClickable)
            OnNodeHovered?.Invoke(this, false);
    }

    private void ColorChange(NodeState state)
    {
        switch (state)
        {
            case NodeState.ACTIVE:
                sr.color = new Color(1f, 203f / 255f, 158f / 255f);
                break;
            case NodeState.INACTIVE:
                sr.color = new Color(103f / 255f, 79f / 255f, 58f / 255f);
                break;
            case NodeState.PASSED:
                sr.color = new Color(190f / 255f, 148f / 255f, 55f / 255f);
                break;
            default:
                sr.color = new Color(103f / 255f, 79f / 255f, 58f / 255f);
                break;
        }
    }

    public void Propagate()
    {
        foreach (Line line in incomingLines)
        {
            line.Resolve();
        }
        foreach (Line line in outgoingLines)
        {
            line.FromNodeBecomesChosen();
            Node outNode = line.toNode;
            outNode.SetNodeState(NodeState.ACTIVE);
        }
        SetNodeState(NodeState.PASSED);
    }

    public void Deactivate()
    {
        SetNodeState(NodeState.INACTIVE);
        foreach (Line line in incomingLines)
        {
            line.Deactivate();
        }
    }
}
