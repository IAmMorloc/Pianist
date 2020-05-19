using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;

namespace Pianist.UI
{
	// ExampleUIs visibility is toggled by typing "/coin" in chat. (See CoinCommand.cs)
	// ExampleUI is a simple UI example showing how to use UIPanel, UIImageButton, and even a custom UIElement.
	internal class PianoUI : UIState
	{
		private UIPanel panel;
		private UIImageButton button;
		public static bool Visible;
		// In OnInitialize, we place various UIElements onto our UIState (this class).
		// UIState classes have width and height equal to the full screen, because of this, usually we first define a UIElement that will act as the container for our UI.
		// We then place various other UIElement onto that container UIElement positioned relative to the container UIElement.
		public override void OnInitialize()
		{
			//实例化一个面板
			panel = new UIPanel();
			//设置面板的宽度
			panel.Width.Set(488f, 0f);
			//设置面板的高度
			panel.Height.Set(568f, 0f);
			//设置面板距离屏幕最左边的距离
			panel.Left.Set(-244f, 0.5f);
			//设置面板距离屏幕最上端的距离
			panel.Top.Set(-284f, 0.5f);
			//设置面板背景色
			panel.BackgroundColor = new Color(73, 94, 171);
			//将这个面板注册到UIState
			Append(panel);

			//用tr原版图片实例化一个图片按钮
			button = new UIImageButton(ModContent.GetTexture("Terraria/UI/ButtonPlay"));
			//设置按钮距宽度
			button.Width.Set(80f, 0f);
			//设置按钮高度
			button.Height.Set(40f, 0f);
			//设置按钮距离所属ui部件的最左端的距离
			button.Left.Set(-40f, 0.5f);
			//设置按钮距离所属ui部件的最顶端的距离
			button.Top.Set(-20f, 0.9f);
			button.OnClick += CloseButtonClicked;
			//将按钮注册入面板中，这个按钮的坐标将以面板的坐标为基础计算
			panel.Append(button);

		}

		private void PlayButtonClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			Main.PlaySound(SoundID.MenuOpen);
		}

		private void CloseButtonClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			Main.NewText("CloseButtonClicked");
			Main.PlaySound(SoundID.MenuClose);
			Visible = false;
		}
	}

}
