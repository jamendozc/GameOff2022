using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public int numberOfLayers;
    public List<List<Node>> layers;
    public List<List<Line>> paths;
    public GameObject nodePrefab;
    public GameObject linePrefab;

    public NodeData startNode;
    public NodeData finishNode;
    public NodePool nodePool;

    private readonly float firstLayerXpos = 8;
    private readonly float distanceBetweenLayers = 2;


    // Start is called before the first frame update
    void Awake()
    {
        layers = new List<List<Node>>();
        paths = new List<List<Line>>();

        InitAllLayers(layers, numberOfLayers);

        InitPathLayers();
    }

    void InitPathLayers()
    {
        for (int i = 0; i < layers.Count - 1; i++)
        {
            List<Node> layerFrom = layers[i];
            List<Node> layerTo = layers[i + 1];
            List<Line> lines = new List<Line>();

            GameObject gmPath = new GameObject("Paths " + i);
            gmPath.transform.SetParent(this.transform);

            //Case 1:n
            if (layerFrom.Count == 1)
            {
                foreach (var node in layerTo)
                {
                    lines.Add(ConnectNodes(layerFrom[0], node, gmPath));
                }
                continue;
            }
            //Case n:1
            if (layerTo.Count == 1)
            {
                foreach (var node in layerFrom)
                {
                    lines.Add(ConnectNodes(node, layerTo[0], gmPath));
                }
                continue;
            }

            //Case 2:4 / 4:2
            if (Mathf.Abs(layerTo.Count - layerFrom.Count) == 2)
            {
                if (layerTo.Count > layerFrom.Count)
                {
                    lines.Add(ConnectNodes(layerFrom[0], layerTo[1], gmPath));
                    lines.Add(ConnectNodes(layerFrom[1], layerTo[2], gmPath));
                }
                else
                {
                    lines.Add(ConnectNodes(layerFrom[1], layerTo[0], gmPath));
                    lines.Add(ConnectNodes(layerFrom[2], layerTo[1], gmPath));
                }
                lines.Add(ConnectNodes(layerFrom[0], layerTo[0], gmPath));
                lines.Add(ConnectNodes(layerFrom[layerFrom.Count - 1], layerTo[layerTo.Count - 1], gmPath));
                Line resultPath;
                if (layerTo.Count > layerFrom.Count)
                {
                    var tempList = new List<Node>
                    {
                        layerTo[1],
                        layerTo[2]
                    };
                    resultPath = SolveForSquare(layerFrom, tempList, gmPath);
                }
                else
                {
                    var tempList = new List<Node>
                    {
                        layerFrom[1],
                        layerFrom[2]
                    };
                    resultPath = SolveForSquare(tempList, layerTo, gmPath);
                }
                if (resultPath != null)
                {
                    lines.Add(resultPath);
                }
                continue;
            }
            //Case n:n
            if (Mathf.Abs(layerTo.Count - layerFrom.Count) == 0)
            {
                Line resultPath;
                var tempListFrom = new List<Node>();
                var tempListTo = new List<Node>();

                for (int j = 0; j < layerTo.Count - 1; j++)
                {
                    lines.Add(ConnectNodes(layerFrom[j], layerTo[j], gmPath));

                    tempListTo.Clear();
                    tempListFrom.Clear();

                    tempListFrom.Add(layerFrom[j]);
                    tempListFrom.Add(layerFrom[j + 1]);
                    tempListTo.Add(layerTo[j]);
                    tempListTo.Add(layerTo[j + 1]);

                    resultPath = SolveForSquare(tempListFrom, tempListTo, gmPath);
                    if (resultPath != null)
                    {
                        lines.Add(resultPath);
                    }
                }
                lines.Add(ConnectNodes(layerFrom[layerFrom.Count - 1], layerTo[layerTo.Count - 1], gmPath));
            }
            //Case n:n+1 / n+1:n
            if (Mathf.Abs(layerTo.Count - layerFrom.Count) == 1)
            {
                int lastValue = 0;
                int counterFrom = 0;
                int counterTo = 0;
                bool flip = true;
                Line resultPath;
                if (layerFrom.Count > layerTo.Count)
                {
                    flip = false;
                }
                for (int j = 0; j < layerTo.Count + layerFrom.Count - 2; j++)
                {
                    //Debug.Log(j);
                    if (flip)
                    {
                        resultPath = SolveForTriangle(layerFrom[counterTo], layerTo[counterFrom], gmPath, lastValue);
                    }
                    else
                    {
                        resultPath = SolveForTriangle(layerFrom[counterFrom], layerTo[counterTo], gmPath, lastValue);
                    }
                    int temp = counterTo;
                    counterTo = counterFrom;
                    if (temp == counterFrom)
                    {
                        counterFrom++;
                    }
                    if (resultPath != null)
                    {
                        lines.Add(resultPath);
                        lastValue = 1;
                    }
                    else
                    {
                        lastValue = 0;
                    }
                }
                lines.Add(ConnectNodes(layerFrom[layerFrom.Count - 1], layerTo[layerTo.Count - 1], gmPath));
            }
            paths.Add(lines);

        }
    }

    Line SolveForTriangle(Node from, Node to, GameObject gmPath, int lastValue)
    {
        int state = Random.Range(0, 2);
        if(lastValue == 0 || state == 0)
        {
            return ConnectNodes(from, to, gmPath);
        }
        return null;
    }

    Line SolveForSquare(List<Node> from, List<Node> to, GameObject gmPath)
    {
        int middleState = Random.Range(0, 3);
        if (middleState == 1)
        {
            return ConnectNodes(from[0], to[1], gmPath);
        }
        if (middleState == 2)
        {
            return ConnectNodes(from[1], to[0], gmPath);
        }
        return null;
    }

    Line ConnectNodes(Node from, Node to, GameObject gmPath)
    {
        GameObject lineGameObject = Instantiate(linePrefab, gmPath.transform, true);
        Line path = lineGameObject.GetComponent<Line>();
        path.SetNodes(from, to);
        return path;
    }


    void InitAllLayers(List<List<Node>> layers, int numberOfLayers)
    {
        List<Node> tempLayer = new List<Node>();
        InitLayer(tempLayer, firstLayerXpos, 1);
        tempLayer[0].isClickable = true;
        tempLayer[0].AssignNodeData(startNode);
        layers.Add(tempLayer);

        for (int i = 0; i < numberOfLayers; i++)
        {
            tempLayer = new List<Node>();
            int numberOfNodes = Random.Range(2, 5);
            InitLayer(tempLayer, 6 - 2 * i, numberOfNodes);
            layers.Add(tempLayer);
        }

        tempLayer = new List<Node>();
        InitLayer(tempLayer, 6 - 2 * numberOfLayers, 1);
        tempLayer[0].AssignNodeData(finishNode);
        layers.Add(tempLayer);
    }
    void InitLayer(List<Node> layer, float x, int numberOfNodes)
    {
        int y0 = -3;
        float yi = y0;
        int count = numberOfNodes;
        if(count == 4)
        {
            yi -= distanceBetweenLayers;
        }
        if(count == 3)
        {
            yi -= distanceBetweenLayers / 2;
        }
        if(count == 1)
        {
            yi += distanceBetweenLayers / 2;
        }
        GameObject gmLayer = new GameObject("Layer " + ((firstLayerXpos - x - distanceBetweenLayers) / distanceBetweenLayers + 1));
        gmLayer.transform.SetParent(this.transform);

        for (int i = 0; i < count; i++)
        {
            yi += distanceBetweenLayers;
            GameObject node = Instantiate(nodePrefab, gmLayer.transform, true);
            node.GetComponent<Node>().AssingRandomNodeData(nodePool);

            Vector3 pos = node.transform.position;
            pos.x = x;
            pos.y = yi;
            node.transform.position = pos;
            layer.Add(node.GetComponent<Node>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
