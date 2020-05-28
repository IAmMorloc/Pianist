using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Pianist.Tiles
{
	class Keyboard : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileLighted[Type] = true;
			Main.tileFrameImportant[Type] = true; //是否有边框。关闭时自动调整sprite。
												  //Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = true;
			drop = ItemType<Items.TiktokSquare>();
		}

		public override bool NewRightClick(int i, int j)
		{
			UI.PianoUI.Visible = true;
			return base.NewRightClick(i, j);
		}
	}
}
