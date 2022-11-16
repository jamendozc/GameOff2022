using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NodeData", menuName = "Data/Map/NodePool", order = 2)]
public class NodePool : ScriptableObject
{
    public List<NodeData> nodePool;
}
