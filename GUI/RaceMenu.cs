using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using System;
using Terraria.ID;
using System.Linq;
using ReLogic.Graphics;
using CosmaliaMod.Races;
using CosmaliaMod.Races.Elf;
using CosmaliaMod.Races.Ram;
using CosmaliaMod.Races.Longtail;

namespace CosmaliaMod.GUI
{
	// modify this soon
	public enum ActivePart
	{
		none = 0,
		horn = 1,
		tail = 2,
		claw = 3,
		eye = 4
	};
	public class RaceMenu : UIState
	{
		public static bool created = false;
		public static bool visible = false;
		public static bool visible2 = false;
		public Color currentColor;
		private ActivePart part;

		public Player player;
		public CosmaliaPlayer race;
		public Elf elf;
		public Ram ram;
		public Longtail longtail;

		// private Main main = new Main();

		public override void OnInitialize()
		{
			QuickAddButton("Sclera", new Vector2(Main.screenWidth / 2, 660), new MouseEvent(Eye2));

			QuickAddButton("Horns", new Vector2(Main.screenWidth / 4, 340), new MouseEvent(Customize1));
			QuickAddButton("Claws", new Vector2(Main.screenWidth / 4, 380), new MouseEvent(Customize2));
			QuickAddButton("Tail", new Vector2(Main.screenWidth / 4, 420), new MouseEvent(Customize3));
			QuickAddButton("Race", new Vector2(Main.screenWidth / 4, 460), new MouseEvent(ChangeRace));
			QuickAddDesc();
		}

		public void DontRemoveChildrenOhMyGodHowCouldYou()
		{
			QuickAddButton("Sclera", new Vector2(Main.screenWidth / 2, 660), new MouseEvent(Eye2));

			QuickAddButton("Horns", new Vector2(Main.screenWidth / 4, 340), new MouseEvent(Customize1));
			QuickAddButton("Claws", new Vector2(Main.screenWidth / 4, 380), new MouseEvent(Customize2));
			QuickAddButton("Tail", new Vector2(Main.screenWidth / 4, 420), new MouseEvent(Customize3));
			QuickAddButton("Race", new Vector2(Main.screenWidth / 4, 460), new MouseEvent(ChangeRace));
			QuickAddDesc();
		}

		private void ChangeToHuman(UIMouseEvent evt, UIElement listeningElement)
		{
			Main.menuMode = 2;
			visible = false;
			visible2 = false;
			base.RemoveAllChildren();
			QuickAddButton("Sclera", new Vector2(Main.screenWidth / 2, 660), new MouseEvent(Eye2));

			QuickAddButton("Horns", new Vector2(Main.screenWidth / 4, 340), new MouseEvent(Customize1));
			QuickAddButton("Claws", new Vector2(Main.screenWidth / 4, 380), new MouseEvent(Customize3));
			QuickAddButton("Tail", new Vector2(Main.screenWidth / 4, 420), new MouseEvent(Customize2));
			QuickAddButton("Race", new Vector2(Main.screenWidth / 4, 460), new MouseEvent(ChangeRace));

			part = ActivePart.none;
		}

		private void Eye2(UIMouseEvent evt, UIElement listeningElement)
		{
			visible2 = true;
			base.RemoveAllChildren();
			QuickAddColor(ColorChannel.r, new Vector2(3 * Main.screenWidth / 4, 300), race.scleraColor.R);
			QuickAddColor(ColorChannel.g, new Vector2(3 * Main.screenWidth / 4, 340), race.scleraColor.G);
			QuickAddColor(ColorChannel.b, new Vector2(3 * Main.screenWidth / 4, 380), race.scleraColor.B);

			part = ActivePart.eye;

			currentColor = new Color(255, 255, 255);
			QuickAddButton("Back", new Vector2(3 * Main.screenWidth / 4, 720), new MouseEvent(ChangeToHuman));
		}

		private void ChangeToRace(UIMouseEvent evt, UIElement listeningElement)
		{

			Main.menuMode = 444436;
			visible = true;
			base.RemoveAllChildren();

			QuickAddButton("Back", new Vector2(Main.screenWidth / 2, 700), new MouseEvent(ChangeToHuman));
			QuickAddButton("Horns", new Vector2(Main.screenWidth / 2, 340), new MouseEvent(Customize1));
			QuickAddButton("Claws", new Vector2(Main.screenWidth / 2, 380), new MouseEvent(Customize2));
			QuickAddButton("Tail", new Vector2(Main.screenWidth / 2, 420), new MouseEvent(Customize3));
			QuickAddButton("Race", new Vector2(Main.screenWidth / 2, 460), new MouseEvent(ChangeRace));

			currentColor = new Color(0, 0, 0);
			part = ActivePart.none;

		}

