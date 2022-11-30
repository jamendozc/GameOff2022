using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusManager : MonoBehaviour
{
    public const int COLD = 0, HUNGRY = 1, SICK = 2, TIRED = 3;

    private Image[] sprites;

    private void Awake()
    {
        sprites = GetComponentsInChildren<Image>();
    }

    public void SetFlags(bool[] flags)
    {
        if(sprites == null)
        {
            sprites = GetComponentsInChildren<Image>();
        }
        for (int i = 0; i < flags.Length; i++)
        {
            sprites[i].enabled = flags[i];
        }
    }

}
