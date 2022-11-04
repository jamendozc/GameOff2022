using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public int numberOfLayers;
    public List<List<GameObject>> layers;
    public List<List<GameObject>> paths;
    public GameObject nodePrefab;
    public GameObject linePrefab;

    // Start is called before the first frame update
    void Start()
    {
        layers = new List<List<GameObject>>();
        paths = new List<List<GameObject>>();

        List<GameObject> tempLayer = new List<GameObject>();
        InitLayer(tempLayer, 8, 1);
        layers.Add(tempLayer);

        for (int i = 0; i < numberOfLayers; i++)
        {
            tempLayer = new List<GameObject>();
            int numberOfNodes = Random.Range(2, 5);
            InitLayer(tempLayer, 6 - 2 * i, numberOfNodes);
            layers.Add(tempLayer);
        }

        tempLayer = new List<GameObject>();
        InitLayer(tempLayer, 6 - 2 * numberOfLayers, 1);
        layers.Add(tempLayer);

        for (int i = 0; i < layers.Count - 1; i++)
        {
            Debug.Log("i" + i);
            List<GameObject> layerFrom = layers[i];
            List<GameObject> layerTo = layers[i + 1];
            List<GameObject> lines = new List<GameObject>();

            GameObject gmPath = new GameObject("Paths "+ i);
            gmPath.transform.SetParent(this.transform);

            if(layerFrom.Count == 1)
            {
                foreach (var node in layerTo)
                {
                    lines.Add(ConnectNodes(layerFrom[0], node, gmPath));
                }
                continue;
            }
            if(layerTo.Count == 1)
            {
                foreach (var node in layerFrom)
                {
                    lines.Add(ConnectNodes(node, layerTo[0], gmPath));
                }
                continue;
            }

            if(Mathf.Abs(layerTo.Count - layerFrom.Count) == 2)
            {
                if(layerTo.Count > layerFrom.Count)
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
                lines.Add(ConnectNodes(layerFrom[layerFrom.Count -1], layerTo[layerTo.Count -1], gmPath));
                GameObject resultPath;
                if (layerTo.Count > layerFrom.Count)
                {
                    var tempList = new List<GameObject>
                    {
                        layerTo[1],
                        layerTo[2]
                    };
                    resultPath = SolveForSquare(layerFrom, tempList, gmPath);
                }
                else
                {
                    var tempList = new List<GameObject>
                    {
                        layerFrom[1],
                        layerFrom[2]
                    };
                    resultPath = SolveForSquare(tempList, layerTo, gmPath);
                }
                if(resultPath != null)
                {
                    lines.Add(resultPath);
                }
                continue;
            }
            if (Mathf.Abs(layerTo.Count - layerFrom.Count) == 0)
            {
                GameObject resultPath;
                var tempListFrom = new List<GameObject>();
                var tempListTo = new List<GameObject>();
                for (int j = 0; j < layerTo.Count - 1; j++)
                {
                    lines.Add(ConnectNodes(layerFrom[j], layerTo[j], gmPath));
                    tempListTo.Clear();
                    tempListFrom.Clear();
                    tempListFrom.Add(layerFrom[j]);
                    tempListFrom.Add(layerFrom[j+1]);
                    tempListTo.Add(layerTo[j]);
                    tempListTo.Add(layerTo[j + 1]);
                    resultPath = SolveForSquare(tempListFrom, tempListTo, gmPath);
                    if (resultPath != null)
                    {
                        lines.Add(resultPath);
                    }
                }
                lines.Add(ConnectNodes(layerFrom[layerFrom.Count -1], layerTo[layerTo.Count -1], gmPath));
            }
            if (Mathf.Abs(layerTo.Count - layerFrom.Count) == 1)
            {
                int lastValue = 0;
                int counterFrom = 0;
                int counterTo = 0;
                bool flip = true;
                GameObject resultPath;
                if (layerFrom.Count > layerTo.Count)
                {
                    flip = false;
                }
                for (int j = 0; j < layerTo.Count + layerFrom.Count - 2; j++)
                {
                    Debug.Log(j);
                    if(flip)
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
                        Debug.Log("yes");
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

    GameObject SolveForTriangle(GameObject from, GameObject to, GameObject gmPath, int lastValue)
    {
        int state = Random.Range(0, 2);
        if(lastValue == 0 || state == 0)
        {
            return ConnectNodes(from, to, gmPath);
        }
        return null;
    }

    GameObject SolveForSquare(List<GameObject> from, List<GameObject> to, GameObject gmPath)
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

    GameObject ConnectNodes(GameObject from, GameObject to, GameObject gmPath)
    {
        var path = Instantiate(linePrefab, gmPath.transform, true);
        path.transform.position = Vector3.zero;
        Vector3[] linePos = { from.transform.position, to.transform.position };
        path.GetComponent<LineRenderer>().SetPositions(linePos);
        return path;
    }

    void InitLayer(List<GameObject> layer, int x, int numberOfNodes)
    {
        int y0 = -3;
        float yi = y0;
        int count = numberOfNodes;
        float distance = 2;
        if(count == 4)
        {
            yi -= distance;
        }
        if(count == 3)
        {
            yi -= distance / 2;
        }
        if(count == 1)
        {
            yi += distance / 2;
        }
        GameObject gmLayer = new GameObject("Layer " + ((6-x)/2 + 1));
        gmLayer.transform.SetParent(this.transform);

        for (int i = 0; i < count; i++)
        {
            yi += distance;
            var node = Instantiate(nodePrefab, gmLayer.transform, true);
            Vector3 pos = node.transform.position;
            pos.x = x;
            pos.y = yi;
            node.transform.position = pos;
            layer.Add(node);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
