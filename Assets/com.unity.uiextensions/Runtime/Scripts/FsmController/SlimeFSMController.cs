using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeFSMController : MonoBehaviour
{
    public SlimeStateBase idleState;
    public SlimeStateBase moveState;
    public SlimeStateBase hideState;
    public SlimeStateBase dieState;
    public bool wasUse;
    public SlimeStateBase currentState;


    public void Init(SlimeBase slimeBase )
    {
        idleState.Init(slimeBase);
        moveState.Init(slimeBase);
        hideState.Init(slimeBase);
        dieState.Init(slimeBase);

        if (UseProfile.CurrentLevel != 1)
        {
            wasUse = true;
            ChangeState(StateType.Move);
        }
        else
        {
            slimeBase.animator.Play("Idle");
           
        }    

    }

    public void ChangeState(StateType newState )
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
    public void Stop()
    {

        if (currentState != null)
        {
            currentState.EndState();
        }
        //currentState = moveState;
        //currentState.StartState();
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