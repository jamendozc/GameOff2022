using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritterAnim : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer sr;
    public bool facingLeft = true;
    private float walkSpeed = 1f;
    private Sprite sprite;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("walkSpeed", walkSpeed);
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSprite(Sprite sprite)
    {
        this.sprite = sprite;
    }
    public void SetWalkSpeed(float speed)
    {
        walkSpeed = speed;
    }

    public bool IsFlipping()
    {
        return animator.GetBool("doFlip");
    }

    public void Flip()
    {
        animator.SetBool("doFlip", true);
    }

    private void endFlip()
    {
        animator.SetBool("doFlip", false);
        facingLeft = !facingLeft;
    }
}
