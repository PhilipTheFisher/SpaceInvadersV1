using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Space_Invaders_V1
{
    class Player : GameObject
    {
        SoundEffect enemyDeathSound;
        SoundEffect shootSound;
        Texture2D bulletTex;
        Bullet bullet;
        List<Bullet> bulletList;
        List<Enemy> enemyList;


        int score;
        int windowWidth;
        int frameCounter;
        int fireRate;
        public Player(Texture2D tex, Texture2D bulletTex, Vector2 pos, int windowWidth, SoundEffect shootSound, SoundEffect enemyDeathSound, List<Enemy> enemyList) : base(tex, pos)
        {
            this.enemyList = enemyList; 
            this.windowWidth = windowWidth;
            this.bulletTex = bulletTex;
            bulletList = new List<Bullet>();
            fireRate = 10;
            this.shootSound = shootSound;
            this.enemyDeathSound = enemyDeathSound;
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (Keyboard.GetState().IsKeyDown(Keys.Left) && pos.X > 0 + tex.Width / borderHit)
            {
                vel.X = - 5;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right) && pos.X < windowWidth - tex.Width / borderHit)
            {
                vel.X = + 5;
            }
            else
            {
                vel.X = 0;
            }
            pos += vel;
            frameCounter++;
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && frameCounter >= fireRate)
            {
                bullet = new Bullet(tex, pos, hitBox);
                bulletList.Add(bullet);
                frameCounter = 0;
                shootSound.Play();
            }
            for(int i = 0; i < bulletList.Count; i++)
            {
                bulletList[i].Update(gameTime);
            }
            for (int i = bulletList.Count - 1; i >= 0; i--)
            {
                Bullet bullet = bulletList[i];
                bullet.Update(gameTime);

                for (int j = enemyList.Count - 1; j >= 0; j--)
                {
                    Enemy enemy = enemyList[j];

                    if (bullet.GetHitbox().Intersects(enemy.GetHitbox()))
                    {
                        bulletList.RemoveAt(i);
                        enemyList.RemoveAt(j);
                        score++;
                        enemyDeathSound.Play();
                        break;
                    }
                }
            }
        }
        public int GetScore()
        {
            return score;
        }

        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
            for(int i = 0;i < bulletList.Count; i++)
            {
                bulletList[i].Draw(sb);
            }
        }
        
    }
}
