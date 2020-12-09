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
            Pawn instigator = dinfo.Instigator as Pawn;
            Map thisMap = this.Map;
            IntVec3 position = instigator.Position;
            Rot4 rotation = instigator.Rotation;
            base.PostApplyDamage(dinfo, totalDamageDealt);            
            if (instigator != null && instigator.Map!=null && !instigator.Dead)
            {
                CellRect rect = GenAdj.OccupiedRect(position, rotation, IntVec2.One).ExpandedBy(1);
                foreach (IntVec3 current in rect.Cells)
                {
                    if (current.InBounds(instigator.Map))
                    {
                        if (current != null) {
                            HashSet<Thing> hashSet = new HashSet<Thing>(current.GetThingList(thisMap));
                            if (hashSet != null)
                            {
                                foreach (Thing thing in hashSet)
                                {
                                    if (thing != null && thing == this || thing == this.Corpse)
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
}
