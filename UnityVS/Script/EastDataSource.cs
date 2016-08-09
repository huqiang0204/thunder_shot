using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.UnityVS.Script
{
    class EastDataSource:StaticParameter
    {
        static Point2[] def_east_points = new Point2[] { new Point2(15, 0.140317f), new Point2(345, 0.140317f), new Point2(194, 0.154367f), new Point2(166, 0.154367f) };


        #region bullet 
        public static BulletPropertyEX b_christmas = new BulletPropertyEX()
        {
            minrange = 0.08f,
            maxrange = 0.8f,
            speed = 0.3f,
            attack = 10,
            image = new ImageProperty() { scale = def_scale, imagepath = "Picture2/bl00", sorting = 3 ,grid=new Grid(2,1)},
            edgepoints=new Point2[] { new Point2(137,0.23067f), new Point2(0, 0.1823f), new Point2(223, 0.23067f) }
        };
        public static BulletPropertyEX b_bagua = new BulletPropertyEX()
        {
            minrange = 0.1341f,
            maxrange = 0.4f,
            speed = 0.3f,
            attack = 40,spt_index=1,radius=0.21f,move=BulletMove.B_ArcTrace,
            image = new ImageProperty() { scale = def_scale, imagepath = "Picture2/bl00", sorting = 3, grid = new Grid(2, 1) },
        };
        public static BulletPropertyEX b_def_01 = new BulletPropertyEX()
        {
            minrange = 0.025f,
            radius = 0.13f,
            speed = 0.03f,
            attack = 50,
            image = new ImageProperty() { scale = def_scale, imagepath = "Picture2/bullet1", sorting = 5, grid = def_8x6 }
        };
        public static BulletPropertyEX b_def_02 = new BulletPropertyEX()
        {
            minrange = 0.015f,
            speed = 0.03f,
            attack = 50,
            maxrange = 0.038f,
            spt_index = 16,
            image = new ImageProperty() { scale = def_scale, imagepath = "Picture2/bullet1", sorting = 5, grid = def_8x6 },
            edgepoints = def_east_points
        };
        public static BulletPropertyEX b_def_03 = new BulletPropertyEX()
        {
            penetrate = true,
            radius = 0.32f,
            minrange = 0.1105f,
            speed = 0.03f,
            attack = 150,
            image = new ImageProperty() { scale = def_scale, imagepath = "Picture2/bullet2", sorting = 5, grid = def_4x1 }
        };
        #endregion

        #region enemy
        public static EnemyPropertyEX e_hat = new EnemyPropertyEX()
        {
            image = new ImageProperty() { imagepath = "Picture2/enemy03", grid = def_3x2 },
            bullet = new BulletPropertyEX[] { b_def_01 },
            enemy = new EnemyBaseEX()
            {
                spt_index=4,
                blood = 2000,
                minrange = 0.1681f,
                maxrange = 0.1681f,
                radius = 0.4f,
            }
        };
        public static EnemyPropertyEX e_bat_l = new EnemyPropertyEX()
        {
            image = new ImageProperty() {imagepath = "Picture2/bat", grid = def_4x2 },
            bullet = new BulletPropertyEX[] { b_def_01 },
            enemy = new EnemyBaseEX()
            {
                play = EnemyMove.Player_0_3,move=EnemyMove.M_LeftToRightC,
                blood = 200,
                minrange = 0.0981f,
                maxrange = 0.0981f,
                radius = 0.3f,
            }
        };
        public static EnemyPropertyEX e_bat_r = new EnemyPropertyEX()
        {
            image = new ImageProperty() { imagepath = "Picture2/bat", grid = def_4x2 },
            bullet = new BulletPropertyEX[] { b_def_01 },
            enemy = new EnemyBaseEX()
            {
                spt_index = 4,
                play = EnemyMove.Player_4_7,
                move = EnemyMove.M_RightToLeftC,
                blood = 200,
                minrange = 0.0981f,
                maxrange = 0.0981f,
                radius = 0.3f,
            }
        };
        public static EnemyPropertyEX e_def_01 = new EnemyPropertyEX()
        {
            image = new ImageProperty() {imagepath = "Picture2/enemy01",grid= def_4x6 },
            bullet = new BulletPropertyEX[] { b_def_01},
            enemy = new EnemyBaseEX()
            {              
                spt_index=11,
                blood = 300,
                minrange = 0.1681f,
                maxrange = 0.1681f,
                radius=0.4f,
            }
        };
        public static EnemyPropertyEX e_def_02 = new EnemyPropertyEX()
        {
            image = new ImageProperty() {imagepath = "Picture2/enemy01", grid = def_4x6 },
            bullet = new BulletPropertyEX[] { b_def_01 },
            enemy = new EnemyBaseEX()
            {
                spt_index=23,
                blood = 300,
                minrange = 0.1681f,
                maxrange = 0.1681f,
                radius=0.4f,
            }
        };
        public static EnemyPropertyEX e_def_03 = new EnemyPropertyEX()
        {
            image = new ImageProperty() {imagepath = "Picture2/enemy02", grid = def_4x2},
            bullet = new BulletPropertyEX[] { b_def_01 },
            enemy = new EnemyBaseEX()
            {
                blood = 600,
                minrange = 0.2581f,
                maxrange = 0.2581f,
                radius=0.5f,
            }
        };
        #endregion

        #region warplane
        public static WarPlane plane_lingmeng = new WarPlane()
        {
            blood = 1000,
            currentblood = 1000,
            image = new ImageProperty()
            {
                grid=def_8x2,
                location = new Vector3(0, 0, 1),
                imagepath = "Picture2/pl00",
                sorting = 4,
                scale = new Vector3(1, 1)
            },
            bullet =b_christmas,shotfrequency=4,
            play=EastProjectMod.Playlingmeng,
            shot = new ShotEX[] { ShotBullet.Shot_lingmeng},
        };
        public static SecondBullet sb_lingmeng = new SecondBullet()
        {
            shot = new ShotEX[] {ShotBullet.Shot_lingmengS },
            bullet = b_bagua,shotfrequency=20,
        };
        #endregion

        #region battelfiled
        static SetBattelField[] SetLeveMod = new SetBattelField[] { SetLevel1 };
        static int index_s;
        public static void GetLevel(int index)
        {
            index_s = index;
            AsyncManage.AsyncDelegate(() => {
                GameControl.SetLevel(SetLeveMod[index_s]);
            });
            GameControl.SetMainPlane(EastDataSource.plane_lingmeng);
            GameControl.SetSecondWeapon(EastDataSource.sb_lingmeng);         
        }

        static void SetLevel1(ref BattelField bf)
        {
            //注册需要的资源让主线程去加载
            GameControl.RegE(ref e_def_01);
            GameControl.RegE(ref e_def_03);
            GameControl.RegE(ref e_bat_l);
            e_bat_r.enemy.dspt_id = e_bat_l.enemy.dspt_id;
            GameControl.RegE(ref e_hat);
            GameControl.RegB(ref b_def_01);
            b_def_02.spt_id = b_def_01.spt_id;
            GameControl.RegB(ref b_def_03);
            GameControl.RegB(ref DataSource.b_def_b03b);
            //
            bf = new BattelField();
            bf.bk_groud = new BackGround[] { new BackGround() { dur_wave = 999999, location= new Vector3(0, 0f, 1),
               mixoffset= new Vector3(0, 11.47f, 0),angle=new Vector3(0,0,90), scale=new Vector3(2,2), imgpath="Picture/cosmos" } };
            bf.wave = new List<EnemyWave>();
            
            EnemyWave ew = new EnemyWave();
            ew.enemyppt=e_def_01;
            ew.enemyppt.bullet = new BulletPropertyEX[] { b_def_01 };
            ew.interval = 10;
            ew.enemyppt.enemy.move = EnemyMove.M_Down_FastToSlow;
            ew.enemyppt.enemy.shotfrequency = 15;
            ew.enemyppt.enemy.shot = ShotBullet.Shot_Aim_3;
            ew.start = FixStartPoint.e_topline3_01;
            ew.sum = 6;
            ew.staytime = 200;
            bf.wave.Add(ew);
            ew.start = FixStartPoint.e_topline3_02;
            bf.wave.Add(ew);
            ew.enemyppt.enemy.move = EnemyMove.M_AimLeft;
            ew.sum = 6;
            ew.start = FixStartPoint.e_lefttop2_01;
            ew.enemyppt.enemy.shotfrequency = 60;
            ew.enemyppt.enemy.shot = ShotBullet.Shot_Aim_Arc6;
            bf.wave.Add(ew);
            ew.enemyppt.enemy.move = EnemyMove.M_AimRight;
            ew.start = FixStartPoint.e_righttop2_01;
            bf.wave.Add(ew);

            ew.enemyppt = e_def_03;
            ew.enemyppt.bullet = bf.wave[0].enemyppt.bullet;
            ew.sum = 1;
            ew.enemyppt.enemy.blood = 2000;
            ew.enemyppt.enemy.shotfrequency = 30;
            ew.enemyppt.enemy.move = EnemyMove.M_Downward_01;
            ew.enemyppt.enemy.shot = ShotBullet.Shot_Aim_Arc18;
            ew.staytime = 200;
            ew.start = S_Up_3;
            bf.wave.Add(ew);

            ew.enemyppt = e_bat_l;
            ew.enemyppt.bullet = bf.wave[0].enemyppt.bullet;
            ew.interval = 10;
            ew.enemyppt.enemy.shotfrequency = 30;
            ew.enemyppt.enemy.shot = ShotBullet.Shot_Aim_Arc6;
            BulletPropertyEX[] bpe= new BulletPropertyEX[] { b_def_01 };
            bpe[0].spt_id = ew.enemyppt.bullet[0].spt_id;            
            ew.enemyppt.bullet = bpe;
            ew.enemyppt.bullet[0].speed = 0.05f;
            ew.sum = 6;
            ew.staytime = 300;
            ew.start = FixStartPoint.e_lefttop2_01;
            bf.wave.Add(ew);

            ew = bf.wave[bf.wave.Count - 2];
            ew.enemyppt.enemy.shot = ShotBullet.Shot_Circle36A;
            ew.start = S_Up_3;
            ew.staytime = 300;
            bf.wave.Add(ew);

            ew.enemyppt = e_bat_r;
            ew.enemyppt.bullet = bf.wave[0].enemyppt.bullet;
            ew.interval = 10;
            ew.enemyppt.enemy.shotfrequency = 30;
            ew.enemyppt.enemy.shot = ShotBullet.Shot_Aim_Arc6;
            ew.enemyppt.bullet=bpe;
            ew.sum = 6;
            ew.staytime = 300;
            ew.start = FixStartPoint.e_righttop2_03;
            bf.wave.Add(ew);

            ew= bf.wave[0];
            ew.enemyppt.enemy.shot = ShotBullet.Shot_Aim_Arc3;
            ew.enemyppt.bullet = bpe;
            ew.staytime = 300;
            bf.wave.Add(ew);
            ew = bf.wave[0];
            ew.enemyppt.enemy.move=EnemyMove.M_AimTop;
            ew.staytime = 300;
            bf.wave.Add(ew);            
            ew.staytime = 500;
            bf.wave.Add(ew);

            ew.enemyppt = e_hat;
            ew.enemyppt.enemy.blood = 50000;
            ew.enemyppt.enemy.boss = true;
            ew.enemyppt.bullet=new BulletPropertyEX[] {b_def_01, b_def_02, b_def_03,DataSource.b_def_b03b,
                b_def_01, b_def_01, b_def_01, b_def_01, b_def_01, b_def_01,b_def_01, b_def_01, b_def_01,};//13 bullet
            ew.enemyppt.enemy.move = EastProjectMod.M_Boss_Eastproject;
            ew.start = S_Up_3;
            ew.sum = 1;
            ew.staytime = 65535;
            bf.wave.Add(ew);
        }
        #endregion

    }
}
