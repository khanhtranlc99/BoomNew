using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackController : MonoBehaviour
{
    public List<int> lsLevelShowPack;

     public void Init()
    {
        if (lsLevelShowPack.Contains(UseProfile.CurrentLevel))
        {
            if (!UseProfile.Fire_Start && !UseProfile.Boom_Start)
            {

                PremiumPackBox.Setup().Show();
            }
            else
            {
                BoosterPackBox.Setup().Show();
            }    
        }
      
    }    
}
