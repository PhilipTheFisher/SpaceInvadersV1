using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Space_Invaders_V1
{
    class Enemy : GameObject
    {
      

        public Enemy(Vector2 pos, Texture2D tex) : base(tex, pos)
        {
            vel = new Vector2(0, 1);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            pos.Y += vel.Y;

        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(tex, pos, null, Color.Red, MathHelper.Pi, origin, size, SpriteEffects.None, 1);
        }
    }
}
