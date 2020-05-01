using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.World.Generation;

namespace CosmaliaMod
{
	public class CosmaliaWorld : ModWorld
	{
		public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
		{
			int ShiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));
			if (ShiniesIndex != -1) {
				tasks.Insert(ShiniesIndex + 1, new PassLegacy("Generating Ores...", Ores));
			}
		}
		
		private void Ores(GenerationProgress progress)
		{
			progress.Message = "Generating Arborite...";
			
			for (int k = 0; k < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 6E-05); k++) {
				int x = WorldGen.genRand.Next(0, Main.maxTilesX);
				int y = WorldGen.genRand.Next((int)WorldGen.worldSurfaceLow, Main.maxTilesY);

				Tile tile = Framing.GetTileSafely(x, y);
				if (tile.active() && tile.type == TileID.Mud)
				{
					WorldGen.TileRunner(x, y, (double)WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(2, 6), mod.TileType("ArboriteOre"), false, 0f, 0f, false, true);
				}
			}
		}
	}
}







