using Terraria;
using Terraria.ModLoader;

namespace CosmaliaMod.Buffs
{
	public class Instinct : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Wild Instinct");
			Description.SetDefault("Your instincts have taken over");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<CosmaliaPlayer>().intelligence -= 40;
			player.meleeDamage *= 1.5f;
		}
	}
}
