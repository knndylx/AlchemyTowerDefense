using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AlchemyTowerDefense.Util;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AlchemyTowerDefense.Editor
{
    public class Toolbox
    {
        private Texture2D background;

        TextureDict toolboxTD;
        TextureDict tilesTextureDict;

        public List<Button> tileButtons = new List<Button>();

        private Rectangle rect;
        public bool active = false;

        public void LoadContent(ContentManager c)
        {
            toolboxTD = new TextureDict(c, "toolbox");
            tilesTextureDict = new TextureDict(c, "tiles");
            background = toolboxTD.dict["toolboxbackground"];
            rect = new Rectangle(1280 - background.Width, 0, background.Width, background.Height);
            PopulateToolbox();
        }

        private void PopulateToolbox()
        {
            List<Texture2D> tileList = new List<Texture2D>(tilesTextureDict.dict.Values);
            int i = 0;
            int k = 0;
            foreach(Texture2D t in tileList)
            {
                tileButtons.Add(new Button(t, new Rectangle((rect.Left + 20 + k * 84), (rect.Top + 20 + i * 84), 64, 64)));
                i++;
                if(i % 10 == 0)
                {
                    i = 0;
                    k++;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (active)
            {
                spriteBatch.Draw(background, rect, Color.White);
                foreach(Button b in tileButtons)
                {
                    b.Draw(spriteBatch);
                }
            }
        }
    }
}
