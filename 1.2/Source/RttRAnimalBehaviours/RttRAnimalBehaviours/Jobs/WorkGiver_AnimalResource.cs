using System;
using RimWorld;
using Verse;

namespace RttRAnimalBehaviours
{
    public class WorkGiver_AnimalResource : WorkGiver_GatherAnimalBodyResources
    {
        protected override JobDef JobDef
        {
            get
            {
                return DefDatabase<JobDef>.GetNamed("RttR_AnimalResource");
            }
        }

        protected override CompHasGatherableBodyResource GetComp(Pawn animal)
        {
            return animal.TryGetComp<CompAnimalProduct>();
        }
    }
}

