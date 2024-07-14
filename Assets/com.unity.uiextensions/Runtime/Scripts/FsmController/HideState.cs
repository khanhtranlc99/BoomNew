using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideState : SlimeStateBase
{
    public override void EndState()
    {
        data.HandleHide(true);
    }

    public override void Init(SlimeBase slimeBase)
    {
        data = slimeBase;
    }

    public override void StartState()
    {
      
        data.HandleHide(false);
        if (data.gridBase.barrierBase  != null)
        {
            data.gridBase.barrierBase.gameObject.GetComponent<Bloomsom>().slimeBase.Add(data);
            data.gridBase.barrierBase.gameObject.GetComponent<Bloomsom>().HandleSlimeHide();
        }

 
    }

    public override void UpdateState()
    {
        if (data.gridBase.barrierBase == null)
        {
           
            data.fSMController.ChangeState(StateType.Move);
        }
        if (data.Hp <= 0)
        {
            data.fSMController.ChangeState(StateType.Die);
        }
    }
}
