using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace CosmaliaMod.Items.Arborite
{
	[AutoloadEquip(EquipType.Legs)]
	public class ArboriteLegs : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Stand high, young one...stand against the ones who threaten all."
			+ "\n+30% movement speed"
			+ "\n+150% jump speed"
			+ "\n+250% jump height"
			+ "\n-40 max life");
		}
		public override void SetDefaults()
		{
			item.maxStack = 1;
			item.value = 10000;
			item.rare = 11;
			item.defense = 5;
		}
		public override void UpdateEquip(Player player)
		{
			Player.jumpHeight = (int)(Player.jumpHeight * 2.5f);
			Player.jumpSpeed *= 1.5f;
			player.moveSpeed *= 1.3f;
			player.statLifeMax2 -= 40;
			player.GetModPlayer<CosmaliaPlayer>().arbLegs = true;
		}
		
		public override void UpdateVanity(Player player, EquipType type)
		{
			player.GetModPlayer<CosmaliaPlayer>().arbLegs = true;
		}
		
		//public override bool IsArmorSet(Item head, Item body, Item legs)
		//{
			//return head.type == mod.ItemType("RKHelm") && body.type == mod.ItemType("RKChest") && legs.type == mod.ItemType("RKLegs");
		//}
		
		public override bool DrawLegs()
		{
			return false;
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.GetItem("ArboriteBar"), 15);
			recipe.AddIngredient(ItemID.Leather, 10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
