using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SlimeStateBase : MonoBehaviour
{
    public StateType state;
    [HideInInspector] public SlimeBase data;
    public abstract void Init(SlimeBase slimeBase );

    public abstract void StartState();

    public abstract void UpdateState();

    public abstract void EndState();
}
