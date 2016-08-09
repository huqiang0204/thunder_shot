using System;
using UnityEngine;

namespace Assets.UnityVS.Script
{
    class ShotBullet:GameControl
    {
        #region bullet shot style  bullet id and enemyid
        public static void Shot_Angle6_Rotate(int id,int enemyid)
        {
            buff_bullet[id].s_count = 6;
            buff_bullet[id].shot = FixStartPoint.GetSixAngle(buff_bullet[id].extra,buff_enemy[enemyid].location);
            buff_bullet[id].extra += 5;
            if (buff_bullet[id].extra > 60)
            {
                buff_bullet[id].extra = 0;
                buff_enemy[id].extra_b = -100;
            }                
        }
        public static void Shot_Angle6_RotateA(int id, int enemyid)
        {
            Vector3 site = buff_enemy[enemyid].location;
            site.y -= 0.5f;
            Vector3 temp = Vector3.zero;
            temp.z = buff_bullet[id].extra;
            buff_bullet[id].s_count = 6;
            buff_bullet[id].shot = new StartPoint[6];
            for (int i = 0; i < 6; i++)
            {
                buff_bullet[id].shot[i] = new StartPoint() { location = site, angle = temp };
                temp.z += 60;
            }
            if (buff_enemy[id].extra_a < 12)
            {
                buff_bullet[id].extra += 5;
                if (buff_enemy[id].extra_a == 11)
                {
                    buff_enemy[id].extra_b = -5;
                }
            }
            else
            {
                buff_bullet[id].extra -= 5;
                if (buff_enemy[id].extra_a >= 23)
                {
                    buff_enemy[id].extra_b = -100;
                    buff_enemy[id].extra_a = 0;
                    return;
                }
            }
            buff_enemy[id].extra_a++;
        }
        public static void Shot_Down_Arc(int id, int enemyid)
        {
            buff_enemy[enemyid].extra_b++;
            if (buff_enemy[enemyid].extra_b == 80)
            {
                buff_enemy[enemyid].extra_b = 0;
                buff_bullet[id].s_count = 7;
                Vector3 temp = buff_enemy[enemyid].location;
                temp.y -= 0.3f;
                buff_bullet[id].shot = new StartPoint[7];
                for (int i = 0; i < 7; i++)
                    buff_bullet[id].shot[i] = new StartPoint() { location = temp, angle = new Vector3(0, 0, i * 10 + 150) };
                return;
            }
        }
        public static void Shot_Down_Arc3(int id, int enemyid)
        {
            if (buff_bullet[id].extra < 3)
            {
                buff_bullet[id].s_count = 7;
                Vector3 temp = buff_enemy[enemyid].location;
                temp.y -= 0.3f;
                buff_bullet[id].shot = new StartPoint[7];
                for (int i = 0; i < 7; i++)
                    buff_bullet[id].shot[i] = new StartPoint() { location = temp, angle = new Vector3(0, 0, i * 10 + 150) };
                buff_bullet[id].extra++;
            }
            else
            {
                buff_bullet[id].extra = 0;
                buff_enemy[enemyid].extra_b = -100;
            }
        }
        public static void Shot_Three_Circle12(int id, int enemyid)
        {
            buff_enemy[enemyid].extra_b++;
            if (buff_enemy[enemyid].extra_b == 100)
            {
                buff_bullet[id].s_count = 12;
                Vector3 temp = buff_enemy[enemyid].location;
                temp.y -= 0.3f;
                buff_bullet[id].shot = new StartPoint[12];
                for (int i = 0; i < 12; i++)
                    buff_bullet[id].shot[i] = new StartPoint() { location = temp, angle = new Vector3(0, 0, i * 30) };
                return;
            }
            if (buff_enemy[enemyid].extra_b == 200)
            {
                buff_bullet[id].s_count = 12;
                Vector3 temp = buff_enemy[enemyid].location;
                temp.y -= 0.3f;
                buff_bullet[id].shot = new StartPoint[12];
                for (int i = 0; i < 12; i++)
                    buff_bullet[id].shot[i] = new StartPoint() { location = temp, angle = new Vector3(0, 0, i * 30 + 10) };
                return;
            }
            if (buff_enemy[enemyid].extra_b == 300)
            {
                buff_enemy[enemyid].extra_b = 0;
                buff_bullet[id].s_count = 12;
                Vector3 temp = buff_enemy[enemyid].location;
                temp.y -= 0.3f;
                buff_bullet[id].shot = new StartPoint[12];
                for (int i = 0; i < 12; i++)
                    buff_bullet[id].shot[i] = new StartPoint() { location = temp, angle = new Vector3(0, 0, i * 30 + 20) };
                return;
            }
        }
        public static void Shot_Three_Circle36(int id, int enemyid)
        {
            buff_enemy[enemyid].extra_b++;
            if (buff_enemy[enemyid].extra_b == 200)
            {
                buff_bullet[id].s_count = 36;
                Vector3 temp = buff_enemy[enemyid].location;
                temp.y -= 0.3f;
                buff_bullet[id].shot = new StartPoint[36];
                for (int i = 0; i < 36; i++)
                    buff_bullet[id].shot[i] = new StartPoint() { location = temp, angle = new Vector3(0, 0, i * 10) };
                return;
            }
            if (buff_enemy[enemyid].extra_b == 250)
            {
                buff_bullet[id].s_count = 36;
                Vector3 temp = buff_enemy[enemyid].location;
                temp.y -= 0.3f;
                buff_bullet[id].shot = new StartPoint[36];
                for (int i = 0; i < 36; i++)
                    buff_bullet[id].shot[i] = new StartPoint() { location = temp, angle = new Vector3(0, 0, i * 10 + 3) };
                return;
            }
            if (buff_enemy[enemyid].extra_b == 300)
            {
                buff_enemy[enemyid].extra_b = -100;
                buff_bullet[id].s_count = 36;
                Vector3 temp = buff_enemy[enemyid].location;
                temp.y -= 0.3f;
                buff_bullet[id].shot = new StartPoint[36];
                for (int i = 0; i < 36; i++)
                    buff_bullet[id].shot[i] = new StartPoint() { location = temp, angle = new Vector3(0, 0, i * 10 + 6) };
                return;
            }
        }
        public static void Shot_Circle36A(int id, int enemyid)
        {
            buff_bullet[id].s_count = 36;
            Vector3 temp = buff_enemy[enemyid].location;
            buff_bullet[id].shot = new StartPoint[36];
            float z = Aim(ref temp,ref core_location);
            z -= 15;
            if (z < 360)
                z += 360;
            int s = 0;
            for (int i = 0; i < 6; i++)
            {
                for (int c = 0; c < 6; c++)
                {
                    buff_bullet[id].shot[s] = new StartPoint() { location = temp, angle = new Vector3(0, 0, z) };
                    z += 5;
                    if (z > 360)
                        z -= 360;
                    s++;
                }
                z += 30;
            }
            buff_bullet[id].extra++;
            if(buff_bullet[id].extra>3)
            {
                buff_bullet[id].extra = 0;
                buff_enemy[enemyid].extra_b = -100;
            }
        }

