using System;
using UnityEngine;

namespace Assets.UnityVS.Script
{
    class FixStartPoint
    {
        static readonly Vector3 fix_left_location = new Vector3(-2.8f,5,1);
        static readonly Vector3 fix_right_location = new Vector3(2.8f,5f,1);
        static readonly Vector3 fix_left_angle = new Vector3(0,0,270);//left to right
        static readonly Vector3 fix_right_angle = new Vector3(0,0,90);//right to left
        static readonly Vector3 fix_angle_135 = new Vector3(0,0,135);
        static readonly Vector3 fix_angle_215 = new Vector3(0,0,215);
        static readonly Vector3 fix_angle_120 = new Vector3(0, 0, 120);
        static readonly Vector3 fix_angle_240 = new Vector3(0, 0, 240);
        static readonly Vector3 fix_angle_down = new Vector3(0,0,180);

        #region fix left arc22
        public static readonly StartPoint[] b_left_arc22 = new StartPoint[]{
new StartPoint() {location=fix_left_location,angle=new Vector3(0,0,180)},
new StartPoint() {location=fix_left_location,angle=new Vector3(0,0,184)},
new StartPoint() {location=fix_left_location,angle=new Vector3(0,0,188)},
new StartPoint() {location=fix_left_location,angle=new Vector3(0,0,192)},
new StartPoint() {location=fix_left_location,angle=new Vector3(0,0,196)},
new StartPoint() {location=fix_left_location,angle=new Vector3(0,0,200)},
new StartPoint() {location=fix_left_location,angle=new Vector3(0,0,204)},
new StartPoint() {location=fix_left_location,angle=new Vector3(0,0,208)},
new StartPoint() {location=fix_left_location,angle=new Vector3(0,0,212)},
new StartPoint() {location=fix_left_location,angle=new Vector3(0,0,216)},
new StartPoint() {location=fix_left_location,angle=new Vector3(0,0,220)},
new StartPoint() {location=fix_left_location,angle=new Vector3(0,0,224)},
new StartPoint() {location=fix_left_location,angle=new Vector3(0,0,228)},
new StartPoint() {location=fix_left_location,angle=new Vector3(0,0,232)},
new StartPoint() {location=fix_left_location,angle=new Vector3(0,0,236)},
new StartPoint() {location=fix_left_location,angle=new Vector3(0,0,240)},
new StartPoint() {location=fix_left_location,angle=new Vector3(0,0,244)},
new StartPoint() {location=fix_left_location,angle=new Vector3(0,0,248)},
new StartPoint() {location=fix_left_location,angle=new Vector3(0,0,252)},
new StartPoint() {location=fix_left_location,angle=new Vector3(0,0,256)},
new StartPoint() {location=fix_left_location,angle=new Vector3(0,0,260)},
new StartPoint() {location=fix_left_location,angle=new Vector3(0,0,264)},
};
        #endregion

