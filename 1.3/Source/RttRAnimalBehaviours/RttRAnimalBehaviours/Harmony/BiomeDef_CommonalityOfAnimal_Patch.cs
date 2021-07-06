using HarmonyLib;
using RimWorld;
using System.Reflection;
using Verse;
using System.Reflection.Emit;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Verse.AI;

namespace RttRAnimalBehaviours
{
    /*This Harmony Postfix multiplies commonality of animals in the biome
    */
    [HarmonyPatch(typeof(BiomeDef))]
    [HarmonyPatch("CommonalityOfAnimal")]
    public static class RaceToTheRim_BiomeDef_CommonalityOfAnimal_Patch
    {
        [HarmonyPostfix]
        public static void MultiplyRaceToTheRimCommonality(PawnKindDef animalDef, ref float __result)

        {

            if (animalDef.defName.Contains("RttR_"))
            {
                __result *= RaceToTheRim_Mod.settings.RttRSpawnMultiplier;

            }


        }
    }
}
