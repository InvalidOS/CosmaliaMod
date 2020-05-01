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

namespace CosmaliaMod.Races.Ram
{
	public class Ram : CosmaliaRace
	{
		//Player player = Main.player[player.whoAmI];
		//CosmaliaRace race = Main.player[Main.myPlayer].GetModPlayer<CosmaliaRace>();

		public override void ResetEffects()
		{
			headFront = mod.GetTexture("Races/Ram/HornFront");
			headBack = mod.GetTexture("Races/Ram/HornBack");

			meleeAttack = 100;
			rangedAttack = 20;
			magicAttack = 45;
			summonAttack = 60;
			mana = 10;
			hp = 225;
			resistance = 60;
			intelligence = 70;
			speed = 30;
		}

		public override TagCompound Save()
		{
			return new TagCompound
			{
				{"ram", isRace},
			};
		}

		public override void Load(TagCompound tag)
		{
			isRace = tag.GetBool("ram");
		}
	}
}