        public static void Shot_Pentagram(int id, int enemyid)
        {
            buff_enemy[enemyid].extra_b++;
            int count = buff_enemy[enemyid].extra_b;
            if (count > 100)
            {
                int c = count - 100;
                if (c % 3 == 0)
                {
                    buff_bullet[id].s_count = 1;
                    Vector3 site = buff_enemy[enemyid].location;
                    Vector3 temp = Vector3.zero;
                    temp.z = 18;
                    buff_bullet[id].shot = new StartPoint[] { new StartPoint() { location = site, angle = temp } };
                }
            }
            if (buff_enemy[enemyid].extra_b > 250)
                buff_enemy[enemyid].extra_b = 0;
        }
        public static void Shot_Ripple(int id, int enemyid)
        {
            buff_enemy[enemyid].extra_b++;
            int count = buff_enemy[enemyid].extra_b;
            if (count > 120)
            {
                int c = count - 120;
                if (c % 5 == 0)
                {
                    buff_bullet[id].s_count = 2;
                    Vector3 site = buff_enemy[enemyid].location;
                    Vector3 temp = Vector3.zero;
                    temp.z = Aim(ref site, ref core_location);
                    buff_bullet[id].shot = new StartPoint[] { new StartPoint() {location=site,angle=temp },
                    new StartPoint() {location=site,angle=temp} };
                }
            }
            if (buff_enemy[enemyid].extra_b > 180)
                buff_enemy[enemyid].extra_b = 0;
        }
        public static void Shot_Aim_3(int id, int enemyid)
        {
            if (buff_bullet[id].extra < 3)
            {
                Vector3 site = buff_enemy[enemyid].location;
                Vector3 temp = Vector3.zero;
                temp.z = Aim(ref site, ref core_location);
                buff_bullet[id].s_count = 1;
                buff_bullet[id].shot = new StartPoint[] { new StartPoint() { location = site, angle = temp } };
                buff_bullet[id].extra++;
            }
            else
            {
                buff_bullet[id].extra = 0;
                buff_enemy[enemyid].extra_b = -100;
            }
        }
        public static void Shot_Aim_12(int id, int enemyid)
        {
            if (buff_bullet[id].extra < 12)
            {
                Vector3 site = buff_enemy[enemyid].location;
                Vector3 temp = Vector3.zero;
                temp.z = Aim(ref site, ref core_location);
                buff_bullet[id].s_count = 1;
                buff_bullet[id].shot = new StartPoint[] { new StartPoint() { location = site, angle = temp } };
                buff_bullet[id].extra++;
            }
            else
            {
                buff_bullet[id].extra = 0;
                buff_enemy[enemyid].extra_b = -100;
            }
        }
        public static void Shot_Aim_Arc3(int id, int enemyid)
        {
            float z = Aim(ref buff_enemy[enemyid].location, ref core_location);
            Vector3 site = buff_enemy[enemyid].location;
            z -= 10;
            if (z < 0)
                z += 360;
            buff_bullet[id].s_count = 3;
            buff_bullet[id].shot = new StartPoint[3];
            for (int i = 0; i < 3; i++)
            {
                buff_bullet[id].shot[i].location = site;
                buff_bullet[id].shot[i].angle.z = z;
                z += 10;
                if (z > 360)
                    z -= 360;
            }
        }
        public static void Shot_Aim_Arc6(int id, int enemyid)
        {
            float z = Aim(ref buff_enemy[enemyid].location,ref core_location);
            Vector3 site = buff_enemy[enemyid].location;
            z -= 30;
            if (z < 0)
                z += 360;
            buff_bullet[id].s_count= 6;
            buff_bullet[id].shot = new StartPoint[6];
            for (int i = 0; i < 6; i++)
            {
                buff_bullet[id].shot[i].location = site;
                buff_bullet[id].shot[i].angle.z = z;
                z += 10;
                if (z > 360)
                    z -= 360;
            }
        }
        public static void Shot_Aim_Arc18(int id, int enemyid)
        {
            float z = Aim(ref buff_enemy[enemyid].location, ref core_location);
            Vector3 site = buff_enemy[enemyid].location;
            z -= 60;
            if (z < 0)
                z += 360;
            buff_bullet[id].s_count = 18;
            buff_bullet[id].shot = new StartPoint[18];
            int s = 0;
            for (int i = 0; i < 3; i++)
            {
                for(int c=0;c<6;c++)
                {
                    buff_bullet[id].shot[s].location = site;
                    buff_bullet[id].shot[s].angle.z = z;
                    z += 5;
                    if (z > 360)
                        z -= 360;
                    s++;
                }
                z += 15;
            }
        }
        public static void Shot_Diamond(int id, int enemyid)
        {
            if(buff_bullet[id].extra<2)
            {
                Vector3 temp = buff_enemy[enemyid].location;
                buff_bullet[id].s_count = 12;
                buff_bullet[id].shot = new StartPoint[12];
                buff_bullet[id].shot[0] = new StartPoint() { location = temp, angle = Vector3.zero };
                for (int i = 1; i < 12; i++)
                    buff_bullet[id].shot[i] = buff_bullet[id].shot[0];
                buff_bullet[id].extra++;
            }
            else
            {
                buff_bullet[id].extra = 0;
                buff_enemy[enemyid].extra_b = -100;
            }            
        }
        public static void Shot_ThreePoint(int id, int enemyid)
        {
            buff_enemy[enemyid].extra_b++;
            int count = buff_enemy[enemyid].extra_b;
            if (count > 100)
            {
                Vector3 site = buff_enemy[enemyid].location;
                Vector3 z = buff_enemy[enemyid].angle;
                buff_bullet[id].s_count = 3;
                buff_enemy[enemyid].extra_b = 0;
                buff_bullet[id].shot = new StartPoint[3];
                buff_bullet[id].shot[0] = new StartPoint() { location = site, angle = z };
                z.z -= 30;
                buff_bullet[id].shot[1] = new StartPoint() { location = site, angle = z };
                z.z += 60;
                buff_bullet[id].shot[2] = new StartPoint() { location = site, angle = z };
            }
        }
        public static void Shot_ThreeBeline(int id, int enemyid)
        {
            if (buff_bullet[id].extra < 20)
            {
                Vector3 site = buff_enemy[enemyid].location;
                Vector3 z = buff_enemy[enemyid].angle;
                buff_bullet[id].s_count = 3;
                z.z = 150;
                buff_bullet[id].shot = new StartPoint[3];
                buff_bullet[id].shot[0] = new StartPoint() { location = site, angle = z };
                z.z = 180;
                buff_bullet[id].shot[1] = new StartPoint() { location = site, angle = z };
                z.z = 210;
                buff_bullet[id].shot[2] = new StartPoint() { location = site, angle = z };
                buff_bullet[id].extra++;
            }
            else
            {
                buff_bullet[id].extra = 0;
                buff_enemy[enemyid].extra_b = -100;
            }
        }
        public static void Shot_Downflowers(int id, int enemyid)
        {
            int count = buff_bullet[id].extra;
            Vector3 site = buff_enemy[enemyid].location;
            buff_bullet[id].s_count = 4;
            buff_bullet[id].shot =
            new StartPoint[] {new StartPoint() { location = site, angle = new Vector3(0, 0, 160 - count) },
            new StartPoint() { location = site, angle = new Vector3(0, 0, 120 - count) },
            new StartPoint() { location = site, angle = new Vector3(0, 0, 240 + count) },
            new StartPoint() { location = site, angle = new Vector3(0, 0, 220 - count) }
            };
            buff_bullet[id].extra += 5;
            if(buff_bullet[id].extra>=60)
            {
                buff_bullet[id].extra = 0;
                buff_enemy[enemyid].extra_b = -100;
            }
        }
        #endregion

