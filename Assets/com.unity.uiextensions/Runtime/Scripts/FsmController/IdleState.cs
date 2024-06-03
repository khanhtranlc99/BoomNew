using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : SlimeStateBase
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
        StartCoroutine(UnFreeze());
    }

    public override void UpdateState()
    {
        if (data.Hp <= 0)
        {
            StopAllCoroutines();
            data.fSMController.ChangeState(StateType.Die);
        }
    }
    public IEnumerator UnFreeze()
    {
        yield return new WaitForSeconds(5);
        if(data.Hp > 0)
        {
            data.fSMController.moveState.GetComponent<MoveState>().HandleClear();      
            data.fSMController.ChangeState(StateType.Move);
        }
      
    }
     
}
