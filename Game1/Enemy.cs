using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    class Enemy : Animate
    {
        public Enemy(Vector2 position, Texture2D textureFront, Texture2D textureBack, double width, double height, double speed, double health) : base(position, textureFront, textureBack, width, height, speed, health)
        {

        }

        public virtual int chooseDirectionX(Vector2 playerPosition)
        {
            return 0;
        }

        public virtual int chooseDirectionY(Vector2 playerPosition)
        {
            return 0;
        }
    }
}
