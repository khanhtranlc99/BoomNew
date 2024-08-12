using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SlimeBaseBlueBoss : SlimeBase
{
    public SlimeBase redSlime;
    public ParticleSystem vfxSpawn;
    public override void Init()
    {
        base.Init();
        SimplePool2.PoolPreLoad(redSlime.gameObject, 4, GamePlayController.Instance.playerContain.levelData.transform);
    }
    public override void TakeDame()
    {
        if (!wasTakeDame && GamePlayController.Instance.stateGame == StateGame.Playing)
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
                SpawnRed();
            }
            else
            {
                collider2D.enabled = false;
                if (!fSMController.wasUse)
                {
                    fSMController.ChangeState(StateType.Die);
                }
            }

            if (fSMController.currentState != null)
            {
                if (fSMController.currentState.state != StateType.Hide)
                {


                    spriteRenderer.DOFade(0.5f, 0.5f).OnComplete(delegate
                    {
                        spriteRenderer.DOFade(1, 0.5f).OnComplete(delegate
                        {
                            spriteRenderer.DOFade(0.5f, 0.3f).OnComplete(delegate
                            {
                                spriteRenderer.DOFade(1, 0.3f).OnComplete(delegate
                                {

                                    //GamePlayController.Instance.HandleCheckLose();
                                });
                            });
                        });
                    });
                }

            }





        }
    }

    private void SpawnRed()
    {
        vfxSpawn.Play();
        var temp = new List<GridBase>();
        foreach (var item in  GamePlayController.Instance.playerContain.levelData.gridBasesId)
        {
            if (item.barrierBase == null)
            {
                temp.Add(item);
            }
        }
        temp.Shuffle();
        if (temp.Count > 0 )
        {
            


            var red_2 = Instantiate(redSlime, GamePlayController.Instance.playerContain.levelData.transform);
            red_2.gridBase = temp[0];
            red_2.transform.position = this.transform.position;
            red_2.transform.DOJump(temp[0].transform.position, 0.2f, 1, 0.3f).OnComplete(delegate { red_2.Init(); });
          
            GamePlayController.Instance.playerContain.levelData.lsSmiles.Add(red_2);
            return;
        }

        if (temp.Count == 0)
        {
            var red_1 = Instantiate(redSlime, GamePlayController.Instance.playerContain.levelData.transform);
            red_1.gridBase = gridBase;
            red_1.transform.position = this.transform.position;
            red_1.transform.DOJump(gridBase.transform.position, 0.2f, 1, 0.3f).OnComplete(delegate { red_1.Init(); });


           
            GamePlayController.Instance.playerContain.levelData.lsSmiles.Add(red_1);
  
            return;
        }

    }
}
