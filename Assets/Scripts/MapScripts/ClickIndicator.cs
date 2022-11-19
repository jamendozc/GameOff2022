using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickIndicator : MonoBehaviour
{
    private SpriteRenderer sr;
    private void Awake()
    {
        Node.OnNodeHovered += OnNodeHovered;
        Node.OnNodeClicked += OnNodeClicked;

        sr = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnNodeClicked(Node node)
    {

    }

    void OnNodeHovered(Node node, bool isHovering)
    {
        if (isHovering)
        {
            transform.position = node.gameObject.transform.position;
            sr.enabled = true;
        }
        else
        {
            sr.enabled = false;
        }

    }
}
