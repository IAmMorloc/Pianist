using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Pianist.Tiles
{
    class Amplifier : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileObsidianKill[Type] = true;

			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
			TileObjectData.addTile(Type);
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Amplifier");
			AddMapEntry(new Color(144, 148, 144), name);
			dustType = 11;
			disableSmartCursor = true;
		}

		public override bool NewRightClick(int i, int j)
		{
			return base.NewRightClick(i, j);
		}
	}
}
