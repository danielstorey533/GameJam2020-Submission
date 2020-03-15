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
    class Animate
    {
        Vector2 position;
        Texture2D textureFront;
        Texture2D textureBack;
        Boolean isFront;
        double width;
        double height;
        double speed;
        double health;

        public Animate(Vector2 position, Texture2D textureFront, Texture2D textureBack, double width, double height, double speed, double health)
        {
            this.position = position;
            this.textureFront = textureFront;
            this.textureBack = textureBack;
            isFront = true;
            this.width = width;
            this.height = height;
            this.speed = speed;
            this.health = health;
        }

        public double getHeight()
        {
            return height;
        }

        public double getWidth()
        {
            return width;
        }

        public void setIsFront(bool b)
        {
            isFront = b;
        }

        public Texture2D getCurrentTexture()
        {
            if (isFront)
            {
                return textureFront;
            }
            return textureBack;
        }
        public Vector2 getPosition()
        {
            return position;
        }

        public virtual void move(int direction, int screenWidth, int screenHeight, List<Animate> animates)
        {
            //up
            if(direction == 0 && position.Y > 0 && this.collide(0, animates))
            {
                position.Y = position.Y - (float)speed;
                isFront = false;
            }
            //right
            if (direction == 1 && position.X + width < screenWidth && this.collide(1, animates))
            {
                position.X = position.X + (float)speed;
                isFront = true;
            }
            //down
            if (direction == 2 && position.Y + height < screenHeight && this.collide(2, animates))
            {
                position.Y = position.Y + (float)speed;
                isFront = true;
            }
            //left
            if (direction == 3 && position.X > 0 && this.collide(3, animates))
            {
                position.X = position.X - (float)speed;
                isFront = true;
            }
        }

        public bool collide(int direction, List<Animate> animates)
        {
            double up = position.Y;
            double down = position.Y + height;
            double left = position.X;
            double right = position.X + width;
            for(int i = 0; i < animates.Count(); i++)
            {
                double leftBound = animates[i].getPosition().X;
                double rightBound = animates[i].getPosition().X + animates[i].getWidth();
                double upperBound = animates[i].getPosition().Y;
                double lowerBound = animates[i].getPosition().Y + animates[i].getHeight();
                //up
                if (direction == 0 && up < lowerBound && up > upperBound && right > leftBound && left < rightBound)
                {
                    return false;
                }
                //right
                if (direction == 1 && right > leftBound && right < rightBound && down > upperBound && up < lowerBound)
                {
                    return false;
                }
                //down
                if (direction == 2 && down > upperBound && down < lowerBound && right > leftBound && left < rightBound)
                {
                    return false;
                }
                //left
                if (direction == 3 && left < rightBound && left > leftBound && down > upperBound && up < lowerBound)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
