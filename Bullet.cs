using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Invaders_V1
{
    internal class Bullet : GameObject
    {
        public Bullet(Texture2D tex, Vector2 pos, Rectangle hitBox) : base(tex, pos)
        {
            size = 0.2f;
            vel = new Vector2(0, -10);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            pos += vel;

        }
    }
}
