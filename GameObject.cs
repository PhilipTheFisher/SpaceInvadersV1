using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Space_Invaders_V1
{
    abstract class GameObject
    {
        protected Texture2D tex;


        public Vector2 pos;
        protected Vector2 origin;
        protected Vector2 vel;
        protected Rectangle hitBox;

        protected float borderHit = 3.8f;
        protected float size = 1;

        
        public GameObject(Texture2D tex, Vector2 pos)
        {
            this.pos = pos;
            this.tex = tex;
            origin.X = tex.Width / 2;
            origin.Y = tex.Height / 2;

            hitBox = new Rectangle((int)pos.X - tex.Width / 2, (int)pos.Y - tex.Height / 2, tex.Width, tex.Height);
        }

        public virtual void Update(GameTime gameTime) 
        {
            hitBox.X = (int)pos.X - tex.Width / 2;
            hitBox.Y = (int)pos.Y + tex.Height / 2;
        }

        public Rectangle GetHitbox() //Method to get the hitbox in the player class
        {
            return hitBox;
        }
        public virtual void Draw(SpriteBatch sb)
        {
            sb.Draw(tex, pos, null, Color.White, 0f, origin, size, SpriteEffects.None, 1);
           // sb.Draw(tex, pos, Color.White);
        }
    }
    
    
}
