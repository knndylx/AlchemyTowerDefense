using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AlchemyTowerDefense
{
    public class GameStateManager
    {
        public int State { get; private set; }

        List<GameState> states = new List<GameState>();
        public GameState currentState { get; private set; }

        public GameStateManager(ContentManager c)
        {
            //add the three states to the states list
            MenuState mainmenu = new MenuState();
            states.Add(mainmenu);
            mainmenu.PressEvent += OnStateChange;
            states.Add(new PlayingState());
            states.Add(new EditorState());
            currentState = states[State];
        }

        public void Initialize()
        {
            foreach(GameState gs in states)
            {
                gs.Initialize();
            }
        }
        public void LoadContent(ContentManager c)
        {
            foreach(GameState gs in states)
            {
                gs.LoadContent(c);
            }
        }

        public void Update()
        {
            states[State].Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            states[State].Draw(spriteBatch);
        }

        public void OnStateChange(int i)
        {
            //Console.Write("state changed " + i + "//");
            State = i;
            currentState = states[State];
        }

        //private void ChangeActiveState(int i)
        //{
        //    State = i;
        //    currentState = states[State];
        //}

    }
}
