﻿using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TopDownShooterFinal
{
    static class Player
    {
        //knife
        public static bool knifeEquiped = true;
        public static bool knifeAttacking = false;

        //handgun
        public static bool handgunEquiped = false;
        public static int handgunAmmo = 60;
        public static int handgunAmmoLoaded = 12;
        public static int handgunMagazineSize = 12;
        public static bool handgunReloading = false;
        public static bool handgunShooting = false;
        public static bool handgunMeleeAttacking = false;

        //rifle
        public static bool rifleEquiped = false;
        public static bool rifleShooting = false;
        public static bool rifleMeleeAttacking = false;
        public static int rifleAmmo = 90;
        public static int rifleAmmoLoaded = 30;
        public static int rifleMagazineSize = 30;
        public static bool rifleReloading = false;

        //shotgun
        public static bool shotgunEquiped = false;
        public static bool shotgunShooting = false;
        public static bool shotgunMeleeAttacking = false;
        public static int shotgunAmmo = 12;
        public static int shotgunAmmoLoaded = 1; //12 střel v jedný shelle
        public static int shotgunMagazineSize = 1;
        public static bool shotgunReloading = false;

        //remaining
        public static bool laserOn = false, moving = false;
        public static Vector2 position = new Vector2(Utils.screenWidth / 2, Utils.screenHeigth / 2);
        public static Texture2D texture = Textures.PlayerKnifeIdle;
        public static Circle hitboxCircle = new Circle(position, 35);
        public static Circle lureCircle;
        public static Rectangle hitboxRectangle = new Rectangle((int)position.X - 30, (int)position.Y - 30, 60, 60);
        public static Animation playerAnimation = new Animation(texture, 3, 4);
        public static int health = 100;
        public static int stamina = 100;
        private static int staminaNum = 36;
        public static int trackIndex = -1;
        private static int numToNextTrackIndex = 15;
        public static bool onHitFlash = false;
        public static int screenFlashNum = 0;
        public static bool rev = false;

        public static void Update(GameTime gameTime)
        {
            playerAnimation.Update(gameTime);
            hitboxCircle.Update(position);

            if (moving)
            {
                hitboxRectangle = new Rectangle((int)position.X - 30, (int)position.Y - 30, 60, 60);
                lureCircle = new Circle(position, 140);

                numToNextTrackIndex--;
                if (numToNextTrackIndex == 0)
                {
                    if (trackIndex < Manager.trackTextures.Count - 1)
                    {
                        trackIndex++;
                    }
                    else
                    {
                        trackIndex = 0;
                    }
                    Manager.tracks.Add(new Tracks(trackIndex, (float)playerAnimation.angle));
                    SetNumToNextTrackIndex();
                }
            }
            else
            {
                lureCircle = new Circle(position, 75);
                trackIndex = 0;
            }
            if (rifleShooting || handgunShooting || shotgunShooting) lureCircle = new Circle(position, 1200);

            if (stamina < 100)
            {
                staminaNum--;
                if (staminaNum == 0)
                {
                    stamina++;
                    staminaNum = 36;
                }
            }
        }

        private static void SetNumToNextTrackIndex()
        {
            if (knifeEquiped)
            {
                numToNextTrackIndex = 15;
            }
            else if (handgunEquiped)
            {
                numToNextTrackIndex = 18;
            }
            else if (shotgunEquiped)
            {
                numToNextTrackIndex = 25;
            }
            else if (rifleEquiped)
            {
                numToNextTrackIndex = 25;
            }
        }

        public static void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            playerAnimation.Draw(spriteBatch, position, gameTime);
            if (handgunEquiped)
            {
                spriteBatch.DrawString(Textures.debug, handgunAmmoLoaded + " / " + handgunAmmo, new Vector2(50, 50), Color.White);
            }
            else if (rifleEquiped)
            {
                spriteBatch.DrawString(Textures.debug, rifleAmmoLoaded + " / " + rifleAmmo, new Vector2(50, 50), Color.White);
            }
            else if (shotgunEquiped)
            {
                spriteBatch.DrawString(Textures.debug, shotgunAmmoLoaded + " / " + shotgunAmmo, new Vector2(50, 50), Color.White);
            }
        }
    }
}
