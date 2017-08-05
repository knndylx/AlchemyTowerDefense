using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AlchemyTowerDefense.Util
{
    public class Button
    {
        public Texture2D texture { get; private set; }
        public Rectangle rect { get; private set; }
        public bool Selected { get; private set; }

        public bool highlight { get; private set; }
        private Texture2D highlightTexture;

        public void Highlight(Texture2D highlightTexture)
        {
            highlight = true;
            this.highlightTexture = highlightTexture;
        }

        public void Dehighlight()
        {
            highlight = false;
        }

        public void Select()
        {
            Selected = true;
        }

        public void Deselect()
        {
            Selected = false;
        }

        public Button(Texture2D texture, Rectangle rect)
        {
            this.texture = texture;
            this.rect = rect;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Selected == false) spriteBatch.Draw(texture, rect, Color.White);
            else spriteBatch.Draw(texture, rect, Color.LightGreen);
            if(highlight == true)
            {
                spriteBatch.Draw(highlightTexture, rect, Color.White);
            }
        }
    }
}
