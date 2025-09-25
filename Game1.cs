using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Invaders_V1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        //Creating Texture2D so i can load them later
        Texture2D backGround;
        Texture2D playerTex;
        Texture2D enemyTex;
        Texture2D bulletTex;

        //Creating sounds so i can load them later
       // Song backGroundMusic;
        SoundEffect shootSound;
        SoundEffect gameOverSound;
        SoundEffect enemyDeathSound;


        Player myPlayer;
        Enemy enemy;
        Bullet bullet;
        List<Enemy> enemyList;
        
        //Creating Vectors
        Vector2 windowSize;
        Vector2 playerSpawnPos;
        Vector2 enemySpawnPos;

        //Int variabels for life
        int lifeLeft;
        int maxLife;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

        }

        protected override void Initialize()
        {
            //List so enemyList gets initialized before i can start shooting bullets so collisions work
            enemyList = new List<Enemy>();

            //Decides the window size
            windowSize = new Vector2(500, 800);


            graphics.PreferredBackBufferHeight = (int)windowSize.Y;
            graphics.PreferredBackBufferWidth = (int)windowSize.X;

            graphics.ApplyChanges();

            base.Initialize();
           
        }

        protected override void LoadContent()
        {
            //Loading all the Content
            spriteBatch = new SpriteBatch(GraphicsDevice);
            backGround = Content.Load<Texture2D>("space2");
            playerTex = Content.Load<Texture2D>("player");
            enemyTex = Content.Load<Texture2D>("enemy");
            bulletTex = Content.Load<Texture2D>("Ship_01-1");
            shootSound = Content.Load<SoundEffect>("shoot");
            gameOverSound = Content.Load<SoundEffect>("explosion");
            enemyDeathSound = Content.Load<SoundEffect>("invaderkilled");
           // backGroundMusic = Content.Load<Song>("musicloop2");

            maxLife = 3;
            lifeLeft = maxLife;


            //Logic to spawn out rows of enemies
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    enemy = new Enemy(new Vector2(enemyTex.Width/2  + i * 50, 50 + j * enemyTex.Height), enemyTex);
                    enemyList.Add(enemy);
                }
            }

            //Decides the player spawn position and also the enemy spawn position
            playerSpawnPos.X = Window.ClientBounds.Width / 2;
            playerSpawnPos.Y = Window.ClientBounds.Height- 50;
            enemySpawnPos = new Vector2(100, 100);

            // Sends information from Game1 to the enemy and player classes
            enemy = new Enemy(enemySpawnPos, enemyTex);
            myPlayer = new Player(playerTex, bulletTex, playerSpawnPos, (int)windowSize.X, shootSound, enemyDeathSound, enemyList);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            myPlayer.Update(gameTime);
          //  MediaPlayer.Play(backGroundMusic);
          //  MediaPlayer.IsRepeating = true;


            //Logic for enemies to make the player lose a life as soon as they reach the bottom
            for (int i = 0; i < enemyList.Count; i++)
            {
                enemyList[i].Update(gameTime);
                if (enemyList[i].pos.Y >= Window.ClientBounds.Height -50)
                {
                    lifeLeft--;
                    enemyList.Remove(enemyList[i]);
                    i--;
                }
                if (lifeLeft == 0)
                {
                    gameOverSound.Play();
                   // Exit();
                }
                    
            }
   
            // Logic to add score and lifes left on the window title
            Window.Title = "Score: " + myPlayer.GetScore() + " Life: " + lifeLeft;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            //Drawing out the background and stretching the image out to fit the whole window
            spriteBatch.Draw(backGround, destinationRectangle: new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
            
            //Drawing out the player
            myPlayer.Draw(spriteBatch);

            //Drawing out the enemies
            foreach (Enemy enemy in enemyList)
            {
                enemy.Draw(spriteBatch);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
