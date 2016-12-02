using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
namespace Animad2
{
	public class bullets
	{
		public Texture2D fuego;
		public Vector2 position;
		public Vector2 velocity;
		public Vector2 origin;
		public Rectangle brec;
		public Game1 rec;
		public bool isVisible;
		public Animation player;
		public bullets (Texture2D newTexture)
		{
			fuego = newTexture;
			isVisible = false;
		} 
		public void Update(GameTime gameTime)
		{
			brec = new Rectangle ((int)player.Position.X,(int)player.Position.Y,
			                      fuego.Width, fuego.Height);
			if (brec.Intersects (rec.rectanguloEnemigo)) {
				rec.puntos = rec.puntos + 100;
			}
		}
		public void Draw(SpriteBatch spriteBatch)
	{
			spriteBatch.Draw (fuego, position, null, Color.White, 0f, origin, 1f, SpriteEffects.None, 0);
	}


	}
}

