using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TalentId
{
    public const string AGL = "AGL";

    public const string BTY = "BTY";

    public const string BLD = "BLD";

    public const string MGT = "MGT";

    public const string VIT = "VIT";

    public const string ESS = "ESS";

    public const string ITU = "ITU";

    public const string PRS = "PRS";

    public const string RSN = "RSN";

    public const string WLL = "WLL";
}

public class Talent
{
    public string Id { get; private set; }

    public string Label { get; private set; }

    public string Mastery { get; private set; }

    public string Grandmastery { get; private set; }

    public Talent(string id, string label, string mastery, string grandmastery)
    {
        Id = id;
        Label = label;
        Mastery = mastery;
        Grandmastery = grandmastery;
    }

    public static Dictionary<string, Talent>
        All =
            new Dictionary<string, Talent>()
            {
                // TODO: BTY/PRS is a hack
                { "BTY/PRS", new Talent("BTY/PRS", "", "", "") },
                {
                    TalentId.AGL,
                    new Talent(TalentId.AGL,
                        "Agility",
                        "Multifarious (1 extra action/turn in action sequences)",
                        "Winged Heels (learns Shen: path of neutrality & arcana)")
                },
                {
                    TalentId.BTY,
                    new Talent(TalentId.BTY,
                        "Beauty",
                        "Blue Steel (target stunned duration of concentration)",
                        "Grace (only targetable by those character already attacked)")
                },
                {
                    TalentId.BLD,
                    new Talent(TalentId.BLD,
                        "Build",
                        "Adamant (natural PL of 2, stacks with armor)",
                        "Marrow of the World (immune to critical eff ects and extra damage)")
                },
                {
                    TalentId.MGT,
                    new Talent(TalentId.MGT,
                        "Might",
                        "The Ox (burden from all armor reduced by half)",
                        "Herculean (3 chances for feats of strength)")
                },
                {
                    TalentId.VIT,
                    new Talent(TalentId.VIT,
                        "Vitality",
                        "Wellspring (healing rate is 4 LP/day)",
                        "Immortal (+3 LP)")
                },
                {
                    TalentId.ESS,
                    new Talent(TalentId.ESS,
                        "Essence",
                        "Dweomer (allies in 5y radius can use 1/2 ESS vs magic)",
                        "Ensorcelled (1/2 damage from ordinary weapons)")
                },
                {
                    TalentId.ITU,
                    new Talent(TalentId.ITU,
                        "Intuition",
                        "Awakened (+1 extra action during surprise, auto-triggers)",
                        "Six Sense (substitute ITU for any VA roll or drain)")
                },
                {
                    TalentId.PRS,
                    new Talent(TalentId.PRS,
                        "Pressence",
                        "Semper Fidelis (allies get +5 on rolls to help character)",
                        "Servant of Two Masters (recruit enemy’s loyal vassal, WLL to resist)")
                },
                {
                    TalentId.RSN,
                    new Talent(TalentId.RSN,
                        "Reason",
                        "Endowment (learn a mastery from any known skill)",
                        "Improbable (use RSN for resistance rolls against magic)")
                },
                {
                    TalentId.WLL,
                    new Talent(TalentId.WLL,
                        "Will",
                        "Indomitable (1/2 to all VA drains & conscious in 5 IL)",
                        "Undying Resolve (recovers normally in the 6th injury level)")
                }
            };
}
