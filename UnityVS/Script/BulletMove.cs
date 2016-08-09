using System;
using UnityEngine;

namespace Assets.UnityVS.Script
{
    class BulletMove:GameControl
    {
        static int diamon_index = 0;
        static Point3[] diamond_increment = new Point3[12] { new Point3(0,-0.1f,180),new Point3(0.033f,0.038f,200),new Point3(0.0165f,0.076f,200),
            new Point3(0.05f,0,180),new Point3(-0.0165f,0.076f,160),new Point3(-0.033f,0.038f,160),new Point3(0,0.1f,180),new Point3(-0.033f,-0.038f,200),
            new Point3(-0.0165f,-0.076f,200),new Point3(-0.05f,0,180),new Point3(0.0165f,-0.076f,160),new Point3(0.033f,-0.038f,160)};

        #region bullet move
        public static int LockEnemy(Vector3 location)
        {
            float x = location.x;
            float y = location.y;
            float d = 100;
            int index = -1;
            for (int l = 0; l < 20; l++)
            {
                if (buff_enemy[l].move != null)
                {
                    float x1 = buff_enemy[l].location.x - x;
                    float y1 = buff_enemy[l].location.y - y;
                    float d1 = x1 * x1 + y1 * y1;
                    if (d > d1)
                    {
                        d = d1;
                        index = l;
                    }
                }
            }
            return index;
        }
        public static void Play_6_1(ref BulletState state)
        {
            if (state.extra3 % 6 == 0)
            {               
                state.sptindex = state.extra3 / 6;
                state.update = true;
            }
            state.extra3++;
            if (state.extra3 > 35)
            {
                state.extra3 = 0;
                state.angle.z =(float) lucky.NextDouble()*360;
            }               
        }
        public static void Play_7_1(ref BulletState state)
        {
            if (state.extra3 % 6 == 0)
            {
                state.sptindex = state.extra3 / 6;
                state.update = true;
            }
            state.extra3++;
            if (state.extra3 > 36)
                state.extra3 = 0;
        }
        public static void Play_7_4(ref BulletState state)
        {
            if (state.extra % 3 == 0)
            {
                state.sptindex = state.extra3 / 3;
                state.update = true;
            }
            if (state.extra3 > 12)
                state.extra3 = 6;
            state.extra3++;
        }
        public static void Play_Def16(ref BulletState state)
        {
            if (state.extra3 % 3 == 0)
            {
                state.angle.z += 3;
                if (state.angle.z > 360)
                    state.angle.z = 0;
                if (state.extra3 > 45)
                    state.extra3 = 0;
                state.sptindex = state.extra3 / 3;
                state.update = true;
            }
            state.extra3++;
        }
        public static void B_Aim(int id, ref BulletState state)
        {           
            if (state.extra == 0)
            {
                float s = buff_bullet[id].speed;
                
                float z = state.angle.z = Aim(ref state.location, ref core_location);
                state.movexyz.x = angle_table[(int)z].x * s;
                state.movexyz.y = angle_table[(int)z].y * s;
            }
            state.extra++;
            float x= state.location.x += state.movexyz.x;
            float y= state.location.y += state.movexyz.y;
            if (y > 5.5f | y < -5.5f | x > 3f | x < -3f)
            {
                state.extra = 0;
                state.active = false;
            }
        }
        public static void B_ArcTrace(int id,ref BulletState state)
        {
            if (state.extra == 0)
            {
                state.extra++;
                state.movexyz.z = state.angle.z;
                int z = (int)state.angle.z;
                state.movexyz.x = angle_table[z].x * buff_bullet[id].speed;
                state.movexyz.y = angle_table[z].y * buff_bullet[id].speed;
                int t = -1;
                float d = 100;
                for (int i = 0; i < 20; i++)
                {
                    float tx = state.location.x;
                    float ty = state.location.y;
                    if (buff_enemy[i].move != null)
                    {
                        float x1 = buff_enemy[i].location.x - tx;
                        float y1 = buff_enemy[i].location.y - ty;
                        float d1 = x1 * x1 + y1 * y1;
                        if (d1 < d)
                        {
                            d = d1;
                            t = i;
                        }
                    }
                }
                state.extra2 = t;//save the enemy id
                return;
            }           
            Vector3 temp = state.location;
            float x = temp.x;
            float y = temp.y;
            if (y > 5.5f | y < -5.5f | x > 3f | x < -3f)
            {
                state.active = false;
                state.extra = 0;
                state.extra2 = 0;
                return;
            }
            if (state.extra2 >= 0)
            {
                if (!buff_enemy[state.extra2].die)
                {
                    float a = Aim(ref state.location,ref buff_enemy[state.extra2].location);
                    float z = state.movexyz.z;
                    if(a>z)//顺时针
                    {
                        float b = a - z;//顺时针
                        float c = 360 - a + z;
                        if (b>c)
                        {
                            z-=5;
                        }
                        else
                        {
                            z+=5;
                        }
                    }
                    else//逆时针
                    {
                        float b = z-a;//逆时针
                        float c = 360 - z+a;
                        if (b>c)
                        {
                            z+=5;
                        }
                        else
                        {
                            z-=5;
                        }
                    }
                    if (z > 359)
                        z = 0;
                    if (z < 0)
                        z = 359;
                    state.movexyz.z = z;
                    state.movexyz.x = angle_table[(int)z].x * buff_bullet[id].speed;
                    state.movexyz.y = angle_table[(int)z].y * buff_bullet[id].speed;
                }
                else
                    state.extra2 = -1;
            }
            state.location.x += state.movexyz.x;
            state.location.y += state.movexyz.y;            
            if((id&1)==0)
            {
                state.angle.z-=5;
                if (state.angle.z < 0)
                    state.angle.z = 359;
            }
            else
            {
                state.angle.z+=5;
                if (state.angle.z > 359)
                    state.angle.z = 0;
            }
        }
        public static void B_RotateWithParentA(int id, ref BulletState state)
        {
            if(state.extra==0)
            {
                int z = (int)state.angle.z;
                state.movexyz.z = z;
                state.movexyz.x = angle_table[z].x * 0.05f;
                state.movexyz.y = angle_table[z].y * 0.05f;
            }
            state.extra++;
            if (state.extra<30)
            {
                state.location.x += state.movexyz.x;
                state.location.y += state.movexyz.y;
                return;
            }
            state.movexyz.z++;
            if (state.movexyz.z > 360)
                state.movexyz.z -= 360;
            int a = (int)state.movexyz.z;
            int eid = buff_bullet[id].parentid;
            state.location.x = buff_enemy[eid].location.x + angle_table[a].x * 1.5f;
            state.location.y = buff_enemy[eid].location.y + angle_table[a].y * 1.5f;
            if(state.extra>300)
            {
                state.active = false;
                state.extra = 0;
            }
        }
        public static void B_RotateWithParentB(int id, ref BulletState state)
        {
            if (state.extra == 0)
            {
                int z = (int)state.angle.z;
                state.movexyz.z = z;
                state.movexyz.x = angle_table[z].x * 0.05f;
                state.movexyz.y = angle_table[z].y * 0.05f;
            }
            state.extra++;
            if (state.extra < 30)
            {
                state.location.x += state.movexyz.x;
                state.location.y += state.movexyz.y;
                return;
            }
            state.movexyz.z--;
            if (state.movexyz.z < 0)
                state.movexyz.z += 360;
            int a = (int)state.movexyz.z;
            int eid = buff_bullet[id].parentid;
            state.location.x = buff_enemy[eid].location.x + angle_table[a].x * 1.5f;
            state.location.y = buff_enemy[eid].location.y + angle_table[a].y * 1.5f;
            if (state.extra > 300)
            {
                state.active = false;
                state.extra = 0;
            }
        }
        public static void B_CiecleDisappear(int id, ref BulletState state)
        {
            state.extra++;
            if (state.extra < 20)
            {
                state.location.y -= 0.02f;
                return;
            }
            if (state.extra == 20)
            {
                int z = (int)state.angle.z;
                state.movexyz.x = angle_table[z].x * 0.02f;
                state.movexyz.y = angle_table[z].y * 0.02f;
                return;
            }
            if (state.extra > 200)
            {
                state.active = false;
                state.extra = 0;
                return;
            }
            else
            {
                state.location.x += state.movexyz.x;
                state.location.y += state.movexyz.y;
            }
        }
        public static void B_CiecleExpolde(int id, ref BulletState state)
        {
            if (state.location.x < -3 | state.location.x > 3 | state.location.y < -5.5f | state.location.x > 5.5f)
            {
                state.active = false;
                state.extra = 0;
                return;
            }
            state.extra++;
            if (state.extra < 80)
            {
                state.location.y -= 0.04f;
                return;
            }
            if (state.extra == 80)
            {
                int z = (int)state.angle.z;
                state.movexyz.x= angle_table[z].x * 0.02f;
                state.movexyz.y = angle_table[z].y * 0.02f;
                return;
            }
            else
            {
                state.location.x += state.movexyz.x;
                state.location.y += state.movexyz.y;
            }
        }
        public static void B_ForwardToDown(int id, ref BulletState state)
        {
            if (state.location.x < -3 | state.location.x > 3 | state.location.y < -5.5f | state.location.x > 5.5f)
            {
                state.active = false;
                state.extra = 0;
                return;
            }
            if (state.extra == 0)
            {
                int z = (int)state.angle.z;
                state.movexyz.x = -angle_table[z].x * buff_bullet[id].speed;
                state.movexyz.y = angle_table[z].y *buff_bullet[id].speed;
            }
            if(state.extra==60)
            {
                if (state.angle.z < 180)
                    state.movexyz.x= -0.008f;                
                else state.movexyz.x = 0.008f;
                state.movexyz.y = -0.02f;
            }
            state.location.x += state.movexyz.x;
            state.location.y += state.movexyz.y;
            state.extra++;
        }
        public static void B_Diamond(int id, ref BulletState state)//frist parament is bullet id
        {
            if (state.extra == 0)
            {
                state.extra++;
                state.extra2 = diamon_index;
                state.angle.z = diamond_increment[diamon_index].z;
                diamon_index++;
                if (diamon_index > 11)
                    diamon_index = 0;
            }
            if (state.extra < 10)
            {
                state.location.x += diamond_increment[state.extra2].x;
                state.location.y += diamond_increment[state.extra2].y;
                state.extra++;
            }
            else
                state.location.y -= 0.05f;
            if (state.location.y < -5.5f)
            {
                state.active = false;
                state.extra = 0;
                state.extra2 = 0;
            }
        }
        public static void B_LockEnemy(int id, ref BulletState state)
        {
            if (state.extra == 0)
            {
                state.movexyz.z = state.angle.z;
                int z = (int)state.angle.z;
                state.movexyz.x= angle_table[z].x * 0.07f;
                state.movexyz.y = angle_table[z].y * 0.07f;
            }
            state.extra++;
            Vector3 temp = state.location;
            float x = temp.x;
            float y = temp.y;
            if (y > 5.5f | y < -5.5f | x > 3f | x < -3f)
            {
                state.active = false;
                state.extra = 0;
                state.extra2 = 0;
                return;
            }
            if (state.extra < 15)
            {
                state.location.x += state.movexyz.x;
                state.location.y += state.movexyz.y;
                return;
            }
            if (state.extra == 15)
            {
                int t = -1;
                float d = 100;
                for (int i = 0; i < 20; i++)
                {
                    if (buff_enemy[i].move != null)
                    {
                        float x1 = buff_enemy[i].location.x - x;
                        float y1 = buff_enemy[i].location.y - y;
                        float d1 = x1 * x1 + y1 * y1;
                        if (d1 < d)
                        {
                            d = d1;
                            t = i;
                        }
                    }
                }
                state.extra2 = t;//save the enemy id
                return;
            }
            if (state.extra2 > -1)
            {
                if (state.extra2 < -1 | state.extra2 > 19)
                    Debug.Log(state.extra2);
                if (buff_enemy[state.extra2].die == false)
                {
                    Aim1(ref state.location,ref buff_enemy[state.extra2].location, 0.2f, ref state.movexyz);
                    return;
                }
                else
                    state.extra2 = -1;
            }
            state.location.x += state.movexyz.x;
            state.location.y += state.movexyz.y;
        }
        public static void B_LockCore(int id, ref BulletState state)
        {
            //Effect_Def16(ref state);
            state.extra++;
            if (state.extra > 400)
            {
                state.active = false;
                state.extra = 0;
                state.extra2 = 0;
                return;
            }
            Aim1(ref state.location, ref core_location, buff_bullet[id].speed);
        }
        public static void B_DownWord(int id, ref BulletState state)
        {
            state.location.y -= 0.05f;
            if (state.location.y < -5.5f)
            {
                state.active = false;
            }
        }
        public static void B_DownWordEX(int id, ref BulletState state)
        {
            if (state.extra < 10)
            {
                state.extra++;
                int z = (int)state.angle.z;
                state.location.x += angle_table[z].x * buff_bullet[id].speed;
                state.location.y += angle_table[z].y * buff_bullet[id].speed;
                return;
            }
            state.location.y -= 0.05f;
            if (state.location.y < -5.5f)
            {
                state.active = false;
                state.extra = 0;
                state.extra2 = 0;
            }
        }
        public static void B_Laser_level1(int id, ref BulletState state)
        {
            state.extra++;        
            if (state.extra > 36)
            {
                state.extra = 0;
                state.active = false;
            }
            if ((state.id&1) == 0)
                state.location.x = current.wing.location.x - 0.05f;
            else
                state.location.x = current.wing.location.x + 1.95f;
            state.location.y = current.wing.location.y + 5.2f;
        }
        public static void B_Laser_level4(int id, ref BulletState state)
        {            
            if ((state.id&1) == 0)
                state.location.x = current.wing.location.x - 0.05f;
            else
                state.location.x = current.wing.location.x + 1.95f;
            state.location.y = current.wing.location.y + 5.2f;
        }
        public static void B_Ripple(int id, ref BulletState state)
        {
            if (id % 2 == 0)//left
            {
                if (state.extra < 6)//left
                {
                    state.angle.z -= 5;
                }
                else//right
                {
                    state.angle.z += 5;
                }
            }
            else//right
            {
                if (state.extra < 6)//rgiht
                {
                    state.angle.z += 5;
                }
                else//left
                {
                    state.angle.z -= 5;
                }
            }
            int z = (int)state.angle.z;
            if (z < 0)
                z += 360;
            if (z > 360)
                z -= 360;
            state.location.x += angle_table[z].x * 0.05f;
            state.location.y += angle_table[z].y * 0.05f;
            state.extra++;
            if (state.extra > 17)
                state.extra = -6;
            if (state.location.y < -5.5f | state.location.x<-3 |state .location.x>3 | state.location.y>5.5f)
            {
                state.active = false;
                state.extra = 0;
            }
        }
        public static void B_Pentagram(int id, ref BulletState state)
        {
            if (state.extra % 50 == 0)
            {
                state.angle.z += 144;
                if (state.angle.z > 360)
                    state.angle.z -= 360;
                int z = (int)state.angle.z;
                state.movexyz.x = angle_table[z].x * 0.17f;
                state.movexyz.y = angle_table[z].y * 0.17f;
            }
            state.location.x += state.movexyz.x;
            state.location.y += state.movexyz.y;
            state.extra++;
            if (state.extra > 250)
            {
                state.active = false;
                state.extra = 0;
            }
        }
        public static void B_AngleToDwon(int id, ref BulletState state)
        {
            if (state.extra == 0)
            {
                int z = (int)state.angle.z;
                state.movexyz.x = angle_table[z].x * 0.1f;
                state.movexyz.y = angle_table[z].y * 0.1f;
            }
            if (state.extra < 30)
            {
                state.location.x += state.movexyz.x;
                state.location.y += state.movexyz.y;
            }
            else
                state.location.y -= 0.03f;
            if (state.location.y < -5.5f)
            {
                state.active = false;
                state.extra = 0;
                return;
            }
            state.extra++;
        }
        internal static void B_DownToCross(int id, ref BulletState state)
        {
            state.extra++;
            if (state.extra < 100)
            {
                state.location.y -= 0.01f;
            }
            else
            {
                int c = state.extra % 4;
                switch (c)
                {
                    case 0:
                        {
                            state.location.y -= 0.01f;
                            break;
                        }
                    case 1:
                        {
                            state.location.y += 0.01f;
                            state.angle.z = 180;
                            break;
                        }
                    case 2:
                        {
                            state.location.x -= 0.01f;
                            state.angle.z = 270;
                            break;
                        }
                    case 3:
                        {
                            state.location.x += 0.01f;
                            state.angle.z = 90;
                            break;
                        }
                }
                if (state.extra > 150)
                {
                    state.active = false;
                    state.extra = 0;
                }
            }
        }
        public static void B_Boss_Tick(int id, ref BulletState state)
        {
            if (state.location.y < -5.5f)
            {
                state.active = false;
                state.extra = 0;
                return;
            }
            if (state.extra == 0)
            {
                int z = (int)state.angle.z;
                state.movexyz.x = angle_table[z].x * 0.17f;
                state.movexyz.y = angle_table[z].y * 0.17f;
            }
            if (state.location.x < -2.5f)
            {
                state.angle.z = 240;
                state.movexyz.x = angle_table[240].x * 0.17f;
                state.movexyz.y = angle_table[240].y * 0.17f;
            }
            else if (state.location.x > 2.5f)
            {
                state.angle.z = 120;
                state.movexyz.x = angle_table[120].x * 0.17f;
                state.movexyz.y = angle_table[120].y * 0.17f;
            }
            state.location.x += state.movexyz.x;
            state.location.y += state.movexyz.y;
            state.extra++;
        }
        internal static void B_Fixd_110(int id, ref BulletState state)
        {
            state.extra++;
            if (state.extra >= 110)
            {
                state.active = false;
                state.extra = 0;
            }
        }
        #endregion
    }
}
