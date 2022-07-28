/*
Questions:
-Is Carrying Capacity 10+mgt OR 10+(mgt+bld)/2
-should Philtrology untrained be -10 or X?
-Do masteries or grandmasteries add a +1 bonus?
-Does Shielding use MGT for weapon damage
-

*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Character
{
    public Dictionary<string, int> Talents { get; private set; }

    public Dictionary<string, int> TalentTempModifiers { get; private set; }

    public Dictionary<string, int> Skills { get; private set; }

    public Dictionary<string, int> Specialties { get; private set; }

    public Dictionary<string, bool> Masteries { get; private set; }

    public Dictionary<string, bool> GrandMasteries { get; private set; }

    public Character(
        Dictionary<string, int> talents = null,
        Dictionary<string, int> talentTempModifiers = null,
        Dictionary<string, int> skills = null,
        Dictionary<string, int> specialties = null,
        Dictionary<string, bool> masteries = null,
        Dictionary<string, bool> grandMasteries = null
    )
    {
        Talents =
            talents != null
                ? talents
                : new Dictionary<string, int>()
                {
                    { TalentId.AGL, 0 },
                    { TalentId.BTY, 0 },
                    { TalentId.BLD, 0 },
                    { TalentId.MGT, 0 },
                    { TalentId.VIT, 0 },
                    { TalentId.ESS, 0 },
                    { TalentId.ITU, 0 },
                    { TalentId.PRS, 0 },
                    { TalentId.RSN, 0 },
                    { TalentId.WLL, 0 }
                };
        TalentTempModifiers =
            talentTempModifiers != null
                ? talentTempModifiers
                : new Dictionary<string, int>()
                {
                    { TalentId.AGL, 0 },
                    { TalentId.BTY, 0 },
                    { TalentId.BLD, 0 },
                    { TalentId.MGT, 0 },
                    { TalentId.VIT, 0 },
                    { TalentId.ESS, 0 },
                    { TalentId.ITU, 0 },
                    { TalentId.PRS, 0 },
                    { TalentId.RSN, 0 },
                    { TalentId.WLL, 0 }
                };
        Skills = skills != null ? skills : new Dictionary<string, int>() { };
        Specialties =
            specialties != null
                ? specialties
                : new Dictionary<string, int>() { };
        Masteries =
            masteries != null ? masteries : new Dictionary<string, bool>() { };
        GrandMasteries =
            grandMasteries != null
                ? grandMasteries
                : new Dictionary<string, bool>() { };
    }

    // private helper functions
    private T
    GetOrDefault<T>(Dictionary<string, T> dict, string key, T defaultVal)
    {
        return (dict.ContainsKey(key) ? dict[key] : defaultVal);
    }

    private int GetTalentLevel(string va)
    {
        // BTY/PRS hack
        if (va == "BTY/PRS")
        {
            return Math
                .Max(GetOrDefault(Talents, TalentId.BTY, 0) +
                GetOrDefault(TalentTempModifiers, TalentId.BTY, 0),
                GetOrDefault(Talents, TalentId.PRS, 0) +
                GetOrDefault(TalentTempModifiers, TalentId.PRS, 0));
        }
        return GetOrDefault(Talents, va, 0) +
        GetOrDefault(TalentTempModifiers, va, 0);
    }

    private int GetSkillLevel(string skill)
    {
        var s = Skill.All[skill];
        var skillLevel = GetOrDefault(Skills, s.Id, 0);
        return GetTalentLevel(s.Talent.Id) +
        (skillLevel > 0 ? 0 : s.Untrained) +
        skillLevel;
    }

    private int GetSpecialtyLevel(string specialty)
    {
        var s = Specialty.All[specialty];
        return GetSkillLevel(s.Skill.Id) + GetOrDefault(Specialties, s.Id, 0);
    }

    // get computed values
    public int GetImpeniment()
    {
        return 0; // TODO
    }

    // todo: this docs are inconsistent, is this just 10+might now?
    public int GetCarryinyCapacity()
    {
        return Mathf
            .RoundToInt(10 +
            (GetTalentLevel(TalentId.BLD) + GetTalentLevel(TalentId.MGT)) / 2f);
    }

    public int GetMovement()
    {
        return GetSkillLevel("Maneuvering") - GetImpeniment();
    }

    public int GetTotalLifePoints()
    {
        return Mathf
            .RoundToInt((
            20 + GetTalentLevel(TalentId.VIT) + GetTalentLevel(TalentId.BLD)
            ) /
            5);
    }

    public int GetTotalQuintessencePoints()
    {
        if (GetSkillLevel("Arcana") <= 0)
        {
            return 0;
        }
        return GetSpecialtyLevel("Variance");
    }

    // weapon helpers
    public int GetWeaponSpeed(int weaponBurden)
    {
        return GetSpecialtyLevel("Reaction Speed") - weaponBurden;
    }

    public int GetWeaponAttackBonus(string specialty)
    {
        return GetSpecialtyLevel("specialty");
    }

    public int GetWeaponDammageBonus(int waeponImpact, string specialty)
    {
        string[] usesMight =
            {
                "Throwing",
                "One-handed wpns",
                "Open hand",
                "Two-handed wpns",
                "Shielding" // todo: is this one true?
            };
        bool useMight = usesMight.Contains(specialty);
        return waeponImpact + (useMight ? GetTalentLevel(TalentId.MGT) : 0);
    }
}
