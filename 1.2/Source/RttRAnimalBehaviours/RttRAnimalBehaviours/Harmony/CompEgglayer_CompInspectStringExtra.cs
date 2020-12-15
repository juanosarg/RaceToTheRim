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
using RimWorld.Planet;



namespace RttRAnimalBehaviours
{


    [HarmonyPatch(typeof(CompEggLayer))]
    [HarmonyPatch("CompInspectStringExtra")]

    public static class RaceToTheRim_CompEggLayer_CompInspectStringExtra_Patch
    {
        [HarmonyPostfix]
        public static void DontDisplayEggForNonAdults(ref CompEggLayer __instance, ref string __result)

        {
            if (__instance.parent is Pawn pawn && __instance.parent.def.defName.Contains("RttR_"))
            {

                if (pawn.ageTracker.CurLifeStage!= DefDatabase<LifeStageDef>.GetNamed("AnimalAdult"))
                {
                    __result = null;
                }
                

            }


        }
    }


}

