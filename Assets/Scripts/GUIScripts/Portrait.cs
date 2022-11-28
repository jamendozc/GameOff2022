using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Portrait : MonoBehaviour
{
    private Sprite critterFace;
    private float currentHappiness;
    public bool[] flags = { false, false, false, false };
    
    private StatusManager statusConditionsManager;
    [SerializeField] private RectMask2D happinessMask;
    [SerializeField] private Image happinessMeter;
    [SerializeField] private Image outline;
    [SerializeField] private Image headshot;
    private void Awake()
    {
        statusConditionsManager = GetComponentInChildren<StatusManager>();
        happinessMask = GetComponentInChildren<RectMask2D>();
        statusConditionsManager.SetFlags(flags);
    }

    private void SetFlags(bool[] flags)
    {
        this.flags = flags;
        statusConditionsManager.SetFlags(flags);
    }

    private void RenderHappiness(float happiness)
    {
        happinessMask.padding = new Vector4(0,0,0, 100 - happiness * 10);
    }
}
