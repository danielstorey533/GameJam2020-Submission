using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D texturePlayerFront;
        Texture2D texturePlayerBack;
        Texture2D textureEnemyFront;
        Texture2D textureEnemyBack;
        Texture2D textureProjectile;
        Song popping;
        float angle = 0;
        Vector2 origin = new Vector2(0, 0);
        Vector2 originE = new Vector2(600, 600);
        Vector2 originE2 = new Vector2(600, 300);
        Player player;

        List<Animate> animates = new List<Animate>();
        List<Enemy> reallyEnemies = new List<Enemy>();

        List<Projectile> projectiles = new List<Projectile>();


        int timeBetweenShots = 300; // Thats 300 milliseconds
        int shotTimer = 1000;

        public Game1()
        {

            graphics = new GraphicsDeviceManager(this);
            graphics.ToggleFullScreen();
            this.IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = 1920;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = 1080;   // set this value to the desired height of your window
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            texturePlayerFront = this.Content.Load<Texture2D>("Goobender Front");
            texturePlayerBack = this.Content.Load<Texture2D>("Goobender Back");
            textureEnemyFront = this.Content.Load<Texture2D>("Robot Front");
            textureEnemyBack = this.Content.Load<Texture2D>("Robot Back");
            textureProjectile = this.Content.Load<Texture2D>("Projectile");
            popping = Content.Load<Song>("pop");
            player = new Player(origin, texturePlayerFront, texturePlayerBack, texturePlayerFront.Width*0.2, texturePlayerBack.Height*0.2, 10, 100);
            animates.Add(new Pursuer(originE, textureEnemyFront, textureEnemyBack, textureEnemyFront.Width * 0.2, textureEnemyBack.Height * 0.2, 2, 100));
            animates.Add(new Shooter(originE2, textureEnemyFront, textureEnemyBack, textureEnemyFront.Width * 0.2, textureEnemyBack.Height * 0.2, 4, 100));
            animates.Add(player);
            reallyEnemies.Add((Enemy)animates[0]);
            reallyEnemies.Add((Enemy)animates[1]);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            MouseState mouseState = Mouse.GetState();

            KeyboardState state = Keyboard.GetState();
            if (IsActive)
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back ==
                    ButtonState.Pressed || Keyboard.GetState().
                    IsKeyDown(Keys.Escape))
                    Exit();
                if (state.IsKeyDown(Keys.E))
                    MediaPlayer.Play(popping);
                if (state.IsKeyDown(Keys.D))
                    player.move(1, this.GraphicsDevice.Viewport.Width, this.GraphicsDevice.Viewport.Height, animates);
                if (state.IsKeyDown(Keys.A))
                    player.move(3, this.GraphicsDevice.Viewport.Width, this.GraphicsDevice.Viewport.Height, animates);
                if (state.IsKeyDown(Keys.W))
                    player.move(0, this.GraphicsDevice.Viewport.Width, this.GraphicsDevice.Viewport.Height, animates);
                if (state.IsKeyDown(Keys.S))
                    player.move(2, this.GraphicsDevice.Viewport.Width, this.GraphicsDevice.Viewport.Height, animates);

                for (int i = 0; i < reallyEnemies.Count; i++)
                {
                    int directionX = reallyEnemies[i].chooseDirectionX(player.getPosition());
                    int directionY = reallyEnemies[i].chooseDirectionY(player.getPosition());
                    if(directionX != 4)
                    {
                        reallyEnemies[i].move(directionX, this.GraphicsDevice.Viewport.Width, this.GraphicsDevice.Viewport.Height, animates);
                    }
                    if (directionY != 4)
                    {
                        reallyEnemies[i].move(directionY, this.GraphicsDevice.Viewport.Width, this.GraphicsDevice.Viewport.Height, animates);
                    }
                }
                base.Update(gameTime);
            }


            for (int i = 0; i < projectiles.Count; i++)
            {

                projectiles[i].move();

            }

            shotTimer += gameTime.ElapsedGameTime.Milliseconds;

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
           
                if (shotTimer > timeBetweenShots)
                {
                    shotTimer = 0;
                    Vector2 projectileDirection = new Vector2(mouseState.Position.X - player.getPosition().X, mouseState.Position.Y - player.getPosition().Y);
                    projectiles.Add(new Projectile(new Vector2(player.getPosition().X + player.getCurrentTexture().Width/10, player.getPosition().Y + player.getCurrentTexture().Height / 10), textureProjectile, projectileDirection, textureProjectile.Width, textureProjectile.Height));
                    base.Update(gameTime);
                }
            }


            base.Update(gameTime);
        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
             
            spriteBatch.Begin();
        
          //  Rectangle sourceRectangle = new Rectangle(0, 0, 400, 400);

            //spriteBatch.Draw(texture, position, sourceRectangle, Color.White, angle, origin, 1.0f, SpriteEffects.None, 1);
            spriteBatch.Draw(player.getCurrentTexture(), player.getPosition(), null, Color.White, 0f, Vector2.Zero, 0.2f, SpriteEffects.None, 0f);
            spriteBatch.Draw(animates[0].getCurrentTexture(), animates[0].getPosition(), null, Color.White, 0f, Vector2.Zero, 0.2f, SpriteEffects.None, 0f);
            spriteBatch.Draw(animates[1].getCurrentTexture(), animates[1].getPosition(), null, Color.White, 0f, Vector2.Zero, 0.2f, SpriteEffects.None, 0f);

            for (int i = 0; i < projectiles.Count; i++)
            {
                angle = projectiles[i].angle;
                if (projectiles[i].direction.X < 0)
                {
                    angle = angle + MathHelper.Pi*2;
                }
                else
                {
                    angle = angle + MathHelper.Pi;
                }
                if (projectiles[i].direction.X < 0)
                {
                    spriteBatch.Draw(textureProjectile, projectiles[i].position, null, Color.White, angle, Vector2.Zero, 0.1f, SpriteEffects.None, 0f);
                }
                else
                {
                    spriteBatch.Draw(textureProjectile, projectiles[i].position, null, Color.White, angle, Vector2.Zero, 0.1f, SpriteEffects.FlipVertically, 0f);
                }     
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