		private void Customize1(UIMouseEvent evt, UIElement listeningElement)
		{
			base.RemoveAllChildren();
			QuickAddColor(ColorChannel.r, new Vector2(3 * Main.screenWidth / 4, 300), race.hornColor.R);
			QuickAddColor(ColorChannel.g, new Vector2(3 * Main.screenWidth / 4, 340), race.hornColor.G);
			QuickAddColor(ColorChannel.b, new Vector2(3 * Main.screenWidth / 4, 380), race.hornColor.B);
			part = ActivePart.horn;
			currentColor = new Color(0, 0, 0);

			QuickAddButton("Back", new Vector2(Main.screenWidth / 2, 700), new MouseEvent(ChangeToHuman));
		}
		private void Customize2(UIMouseEvent evt, UIElement listeningElement)
		{
			base.RemoveAllChildren();
			QuickAddColor(ColorChannel.r, new Vector2(3 * Main.screenWidth / 4, 300), race.tailColor.R);
			QuickAddColor(ColorChannel.g, new Vector2(3 * Main.screenWidth / 4, 340), race.tailColor.G);
			QuickAddColor(ColorChannel.b, new Vector2(3 * Main.screenWidth / 4, 380), race.tailColor.B);
			part = ActivePart.tail;
			currentColor = new Color(0, 0, 0);

			QuickAddButton("Back", new Vector2(Main.screenWidth / 2, 700), new MouseEvent(ChangeToHuman));
		}
		private void Customize3(UIMouseEvent evt, UIElement listeningElement)
		{
			base.RemoveAllChildren();
			QuickAddColor(ColorChannel.r, new Vector2(3 * Main.screenWidth / 4, 300), race.clawColor.R);
			QuickAddColor(ColorChannel.g, new Vector2(3 * Main.screenWidth / 4, 340), race.clawColor.G);
			QuickAddColor(ColorChannel.b, new Vector2(3 * Main.screenWidth / 4, 380), race.clawColor.B);
			part = ActivePart.claw;
			currentColor = new Color(0, 0, 0);

			QuickAddButton("Back", new Vector2(Main.screenWidth / 2, 700), new MouseEvent(ChangeToHuman));
		}

		private void ChangeRace(UIMouseEvent evt, UIElement listeningElement)
		{
			if (elf.isRace)
			{
				elf.isRace = false;
				ram.isRace = true;
				base.RemoveAllChildren();
				DontRemoveChildrenOhMyGodHowCouldYou();
				QuickAddTextbox("big horns lol", new Vector2(3*Main.screenWidth / 4, 600));
			}
			else if (ram.isRace)
			{
				ram.isRace = false;
				longtail.isRace = true;

				base.RemoveAllChildren();
				DontRemoveChildrenOhMyGodHowCouldYou();
				QuickAddTextbox("long tail lol", new Vector2(3*Main.screenWidth / 4, 600));
			}
			else if (longtail.isRace)
			{
				longtail.isRace = false;
				base.RemoveAllChildren();
				DontRemoveChildrenOhMyGodHowCouldYou();
				QuickAddTextbox("MAN :horse:", new Vector2(3*Main.screenWidth / 4, 600));
			}
			else
			{
				elf.isRace = true;
				base.RemoveAllChildren();
				DontRemoveChildrenOhMyGodHowCouldYou();
				QuickAddTextbox("long ears lol", new Vector2(3 * Main.screenWidth / 4, 600));
			}
		}

		/*
		private void Randomize(UIMouseEvent evt, UIElement listeningElement)
		{
			race.hornColor = new Color(Main.rand.Next(255), Main.rand.Next(255), Main.rand.Next(255));
			race.scaleColor = new Color(Main.rand.Next(255), Main.rand.Next(255), Main.rand.Next(255));
			race.bellyColor = new Color(Main.rand.Next(255), Main.rand.Next(255), Main.rand.Next(255));
			race.eyeColor = new Color(Main.rand.Next(255), Main.rand.Next(255), Main.rand.Next(255));
		}
		*/

