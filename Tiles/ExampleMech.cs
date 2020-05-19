using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Terraria.ModLoader.ModContent;

namespace Pianist.Tiles
{
    class ExampleMech : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileObsidianKill[Type] = true;

			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
			TileObjectData.addTile(Type);
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Example Mech");
			AddMapEntry(new Color(144, 148, 144), name);
			dustType = 11;
			disableSmartCursor = true;
		}

		public override void HitWire(int i, int j)
		{
			// Find the coordinates of top left tile square through math
			int y = j - Main.tile[i, j].frameY / 18;
			int x = i - Main.tile[i, j].frameX / 18;

			Wiring.SkipWire(x, y);

			// We add 16 to x to spawn right between the 2 tiles. We also want to right on the ground in the y direction.
			int spawnX = x * 16 + 8;
			int spawnY = y * 16;

			// If you want to make a NPC spawning statue, see below.
			if (Wiring.CheckMech(x, y, 60) && Item.MechSpawn(spawnX, spawnY, ItemID.SilverCoin) && Item.MechSpawn(spawnX, spawnY, ItemID.GoldCoin) && Item.MechSpawn(spawnX, spawnY, ItemID.PlatinumCoin))
			{
				int id = ItemID.SilverCoin;
				if (Main.rand.NextBool(100))
				{
					id++;
					if (Main.rand.NextBool(100))
					{
						id++;
					}
				}
				Item.NewItem(spawnX, spawnY, 0, 0, id, 1, false, 0, false);
			}
		}
	}
}
