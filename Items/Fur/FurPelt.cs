using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace CosmaliaMod.Items.Fur
{
	public class FurPelt : ModItem
	{
		public override void SetStaticDefaults()
		{
			ItemID.Sets.SortingPriorityMaterials[item.type] = 59;
		}

		public override void SetDefaults()
		{
			item.maxStack = 999;
			//item.consumable = true;
			//item.createTile = mod.TileType("ArboriteOre");
			//item.width = 12;
			//item.height = 12;
			item.value = 5;
		}
	}
}