        #region war plane
        public static void Shot_Scatter_level2(int id, int enemyid)//12 hit
        {
            int state = buff_bullet[id].extra;
            if (state ==0)
            {
                current.warplane.shotfrequency = 4;
                buff_bullet[id].extra++;
                Vector3 site = core_location;
                buff_bullet[id].s_count = 6;
                buff_bullet[id].shot = new StartPoint[] { new StartPoint() { location = site,angle= new Vector3(0,0,8)},
                    new StartPoint() { location = site, angle =new Vector3(0,0, 16) },
                      new StartPoint() { location = site, angle =new Vector3(0,0, 24) },
                      new StartPoint() { location = site, angle =new Vector3(0,0, 352) },
                      new StartPoint() { location = site, angle =new Vector3(0,0, 344) },
                    new StartPoint() { location = site, angle =new Vector3(0,0,336)}};
                return;
            }
            else
                if (state == 1)
            {
                buff_bullet[id].extra++;
                Vector3 site = core_location;
                buff_bullet[id].s_count = 6;
                buff_bullet[id].shot = new StartPoint[] { new StartPoint() { location = site, angle =new Vector3(0,0, 6) },
                    new StartPoint() { location = site, angle =new Vector3(0,0, 12) },
                      new StartPoint() { location = site, angle =new Vector3(0,0, 18) },
                      new StartPoint() { location = site, angle =new Vector3(0,0, 354) },
                      new StartPoint() { location = site, angle =new Vector3(0,0, 348) },
                    new StartPoint() { location = site, angle =new Vector3(0,0, 342)}};
            }
            else
            {
                buff_bullet[id].extra = 0;
                Vector3 site = core_location;
                buff_bullet[id].s_count = 6;
                buff_bullet[id].shot = new StartPoint[] { new StartPoint() { location = site, angle =new Vector3(0,0, 3) },
                    new StartPoint() { location =site, angle =new Vector3(0,0, 10) },
                      new StartPoint() { location =site, angle =new Vector3(0,0, 15) },
                      new StartPoint() { location = site, angle =new Vector3(0,0, 357) },
                      new StartPoint() { location = site, angle =new Vector3(0,0, 350) },
                    new StartPoint() { location = site, angle =new Vector3(0,0, 345)}};
            }
        }
        public static void Shot_Scatter_level3(int id, int enemyid)//16 hit
        {
            int state = buff_bullet[id].extra;
            if (state == 0)
            {
                current.warplane.shotfrequency = 4;
                buff_bullet[id].extra++;
                Vector3 site = core_location;
                buff_bullet[id].s_count = 8;
                buff_bullet[id].shot = new StartPoint[] {
                    new StartPoint() { location = site,angle= new Vector3(0,0,4)},
                    new StartPoint() { location = site,angle= new Vector3(0,0,8)},
                    new StartPoint() { location = site, angle =new Vector3(0,0, 16) },
                      new StartPoint() { location = site, angle =new Vector3(0,0, 24) },
                      new StartPoint() { location = site, angle =new Vector3(0,0, 356) },
                      new StartPoint() { location = site, angle =new Vector3(0,0, 352) },
                      new StartPoint() { location = site, angle =new Vector3(0,0, 344) },
                    new StartPoint() { location = site, angle =new Vector3(0,0,336)}};
                return;
            }
            else
                if (state == 1)
            {
                buff_bullet[id].extra++;
                Vector3 site = core_location;
                buff_bullet[id].s_count = 8;
                buff_bullet[id].shot = new StartPoint[] {
                    new StartPoint() { location = site, angle =new Vector3(0,0, 2) },
                    new StartPoint() { location = site, angle =new Vector3(0,0, 6) },
                    new StartPoint() { location = site, angle =new Vector3(0,0, 12) },
                      new StartPoint() { location = site, angle =new Vector3(0,0, 18) },
                      new StartPoint() { location = site, angle =new Vector3(0,0, 358) },
                      new StartPoint() { location = site, angle =new Vector3(0,0, 354) },
                      new StartPoint() { location = site, angle =new Vector3(0,0, 348) },
                    new StartPoint() { location = site, angle =new Vector3(0,0, 342)}};
            }
            else
            {
                buff_bullet[id].extra=0;
                Vector3 site = core_location;
                buff_bullet[id].s_count = 8;
                buff_bullet[id].shot = new StartPoint[] { new StartPoint() { location = site, angle =new Vector3(0,0,3) },
                    new StartPoint() { location =site, angle =new Vector3(0,0, 10) },
                      new StartPoint() { location =site, angle =new Vector3(0,0, 15) },
                      new StartPoint() { location =site, angle =new Vector3(0,0, 20) },
                      new StartPoint() { location =site, angle =new Vector3(0,0, 340) },
                      new StartPoint() { location = site, angle =new Vector3(0,0, 357) },
                      new StartPoint() { location = site, angle =new Vector3(0,0, 350) },
                    new StartPoint() { location = site, angle =new Vector3(0,0, 345)}};
            }
        }
        public static void Shot_Scatter_level4(int id, int enemyid)//super hit 20!!!
        {
            int state = buff_bullet[id].extra;
            if (state ==0)
            {
                current.warplane.shotfrequency = 3;
                buff_bullet[id].extra++;
                Vector3 site = core_location;
                buff_bullet[id].s_count = 8;
                buff_bullet[id].shot = new StartPoint[] {
                    new StartPoint() { location = site,angle= new Vector3(0,0,4)},
                    new StartPoint() { location = site,angle= new Vector3(0,0,8)},
                    new StartPoint() { location = site, angle =new Vector3(0,0, 16) },
                      new StartPoint() { location = site, angle =new Vector3(0,0, 24) },
                      new StartPoint() { location = site, angle =new Vector3(0,0, 356) },
                      new StartPoint() { location = site, angle =new Vector3(0,0, 352) },
                      new StartPoint() { location = site, angle =new Vector3(0,0, 344) },
                    new StartPoint() { location = site, angle =new Vector3(0,0,336)}};
                return;
            }
            else
               if (state == 1)
            {
                buff_bullet[id].extra++;
                Vector3 site = core_location;
                buff_bullet[id].s_count = 8;
                buff_bullet[id].shot = new StartPoint[] {
                    new StartPoint() { location = site, angle =new Vector3(0,0, 2) },
                    new StartPoint() { location = site, angle =new Vector3(0,0, 6) },
                    new StartPoint() { location = site, angle =new Vector3(0,0, 12) },
                      new StartPoint() { location = site, angle =new Vector3(0,0, 18) },
                      new StartPoint() { location = site, angle =new Vector3(0,0, 358) },
                      new StartPoint() { location = site, angle =new Vector3(0,0, 354) },
                      new StartPoint() { location = site, angle =new Vector3(0,0, 348) },
                    new StartPoint() { location = site, angle =new Vector3(0,0, 342)}};
            }
            else
            {
                buff_bullet[id].extra=0;
                Vector3 site = core_location;
                buff_bullet[id].s_count = 8;
                buff_bullet[id].shot = new StartPoint[] { new StartPoint() { location = site, angle =new Vector3(0,0,3) },
                    new StartPoint() { location =site, angle =new Vector3(0,0, 10) },
                      new StartPoint() { location =site, angle =new Vector3(0,0, 15) },
                      new StartPoint() { location =site, angle =new Vector3(0,0, 20) },
                      new StartPoint() { location =site, angle =new Vector3(0,0, 340) },
                      new StartPoint() { location = site, angle =new Vector3(0,0, 357) },
                      new StartPoint() { location = site, angle =new Vector3(0,0, 350) },
                    new StartPoint() { location = site, angle =new Vector3(0,0, 345)}};
            }
        }
        public static void Shot_Thunder_level1(int id, int enemyid)
        {
            Vector3 site = core_location;
            Vector3 site1 = core_location;
            buff_bullet[id].shot = new StartPoint[2];
            buff_bullet[id].s_count = 2;
            site.x -= 0.3f;
            buff_bullet[id].shot[0] = new StartPoint() { location = site, angle = new Vector3(0, 0, 60) };
            site1.x += 0.3f;
            buff_bullet[id].shot[1] = new StartPoint() { location = site1, angle = new Vector3(0, 0, 300) };
        }
        public static void Shot_Thunder_level2(int id, int enemyid)
        {
            Vector3 site = core_location;
            Vector3 site1 = core_location;
            buff_bullet[id].shot = new StartPoint[3];
            buff_bullet[id].s_count = 3;
            buff_bullet[id].shot[2] = new StartPoint() { location = site, angle = Vector3.zero };
            site.x -= 0.3f;
            buff_bullet[id].shot[0] = new StartPoint() { location = site, angle = new Vector3(0, 0, 60) };
            site1.x += 0.3f;
            buff_bullet[id].shot[1] = new StartPoint() { location = site1, angle = new Vector3(0, 0, 300) };
        }
        public static void Shot_Thunder_level3(int id, int enemyid)
        {
            if (buff_bullet[id].extra == 0)
            {
                buff_bullet[id].extra = 1;
                current.B_second.extra_b = 25;
                Vector3 site = core_location;
                Vector3 site1 = core_location;
                buff_bullet[id].shot = new StartPoint[2];
                buff_bullet[id].s_count = 2;
                site.x -= 0.3f;
                buff_bullet[id].shot[0] = new StartPoint() { location = site, angle = new Vector3(0, 0, 20) };
                site1.x += 0.3f;
                buff_bullet[id].shot[1] = new StartPoint() { location = site1, angle = new Vector3(0, 0, 340) };
            }
            else
            {
                buff_bullet[id].extra = 0;
                Vector3 site = core_location;
                Vector3 site1 = core_location;
                buff_bullet[id].shot = new StartPoint[2];
                buff_bullet[id].s_count = 2;
                site.x -= 0.3f;
                buff_bullet[id].shot[0] = new StartPoint() { location = site, angle = new Vector3(0, 0, 60) };
                site1.x += 0.3f;
                buff_bullet[id].shot[1] = new StartPoint() { location = site1, angle = new Vector3(0, 0, 300) };
            }
        }
        public static void Shot_Thunder_level4(int id, int enemyid)
        {
            if (buff_bullet[id].extra==0)
            {
                buff_bullet[id].extra = 1;
                current.B_second.extra_b = 25;
                Vector3 site = core_location;
                Vector3 site1 = core_location;
                buff_bullet[id].shot = new StartPoint[2];
                buff_bullet[id].s_count = 2;
                site.x -= 0.3f;
                buff_bullet[id].shot[0] = new StartPoint() { location = site, angle = new Vector3(0, 0, 20) };
                site1.x += 0.3f;
                buff_bullet[id].shot[1] = new StartPoint() { location = site1, angle = new Vector3(0, 0, 340) };
            }
            else
            {
                buff_bullet[id].extra = 0;
                Vector3 site = core_location;
                Vector3 site1 = core_location;
                buff_bullet[id].shot = new StartPoint[3];
                buff_bullet[id].s_count = 3;
                buff_bullet[id].shot[2] = new StartPoint() { location = site, angle = Vector3.zero };
                site.x -= 0.3f;
                buff_bullet[id].shot[0] = new StartPoint() { location = site, angle = new Vector3(0, 0, 60) };
                site1.x += 0.3f;
                buff_bullet[id].shot[1] = new StartPoint() { location = site1, angle = new Vector3(0, 0, 300) };
            }
        }
        public static void Shot_Laser_level1(int id, int enemyid)
        {
            if (current.level < 3)
            {
                Vector3 temp = current.wing.location;
                Vector3 temp1 = current.wing.location;
                temp1.x += 1.95f;
                temp.x -= 0.06f;
                temp1.y = temp.y += 5.2f;
                temp1.z = temp.z = 1;
                buff_bullet[id].s_count = 2;
                buff_bullet[id].shot = new StartPoint[] { new StartPoint() { location=temp,angle=Vector3.zero } ,
                    new StartPoint() { location = temp1, angle = Vector3.zero } };
            }
            else
            {
                buff_bullet[id].play = BulletMove.Play_7_4;
                buff_bullet[id].move = BulletMove.B_Laser_level4;
            }
        }
        public static void Shot_Laser_level4(int id, int enemyid)
        {
            if (current.level == 3)
            {
                if (buff_bullet[current.wing.bulletid].bulletstate[0].active == false)
                {
                    Vector3 temp = current.wing.location;
                    Vector3 temp1 = current.wing.location;
                    temp1.x += 1.95f;
                    temp.x -= 0.06f;
                    temp1.y = temp.y += 5.2f;
                    temp1.z = temp.z = 1;
                    buff_bullet[id].s_count = 2;
                    buff_bullet[id].shot = new StartPoint[] { new StartPoint() { location=temp,angle=Vector3.zero } ,
                    new StartPoint() { location = temp1, angle = Vector3.zero } };
                    current.wing.extra_b = 0;
                }
            }
            else
            {
                buff_bullet[id].play = BulletMove.Play_7_1;
                buff_bullet[id].move = BulletMove.B_Laser_level1;
            }
        }
        public static void Shot_zhuji1(int id,int enemyid)
        {
            int state = buff_bullet[id].extra;
            if (state<3)
            {
                if (state == 0)
                {
                    buff_bullet[id].spt_index = 5;
                    buff_bullet[id].s_count = 1;
                    buff_bullet[id].shot = new StartPoint[] { new StartPoint { location=core_location} };
                }
                else if (state == 1)
                {
                    buff_bullet[id].spt_index = 4;
                    buff_bullet[id].s_count = 2;
                    buff_bullet[id].shot = new StartPoint[2] ;
                    Vector3 temp = core_location;
                    temp.x -= 0.26f;
                    buff_bullet[id].shot[0].location = temp;
                    temp.x += 0.52f;
                    buff_bullet[id].shot[1].location = temp;
                }
                else {
                    buff_bullet[id].spt_index = 0;
                    buff_bullet[id].s_count = 2;
                    buff_bullet[id].shot = new StartPoint[2];
                    Vector3 temp = core_location;
                    temp.x -= 0.51f;
                    buff_bullet[id].shot[0].location = temp;
                    temp.x += 1.02f;
                    buff_bullet[id].shot[1].location = temp;
                    current.warplane.extra_b = -2;
                }
            }
            else if(state<6)
            {
                if (state == 3)
                {
                    buff_bullet[id].spt_index = 5;
                    buff_bullet[id].s_count = 1;
                    buff_bullet[id].shot = new StartPoint[] { new StartPoint { location = core_location } };
                }
                else if (state == 4)
                {
                    buff_bullet[id].spt_index = 2;
                    buff_bullet[id].s_count = 2;
                    buff_bullet[id].shot = new StartPoint[2];
                    Vector3 temp = core_location;
                    temp.x -= 0.26f;
                    buff_bullet[id].shot[0].location = temp;
                    temp.x += 0.52f;
                    buff_bullet[id].shot[1].location = temp;
                }
                else {
                    buff_bullet[id].spt_index = 3;
                    buff_bullet[id].s_count = 2;
                    buff_bullet[id].shot = new StartPoint[2];
                    Vector3 temp = core_location;
                    temp.x -= 0.51f;
                    buff_bullet[id].shot[0].location = temp;
                    temp.x += 1.02f;
                    buff_bullet[id].shot[1].location = temp;
                    current.warplane.extra_b = -1;
                }
            }
            else
            {
                buff_bullet[id].spt_index = 1;
                buff_bullet[id].s_count = 2;
                buff_bullet[id].shot = new StartPoint[2];
                Vector3 temp = core_location;
                temp.x -= 0.2f;
                buff_bullet[id].shot[0].location = temp;
                temp.x += 0.4f;
                buff_bullet[id].shot[1].location = temp;
                current.warplane.extra_b = -2;
                buff_bullet[id].extra = 0;
                return;
            }
            buff_bullet[id].extra++;
        }
        #endregion

        #region eastproject
        public static void Shot_lingmeng(int id,int enemyid)
        {
            Vector3 site = core_location;
            Vector3 site2 = site;
            site.y += 0.2f;
            current.warplane.extra_b = 0;
            buff_bullet[id].s_count = 4;
            buff_bullet[id].shot = new StartPoint[] { new StartPoint() { location = site,angle= new Vector3(0,0,2)},
                    new StartPoint() { location = site2, angle =new Vector3(0,0, 6) },
                      new StartPoint() { location = site, angle =new Vector3(0,0, 358) },
                      new StartPoint() { location = site2, angle =new Vector3(0,0, 354) }};
        }
        public static void Shot_lingmengS(int id, int enemyid)
        {
            Vector3 site = core_location;
            Vector3 site1 = core_location;
            buff_bullet[id].shot = new StartPoint[2];
            buff_bullet[id].s_count = 2;
            site.x -= 0.3f;
            buff_bullet[id].shot[0] = new StartPoint() { location = site, angle = new Vector3(0, 0, 30) };
            site1.x += 0.3f;
            buff_bullet[id].shot[1] = new StartPoint() { location = site1, angle = new Vector3(0, 0, 330) };
        }
        #endregion
    }
}
