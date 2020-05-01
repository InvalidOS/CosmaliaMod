using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CosmaliaMod.Items.Fur
{
	public class FurEnchantment : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Your intelligence and magic damage are reduced, while your melee speed, damage, and mana consumption are increased"
				+ "\nThese invert when below 50% max health");
		}

		public override void SetDefaults()
		{
			item.value = 1000;
			item.rare = 13;
			item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<Players.FargoPlayer>().furEnch = true;

			if (player.statLife >= player.statLifeMax2 / 2)
			{
				player.magicDamage *= 0.8f;
				player.GetModPlayer<CosmaliaPlayer>().intelligence -= 40;
				player.meleeSpeed *= 1.2f;
				player.meleeDamage *= 1.2f;
				player.manaCost *= 1.2f;
			}
			else
			{
				player.magicDamage *= 1.2f;
				player.GetModPlayer<CosmaliaPlayer>().intelligence += 40;
				player.meleeSpeed *= 0.8f;
				player.meleeDamage *= 0.8f;
				player.manaCost *= 0.8f;
			}
		}

		public override void AddRecipes()
		{
			Mod ech = ModLoader.GetMod("FargowiltasSouls");
			if (ech != null)
			{
				ModRecipe recipe = new ModRecipe(mod);
				recipe.AddIngredient(mod.GetItem("FurCowl"));
				recipe.AddIngredient(mod.GetItem("FurShirt"));
				recipe.AddIngredient(mod.GetItem("FurPants"));
				recipe.AddIngredient(mod.GetItem("Whiskers"));
				recipe.AddTile(TileID.Anvils);
				recipe.SetResult(this);
				recipe.AddRecipe();
			}
		}
	}
}