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


    [HarmonyPatch(typeof(Pawn_TrainingTracker))]
    [HarmonyPatch("TrainingTrackerTickRare")]

    public static class RaceToTheRim_Pawn_TrainingTracker_TrainingTrackerTickRare_Patch
    {
        [HarmonyPrefix]
        public static bool RemoveTamenessDecay(ref Pawn_TrainingTracker __instance)

        {
            Pawn pawn;
            pawn = (Pawn)typeof(Pawn_TrainingTracker).GetField("pawn", AccessTools.all).GetValue(__instance);
            if ((pawn.def.defName.Contains("RttR_")))
            {
                return false;

            }
            else return true;
        }
    }


}
