using System;
using UnityEngine;

namespace Assets.UnityVS.Script
{
    class EnemyMove:GameControl
    {
        public static void Player_0_3(int enemyid)
        {
            buff_enemy[enemyid].extra_a++;
            if (buff_enemy[enemyid].extra_a >= 40)
                buff_enemy[enemyid].extra_a = 0;
            int index = buff_enemy[enemyid].extra_a;
            if (index % 10 == 0)
            {
                buff_enemy[enemyid].update_spt = true;
                buff_enemy[enemyid].spt_index = index / 10;               
            }
        }
        public static void Player_4_7(int enemyid)
        {
            buff_enemy[enemyid].extra_a++;
            if (buff_enemy[enemyid].extra_a >= 40)
                buff_enemy[enemyid].extra_a = 0;
            int index = buff_enemy[enemyid].extra_a;
            if (index % 10 == 0)
            {
                buff_enemy[enemyid].update_spt = true;
                buff_enemy[enemyid].spt_index =4+ index / 10;
            }
        }
        public static void Player_0_5(int enemyid)
        {
            buff_enemy[enemyid].extra_a++;
            if (buff_enemy[enemyid].extra_a >= 50)
                buff_enemy[enemyid].extra_a = 0;
            int index = buff_enemy[enemyid].extra_a;
            if (index % 10 == 0)
            {
                buff_enemy[enemyid].update_spt = true;
                buff_enemy[enemyid].spt_index = index / 10;              
            }
        }
        public static void Player_0_11(int enemyid)
        {
            buff_enemy[enemyid].extra_a++;
            if (buff_enemy[enemyid].extra_a >= 110)
                buff_enemy[enemyid].extra_a = 0;
            int index = buff_enemy[enemyid].extra_a;
            if (index % 10 == 0)
            {
                buff_enemy[enemyid].update_spt = true;
                buff_enemy[enemyid].spt_index = index / 10;               
            }
        }
        public static void Player_11_0(int enemyid)
        {
            buff_enemy[enemyid].extra_a++;
            if (buff_enemy[enemyid].extra_a>10)
            {
                buff_enemy[enemyid].update_spt = true;
                buff_enemy[enemyid].spt_index --;
                if (buff_enemy[enemyid].spt_index < 0)
                    buff_enemy[enemyid].spt_index = 11;
               buff_enemy[enemyid].extra_a = 0;
            }
        }
        public static void Player_12_23(int enemyid)
        {
            buff_enemy[enemyid].extra_a++;
            int index = buff_enemy[enemyid].extra_a;
            if (index % 10 == 0)
            {
                buff_enemy[enemyid].update_spt = true;
                buff_enemy[enemyid].spt_index = index / 10 + 11;
                if (buff_enemy[enemyid].extra_a >= 50)
                    buff_enemy[enemyid].extra_a = 0;
            }
        }
        public static bool M_LeftToRightA(int id)
        {
            if (buff_enemy[id].location.x > 3)
            {
                buff_enemy[id].die = true;
                return false;
            }
            if (buff_enemy[id].extra_b > buff_enemy[id].shotfrequency)
            {
                if (buff_enemy[id].location.x < 2.5f)
                    buff_enemy[id].shot(buff_enemy[id].bulletid[0], id);
                buff_enemy[id].extra_b = 0;
            }
            buff_enemy[id].extra_b++;
            buff_enemy[id].location.x+=0.02f;
            return true;
        }
        public static bool M_LeftToRightB(int id)
        {
            if (buff_enemy[id].location.x > 3)
            {
                buff_enemy[id].die = true;
                return false;
            }
            if (buff_enemy[id].extra_b > buff_enemy[id].shotfrequency)
            {
                if (buff_enemy[id].location.x < 2.5f)
                    buff_enemy[id].shot(buff_enemy[id].bulletid[0], id);
                buff_enemy[id].extra_b = 0;
            }
            buff_enemy[id].extra_b++;
            buff_enemy[id].location.x += 0.02f;
            buff_enemy[id].location.y -= 0.008f;
            return true;
        }
        public static bool M_LeftToRightC(int id)
        {
            if (buff_enemy[id].location.x > 3)
            {
                buff_enemy[id].die = true;
                return false;
            }
            if (buff_enemy[id].extra_b > buff_enemy[id].shotfrequency)
            {
                if (buff_enemy[id].location.x < 2.5f)
                    buff_enemy[id].shot(buff_enemy[id].bulletid[0], id);
                buff_enemy[id].extra_b = 0;
            }
            buff_enemy[id].extra_b++;
            if(buff_enemy[id].location.x<0)
                buff_enemy[id].location.y -= 0.008f;
            else buff_enemy[id].location.y += 0.008f;
            buff_enemy[id].location.x += 0.02f;
            return true;
        }
        public static bool M_RightToLeftA(int id)
        {
            if (buff_enemy[id].location.x <-3)
            {
                buff_enemy[id].die = true;
                return false;
            }
            if (buff_enemy[id].extra_b > buff_enemy[id].shotfrequency)
            {
                if (buff_enemy[id].location.x > -2.5f)
                    buff_enemy[id].shot(buff_enemy[id].bulletid[0], id);
                buff_enemy[id].extra_b = 0;
            }
            buff_enemy[id].extra_b++;
            buff_enemy[id].location.x -= 0.02f;
            return true;
        }
        public static bool M_RightToLeftB(int id)
        {
            if (buff_enemy[id].location.x <-3)
            {
                buff_enemy[id].die = true;
                return false;
            }
            if (buff_enemy[id].extra_b > buff_enemy[id].shotfrequency)
            {
                if (buff_enemy[id].location.x >- 2.5f)
                    buff_enemy[id].shot(buff_enemy[id].bulletid[0], id);
                buff_enemy[id].extra_b = 0;
            }
            buff_enemy[id].extra_b++;
            buff_enemy[id].location.x -= 0.02f;
            buff_enemy[id].location.y -= 0.008f;
            return true;
        }
        public static bool M_RightToLeftC(int id)
        {
            if (buff_enemy[id].location.x <-3)
            {
                buff_enemy[id].die = true;
                return false;
            }
            if (buff_enemy[id].extra_b > buff_enemy[id].shotfrequency)
            {
                if (buff_enemy[id].location.x >- 2.5f)
                    buff_enemy[id].shot(buff_enemy[id].bulletid[0], id);
                buff_enemy[id].extra_b = 0;
            }
            buff_enemy[id].extra_b++;
            if (buff_enemy[id].location.x > 0)
                buff_enemy[id].location.y -= 0.008f;
            else buff_enemy[id].location.y += 0.008f;
            buff_enemy[id].location.x -= 0.02f;
            return true;
        }
        public static bool M_Meteor(int id)
        {
            if (buff_enemy[id].location.y < -6f)
            {
                buff_enemy[id].die = true;
                return false;
            }
            buff_enemy[id].location.y -= 0.01f;
            return true;
        }
        public static bool M_FixedTo_200(int id)
        {
            if (buff_enemy[id].extra_m < 200)
            {
                buff_enemy[id].extra_m++;
                buff_enemy[id].location.y -= 0.015f;
                return true;
            }
            buff_enemy[id].shot(buff_enemy[id].bulletid[0], id);
            return true;
        }
        public static bool M_Downward_Def(int id)
        {
            Vector3 a = buff_enemy[id].location;
            if (a.y < -5.5)
            {
                buff_enemy[id].die = true;
                return false;
            }
            buff_enemy[id].location.y -= 0.015f;
            buff_enemy[id].shot(buff_enemy[id].bulletid[0], id);
            return true;
        }
        public static bool M_Downward_01(int id)
        {
            if (buff_enemy[id].location.y < -5.5f)
            {
                buff_enemy[id].die = true;
                return false;
            }
            buff_enemy[id].location.y -= 0.013f;
            if(buff_enemy[id].extra_b>buff_enemy[id].shotfrequency)
            {
                buff_enemy[id].extra_b = 0;
                buff_enemy[id].shot(buff_enemy[id].bulletid[0], id);
            }
            buff_enemy[id].extra_b++;
            return true;
        }
        public static bool M_Downward_NoStop(int id)
        {
            Vector3 a = buff_enemy[id].location;
            if (a.y < -5.5)
            {
                buff_enemy[id].die = true;
                return false;
            }
            buff_enemy[id].location.y -= 0.03f;
            if (buff_enemy[id].shot != null)
            {
                if(buff_enemy[id].extra_b>buff_enemy[id].shotfrequency)
                {
                    buff_enemy[id].shot(buff_enemy[id].bulletid[0], id);
                    buff_enemy[id].extra_b = 0;
                }                    
                buff_enemy[id].extra_b++;
            }                
            return true;
        }
        public static bool M_Down_Slow(int id)
        {
            Vector3 a = buff_enemy[id].location;
            if (a.y < -5.5)
            {
                buff_enemy[id].die = true;
                return false;
            }
            if (buff_enemy[id].extra_b > buff_enemy[id].shotfrequency)
            {
                if (buff_enemy[id].location.y < 5f)
                    buff_enemy[id].shot(buff_enemy[id].bulletid[0], id);
                buff_enemy[id].extra_b = 0;
            }
            buff_enemy[id].extra_b++;
            buff_enemy[id].location.y -= 0.01f;
            return true;
        }
        public static bool M_Down_FastToSlow(int id)
        {
            Vector3 a = buff_enemy[id].location;
            if (a.y < -5.5f)
            {
                buff_enemy[id].die = true;
                return false;
            }
            if (buff_enemy[id].extra_m < 40)
            {
                buff_enemy[id].extra_m++;
                buff_enemy[id].location.y -= 0.08f;
            }
            else
            {
                if (buff_enemy[id].extra_b > buff_enemy[id].shotfrequency)
                {
                    if (buff_enemy[id].location.y < 5f)
                        buff_enemy[id].shot(buff_enemy[id].bulletid[0], id);
                    buff_enemy[id].extra_b = 0;
                }
                buff_enemy[id].extra_b++;
            }
            return true;
        }
        public static bool M_Forward(int id)//正向
        {
            Vector3 a = buff_enemy[id].location;
            if (a.y < -5.5f)
            {
                buff_enemy[id].die = true;
                return false;
            }
            else
            {
                int z = (int)buff_enemy[id].angle.z;
                buff_enemy[id].location.x += angle_table[z].x * 0.03f;
                buff_enemy[id].location.y -= angle_table[z].y * 0.03f;
                if(buff_enemy[id].shotfrequency<buff_enemy[id].extra_b)
                {
                    buff_enemy[id].shot(buff_enemy[id].bulletid[0], id);
                    buff_enemy[id].extra_b = 0;
                }               
                buff_enemy[id].extra_b++;
            }
            return true;
        }
        public static bool M_RightArc(int id)
        {
            //Vector3 a = buff_enemy[id].location;
            buff_enemy[id].extra_b++;
            int z = (int)buff_enemy[id].angle.z;
            z++;
            if (z > 359)
                z = 0;
            if (z == 60)
            {
                buff_enemy[id].die = true;
                return false;
            }
            buff_enemy[id].angle.z = z;
            buff_enemy[id].location.x -= angle_table[z].x * 0.06f;
            buff_enemy[id].location.y -= angle_table[z].y * 0.06f;
            if (buff_enemy[id].shot != null)
            {
                if (buff_enemy[id].extra_b > buff_enemy[id].shotfrequency)
                {
                    buff_enemy[id].shot(buff_enemy[id].bulletid[0], id);
                    buff_enemy[id].extra_b = 0;
                }
                buff_enemy[id].extra_b++;
            }
            return true;
        }
        public static bool M_LeftArc(int id)
        {
            //Vector3 a = buff_enemy[id].location;
            int z = (int)buff_enemy[id].angle.z;
            z--;
            if (z <= 0)
                z = 359;
            if (z == 300)
            {
                buff_enemy[id].die = true;
                return false;
            }
            buff_enemy[id].angle.z = z;
            buff_enemy[id].location.x -= angle_table[z].x * 0.06f;
            buff_enemy[id].location.y -= angle_table[z].y * 0.06f;
            if (buff_enemy[id].shot != null)
            {
                if (buff_enemy[id].extra_b > buff_enemy[id].shotfrequency)
                {
                    buff_enemy[id].shot(buff_enemy[id].bulletid[0], id);
                    buff_enemy[id].extra_b = 0;
                }
                buff_enemy[id].extra_b++;
            }
            return true;
        }
        public static bool M_Dock_180(int id)
        {
            buff_enemy[id].extra_m++;
            if (buff_enemy[id].extra_m < 80 | buff_enemy[id].extra_m > 280)
            {
                buff_enemy[id].location.y -= 0.03f;
            }
            else
            {
                if (buff_enemy[id].extra_b > 10)
                {
                    buff_enemy[id].shot(buff_enemy[id].bulletid[0], id);
                    buff_enemy[id].extra_b = 0;
                }
                buff_enemy[id].extra_b++;
            }
            if (buff_enemy[id].location.y < -5.5f)
            {
                buff_enemy[id].die = true;
                return false;
            }
            return true;
        }
        public static bool M_Dock_200(int id)
        {
            buff_enemy[id].extra_m++;
            if (buff_enemy[id].extra_m < 100 | buff_enemy[id].extra_m > 300)
            {
                buff_enemy[id].location.y -= 0.03f;
            }
            else if(buff_enemy[id].extra_b > buff_enemy[id].shotfrequency)
            {
                buff_enemy[id].extra_b = 0;
                buff_enemy[id].shot(buff_enemy[id].bulletid[0], id);               
            }
            buff_enemy[id].extra_b++;  
            if (buff_enemy[id].location.y < -5.5f)
            {
                buff_enemy[id].die = true;
                return false;
            }
            return true;
        }
        public static bool M_Dwon_Back(int id)
        {
            buff_enemy[id].extra_m++;
            if (buff_enemy[id].extra_m < 100)
            {
                buff_enemy[id].location.y -= 0.03f;
                return true;
            }
            else
                if (buff_enemy[id].extra_m == 100)
            {
                buff_bullet[buff_enemy[id].bulletid[0]].s_count = 1;
                Vector3 temp = Vector3.zero;
                temp.z = Aim(ref buff_enemy[id].location, ref core_location);
                buff_bullet[buff_enemy[id].bulletid[0]].shot = new StartPoint[] { new StartPoint() { location = buff_enemy[id].location, angle = temp } };
                return true;
            }
            if (buff_enemy[id].extra_m < 120)
            {
                if (buff_enemy[id].location.x <= 0)
                {
                    if (buff_enemy[id].angle.z == 0)
                        buff_enemy[id].angle.z = 360;
                    buff_enemy[id].angle.z -= 9;
                }
                else
                {
                    buff_enemy[id].angle.z += 9;
                }
                return true;
            }
            if (buff_enemy[id].extra_m < 220)
            {
                buff_enemy[id].location.y += 0.03f;
                return true;
            }
            buff_enemy[id].extra_m = 0;
            buff_enemy[id].die = true;
            return false;
        }
        public static bool M_AimLeft(int id)
        {
            if(buff_enemy[id].location.x>3| buff_enemy[id].location.y<-5.5f)
            {
                buff_enemy[id].extra_m = 0;
                buff_enemy[id].die = true;
                return false;
            }
            if(buff_enemy[id].extra_m==0)
            {
                Aim1(ref buff_enemy[id].location,ref core_location,0.03f,ref buff_enemy[id].olt);
                buff_enemy[id].extra_m++;
            }
            if(buff_enemy[id].extra_b>buff_enemy[id].shotfrequency)
            {
                if (buff_enemy[id].location.x > -2.5f)
                    buff_enemy[id].shot(buff_enemy[id].bulletid[0],id);
                buff_enemy[id].extra_b = 0;
            }
            buff_enemy[id].extra_b++;
            buff_enemy[id].location += buff_enemy[id].olt;
            return true;
        }
        public static bool M_AimRight(int id)
        {
            if (buff_enemy[id].location.x <-3 | buff_enemy[id].location.y < -5.5f)
            {
                buff_enemy[id].extra_m = 0;
                buff_enemy[id].die = true;
                return false;
            }
            if (buff_enemy[id].extra_m == 0)
            {
                Aim1(ref buff_enemy[id].location, ref core_location, 0.03f, ref buff_enemy[id].olt);
                buff_enemy[id].extra_m++;
            }
            if (buff_enemy[id].extra_b > buff_enemy[id].shotfrequency)
            {
                if (buff_enemy[id].location.x <2.5f)
                    buff_enemy[id].shot(buff_enemy[id].bulletid[0], id);
                buff_enemy[id].extra_b = 0;
            }
            buff_enemy[id].extra_b++;
            buff_enemy[id].location += buff_enemy[id].olt;
            return true;
        }
        public static bool M_AimTop(int id)
        {
            if ( buff_enemy[id].location.y < -5.5f)
            {
                buff_enemy[id].extra_m = 0;
                buff_enemy[id].die = true;
                return false;
            }
            if (buff_enemy[id].extra_m == 0)
            {
                Aim1(ref buff_enemy[id].location, ref core_location, 0.03f, ref buff_enemy[id].olt);
                buff_enemy[id].extra_m++;
            }
            if (buff_enemy[id].extra_b > buff_enemy[id].shotfrequency)
            {
                if (buff_enemy[id].location.y < 5f)
                    buff_enemy[id].shot(buff_enemy[id].bulletid[0], id);
                buff_enemy[id].extra_b = 0;
            }
            buff_enemy[id].extra_b++;
            buff_enemy[id].location += buff_enemy[id].olt;
            return true;
        }
    }
}
