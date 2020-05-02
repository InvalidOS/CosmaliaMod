using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using CosmaliaMod;
using CosmaliaMod.Races;
using CosmaliaMod.Races.Elf;
using CosmaliaMod.Races.Ram;
using CosmaliaMod.Races.Longtail;

namespace CosmaliaMod
{
	public class CosmaliaPlayer : ModPlayer
	{
		public bool cosmalian = false;
		
		// Stuff to move elsewhere
		public bool elf = false;
		public bool cat = false;
		public bool catClimb = false;
		
		public Color skin; // convenience field

		public Color tailColor = new Color(0, 0, 0);
		public Color hornColor = new Color(0, 0, 0);
		public Color clawColor = new Color(255, 255, 255);
		public Color scleraColor = new Color(255, 255, 255);

		public bool arbHornV = false;
		public bool arbMaskV = false;
		public bool arbLegs = false;
		public byte intelligence = 100;
		
		public bool whiskers2 = false;
		public bool whiskers = false;
		public short sniffTime = 0; // how long until player does the sniff animation
		public byte sniffCount = 0; // how many times the player sniffed in this cycle
		public short sniffFrame = 0; // animation frame
		public short sniffFrame2 = 0; // how long the animation has stayed on a frame
		public bool sniffing = false; // if the player is in the sniff animation
		public bool sniffEnd = false; // if the player has completed the sniff animation
		public sbyte sniff2 = -1; // obsolete variable
		
		public override TagCompound Save()
		{
			return new TagCompound
			{
				{"cosmalian", cosmalian},
				{"hornColor", hornColor},
				{"tailColor", tailColor},
				{"clawColor", clawColor}
			};
		}
		
		public override void Load(TagCompound tag)
		{
			cosmalian = tag.GetBool("cosmalian");
			hornColor = tag.Get<Color>("hornColor");
			tailColor = tag.Get<Color>("tailColor");
			clawColor = tag.Get<Color>("clawColor");
		}
		
		public override void clientClone(ModPlayer clientClone)
		{
			CosmaliaPlayer clone = clientClone as CosmaliaPlayer;
			clone.cosmalian = cosmalian;
		}
		
		public override void ResetEffects()
		{
			intelligence = 100;
			
			arbHornV = false;
			arbMaskV = false;
			arbLegs = false;
			whiskers = false;
			whiskers2 = false;
			
			if (sniff2 >= 0)
			{
				sniff2--;
			}
			if (sniff2 == 0)
			{
				sniffing = false;
				sniff2 = -1;
			}
		}
		
		public override void PreUpdate()
		{
			if (arbLegs)
			{
				//player.height = 70;
				//player.width = 56;
			}
			
			if (whiskers)
			{
				if (sniffTime > 0)
				{
					sniffTime--;
				}
				else if (sniffFrame2 < 20)
				{
					if (sniffFrame2 % 4 == 0)
					{
						sniffFrame++;
					}
					sniffFrame2++;
				}
				else
				{
					sniffEnd = true;
					switch (sniffCount)
					{
						case 0:
							sniffFrame = 0;
							sniffFrame2 = 0;
							sniffCount++;
							sniffing = true;
							break;
						case 1:
							if (ThreadLocalRandom.NextDouble() > 0.333333)
							{
								sniffFrame = 0;
								sniffFrame2 = 0;
								sniffCount++;
							}
							else
							{
								sniffCount = 0;
								sniffTime = (short)ThreadLocalRandom.Next(120, 540);
								sniffing = false;
							}
							break;
						case 2:
							if (ThreadLocalRandom.NextDouble() > 0.5)
							{
								sniffFrame = 0;
								sniffFrame2 = 0;
								sniffCount++;
							}
							else
							{
								sniffCount = 0;
								sniffTime = (short)ThreadLocalRandom.Next(120, 540);
								sniffing = false;
							}
							break;
						case 3:
							sniffCount = 0;
							sniffTime = (short)ThreadLocalRandom.Next(120, 540);
							sniffing = false;
							break;
					}
				}
			}
		}
		
		public override void SetControls()
		{
			if (intelligence < 95 && (player.controlUseItem | player.controlHook) && player.HeldItem.hammer < 1 && player.HeldItem.axe < 1 && player.HeldItem.pick < 1)
			{
				Main.mouseX += (int)(ThreadLocalRandom.NextDouble() * 5 * (Math.Pow(1.2, (96 - intelligence) / 4)));
				Main.mouseY += (int)(ThreadLocalRandom.NextDouble() * 5 * (Math.Pow(1.2, (96 - intelligence) / 4)));
			}
		}
			
