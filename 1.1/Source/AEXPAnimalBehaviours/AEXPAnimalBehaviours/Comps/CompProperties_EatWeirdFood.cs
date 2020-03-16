
using Verse;
using System.Collections.Generic;
namespace RttRAnimalBehaviours
{
    public class CompProperties_EatWeirdFood : CompProperties
    {

        public List<string> customThingToEat = null;
        public int nutrition = 2;

        public string thingToDigIfMapEmpty = "";
        public int customAmountToDig = 1;

        public CompProperties_EatWeirdFood()
        {
            this.compClass = typeof(CompEatWeirdFood);
        }
    }
}
