using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AlchemyTowerDefense
{
    public class GameState
    {
        public int ScreenWidth { get; private set; }
        public int ScreenHeight { get; private set; }
        public TextureDict textures { get; protected set; }

        public GameState()
        {
            ScreenWidth = 1280;
            ScreenHeight = 960;
        }

        public virtual void Update() { }
        public virtual void LoadContent(ContentManager c) { }
        public virtual void Initialize() { }
        public virtual void Draw(SpriteBatch spriteBatch) { }

    }
}
