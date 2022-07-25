using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpecialtyTest : MonoBehaviour
{
    void Start()
    {
        Debug.Log("---Start Specialty Tests---");
        Test(Specialty.All["Cant"].Skill.Talent.Id == "Reason",
        "Specialty.All[\"Cant\"].Skill.Talent.Id fail");
        Debug.Log("---End Specialty Tests---");
    }

    private void Test(bool showMsg, string msg)
    {
        if (showMsg)
        {
            Debug.Log (msg);
        }
    }
}
