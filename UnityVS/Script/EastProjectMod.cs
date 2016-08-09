#define Debug
#undef Debug
using System;
using UnityEngine;

namespace Assets.UnityVS.Script
{
    class EastProjectMod:GameControl
    {
        static int hpid, hpid2;

        #region demage
        static void DamageCalculateMod2A(int bulletid)
        {
            current.warplane.currentblood -= buff_bullet[bulletid].attack;
            if (current.warplane.currentblood <= 0)
                gameover = true;
        }
        static void DamageCalculateMod2C(int enemyid)
        {
            current.warplane.currentblood -= 30;
            if (current.warplane.currentblood <= 0)
                gameover = true;
        }
        static void DamageCalculateMod2B(int bulletid, int enemyid)
        {
            buff_enemy[enemyid].currentblood -= buff_bullet[bulletid].attack;
            buff_enemy[enemyid].bloodpercent = buff_enemy[enemyid].currentblood / buff_enemy[enemyid].blood;
            if (buff_enemy[enemyid].currentblood < 0)
            {
                buff_enemy[enemyid].die = true;
                destory_enemy++;
                generateA(10, buff_enemy[enemyid].location);
                if (bomb_count < 15)
                {
                    bomb_queue[bomb_count] = buff_enemy[enemyid].location;
                    bomb_count++;
                }
            }else
            if (collid_count < 15)
            {
                collid_queue[collid_count] = buff_enemy[enemyid].location;
                collid_queue[collid_count].y -= 0.2f;
                collid_count++;
            }
        }
        #endregion

        #region collid
        static AnimatBase buff_collid = new AnimatBase();
        static int collid_count;
        static Vector3[] collid_queue = new Vector3[16];
        //static EffectProperty ef_collid = new EffectProperty()
        //{
        //    path = "Picture2/collid",
        //    grid = def_4x1
        //};
        static void Collid_Initial()
        {
            buff_collid.sptex_id = CreateSpriteG("Picture2/collid", new Grid(4,1));
            buff_collid.son = new AnimatObject[16];
        }
        static void Collid_Update2(int img_id,int extra)
        {
            if (buff_collid.son[extra].active)
            {
                if (buff_collid.son[extra].extra % 3 == 0)
                {
                    buff_img2[img_id].spriterender.sprite =
                        buff_spriteex[buff_collid.sptex_id].sprites[buff_collid.son[extra].extra / 3];
                    Vector3 t = buff_img2[img_id].transform.localPosition;
                    t.y += 0.05f;
                    buff_img2[img_id].transform.localPosition = t;
                }
                buff_collid.son[extra].extra++;
                if (buff_collid.son[extra].extra > 9)
                {
                    buff_collid.son[extra].extra = 0;
                    buff_collid.son[extra].active = false;
                    buff_img2[img_id].gameobject.SetActive(false);
                    buff_collid.act_count--;
                }
            }
        }
        static void Collid_Update2A()
        {
            int id = 0;
            for (int i = 0; i < collid_count; i++)
            {
                while (buff_collid.son[id].active)
                {
                    id++;
                    if (id > 15)
                    {
                        collid_count = 0;
                        return;
                    }
                }
                int imgid;
                if (buff_collid.son[id].created)
                {
                    imgid = buff_collid.son[id].imageid;
                    buff_img2[imgid].transform.localPosition = collid_queue[i];
                    buff_img2[imgid].gameobject.SetActive(true);
                }
                else
                {
                    buff_collid.son[id].imageid = imgid = CreateImageNull2(Collid_Update2,id);
                    buff_collid.son[id].created = true;
                    buff_img2[imgid].transform.localPosition = collid_queue[i];
                    buff_img2[imgid].transform.localScale = origion_scale;
                    buff_img2[imgid].spriterender.sortingOrder = 8;
                    buff_img2[imgid].spriterender.sprite = buff_spriteex[buff_collid.sptex_id].sprites[0];
                }
                buff_collid.son[id].active = true;
                buff_collid.act_count++;
            }
            collid_count = 0;
        }
        static void Collid_Clear()
        {
            for (int i = 0; i < 10; i++)
            {
                buff_collid.son[i].created = false;
            }
        }
        #endregion

