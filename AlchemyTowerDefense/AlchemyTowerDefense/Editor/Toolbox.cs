using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AlchemyTowerDefense.Util;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AlchemyTowerDefense.Editor
{
    public class Toolbox
    {
        private Texture2D background;

        TextureDict toolboxTD;
        TextureDict tilesTextureDict;
        TextureDict decoTextures;
        TextureDict mouseTextures;

        public List<Button> tileButtons = new List<Button>();

        private Rectangle rect;
        public bool active = false;

        public void LoadContent(ContentManager c, TextureDict mouseTextures)
        {
            toolboxTD = new TextureDict(c, "toolbox");
            tilesTextureDict = new TextureDict(c, "tiles");
            decoTextures = new TextureDict(c, "decos");
            this.mouseTextures = mouseTextures;
            background = toolboxTD.dict["toolboxbackground"];
            rect = new Rectangle(1280 - background.Width, 0, background.Width, background.Height);
            PopulateToolbox();
        }

        private void PopulateToolbox()
        {
            List<Texture2D> tileList = new List<Texture2D>(tilesTextureDict.dict.Values);
            List<Texture2D> decoList = new List<Texture2D>(decoTextures.dict.Values);
            tileList = tileList.Concat(decoList).ToList();
            int i = 0;
            int k = 0;
            foreach(Texture2D t in tileList)
            {
                tileButtons.Add(new Button(t, new Rectangle((rect.Left + 20 + k * 84), (rect.Top + 20 + i * 84), 64, 64)));
                i++;
                if(i % (tileList.Count / 2) == 0)
                {
                    i = 0;
                    k++;
                }
            }
        }

        public void Update()
        {
            if (active)
            {
                foreach (Util.Button b in tileButtons)
                {
                    if (b.rect.Contains(Mouse.GetState().X, Mouse.GetState().Y))
                    {
                        b.Highlight(mouseTextures.dict["highlight"]);
                    }
                    else
                    {
                        b.Dehighlight();
                    }
                }
            }
        }

        private void HandleInput()
        {

        }

        public Texture2D Click()
        {
            Util.Button hButton = null;
            foreach (Util.Button b in tileButtons)
            {
                if (b.highlight == true) hButton = b;
            }
            if (hButton != null)
            {
                return hButton.texture;
            }
            else
            {
                return null;
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