        #region fix left arc30
        public static readonly StartPoint[] b_left_arc30 = new StartPoint[]{
new StartPoint() {location=fix_left_location,angle=new Vector3(0,0,180)},
new StartPoint() {location=fix_left_location,angle=new Vector3(0,0,183)},
new StartPoint() {location=fix_left_location,angle=new Vector3(0,0,186)},
new StartPoint() {location=fix_left_location,angle=new Vector3(0,0,189)},
new StartPoint() {location=fix_left_location,angle=new Vector3(0,0,192)},
new StartPoint() {location=fix_left_location,angle=new Vector3(0,0,195)},
new StartPoint() {location=fix_left_location,angle=new Vector3(0,0,198)},
new StartPoint() {location=fix_left_location,angle=new Vector3(0,0,201)},
new StartPoint() {location=fix_left_location,angle=new Vector3(0,0,204)},
new StartPoint() {location=fix_left_location,angle=new Vector3(0,0,207)},
new StartPoint() {location=fix_left_location,angle=new Vector3(0,0,210)},
new StartPoint() {location=fix_left_location,angle=new Vector3(0,0,213)},
new StartPoint() {location=fix_left_location,angle=new Vector3(0,0,216)},
new StartPoint() {location=fix_left_location,angle=new Vector3(0,0,219)},
new StartPoint() {location=fix_left_location,angle=new Vector3(0,0,222)},
new StartPoint() {location=fix_left_location,angle=new Vector3(0,0,225)},
new StartPoint() {location=fix_left_location,angle=new Vector3(0,0,228)},
new StartPoint() {location=fix_left_location,angle=new Vector3(0,0,231)},
new StartPoint() {location=fix_left_location,angle=new Vector3(0,0,234)},
new StartPoint() {location=fix_left_location,angle=new Vector3(0,0,237)},
new StartPoint() {location=fix_left_location,angle=new Vector3(0,0,240)},
new StartPoint() {location=fix_left_location,angle=new Vector3(0,0,243)},
new StartPoint() {location=fix_left_location,angle=new Vector3(0,0,246)},
new StartPoint() {location=fix_left_location,angle=new Vector3(0,0,249)},
new StartPoint() {location=fix_left_location,angle=new Vector3(0,0,252)},
new StartPoint() {location=fix_left_location,angle=new Vector3(0,0,255)},
new StartPoint() {location=fix_left_location,angle=new Vector3(0,0,258)},
new StartPoint() {location=fix_left_location,angle=new Vector3(0,0,261)},
new StartPoint() {location=fix_left_location,angle=new Vector3(0,0,264)},
new StartPoint() {location=fix_left_location,angle=new Vector3(0,0,267)},
};
        #endregion

        #region fix right arc22
        public static readonly StartPoint[] b_right_arc22= new StartPoint[]{
new StartPoint() {location=fix_right_location,angle=new Vector3(0,0,96)},
new StartPoint() {location=fix_right_location,angle=new Vector3(0,0,100)},
new StartPoint() {location=fix_right_location,angle=new Vector3(0,0,104)},
new StartPoint() {location=fix_right_location,angle=new Vector3(0,0,108)},
new StartPoint() {location=fix_right_location,angle=new Vector3(0,0,112)},
new StartPoint() {location=fix_right_location,angle=new Vector3(0,0,116)},
new StartPoint() {location=fix_right_location,angle=new Vector3(0,0,120)},
new StartPoint() {location=fix_right_location,angle=new Vector3(0,0,124)},
new StartPoint() {location=fix_right_location,angle=new Vector3(0,0,128)},
new StartPoint() {location=fix_right_location,angle=new Vector3(0,0,132)},
new StartPoint() {location=fix_right_location,angle=new Vector3(0,0,136)},
new StartPoint() {location=fix_right_location,angle=new Vector3(0,0,140)},
new StartPoint() {location=fix_right_location,angle=new Vector3(0,0,144)},
new StartPoint() {location=fix_right_location,angle=new Vector3(0,0,148)},
new StartPoint() {location=fix_right_location,angle=new Vector3(0,0,152)},
new StartPoint() {location=fix_right_location,angle=new Vector3(0,0,156)},
new StartPoint() {location=fix_right_location,angle=new Vector3(0,0,160)},
new StartPoint() {location=fix_right_location,angle=new Vector3(0,0,164)},
new StartPoint() {location=fix_right_location,angle=new Vector3(0,0,168)},
new StartPoint() {location=fix_right_location,angle=new Vector3(0,0,172)},
new StartPoint() {location=fix_right_location,angle=new Vector3(0,0,176)},
new StartPoint() {location=fix_right_location,angle=new Vector3(0,0,180)},
};
        #endregion

