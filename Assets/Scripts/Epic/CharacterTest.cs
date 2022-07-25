using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTest : MonoBehaviour
{
    void Start()
    {
        Character character =
            new Character(talents: new Dictionary<string, int>()
                {
                     { TalentId.AGL, -1 },
                    { TalentId.BTY, 1 },
                    { TalentId.BLD, 2 },
                    { TalentId.MGT, 0 },
                    { TalentId.VIT, 2 },
                    { TalentId.ESS, -2 },
                    { TalentId.ITU, 0 },
                    { TalentId.PRS, 0 },
                    { TalentId.RSN, -1 },
                    { TalentId.WLL, 3 }
                });

        Debug.Log("---Start Character Tests---");
        Test(character.GetCarryinyCapacity() != 11,
        "GetCarryinyCapacity fail.");
        Test(character.GetTotalQuintessencePoints() != 0,
        "GetTotalQuintessencePoints fail.");
        Debug.Log("---End Character Tests---");
    }

    private void Test(bool showMsg, string msg)
    {
        if (showMsg)
        {
            Debug.Log (msg);
        }
    }
}
