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
/*
		public override void HitWire(int i, int j)
		{
			//Main.PlaySound(SoundID.Item30, i * 16 + 8, j * 16);
			//Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/a-1").WithVolume(3f).WithPitchVariance(.5f), i * 16 + 8, j * 16);

			if (Main.rand.NextFloat() < .5f)
			{
				Main.PlaySound(SoundLoader.customSoundType, i * 16 + 8, j * 16, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/a-1"), 1f, 1f);
			}
			else
			{
				Main.PlaySound(SoundLoader.customSoundType, i * 16 + 8, j * 16, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/a-1"), 1f, -1f);
			}
		}
		*/
	}
}