        #region fix right arc30
        public static readonly StartPoint[] b_right_arc30= new StartPoint[]{
new StartPoint() {location=fix_right_location,angle=new Vector3(0,0,93)},
new StartPoint() {location=fix_right_location,angle=new Vector3(0,0,96)},
new StartPoint() {location=fix_right_location,angle=new Vector3(0,0,99)},
new StartPoint() {location=fix_right_location,angle=new Vector3(0,0,102)},
new StartPoint() {location=fix_right_location,angle=new Vector3(0,0,105)},
new StartPoint() {location=fix_right_location,angle=new Vector3(0,0,108)},
new StartPoint() {location=fix_right_location,angle=new Vector3(0,0,111)},
new StartPoint() {location=fix_right_location,angle=new Vector3(0,0,114)},
new StartPoint() {location=fix_right_location,angle=new Vector3(0,0,117)},
new StartPoint() {location=fix_right_location,angle=new Vector3(0,0,120)},
new StartPoint() {location=fix_right_location,angle=new Vector3(0,0,123)},
new StartPoint() {location=fix_right_location,angle=new Vector3(0,0,126)},
new StartPoint() {location=fix_right_location,angle=new Vector3(0,0,129)},
new StartPoint() {location=fix_right_location,angle=new Vector3(0,0,132)},
new StartPoint() {location=fix_right_location,angle=new Vector3(0,0,135)},
new StartPoint() {location=fix_right_location,angle=new Vector3(0,0,138)},
new StartPoint() {location=fix_right_location,angle=new Vector3(0,0,141)},
new StartPoint() {location=fix_right_location,angle=new Vector3(0,0,144)},
new StartPoint() {location=fix_right_location,angle=new Vector3(0,0,147)},
new StartPoint() {location=fix_right_location,angle=new Vector3(0,0,150)},
new StartPoint() {location=fix_right_location,angle=new Vector3(0,0,153)},
new StartPoint() {location=fix_right_location,angle=new Vector3(0,0,156)},
new StartPoint() {location=fix_right_location,angle=new Vector3(0,0,159)},
new StartPoint() {location=fix_right_location,angle=new Vector3(0,0,162)},
new StartPoint() {location=fix_right_location,angle=new Vector3(0,0,165)},
new StartPoint() {location=fix_right_location,angle=new Vector3(0,0,168)},
new StartPoint() {location=fix_right_location,angle=new Vector3(0,0,171)},
new StartPoint() {location=fix_right_location,angle=new Vector3(0,0,174)},
new StartPoint() {location=fix_right_location,angle=new Vector3(0,0,177)},
new StartPoint() {location=fix_right_location,angle=new Vector3(0,0,180)},
};
        #endregion

        #region fix line left9 和10进行交叉
        public static readonly StartPoint[] b_line_leftA= new StartPoint[]{
new StartPoint() {angle=fix_left_angle,location=new Vector3(-2.8f,4.75f,1)},
new StartPoint() {angle=fix_left_angle,location=new Vector3(-2.8f,3.75f,1)},
new StartPoint() {angle=fix_left_angle,location=new Vector3(-2.8f,2.75f,1)},
new StartPoint() {angle=fix_left_angle,location=new Vector3(-2.8f,1.75f,1)},
new StartPoint() {angle=fix_left_angle,location=new Vector3(-2.8f,0.75f,1)},
new StartPoint() {angle=fix_left_angle,location=new Vector3(-2.8f,-0.25f,1)},
new StartPoint() {angle=fix_left_angle,location=new Vector3(-2.8f,-1.25f,1)},
new StartPoint() {angle=fix_left_angle,location=new Vector3(-2.8f,-2.25f,1)},
new StartPoint() {angle=fix_left_angle,location=new Vector3(-2.8f,-3.25f,1)},
new StartPoint() {angle=fix_left_angle,location=new Vector3(-2.8f,-4.25f,1)},
};
        #endregion

