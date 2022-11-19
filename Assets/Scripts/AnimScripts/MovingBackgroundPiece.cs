using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingBackgroundPiece : MonoBehaviour
{
    public float velocity;
    private float width;
    private RectTransform rt;

    private void Awake()
    {
        rt = GetComponent<RectTransform>();
        width = rt.rect.width;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rt.anchoredPosition += new Vector2(velocity * Time.fixedDeltaTime, 0);
        if(rt.anchoredPosition.x > 0)
        {
            rt.anchoredPosition -= new Vector2(width/2, 0);
        }
    }
}