		public override void PostUpdate()
		{
			if (elf)
			{
				player.maxRunSpeed *= 1.5f;
				player.statLifeMax2 = (player.statLifeMax2 * 4) / 5;
				player.statManaMax2 = 240;
				player.endurance -= .05f;
				intelligence += 10;
				//player.manaRegen = 10;
				//player.detectCreature = true;
			}
			
			if (cat)
			{
				player.maxRunSpeed *= 2.75f;
				player.runAcceleration *= 1.5f;
				player.runSlowdown *= 1.5f;
				player.statLifeMax2 = (player.statLifeMax2 * 3) / 5;
				player.statManaMax2 = 20;
				//player.endurance -= .05f;
				player.noFallDmg = true;
				player.spikedBoots += 2;
				//player.lifeRegen++;
				player.dangerSense = true;
				intelligence -= 40;
				if (player.controlDown)
				{
					player.spikedBoots--;
				}
				if (player.sliding)
				{
					Player.jumpSpeed *= 3;
					if (player.controlUp)
					{
						catClimb = true;
						player.velocity.Y = -9;
					}
				}
				else if (catClimb && (player.direction == -1 && player.controlLeft ||
				player.direction == 1 && player.controlRight) && player.velocity.X == 0
				&& player.controlUp)
				{
					catClimb = true;
					player.velocity.Y = -9;
				}
				else
				{
					catClimb = false;
				}
			}
			
			player.manaCost /= (intelligence * 3) / 100;
			
			if (intelligence >= 110)
			{
				//player.slotsMinions *= 0.5f;
			}
			if (intelligence <= 95)
			{
				player.slotsMinions *= 2;
				player.meleeDamage *= (1 + ((95 - intelligence) / 95));
			}
		}
		
		public override void ModifyDrawInfo(ref PlayerDrawInfo drawInfo)
		{
			if (arbLegs)
			{
				//drawInfo.position.Y -= 14;
			}

			skin = drawInfo.faceColor;

			drawInfo.eyeWhiteColor = Lighting.GetColor(
					(int)((drawInfo.position.X + player.width / 2f) / 16f),
					(int)((drawInfo.position.Y + player.height / 2f) / 16f),
					scleraColor);

			if (intelligence < 95)
			{
				Filters.Scene.Activate("IntelligenceBlur", player.position).GetShader().UseColor(.15f, player.position.X, player.position.Y);
				Filters.Scene.Activate("IntelligenceBlur2", player.position).GetShader().UseColor(.15f, player.position.X, player.position.Y);
			}
			else
			{
				if (Main.netMode != NetmodeID.Server && Filters.Scene["IntelligenceBlur"].IsActive())
				{
					Filters.Scene["IntelligenceBlur"].Deactivate();
					Filters.Scene["IntelligenceBlur2"].Deactivate();
				}
			}
		}
		
			
		/*
		else if (this.selectedMenu == 9)
					{
						Main.PlaySound(10, -1, -1, 1, 1f, 0f);
						if (Main.PendingPlayer.GetModPlayer<CosmaliaPlayer>().selection == 0)
						{
							Main.PendingPlayer.GetModPlayer<CosmaliaPlayer>().selection++;
						}
						Main.PendingPlayer.GetModPlayer<CosmaliaPlayer>().selection++;

						if (Main.PendingPlayer.GetModPlayer<CosmaliaPlayer>().selection >= 4)
						{
							Main.PendingPlayer.GetModPlayer<CosmaliaPlayer>().selection = 1;
						}
					}
		*/
		
		#region PlayerLayers
		