        #region bomb
        static AnimatBase buff_bomb = new AnimatBase();
        static int bomb_count;
        static Vector3[] bomb_queue = new Vector3[16];
        static void Bomb_Initial()
        {
            buff_bomb.sptex_id = CreateSpriteG("Picture2/fire", new Grid(3,2));
            buff_bomb.son = new AnimatObject[16];
        }
        static void Bomb_Update2A()
        {
            int id = 0;
            for (int i = 0; i < bomb_count; i++)
            {
                while (buff_bomb.son[id].active)
                {
                    id++;
                    if (id > 15)
                    {
                        bomb_count = 0;
                        return;
                    }
                }
                int imgid;
                if (buff_bomb.son[id].created)
                {
                    imgid = buff_bomb.son[id].imageid;
                    buff_img2[imgid].transform.localPosition = bomb_queue[i];
                    buff_img2[imgid].gameobject.SetActive(true);
                }
                else
                {
                    buff_bomb.son[id].imageid = imgid = CreateImageNull2(Bomb_Update2,id);
                    buff_bomb.son[id].created = true;
                    buff_img2[imgid].transform.localPosition = bomb_queue[i];
                    buff_img2[imgid].transform.localScale = origion_scale;
                    buff_img2[imgid].spriterender.sortingOrder = 8;
                    buff_img2[imgid].spriterender.sprite = buff_spriteex[buff_bomb.sptex_id].sprites[0];
                }
                buff_bomb.son[id].active = true;
                buff_bomb.act_count++;
            }
            bomb_count = 0;
        }
        static void Bomb_Update2(int img_id, int extra)
        {
            if (buff_bomb.son[extra].active)
            {
                if (buff_bomb.son[extra].extra % 3 == 0)
                {
                    buff_img2[img_id].spriterender.sprite =
                        buff_spriteex[buff_bomb.sptex_id].sprites[buff_bomb.son[extra].extra / 3];
                    Vector3 t = buff_img2[img_id].transform.localPosition;
                    t.y += 0.1f;
                    buff_img2[img_id].transform.localPosition = t;
                }
                buff_bomb.son[extra].extra++;
                if (buff_bomb.son[extra].extra > 15)
                {
                    buff_bomb.son[extra].extra = 0;
                    buff_bomb.son[extra].active = false;
                    buff_img2[img_id].gameobject.SetActive(false);
                    buff_bomb.act_count--;
                }
            }
        }
        static void Bomb_Clear()
        {
            for (int i = 0; i < 10; i++)
            {
                buff_bomb.son[i].created = false;
            }
        }
        #endregion

        #region sence control
        static void FixedUpdate()//main thread
        {
            if (gameover)
            {
                if (current.warplane.currentblood > 0)
                    GameOverA(true);
                else
                    GameOverA(false);
                MainCamera.update = null;
                return;
            }
            AsyncManage.AsyncDelegate(CalculateEffect);
            UpdateImage2();
            UpdataBackground();
            UpdateEnemy2A();
            UpdateBulletA();
            AsyncManage.AsyncDelegate(CaculateEnemy);
            AsyncManage.AsyncDelegate(() => { Calcul_Bullet2(0,26,2,CollisionCheck); });
            AsyncManage.AsyncDelegate(() => { Calcul_Bullet2(1,26,2,CollisionCheck); });
            AsyncManage.AsyncDelegate(Thread1);
            Update_Effect();
            buff_img[hpid2].spriterender.material.SetFloat("_Progress", current.warplane.currentblood / current.warplane.blood);
            if(current.warplane.play!=null)
            {
                int id = current.warplane.imageid;
                int spid = current.warplane.spt_id;
                int spindex = current.warplane.spt_index;
                buff_img[id].spriterender.sprite = buff_spriteex[spid].sprites[spindex];
            }
            Collid_Update2A();
            Bomb_Update2A();
        }
        public static void GameStart()
        {
            if(buff_wave.bk_groud==null)
            {
                MainCamera.update = GameStart;
                return;
            }
            LoadBackGround(ref buff_wave.bk_groud[0]);
            LoadEnemyResource();
            update = FixedUpdate;
            D_gameover = GameOverA;
            current.warplane.currentblood = current.warplane.blood;
            currentwave = 0;
            img_id2 = 0;           
            gameover = false;
            mouse_move = Plane_Move;
            plane_move = true;
            destory_enemy = 0;
            damageA = DamageCalculateMod2A;
            damageB = DamageCalculateMod2B;
            damageC = DamageCalculateMod2C;
            generateA = GenerateProp;
            LoadPlan();
            Collid_Initial();
            Bomb_Initial();            
            MainCamera.update = FixedUpdate;
        }
        static void GameOverA(bool pass)
        {
            gameover = true;
            plane_move = false;
            mouse_move = null;
            AsyncManage.AsyncDelegate(() => {                
                ClearEnemyAll();
                imageid = 0;
                buff_wave.bk_groud = null;
            });
#if Debug
            ClearImageAllA();
            ClearImageRecycleA();
#else
            RecycleImageAllA();
#endif
            ClearBloodAll();
            ClearPlan();
            ClearBulletAll();
            CanvasControl.GameOverCallBack(pass, destory_enemy);            
            Collid_Clear();
            Bomb_Clear();
            ClearSpriteS(128, 256);
            Resources.UnloadUnusedAssets();
            
        }
        static void Thread1()//sub thread
        {
            PlanShot();
            Calcul_Bullet2(26,38,1,CollisionCheck1);
            if(current.warplane.play!=null)
            {
                current.warplane.play();
            }
            for(int i=0;i<2048;i++)
            {
                if(!buff_img2[i].reg |buff_img2[i].restore)
                {
                    img_id2 = i;
                    break;
                }
            }
            for (int i = 2047; i >=0; i--)
            {
                if (buff_img2[i].reg & !buff_img2[i].restore)
                {
                    update_max = i;
                    break;
                }
            }
        }
        #endregion

