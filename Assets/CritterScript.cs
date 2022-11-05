using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritterScript : MonoBehaviour
{
    public GameObject Void;
    public GameObject Back;
    public float speed = 10f;
    public float currentMove;
    public bool isBusy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isBusy && transform.position.x < Void.transform.position.x)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else if (!isBusy && transform.position.x > Back.transform.position.x)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        
    }
}