		public static readonly PlayerLayer HeadBack = new PlayerLayer("CosmaliaMod", "HeadBack", PlayerLayer.Head, delegate (PlayerDrawInfo drawInfo)
		{
			if (drawInfo.shadow != 0f || drawInfo.drawPlayer.dead)
			{
				return;
			}
			Player drawPlayer = drawInfo.drawPlayer;
			Mod mod = ModLoader.GetMod("CosmaliaMod");
			CosmaliaPlayer modPlayer = drawPlayer.GetModPlayer<CosmaliaPlayer>();
			Rectangle? frame = drawPlayer.bodyFrame;
			Texture2D texture;
			if (modPlayer.arbHornV)
			{
				texture = mod.GetTexture("Items/Arborite/ArboriteHorns_Back");

				int frameSize = texture.Height / 20;
				int drawX = (int)(drawInfo.position.X + drawPlayer.width / 2f - Main.screenPosition.X);
				int drawY = (int)(drawInfo.position.Y + drawPlayer.height / 2f - Main.screenPosition.Y - 3);
				DrawData data = new DrawData(texture, new Vector2(drawX, drawY), frame,
					Lighting.GetColor((int)((drawInfo.position.X + drawPlayer.width / 2f) / 16f),
						(int)((drawInfo.position.Y + drawPlayer.height / 2f) / 16f)),
					0f, new Vector2(texture.Width / 2f, frameSize / 2f), 1f,
					drawInfo.spriteEffects, 0);
				data.shader = drawInfo.headArmorShader;
				Main.playerDrawData.Add(data);
			}
			if (modPlayer.arbMaskV)
			{
				texture = mod.GetTexture("Items/Arborite/ArboriteFacemask_Face");

				int frameSize = texture.Height / 20;
				int drawX = (int)(drawInfo.position.X + drawPlayer.width / 2f - Main.screenPosition.X);
				int drawY = (int)(drawInfo.position.Y + drawPlayer.height / 2f - Main.screenPosition.Y - 3);
				DrawData data = new DrawData(texture, new Vector2(drawX, drawY), frame,
					Lighting.GetColor((int)((drawInfo.position.X + drawPlayer.width / 2f) / 16f),
						(int)((drawInfo.position.Y + drawPlayer.height / 2f) / 16f)),
					0f, new Vector2(texture.Width / 2f, frameSize / 2f), 1f,
					drawInfo.spriteEffects, 0);
				Main.playerDrawData.Add(data);
			}
		});
		
		public static readonly PlayerLayer Ears = new PlayerLayer("CosmaliaMod", "Ears", PlayerLayer.Head, delegate (PlayerDrawInfo drawInfo)
		{
			if (drawInfo.shadow != 0f || drawInfo.drawPlayer.dead)
			{
				return;
			}
			Player drawPlayer = drawInfo.drawPlayer;
			Mod mod = ModLoader.GetMod("CosmaliaMod");
			CosmaliaPlayer modPlayer = drawPlayer.GetModPlayer<CosmaliaPlayer>();
			Rectangle? frame = drawPlayer.bodyFrame;
			Texture2D texture;
			if (modPlayer.arbHornV)
			{
				texture = mod.GetTexture("Items/Arborite/ArboriteHorns_Ears");

				int frameSize = texture.Height / 20;
				int drawX = (int)(drawInfo.position.X + drawPlayer.width / 2f - Main.screenPosition.X);
				int drawY = (int)(drawInfo.position.Y + drawPlayer.height / 2f - Main.screenPosition.Y - 3);
				DrawData data = new DrawData(texture, new Vector2(drawX, drawY), frame,
					drawInfo.bodyColor,
					0f, new Vector2(texture.Width / 2f, frameSize / 2f), 1f,
					drawInfo.spriteEffects, 0);
				Main.playerDrawData.Add(data);
			}
		});
		
		public static readonly PlayerLayer Legs2 = new PlayerLayer("CosmaliaMod", "Legs2", PlayerLayer.Skin, delegate (PlayerDrawInfo drawInfo)
		{
			if (drawInfo.shadow != 0f || drawInfo.drawPlayer.dead)
			{
				return;
			}
			Player drawPlayer = drawInfo.drawPlayer;
			Mod mod = ModLoader.GetMod("CosmaliaMod");
			CosmaliaPlayer modPlayer = drawPlayer.GetModPlayer<CosmaliaPlayer>();
			Rectangle frame = drawPlayer.legFrame;
			frame.Y = (int)(frame.Y / 56 * 70);
			frame.Width = 56;
			frame.Height = 70;
			Texture2D texture;
			if (modPlayer.arbLegs)
			{
				texture = mod.GetTexture("Items/Arborite/ArboriteBody_Legs_2");

				int frameSize = texture.Height / 20;
				int drawX = (int)(drawInfo.position.X + drawPlayer.width / 2f - Main.screenPosition.X);
				int drawY = (int)((drawInfo.position.Y + 14) + drawPlayer.height / 2f - Main.screenPosition.Y - 10);
				DrawData data = new DrawData(texture, new Vector2(drawX, drawY), frame,
					Lighting.GetColor((int)((drawInfo.position.X + drawPlayer.width / 2f) / 16f),
						(int)((drawInfo.position.Y + drawPlayer.height / 2f) / 16f)),
					0f, new Vector2(texture.Width / 2f, frameSize / 2f), 1f,
					drawInfo.spriteEffects, 0);
				data.shader = drawInfo.legArmorShader;
				Main.playerDrawData.Add(data);
			}
		});
		
