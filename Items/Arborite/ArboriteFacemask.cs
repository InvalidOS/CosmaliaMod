using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace CosmaliaMod.Items.Arborite
{
	[AutoloadEquip(EquipType.Head)]
	public class ArboriteFacemask : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Just wearing it makes you love nature more than you ever have..."
			+ "\n+10% ranged crit chance"
			+ "\n+1 minion"
			+ "\n+20% ranged and minion damage"
			+ "\n-20 max life"
			+ "\n+10 intelligence");
		}
		public override void SetDefaults()
		{
			item.maxStack = 1;
			item.value = 10000;
			item.rare = 11;
			item.defense = 15;
		}
		public override void UpdateEquip(Player player)
		{
			player.rangedDamage *= 1.1f;
			player.rangedCrit += (int)(player.rangedCrit * 1.1f);
			player.minionDamage *= 1.1f;
			player.maxMinions += 2;
			player.statLifeMax2 -= 20;
			player.GetModPlayer<CosmaliaPlayer>().intelligence += 10;
		}
		
		public override void UpdateVanity(Player player, EquipType type)
		{
			player.GetModPlayer<CosmaliaPlayer>().arbMaskV = true;
		}
		
		//public override bool IsArmorSet(Item head, Item body, Item legs)
		//{
			//return head.type == mod.ItemType("RKHelm") && body.type == mod.ItemType("RKChest") && legs.type == mod.ItemType("RKLegs");
		//}
		
		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.GetItem("ArboriteBar"), 20);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
