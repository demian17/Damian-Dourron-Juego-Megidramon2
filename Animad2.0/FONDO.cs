using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Animad2
{
	public class FONDO
	{
		public Texture2D texture;
		public Rectangle rectang;
		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw (texture, rectang, Color.White);
		}
	}
	public class Scrolling : FONDO
	{
		public Scrolling(Texture2D newTexture, Rectangle newRectangle)
		{
			texture = newTexture;
			rectang = newRectangle;
		}

		public void Update()
		{
			rectang.X -= 3;

		}

	}

}

