using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<Line> incomingLines;
    public List<Line> outgoingLines;

    public bool isClickable = false;
    private NodeData nodeData;

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
        GetComponent<SpriteRenderer>().sprite = nodeData.eventImage;
    }

    public NodeData GetNodeData()
    {
        return nodeData;
    }

    public void OnMouseDown()
    {
        if(isClickable)
        {
            Debug.Log("Pressed Node");
        }
        
    }
}
