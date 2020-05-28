using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Terraria.ModLoader.ModContent;

namespace Pianist.Tiles
{
    class ExampleMech : ModTile, IInstrument
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style4x2);
			TileObjectData.newTile.Width = 4;
			TileObjectData.newTile.Height = 2;
			TileObjectData.newTile.Direction = TileObjectDirection.None;
			TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16 };
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2; 
			TileObjectData.newTile.Origin = new Point16(2, 1);
			TileObjectData.newTile.StyleHorizontal = false;
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
			TileObjectData.addTile(Type);
		}

		public override bool NewRightClick(int i, int j)
		{
			int soundCode = GetSoundCode(i, j);
			PitchHelper.PlaySound(mod, soundCode, i * 16 + 8, j * 16);
			int dust = Dust.NewDust(new Vector2(i * 16 + 6, j * 16 - 2), 0, 0, DustType<Dusts.MusicalNote>());
			Main.dust[dust].velocity.X = 0f;
			Main.dust[dust].velocity.Y = -1f;
			return base.NewRightClick(i, j);
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 48, 32, ItemType<Items.ExampleMech>());
		}

		public bool PitchOffset(int i, int j)
		{
			return false;
		}

		private readonly int[] MicroKeyboardPitchMap = { 3, 6, 10, 12, 1, 5, 8, 13 };
		public int GetSoundCode(int i, int j)
		{
			Tile tile = Main.tile[i, j];
			int keyIndex = MicroKeyboardPitchMap[tile.frameX / 18 + tile.frameY / 18 * 4];
			if(keyIndex == 13)
			{
				return PitchHelper.EncodeSoundCode((int)EPitchType.Piano, 5, 1);
			}
			return PitchHelper.EncodeSoundCode((int)EPitchType.Piano, 4, keyIndex);
		}
	}
}
