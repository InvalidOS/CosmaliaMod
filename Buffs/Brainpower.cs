using Terraria;
using Terraria.ModLoader;

namespace CosmaliaMod.Buffs
{
	public class Brainpower : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Brainpower");
			Description.SetDefault("You are more intelligent");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<CosmaliaPlayer>().intelligence += 40;
		}
	}
}
