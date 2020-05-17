using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using Pianist.UI;

namespace Pianist
{
	public class Pianist : Mod
	{
		// With the new fonts in 1.3.5, font files are pretty big now so you need to generate the font file before building the mod.
		// You can use https://forums.terraria.org/index.php?threads/dynamicspritefontgenerator-0-4-generate-fonts-without-xna-game-studio.57127/ to make dynamicspritefonts
		public static DynamicSpriteFont exampleFont;

		private UserInterface PianistUserInterface;

		internal PianoUI PianoUI;

		public Pianist()
		{
			// By default, all Autoload properties are True. You only need to change this if you know what you are doing.
			//Properties = new ModProperties()
			//{
			//	Autoload = true,
			//	AutoloadGores = true,
			//	AutoloadSounds = true,
			//	AutoloadBackgrounds = true
			//};
		}

		public override void Load()
		{
			// Will show up in client.log under the ExampleMod name
			Logger.InfoFormat("{0} example logging", Name);

			// All code below runs only if we're not loading on a server
			if (!Main.dedServ)
			{
				if (FontExists("Fonts/ExampleFont"))
					exampleFont = GetFont("Fonts/ExampleFont");

				// Custom UI
				PianoUI = new PianoUI();
				PianoUI.Activate();
				PianistUserInterface = new UserInterface();
				PianistUserInterface.SetState(PianoUI);

			}
		}

		public override void Unload()
		{
			// All code below runs only if we're not loading on a server
			if (!Main.dedServ)
			{
				Main.tileFrame[TileID.Loom] = 0; // Reset the frame of the loom tile
				Main.tileSetsLoaded[TileID.Loom] = false; // Causes the loom tile to reload its vanilla texture
			}
		}

		//public override void AddRecipeGroups()

		public override void AddRecipes()
		{

			RecipeHelper.AddRecipes(this);
		}

		public override void HandlePacket(BinaryReader reader, int whoAmI)
		{
			PacketHelper.handlePacket(reader, whoAmI);
		}
	}
}