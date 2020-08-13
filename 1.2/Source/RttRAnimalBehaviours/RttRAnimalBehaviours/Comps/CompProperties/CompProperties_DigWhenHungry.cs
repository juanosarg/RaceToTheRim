
using Verse;

namespace RttRAnimalBehaviours
{
    public class CompProperties_DigWhenHungry : CompProperties
    {


        public string customThingToDig = "";
        public int customAmountToDig = 1;
        public int timeToDig = 40000;



        public CompProperties_DigWhenHungry()
        {
            this.compClass = typeof(CompDigWhenHungry);
        }


    }
}
