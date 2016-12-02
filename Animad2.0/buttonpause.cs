using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Animad2
{
	public class buttonpause
	{
		Texture2D textura;
		Vector2 posbuton;
		Rectangle recbuton;

		Color colour = new Color(225,225,225,225);

		bool down;
		public bool isClicked;

		public buttonpause()
		{
		}

		public void Load(Texture2D newTexture, Vector2 newPosition)
		{
			textura = newTexture;
			posbuton = newPosition;
		}

		public void Update(MouseState mouse)
		{
			mouse = Mouse.GetState ();

			recbuton = new Rectangle ((int)posbuton.X, (int)posbuton.Y,
			                         textura.Width, textura.Height);

			Rectangle mouserectangle = new Rectangle (mouse.X, mouse.Y, 1, 1);

			if (mouserectangle.Intersects (recbuton)) {
				if (colour.A == 255)
					down = false;
				if (colour.A == 0)
					down = true;
				if (down)
					colour.A += 3;
				else
					colour.A -= 3;
				if (mouse.LeftButton == ButtonState.Pressed) {
					isClicked = true;
					colour.A = 255;
				}
			} else if (colour.A < 255)
				colour.A += 3;
		}

		public void Draw(SpriteBatch spritebatch)
		{
			spritebatch.Draw (textura, recbuton, colour);
		}
	}
}

