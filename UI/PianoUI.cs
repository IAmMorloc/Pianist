using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.GameContent.UI.States;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;

namespace Pianist.UI
{
	public class PianoUI : UIState
	{
		private UIPanel panel;
		private UIHoverImageButton recordButton;
		private UIHoverImageButton closeButton;
		private UIHoverImageButton playButton;
		private UIPianoDisplay pianoDisplay;
		public static bool Visible;

		public int keyRange; //按键数量
		public bool record; //录音开关
		public long recordStartTime; //录音开始时间
		public long recordStopTime; //录音结束时间
		public List<List<long>> recordNotation; //录音临时存储记录。记录每个keyIndex对应的音高的触发时间（从录音开始时间算起）
		public bool play; //播放开关
		public long playStartTime; //播放开始时间
		public int[] playNotationProgress; //播放进度。以recordNotation下标的方式记录进度
		long currentTime;

		// In OnInitialize, we place various UIElements onto our UIState (this class).
		// UIState classes have width and height equal to the full screen, because of this, usually we first define a UIElement that will act as the container for our UI.
		// We then place various other UIElement onto that container UIElement positioned relative to the container UIElement.
		public override void OnInitialize()
		{
			//实例化一个面板
			panel = new UIPanel();
			panel.Width.Set(488f, 0f);
			panel.Height.Set(256f, 0f);
			panel.HAlign = 0.5f;
			panel.VAlign = 0.7f;
			//设置面板背景色
			panel.BackgroundColor = new Color(73, 94, 171);

			//关闭按钮
			closeButton = new UIHoverImageButton(ModContent.GetTexture("Terraria/UI/ButtonPlay"), "close");
			closeButton.Width.Set(40f, 0f);
			closeButton.Height.Set(40f, 0f);
			closeButton.HAlign = 0.75f;
			closeButton.VAlign = 0.9f;
			//绑定事件
			closeButton.OnClick += CloseButtonClicked;
			//将按钮注册入面板中，这个按钮的坐标将以面板的坐标为基础计算
			panel.Append(closeButton);

			//录音按钮
			recordButton = new UIHoverImageButton(ModContent.GetTexture("Terraria/UI/Camera_0"), "record");
			recordButton.Width.Set(40f, 0f);
			recordButton.Height.Set(40f, 0f);
			recordButton.HAlign = 0.25f;
			recordButton.VAlign = 0.9f;
			recordButton.OnClick += RecordButtonClicked;
			panel.Append(recordButton);

			//播放按钮
			playButton = new UIHoverImageButton(ModContent.GetTexture("Terraria/UI/Camera_2"), "play");
			playButton.Width.Set(40f, 0f);
			playButton.Height.Set(40f, 0f);
			playButton.HAlign = 0.5f;
			playButton.VAlign = 0.9f;
			playButton.OnClick += PlayButtonClicked;
			panel.Append(playButton);

			//演奏界面
			pianoDisplay = new UIPianoDisplay();
			pianoDisplay.Width.Set(460f, 0f);
			pianoDisplay.Height.Set(80f, 0f);
			pianoDisplay.HAlign = pianoDisplay.VAlign = 0.5f;
			panel.Append(pianoDisplay);

			//将面板注册到UIState
			Append(panel);
		}

		public override void OnActivate()
		{
			base.OnActivate();
			keyRange = pianoDisplay.keyRange;
			record = false;
			recordStartTime = 0;
			recordStopTime = 0;
			play = false;
			playStartTime = 0;
			recordNotation = new List<List<long>>(keyRange);
			for (int i = 0; i < keyRange; i++)
			{
				recordNotation.Add(new List<long>());
			}
			playNotationProgress = new int[keyRange];
		}

		public override void OnDeactivate()
		{
			base.OnDeactivate();
			for (int i = 0; i < keyRange; i++)
			{
				recordNotation[i].Clear();
			}
			recordNotation.Clear();
		}

		public void RecordModeActivate()
		{
			PlayModeDeactivate();
			record = true;
			for (int i = 0; i < keyRange; i++)
			{
				recordNotation[i].Clear();
			}
			recordStartTime = recordStopTime = currentTime;
		}

		public void RecordModeDeactivate()
		{
			record = false;
			recordStopTime = currentTime;
		}

		public void PlayModeActivate()
		{
			RecordModeDeactivate();
			play = true;
			playStartTime = currentTime;
		}

		public void PlayModeDeactivate()
		{
			play = false;
			for (int i = 0; i < keyRange; i++)
			{
				playNotationProgress[i] = 0;
			}
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime); // don't remove.

			// Checking ContainsPoint and then setting mouseInterface to true is very common. This causes clicks on this UIElement to not cause the player to use current items. 
			if (ContainsPoint(Main.MouseScreen))
			{
				Main.LocalPlayer.mouseInterface = true;
			}
			currentTime = (long)gameTime.TotalGameTime.TotalMilliseconds;
			//演奏模式
			if (!play)
			{
				for (int i = 0; i < keyRange; i++)
				{
					if (!pianoDisplay.KeyLocks[i] && Main.keyState.IsKeyDown(pianoDisplay.KeyIndex2InputName[i]))
					{
						pianoDisplay.PlayPianoSound(i);
						pianoDisplay.KeyLocks[i] = true;
						if (record)
						{
							//演奏录音模式
							recordNotation[i].Add(currentTime - recordStartTime);
						}
					}
					else if (Main.keyState.IsKeyUp(pianoDisplay.KeyIndex2InputName[i]))
					{
						pianoDisplay.KeyLocks[i] = false;
					}
				}
			}
			//播放模式
			else
			{
				//播放结束时
				if (currentTime - playStartTime >= recordStopTime - recordStartTime)
				{
					playButton.Click(new UIMouseEvent(playButton, Vector2.Zero));
				}
				else
				{
					//播放进度轮询
					for (int i = 0; i < keyRange; i++)
					{
						if (playNotationProgress[i] < recordNotation[i].Count()
							&& recordNotation[i][playNotationProgress[i]] <= currentTime - playStartTime)
						{
							pianoDisplay.PlayPianoSound(i);
							playNotationProgress[i]++;
						}
					}
				}
			}
		}

		private void RecordButtonClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			Main.NewText("RecordButtonClicked " + record);
			if (record)
			{
				RecordModeDeactivate();
				recordButton.SetImage(ModContent.GetTexture("Terraria/UI/Camera_0"));
				recordButton.SetHoverText("record");
			}
			else
			{
				RecordModeActivate();
				recordButton.SetImage(ModContent.GetTexture("Terraria/UI/Camera_1"));
				recordButton.SetHoverText("stop");
			}
		}

		private void PlayButtonClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			Main.NewText("PlayButtonClicked " + play);
			if (play)
			{
				PlayModeDeactivate();
				playButton.SetImage(ModContent.GetTexture("Terraria/UI/Camera_2"));
				recordButton.SetHoverText("play");
			}
			else
			{
				PlayModeActivate();
				playButton.SetImage(ModContent.GetTexture("Terraria/UI/Camera_3"));
				recordButton.SetHoverText("stop");
			}
		}

		private void CloseButtonClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			Main.NewText("CloseButtonClicked");
			Main.PlaySound(SoundID.MenuClose);
			Visible = false;
		}
	}

}
