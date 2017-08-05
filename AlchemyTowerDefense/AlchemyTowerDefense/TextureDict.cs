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

namespace AlchemyTowerDefense
{
    public class TextureDict
    {
        public Dictionary<string,Texture2D> dict { get; private set; }

        public TextureDict(ContentManager c, string contentFolder)
        {
            dict = LoadTextureContent(c, contentFolder);
        }

        //load the textures from the folder specified into the textureDict
        private Dictionary<string, Texture2D> LoadTextureContent(ContentManager c, string contentFolder)
        {
            Dictionary<string, Texture2D> td = new Dictionary<string, Texture2D>();

            DirectoryInfo dir = new DirectoryInfo(c.RootDirectory + "/" + contentFolder);
            if (!dir.Exists)
            {
                Console.Write(contentFolder);
                throw new DirectoryNotFoundException();
            }
            FileInfo[] files = dir.GetFiles("*.*");
            foreach (FileInfo file in files)
            {
                string key = Path.GetFileNameWithoutExtension(file.Name);

                td[key] = c.Load<Texture2D>(contentFolder + "/" + key);
            }

            return td;
        }
    }


}
