using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    class Shooter : Enemy
    {
        int destinationX;
        int destinationY;
        Random rnd = new Random();
        public Shooter(Vector2 position, Texture2D textureFront, Texture2D textureBack, double width, double height, double speed, double health) : base(position, textureFront, textureBack, width, height, speed, health)
        {
            this.newDestinationX();
            this.newDestinationY();
        }

        public override int chooseDirectionX(Vector2 playerPosition)
        {
            if (destinationX > this.getPosition().X)
            {
                return 1;
            }

            if (destinationX < this.getPosition().X)
            {
                return 3;
            }
            this.newDestinationX();
            return 4;
        }

        public override int chooseDirectionY(Vector2 playerPosition)
        {
            if (destinationY > this.getPosition().Y)
            {
                return 2;
            }

            if (destinationY < this.getPosition().Y)
            {
                return 0;
            }
            this.newDestinationY();
            return 4;
        }

        public void newDestinationX()
        {
            destinationX = 10*rnd.Next(20, 180);
        }

        public void newDestinationY()
        {
            destinationY = 10*rnd.Next(20, 80);
        }

    }
}
