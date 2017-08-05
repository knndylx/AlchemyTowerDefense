using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AlchemyTowerDefense
{
    public class EditorState: GameState
    {
        GameData.Map map = new GameData.Map();

        TextureDict td;
        TextureDict mousetd;

        Texture2D tileBrushTile;
        Texture2D mouseTexture;
        Texture2D highlightGridTexture;

        InputProcessor mInputProcessor = new InputProcessor();

        int cursorx, cursory, gridx, gridy;
        bool canvasActive = true;

        Editor.Toolbox toolbox = new Editor.Toolbox();


        public override void Initialize()
        {
            //set up the input processor so that it looks for the only key that we need right now which is the t key for the toolbox menu pop-up
            List<Keys> listkeys = new List<Keys>();
            listkeys.Add(Keys.T);
            mInputProcessor.Initialize(listkeys);
        }

        private void UpdateCursor()
        {
            //snaps the cursor x and y coordinates to a grid so that you can draw the cursor highlight over the top of the grid to show which tile you are highlighting
            cursorx = Mouse.GetState().X;
            cursory = Mouse.GetState().Y;

            if (cursorx < 0)
                cursorx = 0;
            else if (cursorx > 1279)
                cursorx = 1279;
            if (cursory < 0)
                cursory = 0;
            else if (cursory > 959)
                cursory = 959;

            gridx = (int)Math.Floor(cursorx / 64.0);
            gridy = (int)Math.Floor(cursory / 64.0);

            cursorx = gridx * 64;
            cursory = gridy * 64;

            //Console.WriteLine(string.Format("Actual X: {0} Actual Y: {1} Grid X: {2} Grid Y: {3}", Mouse.GetState().X, Mouse.GetState().Y, cursorx, cursory));
        }

        public override void LoadContent(ContentManager c)
        {
            td = new TextureDict(c, "tiles");
            mousetd = new TextureDict(c, "icons");
            tileBrushTile = td.dict["towerDefense_tile069"];
            mouseTexture = mousetd.dict["cursor"];
            highlightGridTexture = mousetd.dict["highlight"];
            toolbox.LoadContent(c);
            map.LoadContent(c);
            base.LoadContent(c);
        }

        public override void Update()
        {
            mInputProcessor.Update();
            UpdateCursor();
            HandleInput();
            if(toolbox.active == true)
            {
                foreach(Util.Button b in toolbox.tileButtons)
                {
                    if (b.rect.Contains(Mouse.GetState().X, Mouse.GetState().Y))
                    {
                        b.Highlight(highlightGridTexture);
                    }
                    else
                    {
                        b.Dehighlight();
                    }
                }
            }
            base.Update();
        }

        private void HandleInput()
        {
            //if the left mouse button is pressed then paint the tile onto the map
            if(mInputProcessor.currentMouseState[Util.MouseButtons.Left] == ButtonState.Pressed && canvasActive == true)
            {
                //Console.WriteLine(string.Format("changing tile at x:{0} y:{1}",gridx, gridy));
                if (map.terrainTiles[gridy, gridx].texture != tileBrushTile)
                    map.changeTile(gridx, gridy, tileBrushTile);
            } else if(mInputProcessor.currentMouseState[Util.MouseButtons.Left] == ButtonState.Pressed && toolbox.active == true)
            {
                Util.Button hButton = null;
                foreach(Util.Button b in toolbox.tileButtons)
                {
                    if (b.highlight == true) hButton = b;
                }
                if (hButton != null)
                {
                    tileBrushTile = hButton.texture;
                }
            }

            //transition from the canvas being active to the toolbox being active
            if(canvasActive == true && mInputProcessor.currentButtonStates[Keys.T] == true && mInputProcessor.previousButtonStates[Keys.T] == false)
            {
                canvasActive = false;
                toolbox.active = true;
            }

            //transition from the toolbox being active to the canvas being active
            else if (canvasActive == false && mInputProcessor.currentButtonStates[Keys.T] == true && mInputProcessor.previousButtonStates[Keys.T] == false)
            {
                canvasActive = true;
                toolbox.active = false;
                map.SaveToFile("map.txt");
            }

            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            map.Draw(spriteBatch);
            spriteBatch.Draw(highlightGridTexture, new Vector2(cursorx, cursory), Color.White);
            toolbox.Draw(spriteBatch);
            spriteBatch.Draw(mouseTexture, new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, mouseTexture.Width, mouseTexture.Height), Color.White);
            base.Draw(spriteBatch);
        }


    }
}
