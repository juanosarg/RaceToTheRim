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


  

    /*This Harmony Postfix changes terrain calculation for floating creatures
   */
    [HarmonyPatch(typeof(Verse.AI.Pawn_PathFollower))]
    [HarmonyPatch("CostToMoveIntoCell")]
    [HarmonyPatch(new Type[] { typeof(Pawn), typeof(IntVec3) })]
    public static class Pawn_PathFollower_CostToMoveIntoCell_Patch
    {
        [HarmonyPostfix]
        public static void WaterMovement(Pawn pawn, IntVec3 c, ref int __result)

        {
            if ((pawn.Map != null) && (pawn.TryGetComp<CompWaterMovement>() != null))
            {
               
                    int num;
                    TerrainDef terrainDef = pawn.Map.terrainGrid.TerrainAt(c);
                    if (c.x == pawn.Position.x || c.z == pawn.Position.z)
                    {
                        if (terrainDef.IsWater)
                        {
                            num = pawn.TicksPerMoveCardinal;
                        } else num = pawn.TicksPerMoveCardinal * 4;
                    }
                    else
                    {
                        if (terrainDef.IsWater)
                        {
                            num = pawn.TicksPerMoveDiagonal;
                        }
                        else num = pawn.TicksPerMoveDiagonal * 4;
                   
                    }
                    
                   
                    

                    __result = num;

                

            }




        }
    }









}
