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


    [HarmonyPatch(typeof(CompShearable))]
    [HarmonyPatch("CompInspectStringExtra")]

    public static class RaceToTheRim_CompShearable_CompInspectStringExtra_Patch
    {
        [HarmonyPostfix]
        public static void DisplayScalesInsteadOfWool(ref CompShearable __instance, ref string __result)

        {
            if (__instance.parent is Pawn pawn && __instance.parent.def.defName.Contains("RttR_"))
            {

                if (pawn.Faction != null && !pawn.Suspended && pawn.ageTracker.CurLifeStage.shearable)
                {
                    __result = "RttR_ScaleProduction".Translate() + ": " + __instance.Fullness.ToStringPercent();                   
                }
                else
                {
                    __result = null;
                }

            }


        }
    }


}

