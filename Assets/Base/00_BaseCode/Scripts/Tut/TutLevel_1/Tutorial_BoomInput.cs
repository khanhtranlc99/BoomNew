using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_BoomInput : TutorialFunController
{

    public SlimeBase slimeBoss;
    public SlimeBase slimeRed;
    public static bool isTut = false;

    public override void Init()
    {
        base.Init();
        if(UseProfile.CurrentLevel <= 1)
        {
            SimplePool2.PoolPreLoad(slimeBoss.gameObject,1, GamePlayController.Instance.playerContain.levelData.transform);
            SimplePool2.PoolPreLoad(slimeRed.gameObject, 3, GamePlayController.Instance.playerContain.levelData.transform);
        }    
    }
    public void HandleSpawnBoss()
    {

        Invoke(nameof(SpawnBoss), 2.5f); ;

    }    
    private void SpawnBoss()
    {
        isTut = true;
       

        var vec2 = GamePlayController.Instance.playerContain.levelData.GridBase(11) ;
        var temp2 = SimplePool2.Spawn(slimeRed, new Vector3(vec2.transform.position.x, 17, vec2.transform.position.z), Quaternion.identity);
        temp2.gridBase = vec2;
        GamePlayController.Instance.playerContain.levelData.lsBoss.Add(temp2);
        GamePlayController.Instance.playerContain.levelData.lsSmiles.Add(temp2);

        var vec1 = GamePlayController.Instance.playerContain.levelData.GridBase(7);
        var temp1 = SimplePool2.Spawn(slimeRed, new Vector3(vec1.transform.position.x, 17, vec1.transform.position.z), Quaternion.identity);
        temp1.gridBase = vec1;
        GamePlayController.Instance.playerContain.levelData.lsBoss.Add(temp1);
        GamePlayController.Instance.playerContain.levelData.lsSmiles.Add(temp1);

        var vec0 = GamePlayController.Instance.playerContain.levelData.GridBase(21);
        var temp0 = SimplePool2.Spawn(slimeRed, new Vector3(vec0.transform.position.x, 17, vec0.transform.position.z), Quaternion.identity);
        temp0.gridBase = vec0;
        GamePlayController.Instance.playerContain.levelData.lsBoss.Add(temp0);
        GamePlayController.Instance.playerContain.levelData.lsSmiles.Add(temp0);

        var vec3 = GamePlayController.Instance.playerContain.levelData.GridBase(9);
        var temp = SimplePool2.Spawn(slimeBoss, new Vector3(vec3.transform.position.x, 17, vec3.transform.position.z), Quaternion.identity);
        temp.gridBase = vec3;
        GamePlayController.Instance.playerContain.levelData.lsBoss.Add(temp);
        GamePlayController.Instance.playerContain.levelData.lsSmiles.Add(temp);

        GamePlayController.Instance.playerContain.levelData.difficult = Difficult.Boss;

        GamePlayController.Instance.playerContain.levelData.InitBase();
    }    
}
