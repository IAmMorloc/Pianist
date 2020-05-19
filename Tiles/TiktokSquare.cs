using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Pianist.Tiles
{
	class TiktokSquare : ModTile, IInstrument
	{
		public override void SetDefaults()
		{
			Main.tileLighted[Type] = true;
			Main.tileFrameImportant[Type] = true; //是否有边框。关闭时自动调整sprite。
												  //Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = true;
			drop = ItemType<Items.TiktokSquare>();
		}
		public override void PlaceInWorld(int i, int j, Item item)
		{
			Tile tile = Main.tile[i, j];
			tile.frameY = 0;
			if (Main.netMode == NetmodeID.MultiplayerClient)
			{
				NetMessage.SendTileSquare(-1, Player.tileTargetX, Player.tileTargetY, 1, TileChangeType.None);
			}
		}

		public override bool NewRightClick(int i, int j)
		{
			int encPitch = GetPitch(i, j);
			//string soundsFileName = "piano_" + (encPitch & 15) + "-1";
			float offset = 0f;
			switch (encPitch >> 4) {
				case 3:
					offset = -1f; break;
				case 4:
					offset = 0f; break;
				case 5:
					offset = 1f; break;
			}
			Main.PlaySound(SoundLoader.customSoundType, i * 16 + 8, j * 16, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/a-1"), 1f, offset);
			return base.NewRightClick(i, j);
		}

		public override void MouseOver(int i, int j)
		{
			int encPitch = GetPitch(i, j);
			Main.NewText("GetPitch now Octave:" + (encPitch >> 4) + " Name:" +(encPitch & 15));
			base.MouseOver(i, j);
		}

		public bool PitchOffset(int i, int j)
		{
			Tile tile = Main.tile[i, j];
			int styleX = tile.frameX / 18;
			int styleY = tile.frameY / 18;

			int nextStyleX = styleX < 11 ? styleX + 1 : 0;
			int nextStyleY = nextStyleX == 0 ? (styleY < 2 ? styleY + 1 : 0) : styleY;
			tile.frameX = (short)(nextStyleX * 18);
			tile.frameY = (short)(nextStyleY * 18);
			if (Main.netMode == NetmodeID.MultiplayerClient)
			{
				NetMessage.SendTileSquare(-1, Player.tileTargetX, Player.tileTargetY, 1, TileChangeType.None);
			}
			Dust.NewDust(new Vector2(i * 16, j * 16), 10, 10, 7);
			return true;
		}

		public int GetPitch(int i, int j)
		{
			Tile tile = Main.tile[i, j];
			int styleX = tile.frameX / 18;
			int styleY = tile.frameY / 18;

			return ((styleY + 3) << 4) | (styleX + 1);
		}
	}
}
