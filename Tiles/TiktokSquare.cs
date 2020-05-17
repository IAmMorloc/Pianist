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
	class TiktokSquare : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileLighted[Type] = true;
			Main.tileFrameImportant[Type] = true;
			//Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = true;
			drop = ItemType<Items.TiktokSquare>();
		}

		public override void HitWire(int i, int j)
		{
			Main.PlaySound(SoundID.Item30, i * 16 + 8, j * 16);

		}
	}
}
