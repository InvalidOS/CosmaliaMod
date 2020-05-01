/// 11 pixels back
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CosmaliaMod.Items.Arborite
{
	public class ArboriteCleaver : ModItem
	{
		public override void SetStaticDefaults()
		{
			//Tooltip.SetDefault("Left clicking causes you to grow roots");
		}
		public override void SetDefaults()
		{
			item.maxStack = 1;
			item.value = 1000;
			item.rare = 11;
			item.melee = true;
			item.useTurn = true;
			item.autoReuse = true;
			item.height = 72;
			item.width = 50;
			item.useStyle = 1;
			item.useAnimation = 30;
			item.useTime = 30;
			item.damage = 70;
			item.knockBack = 20;
			item.shootSpeed = 4f;
			//item.shoot = mod.ProjectileType("CursedSword");
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.GetItem("ArboriteBar"), 20);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		
		public override void HoldStyle(Player player)
		{
			//player.itemLocation.X = -24f;
			//player.itemLocation.Y = 4f;
		}
	}
}







