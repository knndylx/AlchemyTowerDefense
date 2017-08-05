using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace AlchemyTowerDefense
{
    public class Menu
    {
        TextureDict textures;
        int screenWidth, screenHeight;

        public Menu(ContentManager c, int w, int h)
        {
            this.screenWidth = w;
            this.screenHeight = h;
            //textures = new TextureDict(c, "button");
        }
    }
}
