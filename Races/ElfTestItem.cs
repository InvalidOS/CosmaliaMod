using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.ModLoader;

namespace CosmaliaMod.Races
{
	public class ElfTestItem : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Switches you from human to elf and vice-versa");
		}

		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.LifeFruit);
			item.maxStack = 1;
			item.value = 0;
			item.rare = 10;
		}

		//public override bool CanUseItem(Player player)
		//{
		//	return !player.GetModPlayer<RacePlayer>().raceSelected;
		//}

		public override bool UseItem(Player player)
		{
			if (player.GetModPlayer<Races.Elf>().isRace)
			{
				player.GetModPlayer<Races.Elf>().isRace = false;
			}
			else
			{
				player.GetModPlayer<Races.Elf>().isRace = true;
			}
			
			//player.GetModPlayer<RacePlayer>().raceSelected = true;
			/// I may remove this in the future.
			//player.name = RaceRenamers.ElfRename(player.name);
			return true;
		}
	}
}