        #region plane control
        static void PlanShot()
        {
            if (current.warplane.extra_b > current.warplane.shotfrequency)
            {
                current.warplane.extra_b=0;
                current.warplane.shot[0](current.warplane.bulletid, 0);
            }
            if(current.B_second.extra_b>current.B_second.shotfrequency)
            {
                current.B_second.extra_b = 0;
                current.B_second.shot[0](current.B_second.bulletid, 0);
            }
            current.warplane.extra_b++;
            current.B_second.extra_b++;
        }
        static void LoadPlan()
        {
            hpid = CreateImage(def_transform, blood_back);
            hpid2 = CreateImage(buff_img[hpid].transform, blood_g);
            buff_img[hpid2].spriterender.material = Mat_Blood;
            buff_img[hpid2].spriterender.material.SetFloat("_Progress", 1);

            coreid = CreateImage(def_transform, core);

            current.warplane.imageid = CreateImageA(buff_img[coreid].transform, current.warplane.image, current.warplane.spt_id);
            current.warplane.bulletid = RegBullet2(ref current.warplane.bullet,26);
            current.B_second.bulletid = RegBullet2(ref current.B_second.bullet,26);
            core_location = origion_location;
        }
        static void ClearPlan()
        {
            ClearImage(coreid);
            ClearImage(current.warplane.imageid);
            ClearImage(hpid);
        }
        #endregion

        public static void Playlingmeng()
        {           
            if (current.warplane.extra_a > 3)
            {
                float a = core_location.x - oldloaction.x;
                if (a != 0)
                {
                    if (a < 0)//left 8-15
                    {
                        if (current.warplane.spt_index < 8)
                        {
                            current.warplane.spt_index = 8;
                        }
                        if (current.warplane.spt_index < 15)
                            current.warplane.spt_index++;
                    }
                    else//right 0-7
                    {
                        if(current.warplane.spt_index>7)
                        {
                            current.warplane.spt_index = 0;
                        }else                         
                        if (current.warplane.spt_index < 7)
                            current.warplane.spt_index++;
                    }                    
                }else
                {
                    current.warplane.spt_index=0;
                }
                current.warplane.extra_a = 0;
            }
            current.warplane.extra_a++;
        }

        static void GenerateProp(int max, Vector3 location)
        {

        }