        #region fix line left20
        public static readonly StartPoint[] b_line_leftB= new StartPoint[]{
new StartPoint() {angle=fix_left_angle,location=new Vector3(-2.8f,4.25f,1)},
new StartPoint() {angle=fix_left_angle,location=new Vector3(-2.8f,3.25f,1)},
new StartPoint() {angle=fix_left_angle,location=new Vector3(-2.8f,2.25f,1)},
new StartPoint() {angle=fix_left_angle,location=new Vector3(-2.8f,1.25f,1)},
new StartPoint() {angle=fix_left_angle,location=new Vector3(-2.8f,0.25f,1)},
new StartPoint() {angle=fix_left_angle,location=new Vector3(-2.8f,-0.75f,1)},
new StartPoint() {angle=fix_left_angle,location=new Vector3(-2.8f,-1.75f,1)},
new StartPoint() {angle=fix_left_angle,location=new Vector3(-2.8f,-2.75f,1)},
new StartPoint() {angle=fix_left_angle,location=new Vector3(-2.8f,-3.75f,1)},
new StartPoint() {angle=fix_left_angle,location=new Vector3(-2.8f,-4.75f,1)},
};
        #endregion

        #region fix line right19和20交叉
        public static readonly StartPoint[] b_line_rightA= new StartPoint[]{
new StartPoint() {angle=fix_right_angle,location=new Vector3(2.8f,4.5f,1)},
new StartPoint() {angle=fix_right_angle,location=new Vector3(2.8f,3.5f,1)},
new StartPoint() {angle=fix_right_angle,location=new Vector3(2.8f,2.5f,1)},
new StartPoint() {angle=fix_right_angle,location=new Vector3(2.8f,1.5f,1)},
new StartPoint() {angle=fix_right_angle,location=new Vector3(2.8f,0.5f,1)},
new StartPoint() {angle=fix_right_angle,location=new Vector3(2.8f,-0.5f,1)},
new StartPoint() {angle=fix_right_angle,location=new Vector3(2.8f,-1.5f,1)},
new StartPoint() {angle=fix_right_angle,location=new Vector3(2.8f,-2.5f,1)},
new StartPoint() {angle=fix_right_angle,location=new Vector3(2.8f,-3.5f,1)},
new StartPoint() {angle=fix_right_angle,location=new Vector3(2.8f,-4.5f,1)},
};
        #endregion

        #region fix line right10
        public static readonly StartPoint[] b_line_rightB= new StartPoint[]{
new StartPoint() {angle=fix_right_angle,location=new Vector3(2.8f,4f,1)},
new StartPoint() {angle=fix_right_angle,location=new Vector3(2.8f,3f,1)},
new StartPoint() {angle=fix_right_angle,location=new Vector3(2.8f,2f,1)},
new StartPoint() {angle=fix_right_angle,location=new Vector3(2.8f,1f,1)},
new StartPoint() {angle=fix_right_angle,location=new Vector3(2.8f,0f,1)},
new StartPoint() {angle=fix_right_angle,location=new Vector3(2.8f,-1f,1)},
new StartPoint() {angle=fix_right_angle,location=new Vector3(2.8f,-2f,1)},
new StartPoint() {angle=fix_right_angle,location=new Vector3(2.8f,-3f,1)},
new StartPoint() {angle=fix_right_angle,location=new Vector3(2.8f,-4f,1)},
new StartPoint() {angle=fix_right_angle,location=new Vector3(2.8f,-5f,1)},
};
        #endregion

        #region fix line top
        public static readonly StartPoint[] b_line_topA = new StartPoint[]{
new StartPoint() {angle=fix_angle_down,location=new Vector3(-2.5f,5f,1)},
new StartPoint() {angle=fix_angle_down,location=new Vector3(-1.5f,5f,1)},
new StartPoint() {angle=fix_angle_down,location=new Vector3(-0.5f,5f,1)},
new StartPoint() {angle=fix_angle_down,location=new Vector3(0.5f,5f,1)},
new StartPoint() {angle=fix_angle_down,location=new Vector3(1.5f,5f,1)},
new StartPoint() {angle=fix_angle_down,location=new Vector3(2.5f,5f,1)},
};
        #endregion

        #region fix line top
        public static readonly StartPoint[] b_line_topB = new StartPoint[]{
new StartPoint() {angle=fix_angle_down,location=new Vector3(-2f,5f,1)},
new StartPoint() {angle=fix_angle_down,location=new Vector3(-1f,5f,1)},
new StartPoint() {angle=fix_angle_down,location=new Vector3(0f,5f,1)},
new StartPoint() {angle=fix_angle_down,location=new Vector3(1f,5f,1)},
new StartPoint() {angle=fix_angle_down,location=new Vector3(2f,5f,1)},
};
        #endregion