		public override void Draw(SpriteBatch spriteBatch)
		{
			if (visible)
			{
				Texture2D tex = ModContent.GetTexture("CosmaliaMod/GUI/Bar");
				spriteBatch.Draw(tex, new Vector2(Main.screenWidth / 2 + 200, 250), tex.Frame(), race.hornColor, 0, tex.Frame().Size() / 2, 2, 0, 0);
				spriteBatch.Draw(tex, new Vector2(Main.screenWidth / 2 + 200, 270), tex.Frame(), race.tailColor, 0, tex.Frame().Size() / 2, 2, 0, 0);
				spriteBatch.Draw(tex, new Vector2(Main.screenWidth / 2 + 200, 290), tex.Frame(), race.clawColor, 0, tex.Frame().Size() / 2, 2, 0, 0);
				// spriteBatch.Draw(tex, new Vector2(Main.screenWidth / 2 + 200, 310), tex.Frame(), race.scleraColor, 0, tex.Frame().Size() / 2, 2, 0, 0);

				spriteBatch.Draw(tex, new Vector2(Main.screenWidth / 2 + 200, 330), tex.Frame(), currentColor, 0, tex.Frame().Size() / 2, 2, 0, 0);

				//player.PlayerFrame();
				player.position.X = Main.screenWidth / 2 - 16;
				player.position.Y = 176f + Main.screenPosition.Y;
				Main.instance.DrawPlayer(player, player.position, 0f, Vector2.Zero, 1f);

				currentColor.A = 255;
			}

			if (visible2)
			{
				Texture2D tex = ModContent.GetTexture("CosmaliaMod/GUI/Bar");

				spriteBatch.Draw(tex, new Vector2(Main.screenWidth / 2 + 200, 330), tex.Frame(), race.scleraColor, 0, tex.Frame().Size() / 2, 2, 0, 0);
				spriteBatch.Draw(tex, new Vector2(Main.screenWidth / 2 + 200, 360), tex.Frame(), player.eyeColor, 0, tex.Frame().Size() / 2, 2, 0, 0);

				//player.PlayerFrame();
				player.position.X = Main.screenWidth / 2 - 16;
				player.position.Y = 176f + Main.screenPosition.Y;
				Main.instance.DrawPlayer(player, player.position, 0f, Vector2.Zero, 1f);
			}

			base.Draw(spriteBatch);
			Recalculate();
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			switch (part)
			{
				case ActivePart.horn: race.hornColor = currentColor; break;
				case ActivePart.tail: race.tailColor = currentColor; break;
				case ActivePart.claw: race.clawColor = currentColor; break;
				case ActivePart.eye: race.scleraColor = currentColor; break;
			}
		}

		private void QuickAddDesc()
		{
			if (elf.isRace)
			{
				QuickAddTextbox("long ears lol", new Vector2(3 * Main.screenWidth / 4, 600));
			}
			else if (ram.isRace)
			{
				QuickAddTextbox("big horns lol", new Vector2(3 * Main.screenWidth / 4, 600));
			}
			else if (longtail.isRace)
			{
				QuickAddTextbox("long tail lol", new Vector2(3 * Main.screenWidth / 4, 600));
			}
			else
			{
				QuickAddTextbox("MAN :horse:", new Vector2(3 * Main.screenWidth / 4, 600));
			}
		}

		private void QuickAddButton(string text, Vector2 pos, MouseEvent OnClick = null)
		{
			TextButton button = new TextButton(text);
			button.Left.Set(pos.X - (int)Main.fontMouseText.MeasureString(text).X * 2f / 2, 0);
			button.Top.Set(pos.Y - (int)Main.fontMouseText.MeasureString(text).Y * 1.2f / 2, 0);
			button.OnClick += OnClick;
			base.Append(button);
		}
		private void QuickAddColor(ColorChannel channel, Vector2 pos, int initialValue = 0)
		{
			ColorSlider slider = new ColorSlider(channel);
			slider.Left.Set(pos.X - 126, 0);
			slider.Top.Set(pos.Y - 10, 0);
			slider.Width.Set(255, 0);
			slider.Height.Set(32, 0);
			slider.sliderPos = initialValue;

			base.Append(slider);

		}
		private void QuickAddTextbox(string text, Vector2 pos)
		{
			Textbox button = new Textbox(text);
			button.Left.Set(pos.X - (int)Main.fontMouseText.MeasureString(text).X * 2f / 2, 0);
			button.Top.Set(pos.Y - (int)Main.fontMouseText.MeasureString(text).Y * 1.2f / 2, 0);
			base.Append(button);
		}
	}