        #region enemy boss
        static Vector3 boss_v = Vector3.zero;
        static bool boss_m;
        static int boss_s_time1, boss_s_time2;
        static bool EastPorjectShot1(int id)//9,10
        {
            int bid;
            if (boss_s_time1 == 0)
            {
                int c = buff_enemy[id].bulletid[0];
                int d;
                for (int i = 9; i < 11; i++)
                {
                    d = buff_enemy[id].bulletid[i];
                    ReSetBullet2A(c, d);
                }
            }
            if ((boss_s_time1 & 1) == 0)
                bid = buff_enemy[id].bulletid[9];
            else bid = buff_enemy[id].bulletid[10];
            buff_bullet[bid].spt_index = 26;
            buff_bullet[bid].s_count = 36;
            switch (boss_s_time1)
            {
                case 0:
                    buff_bullet[bid].shot = FixStartPoint.GetCircle36(new Vector3(0, 4, 1));
                    break;
                case 1:
                    buff_bullet[bid].shot = FixStartPoint.GetCircle36(new Vector3(-2, 0, 1));
                    break;
                case 2:
                    buff_bullet[bid].shot = FixStartPoint.GetCircle36(new Vector3(3, 3, 1));
                    break;
                case 3:
                    buff_bullet[bid].shot = FixStartPoint.GetCircle36(new Vector3(-3, 3, 1));
                    break;
                case 4:
                    buff_bullet[bid].shot = FixStartPoint.GetCircle36(new Vector3(2, 0, 1));
                    break;
                case 5:
                    buff_bullet[bid].shot = FixStartPoint.GetCircle36(new Vector3(0, 2, 1));
                    break;
            }
            boss_s_time1++;
            if (boss_s_time1 > 5)
            {
                buff_enemy[id].extra_b = -50;
                boss_s_time1 = 0;
                return true;
            }
            buff_enemy[id].extra_b = -15;
            return false;
        }
        static bool EastPorjectShot2(int id)//0,4-8
        {    
            if(boss_s_time1==0)
            {
                int c=buff_enemy[id].bulletid[0];
                int d;
                for(int i=4;i<9;i++)
                {
                    d = buff_enemy[id].bulletid[i];
                    ReSetBullet2A(c, d);
                }
            }       
            if ((boss_s_time1 & 1) == 0)
            {
                int bid = buff_enemy[id].bulletid[0];
                buff_bullet[bid].spt_index = 0;
                buff_bullet[bid].s_count = 22;
                buff_bullet[bid].shot = FixStartPoint.b_left_arc22;
                bid = buff_enemy[id].bulletid[4];
                buff_bullet[bid].spt_index = 1;
                buff_bullet[bid].s_count = 22;
                buff_bullet[bid].shot = FixStartPoint.b_right_arc22;
                bid = buff_enemy[id].bulletid[5];
                buff_bullet[bid].spt_index = 2;
                buff_bullet[bid].s_count = 36;
                buff_bullet[bid].shot = FixStartPoint.GetCircle36(buff_enemy[id].location);
            }
            else
            {
                int bid = buff_enemy[id].bulletid[6];
                buff_bullet[bid].spt_index = 3;
                buff_bullet[bid].s_count = 22;
                buff_bullet[bid].shot = FixStartPoint.b_left_arc22;
                bid = buff_enemy[id].bulletid[7];
                buff_bullet[bid].spt_index = 4;
                buff_bullet[bid].s_count = 22;
                buff_bullet[bid].shot = FixStartPoint.b_right_arc22;
                bid = buff_enemy[id].bulletid[8];
                buff_bullet[bid].spt_index = 5;
                buff_bullet[bid].s_count = 36;
                buff_bullet[bid].shot = FixStartPoint.GetCircle36(buff_enemy[id].location);
                if (boss_s_time1 >= 5)
                {
                    boss_s_time1 = 0;
                    buff_enemy[id].extra_b = -100;
                    return true;
                }
            }
            boss_s_time1++;
            buff_enemy[id].extra_b = -15;
            return false;
        }
        static bool EastPorjectShot3(int id)//11,12
        {
            int bid;
            if (boss_s_time1 == 0)
            {
                int c = buff_enemy[id].bulletid[0];
                int d;
                for (int i = 11; i < 13; i++)
                {
                    d = buff_enemy[id].bulletid[i];
                    ReSetBullet2A(c, d);
                }
            }
            if ((boss_s_time1 & 1) == 0)
                bid = buff_enemy[id].bulletid[11];
            else bid = buff_enemy[id].bulletid[12];
            buff_bullet[bid].spt_index = 27;
            buff_bullet[bid].s_count = 36;
            switch (boss_s_time1)
            {
                case 0:
                    buff_bullet[bid].shot = FixStartPoint.GetCircle36(new Vector3(0, 4, 1));
                    break;
                case 1:
                    buff_bullet[bid].shot = FixStartPoint.GetCircle36(new Vector3(-2, 4, 1));
                    break;
                case 2:
                    buff_bullet[bid].shot = FixStartPoint.GetCircle36(new Vector3(4, 4, 1));
                    break;
                case 3:
                    buff_bullet[bid].shot = FixStartPoint.GetCircle36(new Vector3(-1.5f, 3, 1));
                    break;
                case 4:
                    buff_bullet[bid].shot = FixStartPoint.GetCircle36(new Vector3(1.5f, 3, 1));
                    break;
                case 5:
                    buff_bullet[bid].shot = FixStartPoint.GetCircle36(new Vector3(0, 2, 1));
                    break;
            }
            boss_s_time1++;
            if (boss_s_time1 > 5)
            {
                buff_enemy[id].extra_b = -50;
                boss_s_time1 = 0;
                return true;
            }
            buff_enemy[id].extra_b = -15;
            return false;
        }
        static bool EastPorjectShot4(int id)//1,4
        {
            if (boss_s_time1 == 0)
            {
                int c = buff_enemy[id].bulletid[1];
                int d = buff_enemy[id].bulletid[4];
                ReSetBullet2A(c, d);
            }
            if ((boss_s_time1 & 1) == 0)
            {
                int bid = buff_enemy[id].bulletid[1];
                buff_bullet[bid].s_count = 10;
                buff_bullet[bid].move = null;
                buff_bullet[bid].shot = FixStartPoint.fix_line_angle215;
                buff_bullet[bid].spt_index = 17;
            }
            else
            {
                int bid = buff_enemy[id].bulletid[4];
                buff_bullet[bid].s_count = 10;
                buff_bullet[bid].move = null;
                buff_bullet[bid].shot = FixStartPoint.fix_right_angle135;
                buff_bullet[bid].spt_index = 17;
            }
            boss_s_time1++;
            if (boss_s_time1 > 16)
            {
                boss_s_time1 = 0;
                buff_enemy[id].extra_b = -1;
                return true;
            }
            buff_enemy[id].extra_b = -5;
            return false;
        }
        static bool EastPorjectShot5(int id)//5-8
        {
            if (boss_s_time1 == 0)
            {
                int c = buff_enemy[id].bulletid[1];
                int d;
                for (int i = 5; i < 9; i++)
                {
                    d = buff_enemy[id].bulletid[i];
                    ReSetBullet2A(c, d);
                }
            }
            if ((boss_s_time1 & 1) == 0)
            {
                int bid = buff_enemy[id].bulletid[5];
                buff_bullet[bid].speed = 0.03f;
                buff_bullet[bid].s_count = 10;
                buff_bullet[bid].spt_index = 18;
                buff_bullet[bid].move = null;
                buff_bullet[bid].shot = FixStartPoint.b_line_leftB;
                bid = buff_enemy[id].bulletid[6];
                buff_bullet[bid].speed = 0.03f;
                buff_bullet[bid].spt_index = 18;
                buff_bullet[bid].move = null;
                buff_bullet[bid].s_count = 10;
                buff_bullet[bid].shot = FixStartPoint.b_line_rightA;
            }
            else
            {
                int bid = buff_enemy[id].bulletid[7];
                buff_bullet[bid].s_count = 10;
                buff_bullet[bid].shot = FixStartPoint.b_line_leftA;
                bid = buff_enemy[id].bulletid[8];
                buff_bullet[bid].s_count = 10;
                buff_bullet[bid].shot = FixStartPoint.b_line_rightB;
                if (boss_s_time1 >= 7)
                {
                    boss_s_time1 = 0;
                    buff_enemy[id].extra_b = -100;
                    return true;
                }
            }
            boss_s_time1++;
            buff_enemy[id].extra_b = -15;
            return false;
        }
        static bool EastPorjectShot6(int id)//3,9-11
        {
            if (boss_s_time1 == 0)
            {
                int c = buff_enemy[id].bulletid[3];
                buff_bullet[c].move = BulletMove.B_Ripple;
                int d = buff_enemy[id].bulletid[9];
                ReSetBullet2A(c, d);
                c = buff_enemy[id].bulletid[1];
                for (int i = 10; i < 12; i++)
                {
                    d = buff_enemy[id].bulletid[i];
                    ReSetBullet2A(c, d);
                }
            }
            if (boss_s_time1 < 40)
            {
                if (boss_s_time1 % 2 == 0)
                {
                    if (boss_s_time1 % 4 == 0)
                    {
                        int bid = buff_enemy[id].bulletid[10];
                        buff_bullet[bid].move = null;
                        buff_bullet[bid].s_count = 5;
                        buff_bullet[bid].shot = FixStartPoint.b_line_topB;
                    }
                    else
                    {
                        int bid = buff_enemy[id].bulletid[11];
                        buff_bullet[bid].move = null;
                        buff_bullet[bid].s_count = 6;
                        buff_bullet[bid].shot = FixStartPoint.b_line_topA;
                    }
                }
            }
            else if (boss_s_time1 % 10 == 0)
            {
                int bid = buff_enemy[id].bulletid[3];
                buff_bullet[bid].s_count = 36;
                buff_bullet[bid].shot = FixStartPoint.GetCircle36(buff_enemy[id].location);
            }
            else
            {
                int bid = buff_enemy[id].bulletid[9];
                buff_bullet[bid].s_count = 6;
                buff_bullet[bid].shot = FixStartPoint.GetSixAngle(boss_s_time2, buff_enemy[id].location);
                boss_s_time2 += 6;
                if (boss_s_time2 > 30)
                    boss_s_time2 = 0;
            }
            boss_s_time1++;
            if (boss_s_time1 > 70)
            {
                boss_s_time1 = 0;
                boss_s_time2 = 0;
                buff_enemy[id].extra_b = -100;
                return true;
            }
            buff_enemy[id].extra_b = -5;
            return false;
        }
        static bool EastPorjectShot7(int id)//2,4-5
        {
            if (boss_s_time1 == 0)
            {
                int bid = buff_enemy[id].bulletid[2];
                buff_bullet[bid].speed = 0.02f;
                buff_bullet[bid].s_count = 4;
                buff_bullet[bid].move = BulletMove.B_LockCore;
                buff_bullet[bid].shot = new StartPoint[] { new StartPoint() { location=new Vector3(-2.8f,5f,1)},
                    new StartPoint() { location=new Vector3(2.8f,5,1)} ,new StartPoint() {location=new Vector3(-2.8f,-5,1) },
                new StartPoint() { location=new Vector3(2.8f,-5f,1)} };
                int c = buff_enemy[id].bulletid[1];
                int d;
                for (int i = 4; i < 6; i++)
                {
                    d = buff_enemy[id].bulletid[i];
                    ReSetBullet2A(c, d);
                }
            }
            else
            {
                int eid = buff_enemy[id].bulletid[2];
                float a = (float)lucky.NextDouble();
                a *= 360;
                int bid = buff_enemy[id].bulletid[4];
                buff_bullet[bid].s_count = 4;
                Vector3 temp = buff_bullet[eid].bulletstate[0].location;
                buff_bullet[bid].shot = new StartPoint[4];
                buff_bullet[bid].shot[0].angle.z = a;
                buff_bullet[bid].shot[0].location = temp;
                a += 40;
                if (a > 360)
                    a -= 360;
                buff_bullet[bid].shot[1].angle.z = a;
                buff_bullet[bid].shot[1].location = temp;
                a += 40;
                if (a > 360)
                    a -= 360;
                temp = buff_bullet[eid].bulletstate[1].location;
                buff_bullet[bid].shot[2].angle.z = a;
                buff_bullet[bid].shot[2].location = temp;
                a += 40;
                if (a > 360)
                    a -= 360;
                buff_bullet[bid].shot[3].angle.z = a;
                buff_bullet[bid].shot[3].location = temp;
                a += 40;
                if (a > 360)
                    a -= 360;
                bid = buff_enemy[id].bulletid[5];
                buff_bullet[bid].s_count = 4;
                temp = buff_bullet[eid].bulletstate[2].location;
                buff_bullet[bid].shot = new StartPoint[4];
                buff_bullet[bid].shot[0].angle.z = a;
                buff_bullet[bid].shot[0].location = temp;
                a += 40;
                if (a > 360)
                    a -= 360;
                buff_bullet[bid].shot[1].angle.z = a;
                buff_bullet[bid].shot[1].location = temp;
                a += 40;
                if (a > 360)
                    a -= 360;
                temp = buff_bullet[eid].bulletstate[3].location;
                buff_bullet[bid].shot[2].angle.z = a;
                buff_bullet[bid].shot[2].location = temp;
                a += 40;
                if (a > 360)
                    a -= 360;
                buff_bullet[bid].shot[3].angle.z = a;
                buff_bullet[bid].shot[3].location = temp;
                a += 40;
                if (a > 360)
                    a -= 360;
            }
            boss_s_time1++;
            if (boss_s_time1 > 80)
            {
                boss_s_time1 = 0;
                boss_s_time2 = 0;
                buff_enemy[id].extra_b = -100;
                return true;
            }
            buff_enemy[id].extra_b = -5;
            return false;
        }
        static bool EastPorjectShot8(int id)//6-9
        {
            if (boss_s_time1 == 0)
            {
                int c = buff_enemy[id].bulletid[0];
                int d;
                for (int f= 6; f < 10; f++)
                {
                    d = buff_enemy[id].bulletid[f];
                    ReSetBullet2A(c, d);
                }
            }
            int bid= boss_s_time1%4+6;
            bid = buff_enemy[id].bulletid[bid];
            float x = (float)lucky.NextDouble();
            int i = (int)(x * 7);
            x = 2 - x * 4;
            buff_bullet[bid].spt_index = 40 + i;
            buff_bullet[bid].s_count = 36;
            buff_bullet[bid].shot = FixStartPoint.GetCircle36(new Vector3(x, (float)lucky.NextDouble() * 4, 1));
            boss_s_time1++;
            if (boss_s_time1 >= 12)
            {
                buff_enemy[id].extra_b = -100;
                boss_s_time1 = 0;
                return true;
            }
            buff_enemy[id].extra_b = -20;
            return false;
        }
        static bool EastProjectShot9(int id)//2,4-5
        {
            boss_m = true;
            if (boss_s_time1 == 0)
            {
                int bid = buff_enemy[id].bulletid[2];
                buff_bullet[bid].speed = 0.02f;
                buff_bullet[bid].s_count = 3;
                buff_bullet[bid].move = BulletMove.B_RotateWithParentA;
                buff_bullet[bid].shot = FixStartPoint.GetFixAngle3(buff_enemy[id].location);
            }
            else
            {
                int eid = buff_enemy[id].bulletid[2];
                float a = (float)lucky.NextDouble();
                a *= 360;
                int bid = buff_enemy[id].bulletid[4];
                buff_bullet[bid].s_count = 4;
                Vector3 temp = buff_bullet[eid].bulletstate[0].location;
                buff_bullet[bid].shot = new StartPoint[4];
                buff_bullet[bid].shot[0].angle.z = a;
                buff_bullet[bid].shot[0].location = temp;
                a += 60;
                if (a > 360)
                    a -= 360;
                buff_bullet[bid].shot[1].angle.z = a;
                buff_bullet[bid].shot[1].location = temp;
                a += 60;
                if (a > 360)
                    a -= 360;
                temp = buff_bullet[eid].bulletstate[1].location;
                buff_bullet[bid].shot[2].angle.z = a;
                buff_bullet[bid].shot[2].location = temp;
                a += 60;
                if (a > 360)
                    a -= 360;
                buff_bullet[bid].shot[3].angle.z = a;
                buff_bullet[bid].shot[3].location = temp;
                a += 60;
                if (a > 360)
                    a -= 360;
                bid = buff_enemy[id].bulletid[5];
                buff_bullet[bid].s_count = 2;
                temp = buff_bullet[eid].bulletstate[2].location;
                buff_bullet[bid].shot = new StartPoint[2];
                buff_bullet[bid].shot[0].angle.z = a;
                buff_bullet[bid].shot[0].location = temp;
                a += 60;
                if (a > 360)
                    a -= 360;
                buff_bullet[bid].shot[1].angle.z = a;
                buff_bullet[bid].shot[1].location = temp;
            }
            boss_s_time1++;
            if (boss_s_time1 == 60)
            {
                int bid = buff_enemy[id].bulletid[2];
                buff_bullet[bid].move = BulletMove.B_LockCore;
            }
            if (boss_s_time1 > 80)
            {
                boss_s_time1 = 0;
                boss_s_time2 = 0;
                buff_enemy[id].extra_b = -100;
                boss_m = false;
                return true;
            }
            buff_enemy[id].extra_b = -5;
            return false;
        }
        static bool EastProjectShot10(int id)//1,3-8
        {
            if (boss_s_time1 == 0)
            {
                int c = buff_enemy[id].bulletid[3];
                buff_bullet[c].move = BulletMove.B_Ripple;
                int d;
                for (int i = 6; i < 9; i++)
                {
                    d = buff_enemy[id].bulletid[i];
                    
                    ReSetBullet2A(c, d);
                }
            }
            boss_m = true;            
            int bid;
            Vector3 l = buff_enemy[id].location;
            l.x = -1.5f;
            bid = buff_enemy[id].bulletid[3];
            //buff_bullet[bid].spt_index = 9;
            buff_bullet[bid].move = BulletMove.B_Ripple;
            buff_bullet[bid].s_count = 7;
            buff_bullet[bid].shot = FixStartPoint.GetsevenAngleA(l);
            l.x = 1.5f;
            bid = buff_enemy[id].bulletid[6];
            //buff_bullet[bid].spt_index = 9;
            buff_bullet[bid].move = BulletMove.B_Ripple;
            buff_bullet[bid].s_count = 7;
            buff_bullet[bid].shot = FixStartPoint.GetsevenAngleA(l);
            l.x = 1f;
            l.y -= 0.5f;
            bid = buff_enemy[id].bulletid[7];
            //buff_bullet[bid].spt_index = 9;
            buff_bullet[bid].move = BulletMove.B_Ripple;
            buff_bullet[bid].s_count = 7;
            buff_bullet[bid].shot = FixStartPoint.GetsevenAngleA(l);
            l.x = -1f;
            bid = buff_enemy[id].bulletid[8];
            //buff_bullet[bid].spt_index = 9;
            buff_bullet[bid].move = BulletMove.B_Ripple;
            buff_bullet[bid].s_count = 7;
            buff_bullet[bid].shot = FixStartPoint.GetsevenAngleA(l);
            bid = buff_enemy[id].bulletid[1];
            if(boss_s_time1%3==0)
            {
                if (boss_s_time1 % 6 == 0)
                   bid = buff_enemy[id].bulletid[4]; 
                else bid = buff_enemy[id].bulletid[5];
                buff_bullet[bid].s_count = 14;
                buff_bullet[bid].shot = FixStartPoint.GetArcDown14(buff_enemy[id].location);
            }
            boss_s_time1++;
            if (boss_s_time1 >= 30)
            {
                buff_enemy[id].extra_b = -150;
                boss_s_time1 = 0;
                boss_m = false;
                return true;
            }
            buff_enemy[id].extra_b = -5;
            return false;
        }
        
