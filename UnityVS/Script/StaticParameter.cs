using System;
using UnityEngine;

namespace Assets.UnityVS.Script
{
    class StaticParameter
    {
        public static readonly Vector3 def_angle45 = new Vector3(0, 0, 45);
        public static readonly Vector3 def_angle90 = new Vector3(0, 0, 90);
        public static readonly Vector3 def_angle270 = new Vector3(0, 0, 270);
        public static readonly Vector3 def_angle315 = new Vector3(0, 0, 315);
        public static readonly Vector3 def_left1 = new Vector3(-4, 6, 1);
        public static readonly Vector3 def_left2 = new Vector3(-4, 4, 1);
        public static readonly Vector3 def_left3 = new Vector3(-4, 3, 1);
        public static readonly Vector3 def_up1 = new Vector3(-2, 6f, 1);
        public static readonly Vector3 def_up2 = new Vector3(-1, 6f, 1);
        public static readonly Vector3 def_up3 = new Vector3(0, 6f, 1);
        public static readonly Vector3 def_up4 = new Vector3(1, 6f, 1);
        public static readonly Vector3 def_up5 = new Vector3(2, 6f, 1);
        public static readonly Vector3 def_right1 = new Vector3(4, 6, 1);
        public static readonly Vector3 def_right2 = new Vector3(4, 4, 1);
        public static readonly Vector3 def_right3 = new Vector3(4, 3, 1);
        public readonly static Vector2 pivot_center = new Vector2(0.5f, 0.5f);
        public static readonly SpriteInfo[] def_sprite512x512_4 = new SpriteInfo[] {
            new SpriteInfo() {rect= new Rect(0,0,256,256),pivot=pivot_center} ,
            new SpriteInfo() {rect= new Rect(256, 0, 256, 256), pivot=pivot_center},
            new SpriteInfo() {rect= new Rect(256,0,256,256), pivot=pivot_center},
            new SpriteInfo() {rect= new Rect(256, 256, 256, 256),pivot=pivot_center} };
        public static readonly SpriteInfo[] def_sprite1280x512_5x2 = new SpriteInfo[] {
            new SpriteInfo() {rect= new Rect(0, 0, 256, 256),pivot=pivot_center} ,
            new SpriteInfo() {rect= new Rect(256, 0, 256, 256), pivot=pivot_center},
            new SpriteInfo() {rect= new Rect(512, 0, 256, 256), pivot=pivot_center},
            new SpriteInfo() {rect= new Rect(768, 0, 256, 256),pivot=pivot_center},
            new SpriteInfo() {rect= new Rect(1024, 0, 256, 256),pivot=pivot_center} ,
            new SpriteInfo() {rect= new Rect(0, 256, 256, 256), pivot=pivot_center},
            new SpriteInfo() {rect= new Rect(256, 256, 256, 256), pivot=pivot_center},
            new SpriteInfo() {rect= new Rect(512, 256, 256, 256), pivot=pivot_center},
            new SpriteInfo() {rect= new Rect(768, 256, 256, 256), pivot=pivot_center},
            new SpriteInfo() {rect= new Rect(1024, 256, 256, 256),pivot=pivot_center}};
        public readonly static Grid def_2x2 = new Grid(2, 2);
        public readonly static Grid def_3x2 = new Grid(3, 2);
        public readonly static Grid def_4x1 = new Grid(4, 1);
        public readonly static Grid def_4x2 = new Grid(4, 2);
        public readonly static Grid def_4x3 = new Grid(4, 3);
        public readonly static Grid def_4x4 = new Grid(4, 4);
        public readonly static Grid def_4x6 = new Grid(4, 6);
        public readonly static Grid def_8x2 = new Grid(8, 2);
        public readonly static Grid def_8x6 = new Grid(8, 6);
        public readonly static Vector3 def_scale = new Vector3(1, 1), def_scale_l = new Vector3(-1, 1), def_angle180 = new Vector3(0, 0, 180), def_location = new Vector3(0, 0, 1);
        public static Material Mat_effect = Resources.Load("Shader/Effect") as Material;

        #region wave order
        public static System.Random random = new System.Random();
        public static StartPoint[] S_RandomDown_1()
        {
            float x = (float)random.NextDouble() * 4 - 2;
            return new StartPoint[] { new StartPoint() { location = new Vector3(x, 6, 1) } };
        }
        public static readonly StartPoint[] S_Up_1 = new StartPoint[] { new StartPoint() { location = def_up1 } };
        public static readonly StartPoint[] S_Up_2 = new StartPoint[] { new StartPoint() { location = def_up2 } };
        public static readonly StartPoint[] S_Up_3 = new StartPoint[] { new StartPoint() { location = def_up3 } };
        public static readonly StartPoint[] S_Up_4 = new StartPoint[] { new StartPoint() { location = def_up4 } };
        public static readonly StartPoint[] S_Up_5 = new StartPoint[] { new StartPoint() { location = def_up5 } };
        public static readonly StartPoint[] S_Dwon_2 = new StartPoint[] { new StartPoint() { location = def_up2 }, new StartPoint() { location = def_up4 } };
        public static readonly StartPoint[] S_Dwon_3 = new StartPoint[] { new StartPoint() { location = def_up1 },
            new StartPoint() { location = def_up3 } ,new StartPoint() { location = def_up5 }};
        public static readonly StartPoint[] S_UpLeft_1 = new StartPoint[] { new StartPoint() { location = def_left1, angle = def_angle45 } };
        public static readonly StartPoint[] S_UpRight_1 = new StartPoint[] { new StartPoint() { location = def_right1, angle = def_angle315 } };
        public static readonly StartPoint[] S_Left_1 = new StartPoint[] { new StartPoint() { location = def_left2, angle = def_angle90 } };
        public static readonly StartPoint[] S_Left_2 = new StartPoint[] { new StartPoint() { location = def_left2, angle = def_angle90 } ,
        new StartPoint() { location = def_left3, angle = def_angle90 }};
        public static readonly StartPoint[] S_Right_1 = new StartPoint[] { new StartPoint() { location = def_right2, angle = def_angle270 } };
        public static readonly StartPoint[] S_Right_2 = new StartPoint[] { new StartPoint() { location = def_right2, angle = def_angle270 } ,
        new StartPoint() { location = def_right3, angle = def_angle270 }};
        #endregion
    }
}
