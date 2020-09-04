using RimWorld;
using RimWorld.Planet;
using UnityEngine;
using Verse;
using RimWorld;
using System.Collections.Generic;

namespace RttRAnimalBehaviours
{
    public class Filth_ScaldingFoam : Filth
    {

        int tickCounter = 0;

        public override void Tick()
        {
            tickCounter++;
            if (tickCounter > 45)
            {
                if (this.Map != null)
                {

                    List<Thing> list = this.Map.thingGrid.ThingsListAt(this.Position);
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (list[i] is Fire)
                        {
                            list[i].Destroy(DestroyMode.Vanish);
                            this.Destroy(DestroyMode.Vanish);
                        }
                    }


                }
                tickCounter = 0;
            }


        }

       


    }
}
