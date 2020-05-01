using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace CosmaliaMod.Items.Fur
{
	[AutoloadEquip(EquipType.Head)]
	public class FurCowl : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Let the spirits guide you."
			+ "\n+10% magic damage"
			+ "\n-10 max life"
			+ "\n+20 intelligence");
		}
		public override void SetDefaults()
		{
			item.maxStack = 1;
			item.value = 50;
			item.rare = 11;
			item.defense = 2;
		}

		public override void UpdateEquip(Player player)
		{
			player.magicDamage *= 1.1f;
			player.statLifeMax2 -= 10;
			player.GetModPlayer<CosmaliaPlayer>().intelligence += 20;
		}
		
		
		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return head.type == mod.ItemType("FurCowl") && body.type == mod.ItemType("FurShirt") && legs.type == mod.ItemType("FurLegs");
		}
		
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.GetItem("FurPelt"), 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
