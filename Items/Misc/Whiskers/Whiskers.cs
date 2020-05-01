using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CosmaliaMod.Items.Misc.Whiskers
{
	[AutoloadEquip(EquipType.Head)]
	public class Whiskers : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("NPCs percieve you as cuter, charging reduced prices"
				+ "\n-45 intelligence");
		}
		
		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 30;
			item.value = 10000;
			item.rare = 13;
			item.accessory = true;
		}
		
		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = true;
		}
		
		public override void UpdateEquip(Player player)
		{
			//player.GetModPlayer<CosmaliaPlayer>().whiskers2 = true;
			player.GetModPlayer<CosmaliaPlayer>().intelligence -= 45;
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			//if (!hideVisual)
			//{				
			//	player.GetModPlayer<CosmaliaPlayer>().whiskers = true;
			//}
			
			player.GetModPlayer<CosmaliaPlayer>().intelligence -= 45;
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Cobweb, 5);
			//recipe.AddTile(TileID.Workbench);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}