        #region Circle 36
        public static StartPoint[] GetCircle36(Vector3 location)
        {
            StartPoint[] temp = new StartPoint[36];
            for (int i = 0; i < 36; i++)
                temp[i] = new StartPoint() { location = location, angle = new Vector3(0, 0, i * 10) };
            return temp;
        }
        #endregion

        #region three angle fix
        public static StartPoint[] GetFixAngle3(Vector3 location)
        {
            return new StartPoint[] { new StartPoint(){location=location}
            ,new StartPoint() { location=location,angle=fix_angle_120}
            ,new StartPoint() { location=location,angle=fix_angle_240}};
        }
        #endregion

        #region six angle 6
        public static StartPoint[] GetSixAngle(float angle,Vector3 location)
        {
            return
                new StartPoint[] { new StartPoint() { location = location, angle = new Vector3(0, 0, 0 + angle) },
                new StartPoint() { location = location, angle = new Vector3(0, 0, 120 + angle) },
                new StartPoint() { location = location, angle = new Vector3(0, 0, 240 + angle) },
                new StartPoint() { location = location, angle = new Vector3(0, 0, 120 - angle) },
                new StartPoint() { location = location, angle = new Vector3(0, 0, 240 - angle) },
                new StartPoint() { location = location, angle = new Vector3(0, 0, 360 - angle) } };
        }
        #endregion

        #region mesh left 10
        public static readonly StartPoint[] fix_line_angle215 = new StartPoint[]{
new StartPoint() {angle=fix_angle_215,location=new Vector3(-2.8f,4.75f,1)},
new StartPoint() {angle=fix_angle_215,location=new Vector3(-2.8f,3.75f,1)},
new StartPoint() {angle=fix_angle_215,location=new Vector3(-2.8f,2.75f,1)},
new StartPoint() {angle=fix_angle_215,location=new Vector3(-2.8f,1.75f,1)},
new StartPoint() {angle=fix_angle_215,location=new Vector3(-2.8f,0.75f,1)},
new StartPoint() {angle=fix_angle_215,location=new Vector3(-2.8f,-0.25f,1)},
new StartPoint() {angle=fix_angle_215,location=new Vector3(-2.8f,-1.25f,1)},
new StartPoint() {angle=fix_angle_215,location=new Vector3(-2.8f,-2.25f,1)},
new StartPoint() {angle=fix_angle_215,location=new Vector3(-2.8f,-3.25f,1)},
new StartPoint() {angle=fix_angle_215,location=new Vector3(-2.8f,-4.25f,1)},
};
        #endregion

        #region mesh right 10
        public static readonly StartPoint[] fix_right_angle135 = new StartPoint[]{
new StartPoint() {angle=fix_angle_135,location=new Vector3(2.8f,4.75f,1)},
new StartPoint() {angle=fix_angle_135,location=new Vector3(2.8f,3.75f,1)},
new StartPoint() {angle=fix_angle_135,location=new Vector3(2.8f,2.75f,1)},
new StartPoint() {angle=fix_angle_135,location=new Vector3(2.8f,1.75f,1)},
new StartPoint() {angle=fix_angle_135,location=new Vector3(2.8f,0.75f,1)},
new StartPoint() {angle=fix_angle_135,location=new Vector3(2.8f,-0.25f,1)},
new StartPoint() {angle=fix_angle_135,location=new Vector3(2.8f,-1.25f,1)},
new StartPoint() {angle=fix_angle_135,location=new Vector3(2.8f,-2.25f,1)},
new StartPoint() {angle=fix_angle_135,location=new Vector3(2.8f,-3.25f,1)},
new StartPoint() {angle=fix_angle_135,location=new Vector3(2.8f,-4.25f,1)},
};
        #endregion

