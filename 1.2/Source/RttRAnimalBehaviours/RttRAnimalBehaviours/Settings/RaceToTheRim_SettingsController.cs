using RimWorld;
using UnityEngine;
using Verse;
using System.Collections.Generic;
using System.Linq;

namespace RttRAnimalBehaviours
{





    public class RaceToTheRim_Mod : Mod
    {

        public static RaceToTheRim_Settings settings;

        public RaceToTheRim_Mod(ModContentPack content) : base(content)
        {
            settings = GetSettings<RaceToTheRim_Settings>();
        }
        public override string SettingsCategory() => "Race to the Rim";

        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);

            RttRToggleableSpawnDef toggleablespawndef = (from k in DefDatabase<RttRToggleableSpawnDef>.AllDefsListForReading
                                                       where k.defName == "RttR_ToggleableAnimals"
                                                         select k).RandomElement();


            if (settings.pawnSpawnStates == null) settings.pawnSpawnStates = new Dictionary<string, bool>();
            foreach (string defName in toggleablespawndef.toggleablePawns)
            {
                if (!settings.pawnSpawnStates.ContainsKey(defName))
                {
                    settings.pawnSpawnStates[defName] = false;
                }
            }



            settings.DoWindowContents(inRect);


        }
    }
}
