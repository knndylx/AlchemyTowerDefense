﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using AlchemyTowerDefense.Util;

namespace AlchemyTowerDefense
{
    public class Menu

    {
        TextureDict textures;
        int screenWidth, screenHeight;
        public List<Util.Button> buttons = new List<Util.Button>();
        //private int cursorx, cursory, gridx, gridy;

        //InputProcessor input = new InputProcessor();



        public Menu(int w, int h)
        {
            this.screenWidth = w;
            this.screenHeight = h;
        }

        public void Initialize()
        {
            
        }

        public void LoadContent(ContentManager c, List<string> buttonStringList)
        {
            textures = new TextureDict(c, "buttons");
            foreach (var s in buttonStringList)
            {
                buttons.Add(MakeButton(s, 300, 100));
            }
            FinalizeButtons();
        }

        //add a button centered horizontally to the menu
        public Util.Button MakeButton(string buttonName, int buttonWidth, int buttonHeight)
        {
            var texture = textures.dict[buttonName];
            var button = new Util.Button(texture, new Rectangle(0,
                                                                        0,
                                                                        buttonWidth,
                                                                        buttonHeight));
            //buttons.Add(button);
            return button;
        }

        public void FinalizeButtons()
        {
            for(var i = 0; i < buttons.Count; i++)
            {
                var b = buttons[i];
                b.ChangeRect(new Rectangle(screenWidth / 2 - b.rect.Width / 2,
                                          (screenHeight / (buttons.Count + 1)) + ((screenHeight / (buttons.Count + 1)) * i) - (b.rect.Height / 2),
                                          b.rect.Width,
                                          b.rect.Height));
            }
        }

        //draw all buttons
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var b in buttons)
            {
                b.Draw(spriteBatch);
            }
        }
    }
}
