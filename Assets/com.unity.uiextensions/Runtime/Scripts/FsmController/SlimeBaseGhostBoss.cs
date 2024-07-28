using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBaseGhostBoss : SlimeBase
{
    public override void TakeDame()
    {

        if (!wasTakeDame && GamePlayController.Instance.stateGame == StateGame.Playing && initDone)
        {
            wasTakeDame = true;
            StartCoroutine(Helper.HandleActionPlayAndWait(animator, "Hit", delegate { animator.Play("Move"); }));

            Hp -= 1;

            heartBarSlime.HandleSupTrackHeart();
            if (GameController.Instance.useProfile.OnSound)
            {
                takeDame.PlayOneShot(takeDameSFX);
            }


            if (Hp > 0)
            {
                StartCoroutine(HandleCountDown());
                GamePlayController.Instance.HandleCheckLose();
            }
            else
            {
                collider2D.enabled = false;
                if (!fSMController.wasUse)
                {
                    fSMController.ChangeState(StateType.Die);
                }
            }

            





        }

    }
}
