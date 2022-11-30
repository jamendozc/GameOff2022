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

    public GameObject portraitPrefab;
    private GameObject go_portrait;
    private Portrait portrait;
    private CritterState currentState = CritterState.IDLE;

    private PortraitHolder portraitHolder;
    private GameObject originalPosition;
    public GameObject farAwayPosition;

    public float[] timers_getStatus = { 30f, 6f, 25f, 15f };
    public float[] resets_getStatus = { 30f, 6f, 25f, 15f };
    public float[] timers_getDamage = { 3f, 6f, 2f, 3f };
    public float[] resets_getDamage = { 3f, 6f, 2f, 3f };
    public float[] variance_getStatus = { 3f, 1f, 3f, 2f };
    public float[] variance_getDamage = { 1f, 1f, 1f, 1f };

    private float happiness = 10;

    private bool[] statusFlags = { false, false, false, false};

    // Start is called before the first frame update
    private void Awake()
    {
        farAwayPosition = FindObjectOfType<FarAwayPosition>().gameObject;
        portraitHolder = FindObjectOfType<PortraitHolder>();

        critterAnim.SetSprite(critterData.critterSprite);
        critterAnim.SetWalkSpeed(critterData.critterAnimationSpeed);

        originalPosition = new GameObject("Critter Original Position");
        originalPosition.transform.position = this.transform.position;

        go_portrait = Instantiate(portraitPrefab, portraitHolder.transform);
        portrait = go_portrait.GetComponent<Portrait>();
        portrait.SetCritterFace(critterData.critterSprite);

        for (int i = 0; i < timers_getStatus.Length; i++)
        {
            timers_getStatus[i] += Random.Range(-1* variance_getStatus[i], variance_getStatus[i]);
            timers_getDamage[i] += Random.Range(-1 * variance_getDamage[i], variance_getDamage[i]);
        }
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

    private void FixedUpdate()
    {
        float dt = Time.fixedDeltaTime;
        for (int i = 0; i < statusFlags.Length; i++)
        {
            if (statusFlags[i])
            {
                timers_getDamage[i] -= dt;
                if (timers_getDamage[i] < 0)
                {
                    timers_getDamage[i] += resets_getDamage[i] + Random.Range(-1 * variance_getDamage[i], variance_getDamage[i]);
                    AddHappiness(-1);
                }
            }
            else
            {
                timers_getStatus[i] -= dt;
                if (timers_getStatus[i] < 0)
                {
                    timers_getStatus[i] += resets_getStatus[i] + Random.Range(-1 * variance_getStatus[i], variance_getStatus[i]);
                    AddHappiness(-2);
                    statusFlags[i] = true;
                    portrait.SetFlags(statusFlags);
                }
            }
        }

    }

    private void AddHappiness(float dh)
    {
        happiness += dh;
        if (happiness < 0) happiness = 0;
        if (happiness > 10) happiness = 10;
        portrait.RenderHappiness(happiness);
    }
    private void SetCritterState(CritterState state)
    {
        currentState = state;
    }
}
