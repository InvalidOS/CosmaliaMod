using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace CosmaliaMod.Items.Fur
{
	[AutoloadEquip(EquipType.Body)]
	public class FurShirt : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Nice, fluffy, and warm."
			+ "\n+20 mana"
			+ "\n+5% melee speed");
		}
		public override void SetDefaults()
		{
			item.maxStack = 1;
			item.value = 75;
			item.rare = 11;
			item.defense = 5;
		}
		public override void UpdateEquip(Player player)
		{
			player.statManaMax2 += 20;
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
