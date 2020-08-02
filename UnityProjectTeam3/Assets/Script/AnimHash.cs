using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimHash 
{
    public static readonly int ATTACK = Animator.StringToHash("ATTACK");
    public static readonly int JUMP = Animator.StringToHash("JUMP");
    public static readonly int RUN = Animator.StringToHash("RUN");
    public static readonly int DEAD = Animator.StringToHash("DEAD");
    public static readonly int IDLE = Animator.StringToHash("IDLE");
    public static readonly int SIT = Animator.StringToHash("SIT");
}
