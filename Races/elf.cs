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

namespace CosmaliaMod.Races
{
	public class Elf : CosmaliaRace
	{
		//Player player = Main.player[player.whoAmI];
		//CosmaliaRace race = Main.player[Main.myPlayer].GetModPlayer<CosmaliaRace>();
		
		public override void ResetEffects()
		{
			ears = mod.GetTexture("Races/ElfEar");
			head = mod.GetTexture("Races/ElfFace");
			
			meleeAttack = 40;
			rangedAttack = 65;
			magicAttack = 70;
			summonAttack = 30;
			mana = 30;
			hp = 80;
			resistance = 15;
			intelligence = 110;
			speed = 70;
		}
		
		public override TagCompound Save()
		{
			return new TagCompound
			{
				{"elf", isRace},
			};
		}
		
		public override void Load(TagCompound tag)
		{
			isRace = tag.GetBool("elf");
		}
		
		
	}
}








