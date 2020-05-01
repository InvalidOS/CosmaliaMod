using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameInput;
using Terraria.ModLoader.IO;
using CosmaliaMod;

namespace CosmaliaMod.Races.Longtail
{
	public class Longtail : CosmaliaRace
	{
		//Player player = Main.player[player.whoAmI];
		//CosmaliaRace race = Main.player[Main.myPlayer].GetModPlayer<CosmaliaRace>();
		byte frame;
		byte count;

		public override void ResetEffects()
		{
			ears = mod.GetTexture("Races/Longtail/Ear");
			tail = mod.GetTexture("Races/Longtail/Tail");
			clawsFront = mod.GetTexture("Races/Longtail/clawsFront");
			clawsBack = mod.GetTexture("Races/Longtail/clawsBack");

			meleeAttack = 60;
			rangedAttack = 60;
			magicAttack = 35;
			summonAttack = 30;
			mana = 5;
			hp = 40;
			resistance = 20;
			intelligence = 100;
			speed = 100;
		}

		public override TagCompound Save()
		{
			return new TagCompound
			{
				{"longtail", isRace},
			};
		}

		public override void Load(TagCompound tag)
		{
			isRace = tag.GetBool("longtail");
		}

		public override Rectangle? TailAnimation()
		{
			// update animations
			// return new Rectangle?(0, 56 * frame, tail.Width, 56);
			return new Rectangle?(new Rectangle(0, 0, mod.GetTexture("Races/Longtail/Tail").Width, mod.GetTexture("Races/Longtail/Tail").Height));
		}
	}
}