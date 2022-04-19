using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DProject
{
 public class Sprite : Component
{
    protected Texture2D _texture;

    public Vector2 Position { get; set; }

    public Rectangle Rectangle
    {
        get { return new Rectangle((int)Position.X, (int)Position.Y, Game1.ScreenWidth, Game1.ScreenHeight); }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {

            spriteBatch.Draw(_texture, Position, Color.White);
    }

    public Sprite(Texture2D texture)
    {
        _texture = texture;
    }

    public override void Update(GameTime gameTime)
    {

    }
}
}
