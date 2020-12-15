
using Verse;
using System.Collections.Generic;
namespace RttRAnimalBehaviours
{
    public class CompProperties_EatWeirdFood : CompProperties
    {

        public List<string> customThingToEat = null;
        public int nutrition = 2;
        public bool fullyDestroyThing = false;
        public float percentageOfDestruction = 0.1f;
        public bool digThingIfMapEmpty = false;
        public string thingToDigIfMapEmpty = "";
        public int customAmountToDig = 1;
        public string hediffWhenEaten = "";
        public List<string> thingToDigIfMapEmptyRandom = null;

   




        public CompProperties_EatWeirdFood()
        {
            this.compClass = typeof(CompEatWeirdFood);
        }
    }
}