	public class TextButton : UIElement
	{
		private string Text;
		private int Fade;
		public TextButton(string text) { Text = text; }
		public override void OnInitialize()
		{
			Width.Set((int)Main.fontMouseText.MeasureString(Text).X * 1.2f, 0);
			Height.Set((int)Main.fontMouseText.MeasureString(Text).Y * 1.2f, 0);
		}
		public override void Draw(SpriteBatch spriteBatch)
		{
			bool hover = GetDimensions().ToRectangle().Contains(Main.MouseScreen.ToPoint());
			if (!hover && Fade > 0) Fade--;
			if (hover && Fade < 10) Fade++;

			int basecol = 140;
			int intensity = basecol + Fade * 11;
			Color color = new Color(intensity, intensity, basecol - Fade * 14);

			Utils.DrawBorderStringBig(spriteBatch, Text, GetDimensions().Position() + GetDimensions().ToRectangle().Size() / 2, color * (0.8f + Fade / 50f), 0.675f + Fade / 100f, 0.5f, 0.5f);

			Width.Set((int)Main.fontMouseText.MeasureString(Text).X * 2f, 0);
			Height.Set((int)Main.fontMouseText.MeasureString(Text).Y * 1.2f, 0);
		}
		public override void MouseOver(UIMouseEvent evt)
		{
			Main.PlaySound(SoundID.MenuTick);
		}
		public override void Click(UIMouseEvent evt)
		{
			base.Click(evt);
		}
	}

	public enum ColorChannel
	{
		r = 0,
		g = 1,
		b = 2
	};
	public class ColorSlider : UIElement
	{
		public int sliderPos = 0;
		private ColorChannel Channel;
		private Rectangle sliderBox { get => new Rectangle((int)GetDimensions().X + sliderPos - 9, (int)GetDimensions().Y + 2, 18, 28); }
		private Rectangle rect2 { get => new Rectangle((int)GetDimensions().X, (int)GetDimensions().Y + 2, (int)GetDimensions().Width, (int)GetDimensions().Height); }

		public ColorSlider(ColorChannel channel) { Channel = channel; }
		public override void Draw(SpriteBatch spriteBatch)
		{
			Texture2D tex0 = ModContent.GetTexture("CosmaliaMod/GUI/SliderBack");
			Texture2D tex1 = ModContent.GetTexture("CosmaliaMod/GUI/SliderGradient");
			Texture2D tex2 = ModContent.GetTexture("CosmaliaMod/GUI/Slider");
			Texture2D tex3 = ModContent.GetTexture("CosmaliaMod/GUI/SliderOver");

			Color backColor = (Parent as RaceMenu).currentColor;
			if (Channel == ColorChannel.r) backColor.R = 0;
			if (Channel == ColorChannel.g) backColor.G = 0;
			if (Channel == ColorChannel.b) backColor.B = 0;

			int off = (int)Channel * 2;

			spriteBatch.Draw(tex0, GetDimensions().ToRectangle(), tex0.Frame(), backColor);

			spriteBatch.End();
			spriteBatch.Begin(default, BlendState.Additive, SamplerState.PointWrap, default, default);

			spriteBatch.Draw(tex1, new Rectangle((int)GetDimensions().X, (int)GetDimensions().Y + 8, 255, 16), new Rectangle(0, off, 255, 1), Color.White);

			spriteBatch.End();
			spriteBatch.Begin();

			spriteBatch.Draw(tex2, GetDimensions().ToRectangle(), tex2.Frame(), Color.White);
			spriteBatch.Draw(tex3, sliderBox, tex3.Frame(), Color.White);
		}

		public override void Update(GameTime gameTime)
		{
			if (rect2.Contains(Main.MouseScreen.ToPoint()) && Main.mouseLeft)
			{
				sliderPos = (int)(Main.MouseScreen.ToPoint().X - GetDimensions().X);
			}

			if (sliderPos < 0) sliderPos = 0;
			if (sliderPos > 255) sliderPos = 255;

			if (Channel == ColorChannel.r) (Parent as RaceMenu).currentColor.R = (byte)sliderPos;
			if (Channel == ColorChannel.g) (Parent as RaceMenu).currentColor.G = (byte)sliderPos;
			if (Channel == ColorChannel.b) (Parent as RaceMenu).currentColor.B = (byte)sliderPos;
		}
	}

	public class Textbox : UIElement
	{
		private string Text;
		public Textbox(string text) { Text = text; }
		public override void OnInitialize()
		{
			Width.Set((int)Main.fontMouseText.MeasureString(Text).X * 1.2f, 0);
			Height.Set((int)Main.fontMouseText.MeasureString(Text).Y * 1.2f, 0);
		}
		public override void Draw(SpriteBatch spriteBatch)
		{
			int basecol = 140;
			int intensity = basecol * 11;
			Color color = new Color(intensity, intensity, basecol);

			Utils.DrawBorderStringBig(spriteBatch, Text, GetDimensions().Position() + GetDimensions().ToRectangle().Size() / 2, color * (0.8f), 0.675f, 0.5f, 0.5f);

			Width.Set((int)Main.fontMouseText.MeasureString(Text).X * 2f, 0);
			Height.Set((int)Main.fontMouseText.MeasureString(Text).Y * 1.2f, 0);
		}
	}
}