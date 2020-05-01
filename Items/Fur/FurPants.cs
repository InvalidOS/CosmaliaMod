using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace CosmaliaMod.Items.Fur
{
	[AutoloadEquip(EquipType.Legs)]
	public class FurPants : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("The wilderness calls..."
			+ "\n+50% jump height and speed"
			+ "\n+10% movement speed");
		}
		public override void SetDefaults()
		{
			item.maxStack = 1;
			item.value = 75;
			item.rare = 11;
			item.defense = 3;
		}
		public override void UpdateEquip(Player player)
		{
			Player.jumpHeight = (int)(Player.jumpHeight * 1.5f);
			Player.jumpSpeed *= 1.5f;
			player.moveSpeed *= 1.1f;
		}

		public override void DrawHands(ref bool drawHands, ref bool drawArms)
		{
			drawHands = true;
			drawArms = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.GetItem("FurPelt"), 15);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}