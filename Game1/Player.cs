using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Game1
{
    class Player : Animate
    {
        public Player(Vector2 position, Texture2D textureFront, Texture2D textureBack, double width, double height, double speed, double health) : base(position, textureFront, textureBack, width, height, speed, health)
        {
            
        }
    }
}
