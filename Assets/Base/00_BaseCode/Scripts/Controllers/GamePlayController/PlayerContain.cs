using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class PlayerContain : MonoBehaviour
{
    public LevelData levelData;
    public BoomInputController boomInputController;
    public TNT_Booster TNT_Booster;
    public AtomBoom_Booster atomBoom_Booster;
    public Rocket_Booster rocket_Booster;
    public Freeze_Booster freeze_Booster;
    public FlameUp_Item flameUp_Item;
    public FastBoom_Item fastBoom_Item;
    public TimeBoom_Item timeBoom_Item;
    
    public void Init()
    {
        string pathLevel = StringHelper.PATH_CONFIG_LEVEL_TEST;

        Debug.LogError(string.Format(pathLevel, UseProfile.CurrentLevel));
        //levelData = Instantiate(Resources.Load<LevelData>(string.Format(pathLevel, UseProfile.CurrentLevel)));
        levelData.Init();
        boomInputController.Init(levelData);
        TNT_Booster.Init();
        atomBoom_Booster.Init();
        rocket_Booster.Init();
        freeze_Booster.Init();
        flameUp_Item.Init();
        fastBoom_Item.Init();
        timeBoom_Item.Init();
    }

    



}
