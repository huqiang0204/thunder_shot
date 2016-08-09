using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.UnityVS.Script
{
    class Store:GameControl
    {
        #region preview
        static ShotEX[] pv_shot = new ShotEX[3];
        static int[] pv_shot_id = new int[5];
        static WarPlane pv_wp;
        static Wing pv_w;
        static SecondBullet pv_sb;
        static ImageProperty pv_shiled = new ImageProperty() { sorting = 5, scale = new Vector2(1, 1) };
        static EffcetBase pv_skill;
        static int pv_state;

        static void calculate_pv()//core_location
        {
            //if(pv_state==0)
            //{
            //    if(pv_wp.specital)
            //    {
            //        pv_wp.sp_bullet.updatesub();
            //    }
            //}
            if (pv_shot[0] != null)
            {
                pv_wp.extra_b++;
                if (pv_wp.extra_b > pv_wp.shotfrequency)
                {
                    pv_wp.extra_b = 0;
                    pv_shot[0](pv_shot_id[0], 0);
                }
            }
            if (pv_shot[1] != null)
            {
                pv_sb.extra_b++;
                if(pv_sb.extra_b>pv_sb.shotfrequency)
                {
                    pv_sb.extra_b = 0;
                    pv_shot[1](pv_shot_id[1], 0);
                }
            }
            if (pv_shot[2] != null)
            {
                pv_w.extra_b++;
                if(pv_w.extra_b>pv_w.shotfrequency)
                {
                    pv_w.extra_b++;
                    pv_shot[2](pv_shot_id[2], 0);
                }
            }
            Calcul_Bullet2(26, 38, 1, CollisionCheck1);
            for (int i = 0; i < 2048; i++)
            {
                if (!buff_img2[i].reg | buff_img2[i].restore)
                {
                    img_id2 = i;
                    break;
                }
            }
            for (int i = 2047; i >= 0; i--)
            {
                if (buff_img2[i].reg & !buff_img2[i].restore)
                {
                    update_max = i;
                    break;
                }
            }
        }
        static void PreView()
        {
            //if (pv_state == 0)
            //{
            //    if (pv_wp.specital)
            //    {
            //        pv_wp.sp_bullet.updatemian();
            //    }
            //}
            UpdateImage2();
            UpdateBulletA();
            AsyncManage.AsyncDelegate(CalculateEffect);
            AsyncManage.AsyncDelegate(calculate_pv);
            Update_Effect();
            if (pv_skill.son != null)
            {
                if (buff_effect[pv_shot_id[4]].play == false)
                    Effect_Active(pv_shot_id[4]);
            }
        }
        public static void View_Start(Vector3 l)
        {
            core_location = l;
            current.wing.location.x = -1;
            current.wing.location.y = -0.3f;
            current.wing.location2.x = 1;
            current.wing.location2.y = -0.3f;
            MainCamera.update = PreView;
        }
        public static void View_Stop()
        {
            DeletePlaneView();
            DeleteSecondView();
            DeleteWingView();
            DeleteShiledView();
            DeleteSkillView();
            RecycleImageAllA();
            MainCamera.update = null;
        }
        public static void PlaneView(WarPlane wp)
        {
            pv_state = 0;
            pv_wp = wp;
            pv_wp.imageid = CreateImage(def_transform, wp.image);
            if (wp.specital)
            {
                wp.sp_bullet.inital(wp.bullet);
            }
            else
            {
                pv_shot[0] = wp.shot[3];
                pv_shot_id[0] = RegBullet2P(wp.bullet, 26);
            }
        }
        public static void DeletePlaneView()
        {
             if (pv_wp.specital)
             {
                pv_wp.sp_bullet.dispose();
                RecycleImage(pv_wp.imageid);
            }else
            if (pv_shot[0] != null)
            {
                pv_shot[0] = null;
                ClearBullet(pv_shot_id[0]);
                RecycleImage(pv_wp.imageid);
            }
        }
        public static void SecondView(SecondBullet s)
        {
            pv_sb = s;
            if (s.specital)
                s.sp_bullet.inital(s.bullet);
            else
            {
                pv_shot[1] = s.shot[3];
                pv_shot_id[1] = RegBullet2P(s.bullet, 26);
            }
        }
        public static void DeleteSecondView()
        {
            if (pv_sb.specital)
                pv_sb.sp_bullet.dispose();
            if (pv_shot[1] != null)
            {
                pv_shot[1] = null;
                ClearBullet(pv_shot_id[1]);
            }
        }
        public static void WingView(Wing w)
        {
            pv_w = w;
            w.image.location.x = core_location.x - 1;
            w.image.location.y = core_location.y - 0.3f;
            w.image.location.z = 1;
            pv_w.imageid = CreateImage(def_transform, w.image);
            w.image.location.x = core_location.x + 1;
            pv_w.imageid2 = CreateImage(def_transform, w.image);
            if(w.specital)
            {
                w.sp_bullet.inital(w.bullet);
            }else
            {
                pv_shot[2] = w.shot[0];
                pv_shot_id[2] = RegBullet2P(w.bullet, 26);
            }
        }
        public static void DeleteWingView()
        {
            if (pv_w.specital)
            {
                pv_w.sp_bullet.dispose();
                RecycleImage(pv_w.imageid);
                RecycleImage(pv_w.imageid2);
            }
            else
            if (pv_shot[2] != null)
            {
                pv_shot[2] = null;
                ClearBullet(pv_shot_id[2]);
                RecycleImage(pv_w.imageid);
                RecycleImage(pv_w.imageid2);
            }
        }
        public static void ShiledView(Shiled s)
        {
            pv_shiled.imagepath = s.imgpath;
            pv_shiled.location = core_location;
            pv_shot_id[3] = CreateImage(def_transform, pv_shiled);
        }
        public static void DeleteShiledView()
        {
            if (pv_shot_id[3] > 0)
            {
                RecycleImage(pv_shot_id[3]);
                pv_shot_id[3] = 0;
            }
        }
        public static void SkillView(Skill s)
        {
            pv_skill = s.eff_b;
            pv_shot_id[4] = RegEffet(pv_skill);
            Effect_Active(pv_shot_id[4]);
        }
        public static void DeleteSkillView()
        {
            if (pv_skill.son != null)
            {
                DeleteEffect(pv_shot_id[4]);
                pv_skill.son = null;
            }
        }
        #endregion
    }
}