        #region fix arc down 14
        public static StartPoint[] GetArcDown14(Vector3 location)
        {
            StartPoint[] temp = new StartPoint[14];
            float a = 145;
            for (int i = 0; i < 14; i++)
                temp[i] = new StartPoint() { location=location,angle=new Vector3(0,0,a+i*5)};
            return temp;
        }
        #endregion

        #region fix seven angle down 
        public static StartPoint[] GetsevenAngleA(Vector3 location)
        {
            StartPoint[] temp = new StartPoint[7];
            float a = 90;
            for (int i = 0; i < 7; i++)
                temp[i] = new StartPoint() { location = location, angle = new Vector3(0, 0, a + i * 30) };
            return temp;
        }
        #endregion

        #region enemy fix start point
        public static readonly StartPoint[] e_topline3_01 = new StartPoint[] {
            new StartPoint() { location=new Vector3(0f,5.5f,1)},
        new StartPoint() { location=new Vector3(-1f,5.5f,1)},
        new StartPoint() { location=new Vector3(-2f,5.5f,1)} };

        public static readonly StartPoint[] e_topline3_02 = new StartPoint[] {
             new StartPoint() { location=new Vector3(0f,5.5f,1)},
        new StartPoint() { location=new Vector3(1f,5.5f,1)},
        new StartPoint() { location=new Vector3(2f,5.5f,1)} };

        public static readonly StartPoint[] e_topline4_01 = new StartPoint[] {
            new StartPoint() { location=new Vector3(1f,5.5f,1)},
        new StartPoint() { location=new Vector3(0f,5.5f,1)},
        new StartPoint() { location=new Vector3(-1f,5.5f,1)},
        new StartPoint() { location=new Vector3(-2f,5.5f,1)}
        };

        public static readonly StartPoint[] e_lefttop2_01 = new StartPoint[] {
            new StartPoint() { location=new Vector3(-3f,5f,1)},
            new StartPoint() { location=new Vector3(-3f,4f,1)},
        };
        public static readonly StartPoint[] e_lefttop2_02 = new StartPoint[] {
            new StartPoint() { location=new Vector3(-3f,4f,1)},
            new StartPoint() { location=new Vector3(-3f,3f,1)},
        };
        public static readonly StartPoint[] e_lefttop2_03 = new StartPoint[] {
            new StartPoint() { location=new Vector3(-3f,3f,1)},
            new StartPoint() { location=new Vector3(-3f,2f,1)},
        };
        public static readonly StartPoint[] e_lefttop2_04 = new StartPoint[] {
            new StartPoint() { location=new Vector3(-3f,2f,1)},
            new StartPoint() { location=new Vector3(-3f,1f,1)},
        };
        public static readonly StartPoint[] e_lefttop2_05 = new StartPoint[] {
            new StartPoint() { location=new Vector3(-3f,1f,1)},
            new StartPoint() { location=new Vector3(-3f,0f,1)},
        };
        public static readonly StartPoint[] e_righttop2_01 = new StartPoint[] {
            new StartPoint() { location=new Vector3(3f,5f,1)},
            new StartPoint() { location=new Vector3(3f,4f,1)},
        };
        public static readonly StartPoint[] e_righttop2_02 = new StartPoint[] {
            new StartPoint() { location=new Vector3(3f,4f,1)},
            new StartPoint() { location=new Vector3(3f,3f,1)},
        };
        public static readonly StartPoint[] e_righttop2_03 = new StartPoint[] {
            new StartPoint() { location=new Vector3(3f,3f,1)},
            new StartPoint() { location=new Vector3(3f,2f,1)},
        };
        public static readonly StartPoint[] e_righttop2_04 = new StartPoint[] {
            new StartPoint() { location=new Vector3(3f,2f,1)},
            new StartPoint() { location=new Vector3(3f,1f,1)},
        };
        public static readonly StartPoint[] e_righttop2_05 = new StartPoint[] {
            new StartPoint() { location=new Vector3(3f,1f,1)},
            new StartPoint() { location=new Vector3(3f,0f,1)},
        };
        #endregion

    }
}
