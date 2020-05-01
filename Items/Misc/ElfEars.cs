using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CosmaliaMod.Items.Misc
{
	[AutoloadEquip(EquipType.Head)]
	public class ElfEars : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("+10% magic and ranged damage"
				+ "\n-20% melee and summon damage"
				+ "\n+10 intelligence");
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
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{	
			player.GetModPlayer<CosmaliaPlayer>().intelligence += 10;
			player.magicDamage *= 1.1f;
			player.rangedDamage *= 1.1f;
			player.meleeDamage *= .8f;
			player.minionDamage *= .8f;
		}
		
		public override void DrawArmorColor(Player drawPlayer, float shadow, ref Color color, ref int glowMask, ref Color glowMaskColor)
		{
			color = drawPlayer.GetModPlayer<CosmaliaPlayer>().skin;
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.RottenChunk, 5);
			recipe.AddIngredient(ItemID.SoulofLight, 15);
			//recipe.AddTile(TileID.Workbench);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Vertebrae, 5);
			recipe.AddIngredient(ItemID.SoulofLight, 15);
			//recipe.AddTile(TileID.Workbench);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}