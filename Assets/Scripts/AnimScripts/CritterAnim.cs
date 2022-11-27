using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritterAnim : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("doFlip", true);

        }
    }

    private void endFlip()
    {
        animator.SetBool("doFlip", false);
    }
}