		public static readonly PlayerLayer HeadAlt = new PlayerLayer("CosmaliaMod", "HeadAlt", PlayerLayer.Head, delegate (PlayerDrawInfo drawInfo)
		{
			if (drawInfo.shadow != 0f || drawInfo.drawPlayer.dead)
			{
				return;
			}
			Player drawPlayer = drawInfo.drawPlayer;
			Mod mod = ModLoader.GetMod("CosmaliaMod");
			CosmaliaPlayer modPlayer = drawPlayer.GetModPlayer<CosmaliaPlayer>();
			Rectangle frame = drawPlayer.bodyFrame;
			Texture2D texture;
			if (modPlayer.sniffing)
			{
				texture = mod.GetTexture("Items/Misc/Whiskers/sniffHead");

				int frameSize = texture.Height / 20;
				frame.Width = 40;
				frame.X = (int)modPlayer.sniffFrame * 40;
				int drawX = (int)(drawInfo.position.X + drawPlayer.width / 2f - Main.screenPosition.X);
				int drawY = (int)(drawInfo.position.Y + drawPlayer.height / 2f - Main.screenPosition.Y - 3);
				DrawData data = new DrawData(texture, new Vector2(drawX, drawY), frame,
					drawInfo.faceColor,
					0f, new Vector2(texture.Width / 10f, frameSize / 2f), 1f,
					drawInfo.spriteEffects, 0);
				Main.playerDrawData.Add(data);
			}
		});
		
		public static readonly PlayerLayer EyeAlt = new PlayerLayer("CosmaliaMod", "EyeAlt", PlayerLayer.Head, delegate (PlayerDrawInfo drawInfo)
		{
			if (drawInfo.shadow != 0f || drawInfo.drawPlayer.dead)
			{
				return;
			}
			Player drawPlayer = drawInfo.drawPlayer;
			Mod mod = ModLoader.GetMod("CosmaliaMod");
			CosmaliaPlayer modPlayer = drawPlayer.GetModPlayer<CosmaliaPlayer>();
			Rectangle frame = drawPlayer.bodyFrame;
			Texture2D texture;
			if (modPlayer.sniffing)
			{
				texture = mod.GetTexture("Items/Misc/Whiskers/sniffEye");

				int frameSize = texture.Height / 20;
				frame.Width = 40;
				frame.X = (int)modPlayer.sniffFrame * 40;
				int drawX = (int)(drawInfo.position.X + drawPlayer.width / 2f - Main.screenPosition.X);
				int drawY = (int)(drawInfo.position.Y + drawPlayer.height / 2f - Main.screenPosition.Y - 3);
				DrawData data = new DrawData(texture, new Vector2(drawX, drawY), frame,
					drawInfo.faceColor,
					0f, new Vector2(texture.Width / 10f, frameSize / 2f), 1f,
					drawInfo.spriteEffects, 0);
				Main.playerDrawData.Add(data);
			}
		});
		
		public static readonly PlayerLayer EyeWAlt = new PlayerLayer("CosmaliaMod", "EyeWAlt", PlayerLayer.Head, delegate (PlayerDrawInfo drawInfo)
		{
			if (drawInfo.shadow != 0f || drawInfo.drawPlayer.dead)
			{
				return;
			}
			Player drawPlayer = drawInfo.drawPlayer;
			Mod mod = ModLoader.GetMod("CosmaliaMod");
			CosmaliaPlayer modPlayer = drawPlayer.GetModPlayer<CosmaliaPlayer>();
			Rectangle frame = drawPlayer.bodyFrame;
			Texture2D texture;
			if (modPlayer.sniffing)
			{
				texture = mod.GetTexture("Items/Misc/Whiskers/sniffEyeWhite");

				int frameSize = texture.Height / 20;
				frame.Width = 40;
				frame.X = (int)modPlayer.sniffFrame * 40;
				int drawX = (int)(drawInfo.position.X + drawPlayer.width / 2f - Main.screenPosition.X);
				int drawY = (int)(drawInfo.position.Y + drawPlayer.height / 2f - Main.screenPosition.Y - 3);
				DrawData data = new DrawData(texture, new Vector2(drawX, drawY), frame,
					drawInfo.faceColor,
					0f, new Vector2(texture.Width / 10f, frameSize / 2f), 1f,
					drawInfo.spriteEffects, 0);
				Main.playerDrawData.Add(data);
			}
		});
		
