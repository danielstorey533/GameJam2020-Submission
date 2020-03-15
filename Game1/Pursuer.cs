using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    class Pursuer : Enemy
    {
        public Pursuer(Vector2 position, Texture2D textureFront, Texture2D textureBack, double width, double height, double speed, double health) : base(position, textureFront, textureBack, width, height, speed, health)
        {

        }

        public override int chooseDirectionX(Vector2 playerPosition)
        {
            if (playerPosition.X > this.getPosition().X)
            {
                return 1;
            }

            if (playerPosition.X < this.getPosition().X)
            {
                return 3;
            }

            return 4;
        }

        public override int chooseDirectionY(Vector2 playerPosition)
        {
            if (playerPosition.Y > this.getPosition().Y)
            {
                return 2;
            }

            if (playerPosition.Y < this.getPosition().Y)
            {
                return 0;
            }

            return 4;
        }

    }
}
