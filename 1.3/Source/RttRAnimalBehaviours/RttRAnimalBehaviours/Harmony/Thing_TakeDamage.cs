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


    [HarmonyPatch(typeof(Thing))]
    [HarmonyPatch("TakeDamage")]

    public static class RaceToTheRim_Thing_TakeDamage_Patch
    {
        [HarmonyPrefix]
        public static bool DragonsDontTakeFireDamage(DamageInfo dinfo, ref Thing __instance, ref DamageWorker.DamageResult __result)

        {
            if (__instance is Pawn pawn)
            {
                if (pawn.def.defName.Contains("RttR_") && dinfo.Def == DamageDefOf.Flame)
                {
                    __result = new DamageWorker.DamageResult();
                    return false;

                }

            }
            
            return true;
        }
    }


}