		public static readonly PlayerLayer Whiskers = new PlayerLayer("CosmaliaMod", "Whiskers", PlayerLayer.Head, delegate (PlayerDrawInfo drawInfo)
		{
			if (drawInfo.shadow != 0f || drawInfo.drawPlayer.dead)
			{
				return;
			}
			Player drawPlayer = drawInfo.drawPlayer;
			Mod mod = ModLoader.GetMod("CosmaliaMod");
			CosmaliaPlayer modPlayer = drawPlayer.GetModPlayer<CosmaliaPlayer>();
			Rectangle frame = drawPlayer.bodyFrame;
			Texture2D texture;
			if (modPlayer.whiskers)
			{
				texture = mod.GetTexture("Items/Misc/Whiskers/sniffWhiskers");

				int frameSize = texture.Height / 20;
				frame.Width = 40;
				frame.X = 160;
				int drawX = (int)(drawInfo.position.X + drawPlayer.width / 2f - Main.screenPosition.X);
				int drawY = (int)(drawInfo.position.Y + drawPlayer.height / 2f - Main.screenPosition.Y - 3);
				DrawData data = new DrawData(texture, new Vector2(drawX, drawY), frame,
					Lighting.GetColor((int)((drawInfo.position.X + drawPlayer.width / 2f) / 16f),
						(int)((drawInfo.position.Y + drawPlayer.height / 2f) / 16f)),
					0f, new Vector2(texture.Width / 10f, frameSize / 2f), 1f,
					drawInfo.spriteEffects, 0);
				data.shader = drawInfo.headArmorShader;
				Main.playerDrawData.Add(data);
			}
			if (modPlayer.sniffing)
			{
				texture = mod.GetTexture("Items/Misc/Whiskers/sniffWhiskers");

				int frameSize = texture.Height / 20;
				frame.Width = 40;
				frame.X = (int)modPlayer.sniffFrame * 40;
				int drawX = (int)(drawInfo.position.X + drawPlayer.width / 2f - Main.screenPosition.X);
				int drawY = (int)(drawInfo.position.Y + drawPlayer.height / 2f - Main.screenPosition.Y - 3);
				DrawData data = new DrawData(texture, new Vector2(drawX, drawY), frame,
					Lighting.GetColor((int)((drawInfo.position.X + drawPlayer.width / 2f) / 16f),
						(int)((drawInfo.position.Y + drawPlayer.height / 2f) / 16f)),
					0f, new Vector2(texture.Width / 10f, frameSize / 2f), 1f,
					drawInfo.spriteEffects, 0);
				data.shader = drawInfo.headArmorShader;
				Main.playerDrawData.Add(data);
			}
		});
		
		#endregion 
		
		#region Race PlayerLayers
		
		public static readonly PlayerLayer REars = new PlayerLayer("CosmaliaMod", "REars", PlayerLayer.Head, delegate (PlayerDrawInfo drawInfo)
		{
			if (drawInfo.shadow != 0f || drawInfo.drawPlayer.dead)
			{
				return;
			}
			Player drawPlayer = drawInfo.drawPlayer;
			Mod mod = ModLoader.GetMod("CosmaliaMod");
			Elf elf = drawPlayer.GetModPlayer<Elf>();
			Longtail longtail = drawPlayer.GetModPlayer<Races.Longtail.Longtail>();
			Rectangle? frame = drawPlayer.bodyFrame;
			Texture2D texture;
			if (elf.isRace)
			{
				texture = mod.GetTexture("Races/Elf/ElfEar");

				int frameSize = texture.Height / 20;
				int drawX = (int)(drawInfo.position.X + drawPlayer.width / 2f - Main.screenPosition.X);
				int drawY = (int)(drawInfo.position.Y + drawPlayer.height / 2f - Main.screenPosition.Y - 3);
				DrawData data = new DrawData(texture, new Vector2(drawX, drawY), frame,
					drawInfo.faceColor,
					0f, new Vector2(texture.Width / 2f, frameSize / 2f), 1f,
					drawInfo.spriteEffects, 0);
				Main.playerDrawData.Add(data);
			}
			if (longtail.isRace)
			{
				texture = mod.GetTexture("Races/Longtail/Ear");

				int frameSize = texture.Height / 20;
				int drawX = (int)(drawInfo.position.X + drawPlayer.width / 2f - Main.screenPosition.X);
				int drawY = (int)(drawInfo.position.Y + drawPlayer.height / 2f - Main.screenPosition.Y - 3);
				DrawData data = new DrawData(texture, new Vector2(drawX, drawY), frame,
					drawInfo.faceColor,
					0f, new Vector2(texture.Width / 2f, frameSize / 2f), 1f,
					drawInfo.spriteEffects, 0);
				Main.playerDrawData.Add(data);
			}
		});
		
