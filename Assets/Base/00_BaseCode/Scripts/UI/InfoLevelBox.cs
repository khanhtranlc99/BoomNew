using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class InfoLevelBox : BaseBox
{
    public static InfoLevelBox _instance;
    public static InfoLevelBox Setup(InfoData infoData)
    {
        if (_instance == null)
        {
            _instance = Instantiate(Resources.Load<InfoLevelBox>(PathPrefabs.INFO_LEVEL_BOX));
            _instance.Init(infoData);
        }
        _instance.InitState();
        return _instance;
    }
    [SerializeField] Button btnPlay;
    [SerializeField] Button btnClose;
 
    public InfoData currentInfoData;
    public List<InfoSlimePopup> lsInfoSlimePopups;
    public List<InfoTranformPopup> infoTranformPopup;
    public InfoTranformPopup GetLsTranformSlime(int param)
    {
       foreach(var item in infoTranformPopup)
        {
            if(item.data.Count == param)
            {
                return item;
            }
        }
        return null;
    }
    public SlimeTarget GetSlimeTarget(SlimeType slimeType)
    {
        foreach(var item in lsInfoSlimePopups)
        {
            if(item.slimeType == slimeType)
            {
                return item.slimeTarget;
            }
        }
        return null;
    }
    public List<Image> lsWinStreak; 

    public void Init(InfoData infoData)
    {
        currentInfoData = infoData;
        btnPlay.onClick.AddListener(delegate { Initiate.Fade("GamePlay", Color.black, 2f); });
        btnClose.onClick.AddListener(delegate { Close(); });
        if(currentInfoData.isSlimeLevel)
        {
            var templsTranform = GetLsTranformSlime(currentInfoData.conditionSlimes.lsDataSlime.Count);
            for(int i = 0; i <  currentInfoData.conditionSlimes.lsDataSlime.Count; i++)
            {
                var tempSlime = SimplePool2.Spawn(GetSlimeTarget(currentInfoData.conditionSlimes.lsDataSlime[i].slimeType));
                tempSlime.transform.parent = templsTranform.data[i].transform;
                tempSlime.transform.localScale = new Vector3(templsTranform.scale, templsTranform.scale, templsTranform.scale);        
                tempSlime.tvCount.text = "" + currentInfoData.conditionSlimes.lsDataSlime[i].countSlime;
            }
        }
        if (UseProfile.WinStreak > 0)
        {
            for (int i = 0; i < UseProfile.WinStreak; i++)
            {
                lsWinStreak[i].color = Color.yellow;
            }
        }
   

    }
    public void InitState()
    {

    }
}

[System.Serializable]
public class InfoSlimePopup
{
    public SlimeType slimeType;
    public SlimeTarget slimeTarget;

}
[System.Serializable]
public class InfoTranformPopup
{
    public List<Transform> data;
    public float scale;
}
 