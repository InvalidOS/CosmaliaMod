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
using Terraria.Graphics.Shaders;
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

			player.dash = 1;
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

		public override void PostUpdate()
		{
			if (player.dashDelay < 0)
			{
				ApplyDashHitbox();
			}
		}

		public override void ModifyDrawInfo(ref PlayerDrawInfo drawInfo)
		{
			if (player.dashDelay < 0)
			{
				player.headRotation = (float)(Math.PI / 2) * player.direction;
			}
		}

		public void ApplyDashHitbox()
		{
			Rectangle rectangle = new Rectangle((int)(player.position.X + player.velocity.X * 0.5 + (10 * player.direction)), (int)(player.position.Y + player.velocity.Y * 0.5 - 5), player.width + 6, player.height + 10);
			for (int i = 0; i < 200; i++)
			{
				if (Main.npc[i].active && !Main.npc[i].dontTakeDamage && !Main.npc[i].friendly && Main.npc[i].immune[player.whoAmI] <= 0)
				{
					NPC npc = Main.npc[i];
					Rectangle rect = npc.getRect();
					if (rectangle.Intersects(rect) && (npc.noTileCollide || player.CanHit(npc)))
					{
						float dmg = 30f * player.meleeDamage;
						float num2 = 800f;

						if (Main.hardMode) { num2 = 3000; }

						if (NPC.downedPlantBoss) { num2 = 10000; }

						bool crit = false;
						if (Main.rand.Next(100) < player.meleeCrit)
						{
							crit = true;
						}
						int direction = player.direction;
						if (player.velocity.X < 0f)
						{
							direction = -1;
						}
						if (player.velocity.X > 0f)
						{
							direction = 1;
						}
						player.ApplyDamageToNPC(npc, (int)dmg, num2, direction * 6, crit);
						player.Hurt(PlayerDeathReason.ByCustomReason(player.name + " bashed their skull in."), (int)(player.statLifeMax * .125f), direction * -2);
						npc.immune[player.whoAmI] = 6;
						player.immune = true;
						player.immuneNoBlink = true;
						player.immuneTime = 6;
					}
				}
			}
		}
	}
}