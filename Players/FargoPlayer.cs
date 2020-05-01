using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using CosmaliaMod;
using CosmaliaMod.Races;

namespace CosmaliaMod.Players
{
	public class FargoPlayer : ModPlayer
	{
		public bool furEnch = false;

		public override void ResetEffects()
		{
			furEnch = false;
		}

		public override void PreUpdate()
		{
		}
	}
}