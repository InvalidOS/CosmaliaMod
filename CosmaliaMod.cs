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
using Terraria.UI;
using Terraria.ModLoader;
using Terraria.GameInput;
using Terraria.ModLoader.IO;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using CosmaliaMod.GUI;
using MonoMod.Cil;
using Mono.Cecil.Cil;
using CosmaliaMod.Races;
using CosmaliaMod.Races.Ram;
using CosmaliaMod.Races.Longtail;


namespace CosmaliaMod
{
	public class CosmaliaMod : Mod
	{
		public CosmaliaMod()
		{
		}
		
		public override void Load()
		{

			// All of this loading needs to be client-side.
	  
			if (Main.netMode != NetmodeID.Server)
			{
				// First, you load in your shader file.
				// You'll have to do this regardless of what kind of shader it is,
				// and you'll have to do it for every shader file.
				// This example assumes you have both armour and screen shaders.

				/// Ref<Effect> dyeRef = new Ref<Effect>(GetEffect("Effects/MyDyes"));
				/// Ref<Effect> specialRef = new Ref<Effect>(GetEffect("Effects/MySpecials"));
				Ref<Effect> filterRef = new Ref<Effect>(GetEffect("Effects/IntelligenceBlur"));
				Ref<Effect> filterRef2 = new Ref<Effect>(GetEffect("Effects/IntelligenceBlur2"));

				// To add a dye, simply add this for every dye you want to add.
				// "PassName" should correspond to the name of your pass within the *technique*,
				// so if you get an error here, make sure you've spelled it right across your effect file.

				//GameShaders.Armor.BindShader(ItemType<MyDyeItem>(), new ArmorShaderData(dyeRef, "PassName"));

				// If your dye takes specific parameters such as colour, you can append them after binding the shader.
				// IntelliSense should be able to help you out here.	  

				/// GameShaders.Armor.BindShader(ItemType<MyColourDyeItem>(), new ArmorShaderData(dyeRef, "ColourPass")).UseColor(1.5f, 0.15f, 0f);
				/// GameShaders.Armor.BindShader(ItemType<MyNoiseDyeItem>(), new ArmorShaderData(dyeRef, "NoisePass")).UseImage("Images/Misc/noise"); // Uses the default Terraria noise map.

				// To bind a miscellaneous, non-filter effect, use this.
				// If you're actually using this, you probably already know what you're doing anyway.  

				/// GameShaders.Misc["EffectName"] = new MiscShaderData(specialref, "PassName");  

				// To bind a screen shader, use this.
				// EffectPriority should be set to whatever you think is reasonable.	  

				Filters.Scene["IntelligenceBlur"] = new Filter(new ScreenShaderData(filterRef, "IntelligenceBlur"), EffectPriority.High);
				Filters.Scene["IntelligenceBlur"].Load();
				Filters.Scene["IntelligenceBlur2"] = new Filter(new ScreenShaderData(filterRef2, "IntelligenceBlur2"), EffectPriority.High);
				Filters.Scene["IntelligenceBlur2"].Load();

				On.Terraria.Main.DoUpdate += UpdateRaceMenu;
				IL.Terraria.Main.DrawMenu += RaceMenuAttach;
			}
		}

		public override void Unload()
		{
			//On.Terraria.Main.DoUpdate -= UpdateRaceMenu;
			//IL.Terraria.Main.DrawMenu -= RaceMenuAttach;
		}

		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
			CosmaliaPlayer modPlayer = Main.player[Main.myPlayer].GetModPlayer<CosmaliaPlayer>();
			if (modPlayer.intelligence <= 85)
			{
				for (int i = 0; i < layers.Count; i++)
				{
					if (layers[i].Name == ("Vanilla: Entity Health Bars") /*| layers[i].Name == ("Vanilla: Mouse Text")*/)
					{
						layers.RemoveAt(i);
					}
				}
			}
			/*if (modPlayer.intelligence <= 30)
			{
				for (int i = 0; i < layers.Count; i++)
				{
					if (layers[i].Name == ("Vanilla: Emote Bubbles"))
					{
						layers.RemoveAt(i);
					}
				}
			}*/
		}

		#region IL_edits

		/// Character customization menu for races

		private void RaceMenuAttach(ILContext il)
        {
            ILCursor c = new ILCursor(il);
            c.GotoNext(n => n.MatchLdsfld<Main>("menuMode") && n.Next.MatchLdcI4(2));
            c.Index++;

            c.EmitDelegate<RaceMenuDelegate>(EmitRaceDel);
        }
        private delegate void RaceMenuDelegate();
        private RaceMenu raceMenu = new RaceMenu();
        private UserInterface raceMenuUI = new UserInterface();
        private void EmitRaceDel()
        {
            if (Main.menuMode == 2 || RaceMenu.visible)
            {
                if (!RaceMenu.created)
                {
                    raceMenu = new RaceMenu();
                    raceMenu.OnInitialize();
                    raceMenu.race = Main.PendingPlayer.GetModPlayer<CosmaliaPlayer>();
					raceMenu.elf = Main.PendingPlayer.GetModPlayer<Elf>();
					raceMenu.ram = Main.PendingPlayer.GetModPlayer<Ram>();
					raceMenu.longtail = Main.PendingPlayer.GetModPlayer<Longtail>();
					raceMenu.player = Main.PendingPlayer;
                    RaceMenu.created = true;

                    raceMenuUI = new UserInterface();
                    raceMenuUI.SetState(raceMenu);
                }
                SpriteBatch spriteBatch = Main.spriteBatch;

                if (raceMenu != null && raceMenuUI != null)
                {
                    raceMenu.Draw(spriteBatch);                                   
                }

            }
            else
            {
                RaceMenu.created = false;
                raceMenu = null;
                raceMenuUI = null;
            }
        }
		private void UpdateRaceMenu(On.Terraria.Main.orig_DoUpdate orig, Terraria.Main self, GameTime gameTime)
		{
			if (raceMenuUI != null)
			{
				raceMenuUI.Update(gameTime);
			}
			orig(self, gameTime);
		}

		#endregion
	}
}