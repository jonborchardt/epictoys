using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Specialty
{
    public string Id { get; private set; }

    public Skill Skill { get; private set; }

    public string Mastery { get; private set; }

    public string Grandmastery { get; private set; }

    public Specialty(
        string id,
        Skill skill,
        string mastery,
        string grandmastery
    )
    {
        Id = id;
        Skill = skill;
        Mastery = mastery;
        Grandmastery = grandmastery;
    }

    public static Dictionary<string, Specialty>
        All =
            new Dictionary<string, Specialty>()
            {
                {
                    "Gas theory",
                    new Specialty("Gas theory",
                        Skill.All["Alchemy"],
                        "Effortless (Lv1 variants, 0 QP)",
                        "Exalted (x2 duration, range, or target area)")
                },
                {
                    "Liquid theory",
                    new Specialty("Liquid theory",
                        Skill.All["Alchemy"],
                        "Effortless (Lv1 variants, 0 QP)",
                        "Exalted (x2 duration, range, or target area)")
                },
                {
                    "Reaction theory",
                    new Specialty("Reaction theory",
                        Skill.All["Alchemy"],
                        "Effortless (Lv1 variants, 0 QP)",
                        "Exalted (x2 duration, range, or target area)")
                },
                {
                    "Solid theory",
                    new Specialty("Solid theory",
                        Skill.All["Alchemy"],
                        "Effortless (Lv1 variants, 0 QP)",
                        "Exalted (x2 duration, range, or target area)")
                },
                {
                    "Arcane lore",
                    new Specialty("Arcane lore",
                        Skill.All["Arcana"],
                        "Lingua Obscura (only targets understand)",
                        "Enchanted (drain QP instead of LP or VAs)")
                },
                {
                    "Variance",
                    new Specialty("Variance",
                        Skill.All["Arcana"],
                        "The Source (QP regeneration 1 per 30 min)",
                        "The Canon (learn all variants in a specialty)")
                },
                {
                    "Resistance",
                    new Specialty("Resistance",
                        Skill.All["Arcana"],
                        "Spellweather (defensive variants 1/2 QP)",
                        "Unassailable (unlimited defensive variants)")
                },
                {
                    "2-d art",
                    new Specialty("2-d art",
                        Skill.All["Arts"],
                        "The Artist’s Eye (art as surveillance/ITU)",
                        "Form and Function (+4 melee/mksmn/med.)")
                },
                {
                    "3-d art",
                    new Specialty("3-d art",
                        Skill.All["Arts"],
                        "Hand of the Muse (art as craftsman)",
                        "Form and Function (+4 melee/mksmn/med.)")
                },
                {
                    "Endurance",
                    new Specialty("Endurance",
                        Skill.All["Athletics"],
                        "The Wall (+2 talent body VA progression)",
                        "Ultra (can run 100 miles/day)")
                },
                {
                    "Riding",
                    new Specialty("Riding",
                        Skill.All["Athletics"],
                        "Trot (split riding move among actions)",
                        "Horse Whistler (summon mount far away)")
                },
                {
                    "Parkour",
                    new Specialty("Parkour",
                        Skill.All["Athletics"],
                        "Unstoppable (pass 4y obstacles at speed)",
                        "Rally (+1 action after falls, 50% fall dmg)")
                },
                {
                    "Carpentry",
                    new Specialty("Carpentry",
                        Skill.All["Craftsman"],
                        "Journeyman (fame & quality)",
                        "Master of Saws (craft/repair imbued)")
                },
                {
                    "Engineering",
                    new Specialty("Engineering",
                        Skill.All["Craftsman"],
                        "Journeyman (fame & quality)",
                        "Master of Calipers (craft/repair imbued)")
                },
                {
                    "Masonry",
                    new Specialty("Masonry",
                        Skill.All["Craftsman"],
                        "Journeyman (fame & quality)",
                        "Master of Trowels (craft/repair imbued)")
                },
                {
                    "Smithing",
                    new Specialty("Smithing",
                        Skill.All["Craftsman"],
                        "Journeyman (fame & quality)",
                        "Master of Hammers (craft/repair imbued)")
                },
                {
                    "Pathfinding",
                    new Specialty("Pathfinding",
                        Skill.All["Expedition"],
                        "Eagle Eye (+5 navigating environment)",
                        "Unburdened Trek (immune to impediment)")
                },
                {
                    "Survival",
                    new Specialty("Survival",
                        Skill.All["Expedition"],
                        "Grizzly’s Cabin (camp gets +10 vs surprise)",
                        "Big Game (game heals 1 IL)")
                },
                {
                    "Stealth",
                    new Specialty("Stealth",
                        Skill.All["Fieldcraft"],
                        "Slayer (+5 on critical att area roll)",
                        "The Shadow (full move, repeated stealth)")
                },
                {
                    "Surveillance",
                    new Specialty("Surveillance",
                        Skill.All["Fieldcraft"],
                        "Revelation (“unknowable” fact about target)",
                        "Event Horizon (perception obstacles nulled)")
                },
                {
                    "Tracking",
                    new Specialty("Tracking",
                        Skill.All["Fieldcraft"],
                        "Scout (learn statistics from tracks)",
                        "Seeker (know target’s present location)")
                },
                {
                    "Animal handling",
                    new Specialty("Animal handling",
                        Skill.All["Husbandry"],
                        "Subtle Command (command by a look)",
                        "Animal Empathy (give and get basic feels)")
                },
                {
                    "Farming",
                    new Specialty("Farming",
                        Skill.All["Husbandry"],
                        "Good Earth (all poison effects cut by 1/2)",
                        "Harvest Moon (restores +1d5 LP, QP, & VA)")
                },
                {
                    "Lore",
                    new Specialty("Lore",
                        Skill.All["Letters"],
                        "Local Customs (lore skill to sway)",
                        "Social Chameleon (blend in group/culture)")
                },
                {
                    "Linguistics",
                    new Specialty("Linguistics",
                        Skill.All["Letters"],
                        "Polyglot (learn language in 1 week)",
                        "Translator (understand foreign languages)")
                },
                {
                    "Philosophy",
                    new Specialty("Philosophy",
                        Skill.All["Letters"],
                        "Pearls of Wisdom (allies +1 progress rolls)",
                        "Cogito (Letters for mind VA rolls & drains)")
                },
                {
                    "Evasion",
                    new Specialty("Evasion",
                        Skill.All["Maneuvering"],
                        "Evasive Posture (1 extra evasion/turn)",
                        "Untouchable (unlimited evasions per turn)")
                },
                {
                    "Footwork",
                    new Specialty("Footwork",
                        Skill.All["Maneuvering"],
                        "Maestro (move combatants=footwork)",
                        "Blaze (move up to 4/order)")
                },
                {
                    "Reaction speed",
                    new Specialty("Reaction speed",
                        Skill.All["Maneuvering"],
                        "Dashing (can split move between actions)",
                        "Quicksilver (single actions for free/+5 move)")
                },
                {
                    "Shooting",
                    new Specialty("Shooting",
                        Skill.All["Marksman"],
                        "Split Strike (attack twice at ½ bonus)",
                        "Triple Strike (attack three times at ½ bonus)")
                },
                {
                    "Throwing",
                    new Specialty("Throwing",
                        Skill.All["Marksman"],
                        "Split Strike (attack twice at ½ bonus)",
                        "Triple Strike (attack three times at ½ bonus)")
                },
                {
                    "Forensics",
                    new Specialty("Forensics",
                        Skill.All["Medicine"],
                        "Crime Scene (1 unknowable phys. detail)",
                        "Investigator (2 unknowable details)")
                },
                {
                    "Surgery",
                    new Specialty("Surgery",
                        Skill.All["Medicine"],
                        "Triage (1 LP PDL healing after damage)",
                        "Resuscitation (revive from IL 6)")
                },
                {
                    "One-handed wpns",
                    new Specialty("One-handed wpns",
                        Skill.All["Melee"],
                        "Split Strike (attack twice at ½ bonus)",
                        "Triple Strike (attack three times at ½ bonus)")
                },
                {
                    "Open hand",
                    new Specialty("Open hand",
                        Skill.All["Melee"],
                        "Split Strike (attack twice at ½ bonus)",
                        "Triple Strike (attack three times at ½ bonus)")
                },
                {
                    "Shielding",
                    new Specialty("Shielding",
                        Skill.All["Melee"],
                        "Bulwark (1 extra block per turn)",
                        "Aegis (shield cover score is doubled)")
                },
                {
                    "Two-handed wpns",
                    new Specialty("Two-handed wpns",
                        Skill.All["Melee"],
                        "Split Strike (attack twice at ½ bonus)",
                        "Triple Strike (attack three times at ½ bonus)")
                },
                {
                    "Management",
                    new Specialty("Management",
                        Skill.All["Merchantry"],
                        "The Executive (associates +2 VA prog rolls)",
                        "Magnate (associates gain masteries easier)")
                },
                {
                    "Sales",
                    new Specialty("Sales",
                        Skill.All["Merchantry"],
                        "Caveat Emptor (double/half sales price)",
                        "Collector (history & imbued properties)")
                },
                {
                    "Calescent theory",
                    new Specialty("Calescent theory",
                        Skill.All["Metaphysics"],
                        "Effortless (Lv1 variants, 0 QP)",
                        "Exalted (x2 duration, range, or target area)")
                },
                {
                    "Gravity theory",
                    new Specialty("Gravity theory",
                        Skill.All["Metaphysics"],
                        "Effortless (Lv1 variants, 0 QP)",
                        "Exalted (x2 duration, range, or target area)")
                },
                {
                    "Radiant theory",
                    new Specialty("Radiant theory",
                        Skill.All["Metaphysics"],
                        "Effortless (Lv1 variants, 0 QP)",
                        "Exalted (x2 duration, range, or target area)")
                },
                {
                    "Submaterial theory",
                    new Specialty("Submaterial theory",
                        Skill.All["Metaphysics"],
                        "Effortless (Lv1 variants, 0 QP)",
                        "Exalted (x2 duration, range, or target area)")
                },
                {
                    "Command",
                    new Specialty("Command",
                        Skill.All["Military arts"],
                        "Lion Heart (+1 “PL” on MM)",
                        "Valor (allies within 5y get +1 on combat rolls)")
                },
                {
                    "Operations",
                    new Specialty("Operations",
                        Skill.All["Military arts"],
                        "Conscription (1d5 bystanders follow)",
                        "Bivouac (doubles triage MM bonus)")
                },
                {
                    "Strategy/Tactics",
                    new Specialty("Strategy/Tactics",
                        Skill.All["Military arts"],
                        "Subtlety (+1 attacks & battle rolls)",
                        "The Art of War (choose order)")
                },
                {
                    "Acting",
                    new Specialty("Acting",
                        Skill.All["Performance"],
                        "Poor Player (+5 on skill role would know)",
                        "The Method (+1 mastery: The Method)")
                },
                {
                    "Dancing",
                    new Specialty("Dancing",
                        Skill.All["Performance"],
                        "Gliding (use dance as an extra evasion)",
                        "Poetry in Motion (dance as maneuver/ath)")
                },
                {
                    "Music",
                    new Specialty("Music",
                        Skill.All["Performance"],
                        "Euphonics (sway/influence with music)",
                        "Pied Piper (sway/influence 50 PDL w/music)")
                },
                {
                    "Alteration formulae",
                    new Specialty("Alteration formulae",
                        Skill.All["Philtrology"],
                        "Bubbling Cauldron (Arcana at lvl 1)",
                        "Eye of Newt (1 dose PDL/brew)")
                },
                {
                    "Curative formulae",
                    new Specialty("Curative formulae",
                        Skill.All["Philtrology"],
                        "Bubbling Cauldron (Arcana at lvl 1)",
                        "Eye of Newt (1 dose PDL/brew)")
                },
                {
                    "Poison formulae",
                    new Specialty("Poison formulae",
                        Skill.All["Philtrology"],
                        "Bubbling Cauldron (Arcana at lvl 1)",
                        "Eye of Newt (1 dose PDL/brew)")
                },
                {
                    "Rituals",
                    new Specialty("Rituals",
                        Skill.All["Religion"],
                        "Liturgy (ritual grants 1 re-roll/24 hrs)",
                        "Sacrament (disunite magic, imprecations...)")
                },
                {
                    "Meditation",
                    new Specialty("Meditation",
                        Skill.All["Religion"],
                        "Illumination (learns Shen: path of harmony)",
                        "Transcend (transfer LP & VA levels after 1hr)")
                },
                {
                    "Inspiration",
                    new Specialty("Inspiration",
                        Skill.All["Religion"],
                        "Apotheosis (“heal” drained VA lvls: 2 PDL)",
                        "Divine Intervention (request a miracle)")
                },
                {
                    "Navigation",
                    new Specialty("Navigation",
                        Skill.All["Seamanship"],
                        "Crow’s nest (know mood of taverns/locales)",
                        "Buried Treasure (many secret caches)")
                },
                {
                    "Watercraft",
                    new Specialty("Watercraft",
                        Skill.All["Seamanship"],
                        "Ropework (ropes/knots dur: 3, snap untied)",
                        "Swarthy Crew (1d10 bystanders join up)")
                },
                {
                    "Mathematics",
                    new Specialty("Mathematics",
                        Skill.All["Science"],
                        "Optimize (+2 quality level with craftsmen)",
                        "Number Theory (+1 to 3 VAs/skills)")
                },
                {
                    "Life science",
                    new Specialty("Life science",
                        Skill.All["Science"],
                        "Origins (+5 to all animal interactions)",
                        "Naturalist (+1 to 3 VAs/skills)")
                },
                {
                    "Physical science",
                    new Specialty("Physical science",
                        Skill.All["Science"],
                        "The Apparatus (sub science for craftsman)",
                        "Principia (+1 to 3 VAs/skills)")
                },
                {
                    "Path of conflict",
                    new Specialty("Path of conflict",
                        Skill.All["Shen"],
                        "Empower (extra action for this path)",
                        "Effortless (Lv1 variants, 0 QP)")
                },
                {
                    "Path of harmony",
                    new Specialty("Path of harmony",
                        Skill.All["Shen"],
                        "Empower (extra action for this path)",
                        "Effortless (Lv1 variants, 0 QP)")
                },
                {
                    "Path of neutrality",
                    new Specialty("Path of neutrality",
                        Skill.All["Shen"],
                        "Empower (extra action for this path)",
                        "Effortless (Lv1 variants, 0 QP)")
                },
                {
                    "Deception",
                    new Specialty("Deception",
                        Skill.All["Sociability"],
                        "Fool (target won't change mind)",
                        "Judge (automatically detect lies)")
                },
                {
                    "Influence",
                    new Specialty("Influence",
                        Skill.All["Sociability"],
                        "Silver-tongue (persuade enemies)",
                        "The Eye (influence by glance)")
                },
                {
                    "Network",
                    new Specialty("Network",
                        Skill.All["Sociability"],
                        "Insider (insider will sacrifice/betray)",
                        "Speakeasy (accepted into any network)")
                },
                {
                    "Courtier",
                    new Specialty("Courtier",
                        Skill.All["Statecraft"],
                        "Decorum (machinations cost 1/2)",
                        "Collusion (re-roll random event and choose)")
                },
                {
                    "Ruling",
                    new Specialty("Ruling",
                        Skill.All["Statecraft"],
                        "Scion (+1 ROI)",
                        "Dominion (+1 action, SC, and hoard per turn)")
                },
                {
                    "Clothier",
                    new Specialty("Clothier",
                        Skill.All["Stewardship"],
                        "Journeyman (fame & quality)",
                        "Master of Seams (craft/repair imbued)")
                },
                {
                    "Foodcraft",
                    new Specialty("Foodcraft",
                        Skill.All["Stewardship"],
                        "Gastronomy (products renew 1LP)",
                        "Epicurean (sway/influence with products)")
                },
                {
                    "Hospitality",
                    new Specialty("Hospitality",
                        Skill.All["Stewardship"],
                        "Haven (x2 recovery of VAs, QP, & LP)",
                        "Sanctuary (all allies alive in the 6th IL)")
                },
                {
                    "Cant",
                    new Specialty("Cant",
                        Skill.All["Thiefcraft"],
                        "Watcher (auto-spot people who know cant)",
                        "Pidgin (only intended targets understand)")
                },
                {
                    "Escapology",
                    new Specialty("Escapology",
                        Skill.All["Thiefcraft"],
                        "In Plain Sight (escape & stealth)",
                        "Great Escape (companions get +5 on roll)")
                },
                {
                    "Mechanisms",
                    new Specialty("Mechanisms",
                        Skill.All["Thiefcraft"],
                        "Smooth release (pick at -5 by thumping)",
                        "Saboteur (1 turn mechanical hack, no tools)")
                },
                {
                    "Slight of hand",
                    new Specialty("Slight of hand",
                        Skill.All["Thiefcraft"],
                        "Slippery (can’t be caught)",
                        "Legerdemain (increase wgt lifted to 10 lbs)")
                },
                {
                    "Art of channeling",
                    new Specialty("Art of channeling",
                        Skill.All["Theurgy"],
                        "Effortless (Lv1 variants, 0 QP)",
                        "Exalted (x2 duration, range, or target area)")
                },
                {
                    "Art of conveyance",
                    new Specialty("Art of conveyance",
                        Skill.All["Theurgy"],
                        "Effortless (Lv1 variants, 0 QP)",
                        "Exalted (x2 duration, range, or target area)")
                },
                {
                    "Art of divination",
                    new Specialty("Art of divination",
                        Skill.All["Theurgy"],
                        "Effortless (Lv1 variants, 0 QP)",
                        "Exalted (x2 duration, range, or target area)")
                },
                {
                    "Art of imprecation",
                    new Specialty("Art of imprecation",
                        Skill.All["Theurgy"],
                        "Effortless (Lv1 variants, 0 QP)",
                        "Exalted (x2 duration, range, or target area)")
                },
                {
                    "Art of summoning",
                    new Specialty("Art of summoning",
                        Skill.All["Theurgy"],
                        "Effortless (Lv1 variants, 0 QP)",
                        "Exalted (x2 duration, range, or target area)")
                }
            };
}