		public static readonly PlayerLayer RFace = new PlayerLayer("CosmaliaMod", "RFace", PlayerLayer.Head, delegate (PlayerDrawInfo drawInfo)
		{
			if (drawInfo.shadow != 0f || drawInfo.drawPlayer.dead)
			{
				return;
			}
			Player drawPlayer = drawInfo.drawPlayer;
			Mod mod = ModLoader.GetMod("CosmaliaMod");
			Elf elf = drawPlayer.GetModPlayer<Elf>();
			CosmaliaPlayer modPlayer = drawPlayer.GetModPlayer<CosmaliaPlayer>();
			Rectangle? frame = drawPlayer.bodyFrame;
			Texture2D texture;
			if (elf.isRace)
			{
				//texture = elf.head;
				texture = mod.GetTexture("Races/Elf/ElfFace");

				int frameSize = texture.Height / 20;
				int drawX = (int)(drawInfo.position.X + drawPlayer.width / 2f - Main.screenPosition.X);
				int drawY = (int)(drawInfo.position.Y + drawPlayer.height / 2f - Main.screenPosition.Y - 3);
				DrawData data = new DrawData(texture, new Vector2(drawX, drawY), frame,
					drawInfo.faceColor,
					0f, new Vector2(texture.Width / 2f, frameSize / 2f), 1f,
					drawInfo.spriteEffects, 0);
				Main.playerDrawData.Add(data);
			}
		});

		public static readonly PlayerLayer RHeadFront = new PlayerLayer("CosmaliaMod", "RHeadFront", PlayerLayer.Head, delegate (PlayerDrawInfo drawInfo)
		{
			if (drawInfo.shadow != 0f || drawInfo.drawPlayer.dead)
			{
				return;
			}
			Player drawPlayer = drawInfo.drawPlayer;
			Mod mod = ModLoader.GetMod("CosmaliaMod");
			Ram ram = drawPlayer.GetModPlayer<Ram>();
			CosmaliaPlayer modPlayer = drawPlayer.GetModPlayer<CosmaliaPlayer>();
			Rectangle? frame = drawPlayer.bodyFrame;
			Texture2D texture;
			if (ram.isRace)
			{
				texture = mod.GetTexture("Races/Ram/HornFront");

				int frameSize = texture.Height / 20;
				int drawX = (int)(drawInfo.position.X + drawPlayer.width / 2f - Main.screenPosition.X);
				int drawY = (int)(drawInfo.position.Y + drawPlayer.height / 2f - Main.screenPosition.Y - 3);
				DrawData data = new DrawData(texture, new Vector2(drawX, drawY), frame,
					Color.Lerp(Lighting.GetColor((int)((drawInfo.position.X + drawPlayer.width / 2f) / 16f),
						(int)((drawInfo.position.Y + drawPlayer.height / 2f) / 16f)), modPlayer.hornColor, .5f),
					drawPlayer.headRotation, new Vector2(texture.Width / 2f, frameSize / 2f), 1f,
					drawInfo.spriteEffects, 0);
				data.shader = drawInfo.headArmorShader;

				if (drawPlayer.dashDelay > 0)
				{
					//data.position.X += 3;
				}
				Main.playerDrawData.Add(data);
			}
		});

