using Terraria.ModLoader;
using Terraria.ID;

namespace CosmaliaMod.Items.Potions
{
    public class InstinctPotion : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Makes your instincts take hold, increasing melee damage, but reducing intelligence.");
        }
        public override void SetDefaults()
        {
            item.useStyle = ItemUseStyleID.EatingUsing;
            item.useAnimation = 15;
            item.useTime = 15;
            item.useTurn = true;
            item.UseSound = SoundID.Item3;
            item.maxStack = 30;
            item.consumable = true;
            item.rare = 3;
            item.value = 10000;
            item.buffType = ModContent.BuffType<Buffs.Instinct>();
            item.buffTime = 25200;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddIngredient(ItemID.Deathweed);
            recipe.AddIngredient(ItemID.SharkFin);
            recipe.AddIngredient(mod.GetItem("FurPelt"), 1);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}