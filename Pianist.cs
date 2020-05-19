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

		private UserInterface pianistUserInterface;

		internal PianoUI pianoUI;

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
				pianoUI = new PianoUI();
				pianoUI.Activate();
				pianistUserInterface = new UserInterface();
				pianistUserInterface.SetState(pianoUI);

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

		public override void UpdateUI(GameTime gameTime)
		{
			//当Visible为true时（当UI开启时）
			if (PianoUI.Visible)
				//如果exampleUserInterface不是null就执行Update方法
				pianistUserInterface?.Update(gameTime);
			base.UpdateUI(gameTime);
		}

		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
			//寻找一个名字为Vanilla: Mouse Text的绘制层，也就是绘制鼠标字体的那一层，并且返回那一层的索引
			int MouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
			//寻找到索引时
			if (MouseTextIndex != -1)
			{
				//往绘制层集合插入一个成员，第一个参数是插入的地方的索引，第二个参数是绘制层
				layers.Insert(MouseTextIndex, new LegacyGameInterfaceLayer(
				   //这里是绘制层的名字
				   "Test : ExampleUI",
				   //这里是匿名方法
				   delegate
				   {
			   //当Visible开启时（当UI开启时）
			   if (PianoUI.Visible)
				   //绘制UI（运行exampleUI的Draw方法）
				   pianoUI.Draw(Main.spriteBatch);
					   return true;
				   },
				   //这里是绘制层的类型
				   InterfaceScaleType.UI)
			   );
			}
			base.ModifyInterfaceLayers(layers);
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