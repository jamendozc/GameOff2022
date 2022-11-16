using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NodeData", menuName = "Data/Map/NodeData", order = 1)]
public class NodeData : ScriptableObject
{
    public enum MapEventType
    {
        Friend,
        Shop,
        Resources,
        Mistery,
        Decision,
        Start,
        Finish
    }

    public Sprite eventImage;
    public MapEventType mapEventType;
    public string eventName;

}
