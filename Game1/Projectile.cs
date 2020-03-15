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
    class Projectile
    {
        public Vector2 position;
        public Vector2 direction;
        public Texture2D texture;

        public double width;
        public double height;
        public float angle;
        public int speed = 10;


        public Projectile(Vector2 position, Texture2D texture, Vector2 direction, double width, double height) 
        {
            this.position = position;
            this.texture= texture;
            this.width = width;
            this.height = height;
            this.direction = direction;

            this.angle = (float) Math.Atan(direction.Y/direction.X);

        }

        public void move()
        {
            double magnitude = (double) Math.Sqrt((this.direction.X * this.direction.X) + (this.direction.Y * this.direction.Y));
            this.direction.X = (float) direction.X / (float) magnitude;
            this.direction.Y = (float) direction.Y / (float) magnitude;


            this.position.X = this.position.X + this.direction.X*speed;
            this.position.Y = this.position.Y + this.direction.Y*speed;

        }


    }
}
