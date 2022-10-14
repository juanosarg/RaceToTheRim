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

    [HarmonyPatch(typeof(MapTemperature))]
    [HarmonyPatch("SeasonAcceptableFor")]
    public static class RaceToTheRim_MapTemperature_SeasonAcceptableFor_Patch
    {
        [HarmonyPostfix]
        public static void AllowAnimalSpawns(ThingDef animalRace, ref bool __result)

        {

           

            if (RaceToTheRim_Mod.settings.pawnSpawnStates != null && RaceToTheRim_Mod.settings.pawnSpawnStates.Keys.Contains(animalRace.defName))
            {
                if (RaceToTheRim_Mod.settings.pawnSpawnStates[animalRace.defName])
                {

                    __result = false;
                }

            }


        }
    }





}
