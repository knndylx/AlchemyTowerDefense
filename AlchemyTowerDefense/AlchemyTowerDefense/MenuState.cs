using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace AlchemyTowerDefense
{
    public class MenuState: GameState
    {
        //Menu menu;
        private int buttonCursorIndex = 0;
        List<Keys> kList = new List<Keys>
        {
            Keys.Up,
            Keys.Down,
            Keys.Enter
        };
        Menu menu;
        //Util.Button gamebutton;
        //Util.Button editorbutton;



        public delegate void PressDelegate(int i);
        public event PressDelegate PressEvent;

        public void Press()
        {
            PressEvent?.Invoke(buttonCursorIndex + 1);
        }

        //must specify the number of buttons that you want in the menu
        //FRAGILE SOLUTION
        //BUG
        public MenuState()
        {
        }

        public override void Initialize()
        {
            menu = new Menu(ScreenWidth, ScreenHeight);
            //menu.Initialize();
            base.LoadButtonStates(kList);
            base.Initialize();
        }


        public override void LoadContent(ContentManager c)
        {
            //Console.Write("load content in menu state class");
            menu.LoadContent(c, new List<string>
            {
                "gamebutton",
                "editorbutton"
            });
        }

        public override void Update()
        {
            ProcessInput();
            SelectButton();
            base.Update();
        }

        private void SelectButton()
        {
            //make the button selected
            for (int i = 0; i < menu.buttons.Count; i++)
            {
                Util.Button currentButton = menu.buttons[i];
                if (i == buttonCursorIndex) currentButton.Select();
                else currentButton.Deselect();
            }
        }

        public void ProcessInput()
        {
            previousButtonStates = new Dictionary<Keys, bool>(currentButtonStates);
            KeyboardState keyState = Keyboard.GetState();
            currentButtonStates[Keys.Up] = keyState.IsKeyDown(Keys.Up);
            currentButtonStates[Keys.Down] = keyState.IsKeyDown(Keys.Down);
            currentButtonStates[Keys.Enter] = keyState.IsKeyDown(Keys.Enter);

            //if the up button was pressed
            if(currentButtonStates[Keys.Up] && !previousButtonStates[Keys.Up])
            {
                if (buttonCursorIndex != 0) buttonCursorIndex--;
            }
            //else if the down button was pressed
            else if(currentButtonStates[Keys.Down] && !previousButtonStates[Keys.Down])
            {
                if (buttonCursorIndex != menu.buttons.Count - 1) buttonCursorIndex++;
            }
            //else if the enter button was pressed
            else if(currentButtonStates[Keys.Enter] && !previousButtonStates[Keys.Enter])
            {
                Press();
            }
        }



        //draw all buttons
        public override void Draw(SpriteBatch spriteBatch)
        {
            menu.Draw(spriteBatch);
        }
    }
}
