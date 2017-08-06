using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace AlchemyTowerDefense.GameData
{
    public class Map
    {
        TextureDict textureDict;

        //creates a new array of tiles based on row and height
        //List<Tile> terrainTiles = new List<Tile>();

        //y, x OR row, column
        public Tile[,] terrainTiles = new Tile[15, 20];
        public List<Decoration> Decorations { get; private set; }


        //size of the terrain tiles that will be used
        int size = 64;

        public void LoadContent(ContentManager c, string fileName = null)
        {
            textureDict = new TextureDict(c, "tiles");
            if (fileName == null)
            {
                terrainTiles = CreateBlankMap();
            }
            else
            {
                terrainTiles = LoadFromFile("map.txt");
            }
        }

        public Tile[,] CreateBlankMap()
        {
            Tile[,] terrainTiles = new Tile[15,20];
            string tileType = "blank";

            int x = 0;
            int y = 0;
            for (x = 0; x < 20; x++)
            {
                for (y = 0; y < 15; y++)
                {
                    terrainTiles[y,x] = new Tile(new Rectangle(x * size, y * size, size, size), textureDict.dict[tileType]);

                }
            }
            return terrainTiles;
        }



        //generates the map based on the file specified
        public Tile[,] LoadFromFile(string mapFileName)
        {
            //List<List<string>> mapText = new List<List<string>>();
            Tile[,] terrainTiles = new Tile[15,20];

            List<string> mapText = GetTerrainTextFromFile(mapFileName);

            int x = 0;
            int y = 0;
            for (x = 0; x < 20; x++)
            {
                for (y = 0; y < 15; y++)
                {
                    string tileType = mapText[x*y];
                    terrainTiles[y,x] = new Tile(new Rectangle(x * size, y * size, size, size), textureDict.dict[tileType]);

                }
            }
            return terrainTiles;
        }

        //save to file
        public void SaveToFile(string mapFileName)
        {
            using (var sw = new StreamWriter(mapFileName))
            {
                foreach(Tile t in terrainTiles)
                {
                    sw.WriteLine(t.texture.Name);
                }
            }
        }

        //get the strings of the tile types from a text file
        private List<string> GetTerrainTextFromFile(string mapFileName)
        {
            List<string> mapText = new List<string>();

            string line;
            // Read the file and display it line by line.
            System.IO.StreamReader file = new System.IO.StreamReader(mapFileName);
            while ((line = file.ReadLine()) != null)
            {
                mapText.Add(line);
            }
            file.Close();
            return mapText;
        }

        public void changeTile(int x, int y, Texture2D t)
        {
            terrainTiles[y,x] = new Tile(new Rectangle(x * size, y * size, size, size), t); 
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(Tile t in terrainTiles)
            {
                t.draw(spriteBatch);
            }
        }

    }
}
