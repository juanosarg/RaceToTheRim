using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using Verse.AI;
using RimWorld;

namespace RttRAnimalBehaviours
{
    public class JobDriver_IngestWeirdBU : JobDriver
    {
        private Thing IngestibleSource
        {
            get
            {
                return this.job.GetTarget(TargetIndex.A).Thing;
            }
        }

      

        public override void ExposeData()
        {
            base.ExposeData();
           
        }

        public override string GetReport()
        {
            
            Thing thing = this.job.targetA.Thing;
           /* if (thing != null && thing.def.ingestible != null)
            {
                if (!thing.def.ingestible.ingestReportStringEat.NullOrEmpty() && (thing.def.ingestible.ingestReportString.NullOrEmpty() || this.pawn.RaceProps.intelligence < Intelligence.ToolUser))
                {
                    return thing.def.ingestible.ingestReportStringEat.Formatted(this.job.targetA.Thing.LabelShort, this.job.targetA.Thing);
                }
                if (!thing.def.ingestible.ingestReportString.NullOrEmpty())
                {
                    return thing.def.ingestible.ingestReportString.Formatted(this.job.targetA.Thing.LabelShort, this.job.targetA.Thing);
                }
            }*/
            return base.GetReport();
        }

        public override void Notify_Starting()
        {
            base.Notify_Starting();
           
        }

        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
          
            return pawn.Reserve(this.job.targetA, job, 1, -1, null, errorOnFailed);
        }

        protected override IEnumerable<Toil> MakeNewToils()
        {
           
            this.FailOn(() => !this.IngestibleSource.Destroyed);
           
            Toil chew = ChewAnything(this.pawn, 1f, TargetIndex.A, TargetIndex.B).FailOn((Toil x) => !this.IngestibleSource.Spawned && (this.pawn.carryTracker == null || this.pawn.carryTracker.CarriedThing != this.IngestibleSource)).FailOnCannotTouch(TargetIndex.A, PathEndMode.Touch);
            foreach (Toil toil in this.PrepareToIngestToils(chew))
            {
                yield return toil;
            }
            IEnumerator<Toil> enumerator = null;
            yield return chew;
            //yield return Toils_Ingest.FinalizeIngest(this.pawn, TargetIndex.A);
            yield return Toils_Jump.JumpIf(chew, () => this.job.GetTarget(TargetIndex.A).Thing is Corpse && this.pawn.needs.food.CurLevelPercentage < 0.9f);
            yield break;
            yield break;
        }

        private IEnumerable<Toil> PrepareToIngestToils(Toil chewToil)
        {
           
            return this.PrepareToIngestToils_NonToolUser();
        }

     

        private IEnumerable<Toil> PrepareToIngestToils_NonToolUser()
        {
            yield return this.ReserveFood();
            yield return Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.Touch);
            yield break;
        }

        private Toil ReserveFood()
        {
            return new Toil
            {
                initAction = delegate ()
                {
                    if (this.pawn.Faction == null)
                    {
                        return;
                    }
                    Thing thing = this.job.GetTarget(TargetIndex.A).Thing;
                    if (this.pawn.carryTracker.CarriedThing == thing)
                    {
                        return;
                    }
                    int maxAmountToPickup = 1;//FoodUtility.GetMaxAmountToPickup(thing, this.pawn, this.job.count);
                    if (maxAmountToPickup == 0)
                    {
                        return;
                    }
                    if (!this.pawn.Reserve(thing, this.job, 10, maxAmountToPickup, null, true))
                    {
                        Log.Error(string.Concat(new object[]
                        {
                            "Pawn food reservation for ",
                            this.pawn,
                            " on job ",
                            this,
                            " failed, because it could not register food from ",
                            thing,
                            " - amount: ",
                            maxAmountToPickup
                        }), false);
                        this.pawn.jobs.EndCurrentJob(JobCondition.Errored, true, true);
                    }
                    this.job.count = maxAmountToPickup;
                },
                defaultCompleteMode = ToilCompleteMode.Instant,
                atomicWithPrevious = true
            };
        }


        public static Toil ChewAnything(Pawn chewer, float durationMultiplier, TargetIndex ingestibleInd, TargetIndex eatSurfaceInd = TargetIndex.None)
        {
            Toil toil = new Toil();
            toil.initAction = delegate ()
            {
                Pawn actor = toil.actor;
                Thing thing = actor.CurJob.GetTarget(ingestibleInd).Thing;
               /* if (!thing.IngestibleNow)
                {
                    chewer.jobs.EndCurrentJob(JobCondition.Incompletable, true, true);
                    return;
                }*/
                actor.jobs.curDriver.ticksLeftThisToil = 500;
                if (thing.Spawned)
                {
                    thing.Map.physicalInteractionReservationManager.Reserve(chewer, actor.CurJob, thing);
                }
            };
            toil.tickAction = delegate ()
            {
                if (chewer != toil.actor)
                {
                    toil.actor.rotationTracker.FaceCell(chewer.Position);
                }
                else
                {
                    Thing thing = toil.actor.CurJob.GetTarget(ingestibleInd).Thing;
                    if (thing != null && thing.Spawned)
                    {
                        toil.actor.rotationTracker.FaceCell(thing.Position);
                    }
                    else if (eatSurfaceInd != TargetIndex.None && toil.actor.CurJob.GetTarget(eatSurfaceInd).IsValid)
                    {
                        toil.actor.rotationTracker.FaceCell(toil.actor.CurJob.GetTarget(eatSurfaceInd).Cell);
                    }
                }
                toil.actor.GainComfortFromCellIfPossible(false);
            };
            toil.WithProgressBar(ingestibleInd, delegate
            {
                Thing thing = toil.actor.CurJob.GetTarget(ingestibleInd).Thing;
                if (thing == null)
                {
                    return 1f;
                }
                return 1f - (float)toil.actor.jobs.curDriver.ticksLeftThisToil / Mathf.Round((float)500 * durationMultiplier);
            }, false, -0.5f);
            toil.defaultCompleteMode = ToilCompleteMode.Delay;
            toil.FailOnDestroyedOrNull(ingestibleInd);
            toil.AddFinishAction(delegate
            {
                if (chewer == null)
                {
                    return;
                }
                if (chewer.CurJob == null)
                {
                    return;
                }
                Thing thing = chewer.CurJob.GetTarget(ingestibleInd).Thing;
                if (thing == null)
                {
                    return;
                }
                if (chewer.Map.physicalInteractionReservationManager.IsReservedBy(chewer, thing))
                {
                    chewer.Map.physicalInteractionReservationManager.Release(chewer, toil.actor.CurJob, thing);
                }
            });
            toil.handlingFacing = true;
            Toils_Ingest.AddIngestionEffects(toil, chewer, ingestibleInd, eatSurfaceInd);
            return toil;
        }






        public const float EatCorpseBodyPartsUntilFoodLevelPct = 0.9f;

        public const TargetIndex IngestibleSourceInd = TargetIndex.A;

        private const TargetIndex TableCellInd = TargetIndex.B;

        private const TargetIndex ExtraIngestiblesToCollectInd = TargetIndex.C;
    }
}
