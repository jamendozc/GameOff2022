using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritterManager : MonoBehaviour
{
    public enum CritterState
    {
        IDLE,
        BUSY,
        WANDERING,
    }

    public enum CritterActivity
    {
        NONE,
        GATHER_FOOD,
        GATHER_FLOWER,
        GATHER_WOOD,
        GATHER_ROCKS,
        COOK_FOOD,
        MAKE_MEDICINE,
        REST
    }

    public CritterData critterData;
    public CritterAnim critterAnim;
    private CritterState currentState = CritterState.IDLE;

    private GameObject originalPosition;
    public GameObject farAwayPosition;
    // Start is called before the first frame update
    private void Awake()
    {
        farAwayPosition = FindObjectOfType<FarAwayPosition>().gameObject;
        critterAnim.SetSprite(critterData.critterSprite);
        critterAnim.SetWalkSpeed(critterData.critterAnimationSpeed);
        originalPosition = new GameObject("Critter Original Position");
        originalPosition.transform.position = this.transform.position;
    }

    private void OnValidate()
    {
        critterAnim.SetSprite(critterData.critterSprite);
        critterAnim.SetWalkSpeed(critterData.critterAnimationSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (critterAnim.IsFlipping())
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(currentState == CritterState.IDLE)
            {
                SetCritterState(CritterState.BUSY);
            } else
            {
                SetCritterState(CritterState.IDLE);
            }

        }
        switch (currentState)
        {
            case CritterState.IDLE:
                if(transform.position.x > originalPosition.transform.position.x)
                {
                    if(!critterAnim.facingLeft)
                    {
                        critterAnim.Flip();
                        break;
                    }
                    transform.Translate(Vector2.left * critterData.critterMovementSpeed * 0.5f * Time.deltaTime);
                }
                break;
            case CritterState.BUSY:
                if (transform.position.x < farAwayPosition.transform.position.x)
                {
                    if (critterAnim.facingLeft)
                    {
                        critterAnim.Flip();
                        break;
                    }
                    transform.Translate(Vector2.right * critterData.critterMovementSpeed * Time.deltaTime);
                }
                break;
            case CritterState.WANDERING:
                break;
            default:
                break;
        }
    }

    private void SetCritterState(CritterState state)
    {
        currentState = state;
    }
}
