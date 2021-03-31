using System;
using RimWorld;

using Verse;
using System.Collections.Generic;

namespace RttRAnimalBehaviours
{
	public class Verb_Shoot_Prelightning : Verb_Shoot
	{

		protected override bool TryCastShot()
		{
			bool flag = base.TryCastShot();
			this.caster.Map.weatherManager.eventHandler.AddEvent(new WeatherEvent_CustomLightningStrike(this.caster.Map, this.caster.Position));
			return flag;
		}
	}
}
