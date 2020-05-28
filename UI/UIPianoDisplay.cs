using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;

namespace Pianist.UI
{
	public class UIPianoDisplay : UIElement
	{
		public readonly int keySizeX; //按键宽度。绘图时使用
		public readonly int keySizeY; //按键高度。绘图时使用
		public readonly int keyRange = 17; //按键数量

		public readonly Microsoft.Xna.Framework.Input.Keys[] KeyIndex2InputName = {
			Microsoft.Xna.Framework.Input.Keys.A,
			Microsoft.Xna.Framework.Input.Keys.S,
			Microsoft.Xna.Framework.Input.Keys.D,
			Microsoft.Xna.Framework.Input.Keys.F,
			Microsoft.Xna.Framework.Input.Keys.G,
			Microsoft.Xna.Framework.Input.Keys.Q,
			Microsoft.Xna.Framework.Input.Keys.W,
			Microsoft.Xna.Framework.Input.Keys.E,
			Microsoft.Xna.Framework.Input.Keys.R,
			Microsoft.Xna.Framework.Input.Keys.T,
			Microsoft.Xna.Framework.Input.Keys.Y,
			Microsoft.Xna.Framework.Input.Keys.U,
			Microsoft.Xna.Framework.Input.Keys.I,
			Microsoft.Xna.Framework.Input.Keys.O,
			Microsoft.Xna.Framework.Input.Keys.P,
			Microsoft.Xna.Framework.Input.Keys.OemOpenBrackets,
			Microsoft.Xna.Framework.Input.Keys.OemCloseBrackets,
		};
		public int[] KeyIndex2Pitch = { -11, -9, -7, -6, -4, -2, 0, 1, 3, 5, 6, 8, 10, 12, 13, 15, 17 };
		public bool[] KeyLocks;


		public UIPianoDisplay()
		{
			keySizeX = 24;
			keySizeY = 24;
			KeyLocks = new bool[keyRange];
		}

		public override void MouseDown(UIMouseEvent evt)
		{
			base.MouseDown(evt);

			CalculatedStyle innerDimensions = GetInnerDimensions();
			float pianox = innerDimensions.X;
			float pianoy = innerDimensions.Y;

			int keyIndex = (int)(evt.MousePosition.X - pianox) / keySizeX;
			if (keyIndex < keyRange && evt.MousePosition.Y > pianoy + keySizeY)
			{
				PlayPianoSound(keyIndex);
				KeyLocks[keyIndex] = true;
			}
		}

		public override void MouseUp(UIMouseEvent evt)
		{
			base.MouseUp(evt);

		}

		public void PlayPianoSound(int keyIndex)
		{
			if (keyIndex < keyRange)
			{
				int soundCode;
				if (keyIndex < 7)
				{
					soundCode = PitchHelper.EncodeSoundCode(1, 3, KeyIndex2Pitch[keyIndex] + 12);
				}
				else if (keyIndex < 14)
				{
					soundCode = PitchHelper.EncodeSoundCode(1, 4, KeyIndex2Pitch[keyIndex]);
				}
				else
				{
					soundCode = PitchHelper.EncodeSoundCode(1, 5, KeyIndex2Pitch[keyIndex] - 12);
				}
				PitchHelper.PlaySound(ModLoader.GetMod("Pianist"), soundCode);
				Main.NewText("PlayPianoSound " + keyIndex);
			}
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime); // don't remove.
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			CalculatedStyle innerDimensions = GetInnerDimensions();
			float pianox = innerDimensions.X;
			float pianoy = innerDimensions.Y;

			for (int j = 0; j < keyRange; j++)
			{
				if (KeyLocks[j])
				{
					spriteBatch.Draw(ModContent.GetTexture("Pianist/UI/PianoKeyDown"), new Vector2(pianox + (keySizeX * j), pianoy + keySizeY + 12f), Color.White);
				}
				else
				{
					spriteBatch.Draw(ModContent.GetTexture("Pianist/UI/PianoKeyUp"), new Vector2(pianox + (keySizeX * j), pianoy + keySizeY + 12f), Color.White);
				}
				Utils.DrawBorderStringFourWay(spriteBatch, Pianist.exampleFont ?? Main.fontItemStack, (j + 1).ToString(), pianox + (keySizeX * j), pianoy + keySizeY, Color.Gold, Color.SeaGreen, new Vector2(0.3f), 0.75f);
			}

		}

	}
}