		public static readonly PlayerLayer RHeadBack = new PlayerLayer("CosmaliaMod", "RHeadBack", PlayerLayer.Head, delegate (PlayerDrawInfo drawInfo)
		{
			if (drawInfo.shadow != 0f || drawInfo.drawPlayer.dead)
			{
				return;
			}
			Player drawPlayer = drawInfo.drawPlayer;
			Mod mod = ModLoader.GetMod("CosmaliaMod");
			Ram ram = drawPlayer.GetModPlayer<Races.Ram.Ram>();
			CosmaliaPlayer modPlayer = drawPlayer.GetModPlayer<CosmaliaPlayer>();
			Rectangle? frame = drawPlayer.bodyFrame;
			Texture2D texture;
			if (ram.isRace)
			{
				// texture = ram.headBack;
				texture = mod.GetTexture("Races/Ram/HornBack");

				int frameSize = texture.Height / 20;
				int drawX = (int)(drawInfo.position.X + drawPlayer.width / 2f - Main.screenPosition.X);
				int drawY = (int)(drawInfo.position.Y + drawPlayer.height / 2f - Main.screenPosition.Y - 3);
				DrawData data = new DrawData(texture, new Vector2(drawX, drawY), frame,
					Color.Lerp(Lighting.GetColor((int)((drawInfo.position.X + drawPlayer.width / 2f) / 16f),
						(int)((drawInfo.position.Y + drawPlayer.height / 2f) / 16f)), modPlayer.hornColor, .5f),
					drawPlayer.headRotation, new Vector2(texture.Width / 2f, frameSize / 2f), 1f,
					drawInfo.spriteEffects, 0);
				data.shader = drawInfo.headArmorShader;

				if (drawPlayer.dashDelay > 0)
				{
					//data.position.X += 3;
				}
				Main.playerDrawData.Add(data);
			}
		});

		public static readonly PlayerLayer RTail = new PlayerLayer("CosmaliaMod", "RTail", PlayerLayer.BackAcc, delegate (PlayerDrawInfo drawInfo)
		{
			if (drawInfo.shadow != 0f || drawInfo.drawPlayer.dead)
			{
				return;
			}
			Player drawPlayer = drawInfo.drawPlayer;
			Mod mod = ModLoader.GetMod("CosmaliaMod");
			Longtail longtail = drawPlayer.GetModPlayer<Longtail>();
			Ram ram = drawPlayer.GetModPlayer<Races.Ram.Ram>();
			CosmaliaPlayer modPlayer = drawPlayer.GetModPlayer<CosmaliaPlayer>();
			Rectangle? frame = drawPlayer.bodyFrame;
			Texture2D texture;

			if (longtail.isRace)
			{
				DrawData data = (DrawData)longtail.DrawTail(drawInfo);
				Main.playerDrawData.Add(data);
			}

			if (ram.isRace)
			{
				// texture = ram.headBack;
				texture = mod.GetTexture("Races/Ram/Tail");

				int frameSize = texture.Height / 20;
				int drawX = (int)(drawInfo.position.X + drawPlayer.width / 2f - Main.screenPosition.X);
				int drawY = (int)(drawInfo.position.Y + drawPlayer.height / 2f - Main.screenPosition.Y - 3);
				DrawData data = new DrawData(texture, new Vector2(drawX, drawY), frame,
					Color.Lerp(Lighting.GetColor((int)((drawInfo.position.X + drawPlayer.width / 2f) / 16f),
						(int)((drawInfo.position.Y + drawPlayer.height / 2f) / 16f)), modPlayer.hornColor, .5f),
					drawPlayer.headRotation, new Vector2(texture.Width / 2f, frameSize / 2f), 1f,
					drawInfo.spriteEffects, 0);
				data.shader = drawInfo.headArmorShader;
				Main.playerDrawData.Add(data);
			}
		});

		public static readonly PlayerLayer RClawsFront = new PlayerLayer("CosmaliaMod", "RClawsFront", PlayerLayer.Arms, delegate (PlayerDrawInfo drawInfo)
		{
			if (drawInfo.shadow != 0f || drawInfo.drawPlayer.dead)
			{
				return;
			}
			Player drawPlayer = drawInfo.drawPlayer;
			Mod mod = ModLoader.GetMod("CosmaliaMod");
			Longtail longtail = drawPlayer.GetModPlayer<Longtail>();
			CosmaliaPlayer modPlayer = drawPlayer.GetModPlayer<CosmaliaPlayer>();
			Rectangle? frame = drawPlayer.bodyFrame;
			Texture2D texture;
			if (longtail.isRace)
			{
				//texture = longtail.clawsFront;
				texture = mod.GetTexture("Races/Longtail/clawsFront");

				int frameSize = texture.Height / 20;
				int drawX = (int)(drawInfo.position.X + drawPlayer.width / 2f - Main.screenPosition.X);
				int drawY = (int)(drawInfo.position.Y + drawPlayer.height / 2f - Main.screenPosition.Y - 3);
				DrawData data = new DrawData(texture, new Vector2(drawX, drawY), frame,
					Color.Lerp(Lighting.GetColor((int)((drawInfo.position.X + drawPlayer.width / 2f) / 16f),
						(int)((drawInfo.position.Y + drawPlayer.height / 2f) / 16f)), modPlayer.clawColor, .5f),
					0f, new Vector2(texture.Width / 2f, frameSize / 2f), 1f,
					drawInfo.spriteEffects, 0);
				data.shader = drawInfo.bodyArmorShader;
				Main.playerDrawData.Add(data);
			}
		});