        static Shot[] eastprojectshot = new Shot[] { EastPorjectShot1, EastPorjectShot2, EastPorjectShot3,
        EastPorjectShot4,EastPorjectShot5,EastPorjectShot6,EastPorjectShot7,EastPorjectShot8,EastProjectShot9,
            EastProjectShot10 };

        internal static bool M_Boss_Eastproject(int id)
        {
            //if (buff_enemy[id].animation.img_path != null)
            //{
            //    buff_enemy[id].animation.location = buff_enemy[id].location;
            //    CalculAnimation(ref buff_enemy[id].animation, 0);
            //}
            if (buff_enemy[id].extra_m < 100)
            {
                if (buff_enemy[id].play!=null)
                    buff_enemy[id].play(id);
                buff_enemy[id].extra_m++;
                buff_enemy[id].location.y -= 0.03f;
                return true;
            }
            if (buff_enemy[id].extra_b >= 0)
            {
                if (eastprojectshot[buff_enemy[id].extra_a](id))
                {
                    if (buff_enemy[id].extra_a < 9)
                    {
                        buff_enemy[id].extra_a++;
                    }
                    else
                        buff_enemy[id].extra_a = 0;
                }
            }
            buff_enemy[id].extra_b++;
            if(boss_m)
            {
                if (buff_enemy[id].extra_m == 100)
                {
                    buff_enemy[id].extra_m++;
                    boss_v.x = -0.01f - (float)lucky.NextDouble() / 50;
                    boss_v.y = 0.01f + (float)lucky.NextDouble() / 50;
                }
                if (buff_enemy[id].location.x > 0.8f)
                {
                    boss_v.x = -0.01f - (float)lucky.NextDouble() / 50;
                }
                if (buff_enemy[id].location.x < -0.8f)
                {
                    boss_v.x = 0.01f + (float)lucky.NextDouble() / 50;
                }
                if (buff_enemy[id].location.y > 4f)
                {
                    boss_v.y = -0.01f - (float)lucky.NextDouble() / 50;
                    if (boss_v.x == 0)
                        boss_v.x = -0.04f + (float)lucky.NextDouble() / 12.5f;
                }
                if (buff_enemy[id].location.y < 2.6f)
                {
                    boss_v.y = 0.01f + (float)lucky.NextDouble() / 50;
                    if (boss_v.x == 0)
                        boss_v.x = -0.04f + (float)lucky.NextDouble() / 12.5f;
                }
                buff_enemy[id].location += boss_v;
            }
            return true;
        }
        #endregion

    }
}
