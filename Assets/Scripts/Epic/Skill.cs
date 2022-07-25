using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    public string Id { get; private set; }

    public Talent Talent { get; private set; }

    public int Untrained { get; private set; }

    public Skill(string id, Talent talent, int untrained)
    {
        Id = id;
        Talent = talent;
        Untrained = untrained;
    }

    public static Dictionary<string, Skill>
        All =
            new Dictionary<string, Skill>()
            {
                { "Alchemy", new Skill("Alchemy", Talent.All[TalentId.RSN], -99) },
                { "Arcana", new Skill("Arcana", Talent.All[TalentId.ESS], -99) },
                { "Arts", new Skill("Arts", Talent.All[TalentId.ITU], -5) },
                { "Athletics", new Skill("Athletics", Talent.All[TalentId.AGL], 0) },
                { "Craftsman", new Skill("Craftsman", Talent.All[TalentId.WLL], -5) },
                { "Expedition", new Skill("Expedition", Talent.All[TalentId.ITU], 0) },
                { "Fieldcraft", new Skill("Fieldcraft", Talent.All[TalentId.ITU], 0) },
                { "Husbandry", new Skill("Husbandry", Talent.All[TalentId.WLL], -5) },
                { "Letters", new Skill("Letters", Talent.All[TalentId.RSN], -5) },
                {
                    "Maneuvering",
                    new Skill("Maneuvering", Talent.All[TalentId.AGL], 0)
                },
                { "Marksman", new Skill("Marksman", Talent.All[TalentId.ITU], 0) },
                { "Medicine", new Skill("Medicine", Talent.All[TalentId.RSN], -5) },
                { "Melee", new Skill("Melee", Talent.All[TalentId.WLL], 0) },
                {
                    "Merchantry",
                    new Skill("Merchantry", Talent.All[TalentId.PRS], -5)
                },
                {
                    "Metaphysics",
                    new Skill("Metaphysics", Talent.All[TalentId.RSN], -99)
                },
                {
                    "Military arts",
                    new Skill("Military arts", Talent.All[TalentId.PRS], -5)
                },
                {
                    "Performance",
                    new Skill("Performance", Talent.All[TalentId.PRS], 0)
                },
                {
                    "Philtrology",
                    new Skill("Philtrology", Talent.All[TalentId.RSN], -10)
                },
                { "Religion", new Skill("Religion", Talent.All[TalentId.ITU], -5) },
                {
                    "Seamanship",
                    new Skill("Seamanship", Talent.All[TalentId.WLL], -5)
                },
                { "Science", new Skill("Science", Talent.All[TalentId.RSN], -5) },
                { "Shen", new Skill("Shen", Talent.All[TalentId.ITU], -99) },
                {
                    "Sociability",
                    new Skill("Sociability", Talent.All["BTY/PRS"], 0)
                },
                {
                    "Statecraft",
                    new Skill("Statecraft", Talent.All[TalentId.PRS], -5)
                },
                {
                    "Stewardship",
                    new Skill("Stewardship", Talent.All[TalentId.PRS], 0)
                },
                {
                    "Thiefcraft",
                    new Skill("Thiefcraft", Talent.All[TalentId.RSN], -5)
                },
                { "Theurgy", new Skill("Theurgy", Talent.All[TalentId.PRS], -99) }
            };
}