		public static readonly PlayerLayer RClawsBack = new PlayerLayer("CosmaliaMod", "RClawsBack", PlayerLayer.Body, delegate (PlayerDrawInfo drawInfo)
		{
			if (drawInfo.shadow != 0f || drawInfo.drawPlayer.dead)
			{
				return;
			}
			Player drawPlayer = drawInfo.drawPlayer;
			Mod mod = ModLoader.GetMod("CosmaliaMod");
			Longtail longtail = drawPlayer.GetModPlayer<Longtail>();
			CosmaliaPlayer modPlayer = drawPlayer.GetModPlayer<CosmaliaPlayer>();
			Rectangle? frame = drawPlayer.bodyFrame;
			Texture2D texture;
			if (longtail.isRace)
			{
				//texture = longtail.clawsBack;
				texture = mod.GetTexture("Races/Longtail/clawsBack");

				int frameSize = texture.Height / 20;
				int drawX = (int)(drawInfo.position.X + drawPlayer.width / 2f - Main.screenPosition.X);
				int drawY = (int)(drawInfo.position.Y + drawPlayer.height / 2f - Main.screenPosition.Y - 3);
				DrawData data = new DrawData(texture, new Vector2(drawX, drawY), frame,
					Color.Lerp(Lighting.GetColor((int)((drawInfo.position.X + drawPlayer.width / 2f) / 16f),
						(int)((drawInfo.position.Y + drawPlayer.height / 2f) / 16f)), modPlayer.clawColor, .5f),
					0f, new Vector2(texture.Width / 2f, frameSize / 2f), 1f,
					drawInfo.spriteEffects, 0);
				data.shader = drawInfo.bodyArmorShader;
				Main.playerDrawData.Add(data);
			}
		});

		#endregion

		//5
		public override void ModifyDrawLayers(List<PlayerLayer> layers)
		{
			int headIndex = layers.IndexOf(PlayerLayer.Head);
			if (player.GetModPlayer<Races.Elf.Elf>().isRace)
			{
				layers.Insert(headIndex + 1, REars);
				layers.Insert(headIndex - 1, RFace);
			}
			if (player.GetModPlayer<Races.Ram.Ram>().isRace)
			{
				int faceIndex = layers.IndexOf(PlayerLayer.Face);
				layers.Insert(headIndex + 1, RHeadFront);
				layers.Insert(faceIndex - 1, RHeadBack);
			}
			if (player.GetModPlayer<Races.Longtail.Longtail>().isRace)
			{
				int bodyIndex = layers.IndexOf(PlayerLayer.Body);
				int bodyIndex2 = layers.IndexOf(PlayerLayer.Wings);
				int armIndex = layers.IndexOf(PlayerLayer.Arms);
				layers.Insert(headIndex + 1, REars);
				layers.Insert(bodyIndex + 1, RClawsBack);
				layers.Add(RClawsFront);
				layers.Insert(bodyIndex2 - 1, RTail);
			}

			/*
			if (arbHornV)
			{
				//int faceIndex = layers.IndexOf(PlayerLayer.Face);
				layers.Insert(headIndex + 1, Ears);
				layers.Insert(headIndex - 1, HeadBack);
				//layers.IndexOf(PlayerLayer.Hair) + 1
			}
			
			if (arbMaskV)
			{
				int faceIndex = layers.IndexOf(PlayerLayer.Face);
				layers.Insert(faceIndex + 1, HeadBack);
			}
			
			if (arbLegs)
			{
				int legsIndex = layers.IndexOf(PlayerLayer.Body);
				layers.Insert(legsIndex + 1, Legs2);
				PlayerLayer.Legs.visible = false;
			}
			
			if (whiskers)
			{
				HeadAlt.visible = false;
				if (sniffing)
				{
					if (!sniffEnd)
					{
						//PlayerLayer.Face.visible = false;
					}
					else
					{
						sniffEnd = false;
					}
					PlayerLayer.Face.visible = false;
					layers.Insert(headIndex, EyeAlt);
					layers.Insert(headIndex, EyeWAlt);
					HeadAlt.visible = true;
					layers.Insert(layers.IndexOf(PlayerLayer.Face), HeadAlt);
				}
				layers.Insert(headIndex, Whiskers);
			}
			*/
		}
	}
}




