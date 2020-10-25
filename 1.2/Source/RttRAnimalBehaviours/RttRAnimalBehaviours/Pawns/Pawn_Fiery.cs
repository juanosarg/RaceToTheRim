using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;
using Verse.AI;
using UnityEngine;

namespace RttRAnimalBehaviours
    
{
    [StaticConstructorOnStartup]
    public class Pawn_Fiery : Pawn
    {
        private static readonly Material FireOverlay = MaterialPool.MatFrom("Things/Special/Fire/FireA", ShaderDatabase.MetaOverlay);

        private static readonly float BaseAlt = AltitudeLayer.MetaOverlays.AltitudeFor();

        public override void Tick()
        {
            base.Tick();
            if (this.Awake()) {
                if (this.jobs.curDriver is JobDriver_AttackMelee)
                {
                    Vector3 drawPos = this.DrawPos;
                    drawPos.y = BaseAlt + 0.181818187f;
                    float num = ((float)Math.Sin((double)((Time.realtimeSinceStartup + 397f * (float)(this.thingIDNumber % 571)) * 4f)) + 1f) * 0.5f;
                    num = 0.3f + num * 0.7f;
                    Material material = FadedMaterialPool.FadedVersionOf(FireOverlay, num);
                    Graphics.DrawMesh(MeshPool.plane08, drawPos, Quaternion.identity, material, 0);

                }
            }
            
        }

    }
}