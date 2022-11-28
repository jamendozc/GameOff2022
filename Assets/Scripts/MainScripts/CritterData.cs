using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CritterData", menuName = "Data/Main/CritterData", order = 1)]
public class CritterData : ScriptableObject
{
    public Sprite critterSprite;
    public string critterName;
    public float critterAnimationSpeed;
    public float critterMovementSpeed;
}
