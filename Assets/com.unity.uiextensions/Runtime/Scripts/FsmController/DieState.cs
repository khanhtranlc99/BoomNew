using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : SlimeStateBase
{
    public override void EndState()
    {

    }

    public override void Init(SlimeBase slimeBase)
    {
        data = slimeBase;
    }

    public override void StartState()
    {
        data.animator.Play("Die");
    }

    public override void UpdateState()
    {

    }

    public void HandleActionDie()
    {
        data.gridBase.barrierBase = null;
        Destroy(data.gameObject);
    }


}
