﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TopDownShooterFinal
{
    static class Cursor
    {
        public static int posX { get; set; }
        public static int posY { get; set; }
        public static Vector2 position { get; set; }
        public static Rectangle mouseRectangle { get; set; }
        public static Vector2 mousePosition { get; set; }
        private static Matrix invertedMatrix;

        public static void Update(GameTime gameTime)
        {
            posX = Mouse.GetState().X;
            posY = Mouse.GetState().Y;
            mousePosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            position = new Vector2(posX, posY);
            //source: https://community.monogame.net/t/solved-how-can-i-get-the-world-coords-of-the-mouse-2d/11263
            invertedMatrix = Matrix.Invert(Game1.camera.Transform);
            position = Vector2.Transform(position, invertedMatrix);
            posX = (int)position.X;
            posY = (int)position.Y;
            mouseRectangle = new Rectangle(posX, posY, 1, 1);
        }
    }
}
