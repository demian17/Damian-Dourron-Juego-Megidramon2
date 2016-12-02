using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
    public class Game1 : Game
    {
        public GraphicsDeviceManager graphics;
		public Vector2 cont;
		public SpriteBatch spriteBatch;
		public Texture2D sprite;
		public Texture2D enemigo;
		public Texture2D cor;
		public Texture2D fondo;
		public Vector2 possprite;
		public Vector2 cordes;
		public Vector2 posfondo;
		public Rectangle rectanguloPersona;
		public Rectangle rectanguloEnemigo;
		public Rectangle bulletrec;
		public List<Vector2> posicionesEnemigo = new List<Vector2> ();
		public double probabilidadEnemigo = 0.03;
		public int velocidadEnemigo = 2;
		public Random aleatorio = new Random ();
		public List<bullets> bullets = new List<bullets>();
		public KeyboardState pastkey;
		public SpriteFont fuente1;
		public SpriteFont fuente2;
		public SpriteFont fuente3;
		public Vector2 posicioncor;
		Vector2 posvidas;
		Vector2 poslev;
		Vector2 poscicionfv;
		Vector2 poscicionfp; 
		public Song musicadefondo;
		Scrolling scrolling1;
		Scrolling scrolling2;
		Scrolling scroll1;
		Scrolling scroll2;
		Scrolling level3a;
		Scrolling level3b;
		Scrolling level4a;
		Scrolling level4b;

		public Animation player;
		public bullets bala;
		int level;

		bool paused = false;
		Texture2D pausedtexture;
		Rectangle pausedrec;
		buttonpause btnPlay, btnQuit;
		public int puntos=0;
		public int vidas = 3;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "../../Content";	            
			graphics.IsFullScreen = false;		
        }

        protected override void Initialize()
        {
			possprite = new Vector2 (255, 300);
			poscicionfp = new Vector2 (260, 220);
			poscicionfv = new Vector2 (250, 260);
			posicioncor = new Vector2 (27, 27);
			cordes = new Vector2 (100,250);
			posfondo = new Vector2 (0, 0);
			graphics.PreferredBackBufferWidth = 640;
			graphics.PreferredBackBufferHeight = 480;
			posvidas = new Vector2 (500, 450);
			poslev = new Vector2 (500, 0);
			base.Initialize(); 
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
			scrolling1 = new Scrolling(Content.Load<Texture2D>("fondo/dd1"), new Rectangle(0, 0, 1600, 500));
			scrolling2 = new Scrolling(Content.Load<Texture2D>("fondo/dd2"), new Rectangle(1600, 0, 1600, 500));
			scroll1 = new Scrolling (Content.Load<Texture2D> ("fondo/ee1"), new Rectangle (0, 0, 1600, 500));
			scroll2 = new Scrolling (Content.Load<Texture2D> ("fondo/ee2"), new Rectangle (1600, 0, 1600, 500));
			level3a = new Scrolling (Content.Load<Texture2D> ("fondo/ff2"), new Rectangle (0, 0, 1600, 500));
			level3b = new Scrolling (Content.Load<Texture2D> ("fondo/ff1"),new Rectangle(1600,0,1600,500));
			level4a = new Scrolling (Content.Load<Texture2D> ("fondo/ArcheAge-wallpaper-6"), new Rectangle (0, 0, 2000, 500));
			level4b = new Scrolling (Content.Load<Texture2D> ("fondo/ArcheAge-wallpaper-6"), new Rectangle (2000, 0, 2000, 500));
			player = new Animation ();
			fondo = Content.Load<Texture2D>("fondo/win");                    
			musicadefondo = Content.Load<Song>("musica/mus.wav");
			MediaPlayer.Play(musicadefondo); // Reproducimos
			MediaPlayer.IsRepeating = true; // Repetimos
			MediaPlayer.Volume = 0.3f;
			fuente1 = Content.Load<SpriteFont>("Fuentes/fuente1");
			fuente2 = Content.Load<SpriteFont> ("Fuentes/fuente2");
			enemigo = Content.Load<Texture2D> ("Graficos/enemigo");
			//fondo = Content.Load<Texture2D>("fondo/medabots-medals");
			sprite = Content.Load<Texture2D>("Graficos/megidramon");
			player.Initialize(sprite, cordes, 88, 90, 8, 180, Color.White, 1.0f, true);
			//playeranimation.Initialize(sprite, cordes, 88, 90, 8, 180, Color.White, 1.0f, true);
			IsMouseVisible = true;

			//pausedtexture = 
			}

        protected override void Update(GameTime gameTime)
        {
            // For Mobile devices, this logic will close the Game when the Back button is pressed
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
			Exit();
			KeyboardState keyboard = Keyboard.GetState();

			if (keyboard.IsKeyDown(Keys.Escape)) this.Exit();

			if (keyboard.IsKeyDown(Keys.W)) player.Position.Y -=5;
			if (keyboard.IsKeyDown(Keys.S)) player.Position.Y +=5;
			if (keyboard.IsKeyDown(Keys.D)) player.Position.X += 5;
			if (keyboard.IsKeyDown(Keys.A)) player.Position.X -= 5;

			if (keyboard.IsKeyDown(Keys.Up)) player.Position.Y -=5;
			if (keyboard.IsKeyDown(Keys.Down)) player.Position.Y +=5;
			if (keyboard.IsKeyDown(Keys.Right)) player.Position.X += 5;
			if (keyboard.IsKeyDown(Keys.Left)) player.Position.X -= 5;
			if (aleatorio.NextDouble () < probabilidadEnemigo) { 
				float y = (float)aleatorio.NextDouble () * 
					Window.ClientBounds.Width; 
				posicionesEnemigo.Add (new Vector2 (500, y)); 
			}

			if (scrolling1.rectang.X + scrolling1.texture.Width <= 0)
				scrolling1.rectang.X = scrolling2.rectang.X + scrolling2.texture.Width;

			if (scrolling2.rectang.X + scrolling2.texture.Width <= 0)
				scrolling2.rectang.X = scrolling1.rectang.X + scrolling1.texture.Width;
			scrolling1.Update ();
			scrolling2.Update ();

			//nivel2
			if (scroll1.rectang.X + scroll1.texture.Width <= 0)
				scroll1.rectang.X = scroll2.rectang.X + scroll2.texture.Width;
			if (scroll2.rectang.X + scroll2.texture.Width <= 0)
				scroll2.rectang.X = scroll1.rectang.X + scroll1.texture.Width;
			scroll1.Update ();
			scroll2.Update ();

			//nivel3
			if (level3a.rectang.X + level3a.texture.Width <= 0)
				level3a.rectang.X = level3b.rectang.X + level3b.texture.Width;
			if (level3b.rectang.X + level3b.texture.Width <= 0)
				level3b.rectang.X = level3a.rectang.X + level3a.texture.Width;
			level3a.Update ();
			level3b.Update ();

			//nivel4
			if (level4a.rectang.X + level4a.texture.Width <= 0)
				level4a.rectang.X = level4b.rectang.X + level4b.texture.Width;
			if (level4b.rectang.X + level4b.texture.Width <= 0)
				level4b.rectang.X = level4a.rectang.X + level4a.texture.Width;
			level4a.Update ();
			level4b.Update ();


			//player.Position;

			if (Keyboard.GetState ().IsKeyDown (Keys.Space) && pastkey.IsKeyUp (Keys.Space)) {
				shoot ();

			}
			pastkey = Keyboard.GetState ();
			UpdateBullets();
		player.Update (gameTime);			
            base.Update(gameTime);

			switch (level)
			{
			case 0:
				if (puntos == 1000) {
					level = 1;
					enemigo = Content.Load<Texture2D> ("Graficos/enemigo2");
					probabilidadEnemigo = 0.05;
				}
				break;

			case 1:
				if (puntos == 2000) {
					level = 2;
					enemigo = Content.Load<Texture2D> ("Graficos/Enemigo3");
					probabilidadEnemigo = 0.07;
				}
				break;
			case 2:
				if (puntos == 4000) {
					level = 3;
					enemigo = Content.Load<Texture2D> ("Graficos/enemy");
					probabilidadEnemigo = 0.09;
				}
				break;
			case 3:
				break;
			}

        }
		public void UpdateBullets()
		{
			rectanguloPersona =
				new Rectangle ((int)player.Position.X-40, (int)player.Position.Y-40,
				               player.FrameWidth, player.FrameHeight);
			//player.Position;
			for (int i = 0; i < posicionesEnemigo.Count; i++) { 
				// actualizo las posiciones de las nave enemiga 
				posicionesEnemigo [i] = new Vector2 (posicionesEnemigo [i].X - velocidadEnemigo, 
				                                     posicionesEnemigo [i].Y);

				// obtener el rectangulo del enemigo
				rectanguloEnemigo = new Rectangle ((int)posicionesEnemigo [i].X,
				                                   (int)posicionesEnemigo [i].Y,
				                                   enemigo.Width, enemigo.Height);
				if (posicionesEnemigo [i].Y > Window.ClientBounds.Height) { 
					posicionesEnemigo.RemoveAt (i);
					// decrecemos i, por que hay un enemigo menos 
					i--;
				}
				if (rectanguloPersona.Intersects (rectanguloEnemigo)) {
					vidas = vidas - 1;
				}


				if (rectanguloPersona.Intersects (rectanguloEnemigo)) {
					posicionesEnemigo.RemoveAt (i);
					i--;
				}
			foreach (bullets bullet in bullets)
			{

				bullet.position += bullet.velocity;
				if (Vector2.Distance (bullet.position, player.Position) > 600)
					bullet.isVisible = false;
				bullet.brec = new Rectangle ((int)bullet.position.X, (int)bullet.position.Y,
				                           bullet.fuego.Width, bullet.fuego.Height);
					if (bullet.brec.Intersects (rectanguloEnemigo)) {
						bullet.isVisible = false;
					}

				
					if (bullet.brec.Intersects (rectanguloEnemigo)) {
						puntos = puntos + 100;
					}
					if (bullet.brec.Intersects (rectanguloEnemigo)) {
 						posicionesEnemigo.RemoveAt (i);
 						i--;
					}
				}

				                         
			}
			for(int i =  0; i < bullets.Count; i++)
			{
				if (!bullets [i].isVisible)
				{
					bullets.RemoveAt (i);
					i--;
				}
				//if (bulletrec.Intersects (rectanguloEnemigo)) {
 				//	bullets.RemoveAt (i);
				//	i--;
				//}

			}


		}
		public void shoot()
		{
			bullets newbullet = new Animad2.bullets (Content.Load<Texture2D>("Graficos/fuego"));
			newbullet.velocity = new Vector2 (newbullet.velocity.X += 2, newbullet.velocity.X -=2 );
			//newbullet.velocity = new Vector2 ((float)Math.Cos (rotation), (float)Math.Sin (rotation)) * 5f + spritevelocity;
			newbullet.position = player.Position + newbullet.velocity * 0;
			newbullet.isVisible = true;
			if (bullets.Count() < 20)
				bullets.Add (newbullet);

			//bulletrec = ((int) newbullet.position.X, (int) newbullet.position.Y,
			  //           newbullet.fuego.Width, newbullet.fuego.Height);

			
		}

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
           	
		
		
			switch (level)
			{
				case 0:
				spriteBatch.Begin();
				scrolling1.Draw (spriteBatch);
				scrolling2.Draw (spriteBatch);
				break;
			
				case 1:
				spriteBatch.Begin();
				scroll1.Draw (spriteBatch);
				scroll2.Draw (spriteBatch);
				break;

			case 2:
				spriteBatch.Begin();
				level3a.Draw (spriteBatch);
				level3b.Draw (spriteBatch);
				break;
			case 3:
				spriteBatch.Begin ();
				level4a.Draw (spriteBatch);
				level4b.Draw (spriteBatch);
				break;
			}
		//spriteBatch.Draw (fondo, posfondo, Color.White);
			foreach (bullets bullet in bullets) 
				bullet.Draw (spriteBatch);
			
		foreach (Vector2 posicionEnemigo in posicionesEnemigo) { 
			spriteBatch.Draw (enemigo, posicionEnemigo, Color.White);
		}

			spriteBatch.DrawString(fuente1, "destruyelos:"+puntos, posicioncor, Color.DarkRed);
			spriteBatch.DrawString(fuente1, "vidas:"+vidas, posvidas, Color.GhostWhite);
			spriteBatch.DrawString(fuente2,"nivel:"+level,poslev,Color.Goldenrod);
		player.Draw (spriteBatch);
			if (puntos >= 6000) {
				spriteBatch.Begin ();
				spriteBatch.Draw (fondo, posfondo, Color.AliceBlue);
				KeyboardState keyboard = Keyboard.GetState ();
				EndRun ();
				EndDraw ();
				posicionesEnemigo.Clear ();
				spriteBatch.DrawString(fuente1, "puntos:"+puntos, poscicionfp, Color.Black);
				spriteBatch.DrawString(fuente1, "vidas:"+vidas, poscicionfv, Color.DarkMagenta);
				spriteBatch.DrawString (fuente2, "oprima enter para salir", possprite, Color.DarkOrchid);
				if(keyboard.IsKeyDown (Keys.Enter))
				{
					this.Exit ();
				}
			}
				spriteBatch.End ();

				base.Draw (gameTime);
				if (vidas == 0) {
					Thread.Sleep (3000);
					this.Exit ();

				}
			if (puntos == 4000) {
				vidas = vidas + 1;
			}

        }
    }
}

