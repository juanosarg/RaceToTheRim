using RimWorld;
using RimWorld.Planet;
using UnityEngine;
using Verse;
using System.Collections.Generic;

namespace RttRAnimalBehaviours
{
    public class Filth_LiquefiedHelixienGas: Filth
    {

        int tickCounter = 0;

        public override void Tick()
        {
            tickCounter++;
            if (tickCounter > 15)
            {
                if (this.Map != null && FireAtPosition(this.Position, this.Map))
                {
                    List<IntVec3> adjacentCells = GenAdjFast.AdjacentCells8Way(this.Position);
                    foreach (IntVec3 element in adjacentCells)
                    {
                        FireUtility.TryStartFireIn(element, this.Map, 2f);
                    }


                }
                tickCounter = 0;
            }

            
        }

        public static bool FireAtPosition(IntVec3 c, Map map)
        {
            List<Thing> list = map.thingGrid.ThingsListAt(c);
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] is Fire)
                {
                    return true;
                }
            }
            return false;
        }


    }
}
