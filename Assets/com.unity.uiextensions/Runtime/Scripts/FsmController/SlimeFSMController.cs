using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeFSMController : MonoBehaviour
{
    public SlimeStateBase idleState;
    public SlimeStateBase moveState;
    public SlimeStateBase hideState;
    public SlimeStateBase dieState;
    private bool wasUse;

    public SlimeStateBase currentState;


    public void Init(SlimeBase slimeBase )
    {
        idleState.Init(slimeBase);
        moveState.Init(slimeBase);
        hideState.Init(slimeBase);
        dieState.Init(slimeBase);
        wasUse = true;
        ChangeState(StateType.Move);
    }

    public void ChangeState(StateType newState, bool isInit = false)
    {
        if (currentState != null)
        {
            currentState.EndState();
        }

        switch (newState)
        {
            case StateType.Idle:
                idleState.EndState();
                currentState = idleState;

                break;
            case StateType.Move:
                moveState.EndState();
                currentState = moveState;
                 
                break;
            case StateType.Hide:
                hideState.EndState();
                currentState = hideState;
               
                break;
            case StateType.Die:
                dieState.EndState();
                currentState = dieState;

                break;
        }

        currentState.StartState();
    }


    private void Update()
    {
        if (wasUse)
        {
            switch (currentState.state)
            {
                case StateType.Idle:
                    idleState.UpdateState();
                    break;
                case StateType.Move:
                    moveState.UpdateState();
                    break;
                case StateType.Hide:
                    hideState.UpdateState();
                    break;
                case StateType.Die:
                    dieState.UpdateState();
                    break;
            }
        }
    }




}
public enum StateType
{
    None = 0,
    Idle = 1,
    Move = 2,
    Hide = 3,
    Die = 4,
}