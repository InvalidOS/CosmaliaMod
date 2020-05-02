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

namespace CosmaliaMod
{
	public abstract class CosmaliaRace : ModPlayer
	{
		// Maximum for an attack stat, resistance, and speed is 100
		// Maximum for intelligence is 120
		// Maximum for HP is 250, refers to starting health
		// Maximum for mana is 60, refers to starting mana

		public byte meleeAttack = 50;
		public byte rangedAttack = 50;
		public byte magicAttack = 50;
		public byte summonAttack = 50;
		public byte mana = 20;
		public byte hp = 100;
		public byte resistance = 50;
		public byte intelligence = 100;
		public byte speed = 50;
		public bool isRace = false;
		// public string description = "";

		public virtual Texture2D ears() => null;
		public virtual Texture2D head() => null;
		public virtual Texture2D eyes() => null;
		public virtual Texture2D headFront() => null;
		public virtual Texture2D headBack() => null;
		public virtual Texture2D tail() => null;
		public virtual Texture2D legs() => null;
		public virtual Texture2D clawsFront() => null;
		public virtual Texture2D clawsBack() => null;

		public virtual void PreUpdateRace()
		{
			
		}

		public virtual Rectangle? TailAnimation()
		{
			return null;
		}

		public virtual DrawData? DrawTail(PlayerDrawInfo drawInfo)
		{
			return null;
		}

		public override void PreUpdate()
		{
			if (isRace)
			{
				PreUpdateRace();
				player.GetModPlayer<CosmaliaPlayer>().intelligence = intelligence;
			}
		}

		public override void PreUpdateBuffs()
		{
			if (isRace)
			{
				player.meleeDamage *= (float)((meleeAttack - 50) / 50 + 1);
				player.rangedDamage *= (float)((rangedAttack - 50) / 50 + 1);
				player.magicDamage *= (float)((magicAttack - 50) / 50 + 1);
				player.minionDamage *= (float)((summonAttack - 50) / 50 + 1);
				player.endurance += (float)((resistance - 50) / (100 * (1.5f - (Math.Sign(resistance - 49.9f) * .5f))));
				player.maxRunSpeed *= (float)((speed - 50) / 50 + 1);
				player.statLifeMax2 = (int)((hp / 5) * (player.statLifeMax / 20));
				player.statManaMax2 = (int)(mana * (player.statManaMax / 20));
			}
		}
		
		//public override void PostUpdate()
		//{
			//CosmaliaPlayer modPlayer = player.GetModPlayer<CosmaliaPlayer>();
		//}
	}
}




