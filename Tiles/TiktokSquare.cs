using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
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
			Main.tileLavaDeath[Type] = true;
			drop = ItemType<Items.TiktokSquare>();
		}
		public override void PlaceInWorld(int i, int j, Item item)
		{
			Tile tile = Main.tile[i, j];
			tile.frameX = 0;
			tile.frameY = 0;
			if (Main.netMode == NetmodeID.MultiplayerClient)
			{
				NetMessage.SendTileSquare(-1, Player.tileTargetX, Player.tileTargetY, 1, TileChangeType.None);
			}
		}

		public override bool NewRightClick(int i, int j)
		{
			int soundCode = GetSoundCode(i, j);
			PitchHelper.PlaySound(mod, soundCode, i * 16 + 8, j * 16);
			int dust = Dust.NewDust(new Vector2(i * 16 - 6, j * 16 - 2), 0, 0, DustType<Dusts.MusicalNote>());
			Main.dust[dust].velocity.X = 0f;
			Main.dust[dust].velocity.Y = -1f;
			return base.NewRightClick(i, j);
		}

		public override void MouseOver(int i, int j)
		{
			int soundCode = GetSoundCode(i, j);
			Main.NewText("GetPitch now Octave:" + PitchHelper.DecodeOctave(soundCode) + " Name:" + PitchHelper.DecodePitchName(soundCode));
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
			return true;
		}

		public int GetSoundCode(int i, int j)
		{
			Tile tile = Main.tile[i, j];
			int styleX = tile.frameX / 18;
			int styleY = tile.frameY / 18;

			return PitchHelper.EncodeSoundCode((int)EPitchType.Piano, styleY + 3, styleX + 1);
		}
	}
}
