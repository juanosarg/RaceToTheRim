using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;

namespace RttRAnimalBehaviours
{
    public class Pawn_Spiky: Pawn
    {
        public override void PostApplyDamage(DamageInfo dinfo, float totalDamageDealt)
        {
            base.PostApplyDamage(dinfo, totalDamageDealt);
            Pawn instigator = dinfo.Instigator as Pawn;
            if (instigator != null && instigator.Map!=null)
            {
                CellRect rect = GenAdj.OccupiedRect(instigator.Position, instigator.Rotation, IntVec2.One).ExpandedBy(1);
                foreach (IntVec3 current in rect.Cells)
                {
                    if (current.InBounds(instigator.Map))
                    {
                        HashSet<Thing> hashSet = new HashSet<Thing>(current.GetThingList(this.Map));
                        if (hashSet != null)
                        {
                            foreach (Thing thing in hashSet)
                            {
                                if (thing == this)
                                {
                                    instigator.TakeDamage(new DamageInfo(DamageDefOf.Cut, 20, 0f, -1f, null, null, null, DamageInfo.SourceCategory.ThingOrUnknown));

                                }
                            }
                        }

                    }

                }
            }
           
        }

    }
}
