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
        List<Util.Button> buttons = new List<Util.Button>();
        private int numButtons = 0;
        private int buttonCursorIndex = 0;
        Util.Button gamebutton;
        Util.Button editorbutton;

        Dictionary<Keys, bool> previousButtonStates = new Dictionary<Keys, bool>();
        Dictionary<Keys, bool> currentButtonStates = new Dictionary<Keys, bool>();

        public delegate void PressDelegate(int i);
        public event PressDelegate pressEvent;

        public void Press()
        {
            pressEvent?.Invoke(buttonCursorIndex + 1);
        }

        //must specify the number of buttons that you want in the menu
        //FRAGILE SOLUTION
        //BUG
        public MenuState(ContentManager c, int numButtons = 2)
        {
            this.numButtons = numButtons;
        }

        public void Initialize()
        {
            LoadButtonStates();

        }

        private void LoadButtonStates()
        {
            KeyboardState keyState = Keyboard.GetState();
            currentButtonStates.Add(Keys.Up, keyState.IsKeyDown(Keys.Up));
            currentButtonStates.Add(Keys.Down, keyState.IsKeyDown(Keys.Down));
            currentButtonStates.Add(Keys.Enter, keyState.IsKeyDown(Keys.Enter));

            previousButtonStates = new Dictionary<Keys, bool>(currentButtonStates);
        }

        public override void LoadContent(ContentManager c)
        {
            //Console.Write("load content in menu state class");
            textures = new TextureDict(c, "buttons");
            gamebutton = MakeButton("gamebutton", 300, 100);
            buttons.Add(gamebutton);
            editorbutton = MakeButton("editorbutton", 300, 100);
            buttons.Add(editorbutton);
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
            for (int i = 0; i < buttons.Count; i++)
            {
                Util.Button currentButton = buttons[i];
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
                if (buttonCursorIndex != buttons.Count - 1) buttonCursorIndex++;
            }
            //else if the enter button was pressed
            else if(currentButtonStates[Keys.Enter] && !previousButtonStates[Keys.Enter])
            {
                Press();
            }
        }

        //add a button centered horizontally to the menu
        public Util.Button MakeButton(string buttonName, int buttonWidth, int buttonHeight)
        {
            Texture2D texture = textures.dict[buttonName];
            Util.Button button = new Util.Button(texture, new Rectangle(ScreenWidth / 2 - buttonWidth / 2,
                                                              (ScreenHeight / (numButtons + 1) ) + ( (ScreenHeight / (numButtons + 1) ) * buttons.Count ) - (buttonHeight / 2),
                                                              buttonWidth,
                                                              buttonHeight));
            //buttons.Add(button);
            return button;
        }

        //draw all buttons
        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach(Util.Button b in buttons)
            {
                b.Draw(spriteBatch);
            }
        }
    }
}
