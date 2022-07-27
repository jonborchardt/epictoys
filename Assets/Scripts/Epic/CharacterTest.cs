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
                },
                skills: new Dictionary<string, int>()
                {
                    { "Arts", 1 },
                    { "Athletics", 1 },
                    { "Expedition", 1 },
                    { "Fieldcraft", 1 },
                    { "Husbandry", 2 },
                    { "Maneuvering", 1 },
                    { "Marksman", 2 },
                    { "Melee", 3 }
                });

        Debug.Log("---Start Character Tests---");
        Test(character.GetCarryinyCapacity() != 11,
        "GetCarryinyCapacity fail.");
        Test(character.GetTotalQuintessencePoints() != 0,
        "GetTotalQuintessencePoints fail.");
        Debug.Log("---End Character Tests---");

        var rand = new System.Random();
        int tries = 10000;
        for (int va = -4; va <= 9; va++)
        {
            for (int level = 0; level <= 9; level++)
            {
                int totalTriesToSuccessSkillOnly = 0;
                int totalTriesToSuccessSkillAndVav = 0;
                int totalTriesToSuccessSkillAndVas = 0;
                for (int t = 0; t < tries; t++)
                {
                    // skill only
                    int gmRoll;
                    int roll;
                    int attempts = 0;
                    do
                    {
                        attempts++;
                        gmRoll = rand.Next(1, 11) + rand.Next(1, 11) + level;
                        roll = rand.Next(1, 11) + rand.Next(1, 11) + va;
                        if (roll >= gmRoll)
                        {
                            totalTriesToSuccessSkillOnly += attempts;
                        }
                    }
                    while (roll < gmRoll);

                    // skill and va
                    //va
                    int gmRollv;
                    int rollv;
                    int attemptsv = 0;
                    do
                    {
                        attemptsv++;
                        gmRollv = rand.Next(1, 11) + rand.Next(1, 11) + va;
                        rollv = rand.Next(1, 11) + rand.Next(1, 11);
                        if (rollv >= gmRollv)
                        {
                            totalTriesToSuccessSkillAndVav += attemptsv;
                        }
                    }
                    while (rollv < gmRollv);

                    // then skill
                    int gmRolls;
                    int rolls;
                    int attemptss = 0;
                    do
                    {
                        attemptss++;
                        gmRolls = rand.Next(1, 11) + rand.Next(1, 11) + level;
                        rolls = rand.Next(1, 11) + rand.Next(1, 11) + va + 1;
                        if (rolls >= gmRolls)
                        {
                            totalTriesToSuccessSkillAndVas += attemptss;
                        }
                    }
                    while (rolls < gmRolls);
                }
                if (false)
                {
                    Debug
                        .Log(va +
                        ", " +
                        level +
                        ", " +
                        (
                        (
                        totalTriesToSuccessSkillOnly <
                        (
                        totalTriesToSuccessSkillAndVav +
                        totalTriesToSuccessSkillAndVas
                        )
                        )
                            ? "SKILL, " + totalTriesToSuccessSkillOnly / tries
                            : "VA, " +
                            totalTriesToSuccessSkillAndVav / tries +
                            ", " +
                            totalTriesToSuccessSkillAndVas / tries
                        ));
                }
                if (true)
                {
                    Debug
                        .Log(va +
                        ", " +
                        level +
                        ", " +
                        totalTriesToSuccessSkillOnly*10 / tries +
                        ", " +
                        totalTriesToSuccessSkillAndVav*10 / tries);
                }
            }
        }
    }

    private void Test(bool showMsg, string msg)
    {
        if (showMsg)
        {
            Debug.Log (msg);
        }
    }
}
