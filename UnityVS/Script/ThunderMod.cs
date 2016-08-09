#define Debug
#undef Debug
#define desktop
using System;
using UnityEngine;
using System.Collections.Generic;

namespace Assets.UnityVS.Script
{
    class ThunderMod:GameControl
    {
        #region data source
        static string[] change_bs = new string[] { "更换武器", "change bullet" };
        static string[] change_wing = new string[] { "更换僚机","change wing"};
        static EdgePropertyEx change_weapon = new EdgePropertyEx()
        {
            imagebase = new ImageProperty()
            {
                imagepath = "Picture/six_angle",
                scale = new Vector3(1, 1),
                sorting = 4,
                location = new Vector3(2.3f, -2.6f, 1)
            },
            text = new TextProperty()
            {
                color = Color.red,
                scale = new Vector2(0.1f, 0.1f)
            },
            property = new EdgeButtonBase()
            {
                points = six_angle_button
            }
        };
        static CirclePropertyEx skill_button = new CirclePropertyEx()
        {
            imagebase = new ImageProperty()
            {
                location = new Vector3(-2.15f, -2.6f, 1),
                imagepath = "Picture/magic_001",
                scale = new Vector3(1, 1),
                sorting = 4
            },
            property = new CircleButtonBase() { click = Skill_Click, r = 0.5f },
            text = new TextProperty() { text = "W", scale = new Vector3(0.1f, 0.1f) }
        };

        static ImageProperty big_boom_f = new ImageProperty()
        {
            sorting = 7,
            scale = origion_scale,
            location = new Vector3(-2.15f, -3.6f, 1),
            imagepath = "Picture/Center"
        };
        static ImageProperty big_boom_b = new ImageProperty()
        {
            sorting = 6,
            scale = origion_scale,
            location = new Vector3(-2.15f, -3.6f, 1),
            imagepath = "Picture/burn"
        };
        #endregion

        static CircleButtonBase big_boom_button = new CircleButtonBase() { click = BigBoom, r = 0.5f };
        static int  hpid, hpid2, spid, spid2, epid, epid2;

        #region sence control
        static bool Collision(int id, ref BulletState state)
        {
            float x, y, d;
            float z = state.angle.z;
            if (buff_bullet[id].penetrate == false)
            {
                if (big_boom.active)
                {
                    x = big_boom.location.x - state.location.x;
                    y = big_boom.location.y - state.location.y;
                    d = x * x + y * y;
                    float d1 = big_boom.r * big_boom.r + buff_bullet[id].minrange +
                        2 * big_boom.r * Mathf.Sqrt(buff_bullet[id].minrange);
                    if (d < d1)
                    {
                        state.active = false;
                        return true;
                    }
                }
                if (current.skill.active & current.skill.force_clear)
                {
                    x = current.skill.location.x - state.location.x;
                    y = current.skill.location.y - state.location.y;
                    d = x * x + y * y;
                    float d1 = current.skill.r * current.skill.r + buff_bullet[id].minrange +
                        2 * current.skill.r * Mathf.Sqrt(buff_bullet[id].minrange);
                    if (d < d1)
                    {
                        int h = (int)(buff_bullet[id].attack / 5);
                        current.skill.during -= h;
                        state.active = false;
                        return true;
                    }
                }
            }
            x = core_location.x - state.location.x;
            y = core_location.y - state.location.y;
            d = x * x + y * y;
            if (d < buff_bullet[id].minrange)
            {
                damageA(id);
                if (buff_bullet[id].penetrate == false)
                    state.active = false;
                return true;
            }
            bool collid = false;
            if (buff_bullet[id].radius > 0)
            {
                if (area & d < 1)
                {
                    collid = CircleToPolygon(new Point2(state.location.x, state.location.y)
                       , buff_bullet[id].radius, areapoints);
                }
            }
            else
            {
                if (area & d < 1)
                {
                    collid = PToP2(areapoints, RotatePoint2(ref buff_bullet[id].edgepoints, new Point2(state.location.x, state.location.y), z));
                }
                else
                if (d < buff_bullet[id].maxrange)
                {
                    collid = CircleToPolygon(xy, 0.09f, RotatePoint2(ref buff_bullet[id].edgepoints,
                        new Point2(state.location.x, state.location.y), z));
                }
            }
            if (collid)
            {
                damageA(id);
                if (buff_bullet[id].penetrate == false)
                    state.active = false;
                return true;
            }
            return false;
        }
        public static void PreLoad()
        {           
            RegSpriteA(big_boom_f.imagepath, ref big_boom_f.spt_id);
            RegSpriteA(big_boom_b.imagepath, ref big_boom_b.spt_id);
            bigboom_effcetid = RegEffet(DataSource.eff_explode_big);
            current.skill.effcetid = RegEffet(current.skill.eff_b);
        }
        public static void UnPreLoad()
        {
            DeleteEffect(big_boom.effcetid);
            DeleteEffect(current.skill.effcetid);
            ClearSpriteS(128, 256);
            Resources.UnloadUnusedAssets();
        }
        static void FixedUpdate()
        {
            if (gameover)
            {
                if (current.warplane.currentblood > 0)
                    GameOver(true);
                else
                    GameOver(false);
                MainCamera.update = null;
                return;
            }
            AsyncManage.AsyncDelegate(CaculateEnemy);
            AsyncManage.AsyncDelegate(CalculateEffect);
            UpdateImage2();
            UpdataBackground();
            UpdateProp2A();
            UpdateWingLocation();
            Bomb_Update2A();
            UpdateEnemy2A();
            UpdateBulletA();
            AsyncManage.AsyncDelegate(ThreadB);
            AsyncManage.AsyncDelegate(ThreadA);
            Update_Effect();
#if desktop
            if (Input.GetKeyDown(KeyCode.Space))
                BigBoom();
            if (Input.GetKeyDown(KeyCode.W))
                Skill_Click();
            if (Input.GetKeyDown(KeyCode.A))
                ChangeSecondWeapon();
            if (Input.GetKeyDown(KeyCode.D))
                ChangeWing();
#endif
        }
        public static void GameStart()
        {
            // AsyncManage.Inital();//pc
            // AsyncManage.AsyncDelegate(LoadEnemyPicture);//pc
            // LoadEnemyPicture();//pc
            if (buff_wave.bk_groud == null)
            {
                MainCamera.update = GameStart;
                return;
            }
            LoadBackGround(ref buff_wave.bk_groud[0]);
            LoadEnemyResource();
            update = FixedUpdate;
            D_gameover = GameOverA;
            current.level = 0;
            current.warplane.currentblood = current.warplane.blood;
            currentwave = 0;
            img_id2 = 0;
            LoadWarPlane();
            loadgamebutton();
            Prop_Inital();
            propcollid = Prop_Collider;
            Bomb_Initial();           
            gameover = false;
            mouse_move = Plane_Move;
            plane_move = true;
            destory_enemy = 0;
            damageA = DamageCalculateA;
            damageB = DamageCalculateB;
            damageC = DamageCalculateC;
            generateA = GenerateProp;
            MainCamera.update = FixedUpdate;
        }
        static void GameOverA(bool pass)
        {
            gameover = true;
            plane_move = false;
            mouse_move = null;
            AsyncManage.AsyncDelegate(() => {               
                Bomb_Clear();
                Prop_Destory();
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
            ClearWarPlane();
            ClearBulletAll();
            unloadgamebutton();
            CanvasControl.GameOverCallBack(pass, destory_enemy);
            Effect_Stop_Force(big_boom.effcetid);
            DeleteEffect(big_boom.effcetid);
            Effect_Stop_Force(current.skill.effcetid);
            DeleteEffect(current.skill.effcetid);
            ClearSpriteS(128, 256);
            Resources.UnloadUnusedAssets();
        }
        static void ThreadA()
        {
            Calcul_Bullet2(0, 26,2,Collision);
            Calcul_Bullet2(1, 26, 2, Collision);
            Prop_Move();
        }
        static void ThreadB()
        {
            WingMove();
            WarPlaneShot();
            CalculateSkill();
            Calcul_Bullet2(26,38,1,CollisionCheck1);
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
        #endregion

        #region plane control
        static int level4_during = 0;
        static bool bs_back, w_back;
        static void freshgrade()
        {
            int index;
            if (!current.warplane.specital)
            {
                index = current.warplane.bulletid;
                float at = current.warplane.bullet.attack;
                at *= 5 + current.level;
                at /= 5;
                buff_bullet[index].attack = at;
            }
            if (!current.B_second.specital)
            {
                index = current.B_second.bulletid;
                float at = current.B_second.bullet.attack;
                at *= 5 + current.level;
                at /= 5;
                buff_bullet[index].attack = at;
            }
            if (!current.wing.specital)
            {
                index = current.wing.bulletid;
                float at = current.wing.bullet.attack;
                at *= 5 + current.level;
                at /= 5;
                buff_bullet[index].attack = at;
            }
            if (!current.bs_back.specital)
            {
                index = current.bs_back.bulletid;
                float at = current.bs_back.bullet.attack;
                at *= 5 + current.level;
                at /= 5;
                buff_bullet[index].attack = at;
            }
            if (!current.w_back.specital)
            {
                index = current.w_back.bulletid;
                float at = current.w_back.bullet.attack;
                at *= 5 + current.level;
                at /= 5;
                buff_bullet[index].attack = at;
            }
        }
        static void UpGrade()
        {
            if (current.level < 3)
            {
                current.level++;
                if (current.level == 3)
                    level4_during = 200;
                freshgrade();
            }
            else level4_during = 200;
        }
        static void DeGrade()
        {
            if (current.level > 0)
            {
                current.level--;
                //for (int i = 21; i < 31; i++)
                //{
                //    if (buff_bullet[i].attack > 0)
                //        buff_bullet[i].attack /= 1.1f;
                //    else break;
                //}
                freshgrade();
            }
        }
        static void GenerateProp(int max, Vector3 location)
        {
            if (propsum > 7)
                return;
            int i = lucky.Next(0, max);
            if (i < 2)//upgard
            {
                generate = true;
                build_type = 0;
                build_position = location;
                return;
            }
            else
            if (i < 3)//blood
            {
                generate = true;
                build_type = 1;
                build_position = location;
                return;
            }
        }
        static void WarPlaneShot()
        {
            if (mousedown)
            {
                if (current.level > 3)
                    current.level = 3;
                if (current.level < 0)
                    current.level = 0;
                if (current.warplane.extra_b > current.warplane.shotfrequency)
                {
                    current.warplane.extra_b = 0;
                    if (current.warplane.specital)
                        current.warplane.sp_bullet.shot();
                    else current.warplane.shot[current.level](current.warplane.bulletid, 0);
                }
                current.warplane.extra_b++;
                if(bs_back)
                {
                    if (current.bs_back.extra_b > current.bs_back.shotfrequency)
                    {
                        current.bs_back.extra_b = 0;
                        if (current.bs_back.specital)
                            current.bs_back.sp_bullet.shot();
                        else
                            current.bs_back.shot[current.level](current.bs_back.bulletid, 0);
                    }
                    current.bs_back.extra_b++;
                }
                else
                {
                    if (current.B_second.extra_b > current.B_second.shotfrequency)
                    {
                        current.B_second.extra_b = 0;
                        if (current.B_second.specital)
                            current.B_second.sp_bullet.shot();
                        else
                            current.B_second.shot[current.level](current.B_second.bulletid, 0);
                    }
                    current.B_second.extra_b++;
                }
                if (w_back)
                {
                    if (current.w_back.extra_b > current.w_back.shotfrequency)
                    {
                        current.w_back.extra_b = 0;
                        if (current.w_back.specital)
                            current.w_back.sp_bullet.shot();
                        else
                            current.w_back.shot[current.level](current.w_back.bulletid, 0);
                    }
                    current.w_back.extra_b++;
                }
                else
                {
                    if (current.wing.extra_b > current.wing.shotfrequency)
                    {
                        current.wing.extra_b = 0;
                        if (current.wing.specital)
                            current.wing.sp_bullet.shot();
                        else
                            current.wing.shot[current.level](current.wing.bulletid, 0);
                    }
                    current.wing.extra_b++;
                }
            }
            else
            {
                current.shiled.current += current.shiled.resume*3;
                if (current.shiled.current > current.shiled.max)
                {
                    current.shiled.current = current.shiled.max;
                    current.skill.energy_c += current.skill.energy_re;
                    if (current.skill.energy_c > current.skill.energy_max)
                        current.skill.energy_c = current.skill.energy_max;
                }
            }
            if (current.warplane.specital & current.warplane.sp_bullet.move != null)
                current.warplane.sp_bullet.move();
            if (current.B_second.specital & current.B_second.sp_bullet.move != null)
                current.B_second.sp_bullet.move();
            if (current.wing.specital & current.wing.sp_bullet.move != null)
                current.wing.sp_bullet.move();
            if (current.bs_back.specital & current.bs_back.sp_bullet.move != null)
                current.bs_back.sp_bullet.move();
            if (current.w_back.specital & current.w_back.sp_bullet.move != null)
                current.w_back.sp_bullet.move();
        }
        static void LoadWarPlane()
        {
            hpid = CreateImage(def_transform, blood_back);
            hpid2 = CreateImage(buff_img[hpid].transform, blood_g);
            buff_img[hpid2].spriterender.material = Mat_Blood;
            buff_img[hpid2].spriterender.material.SetFloat("_Progress", 1);

            blood_back.location.y -= 0.16f;
            spid = CreateImage(def_transform, blood_back);
            spid2 = CreateImage(buff_img[spid].transform, blood_b);
            buff_img[spid2].spriterender.material = Mat_Blood;
            buff_img[spid2].spriterender.material.SetFloat("_Progress", 0);

            blood_back.location.y -= 0.16f;
            epid = CreateImage(def_transform, blood_back);
            epid2 = CreateImage(buff_img[epid].transform, blood_y);
            buff_img[epid2].spriterender.material = Mat_Blood;
            buff_img[epid2].spriterender.material.SetFloat("_Progress", 0);
            blood_back.location.y += 0.32f;

            coreid = CreateImage(def_transform, core);

            current.warplane.imageid = CreateImageA(buff_img[coreid].transform, current.warplane.image, current.warplane.spt_id);
            if (current.warplane.specital)
                current.warplane.sp_bullet.inital(current.warplane.bullet);
            else
                current.warplane.bulletid = RegBullet2(ref current.warplane.bullet,26);
            if (current.B_second.specital)
                current.B_second.sp_bullet.inital(current.B_second.bullet);
            else
                current.B_second.bulletid = RegBullet2(ref current.B_second.bullet,26);
            if (current.wing.specital)
                current.wing.sp_bullet.inital(current.wing.bullet);
            else
                current.wing.bulletid = RegBullet2(ref current.wing.bullet,26);
            if (current.bs_back.specital)
                current.bs_back.sp_bullet.inital(current.bs_back.bullet);
            else current.bs_back.bulletid = RegBullet2(ref current.bs_back.bullet,26);
            if (current.w_back.specital)
                current.w_back.sp_bullet.inital(current.w_back.bullet);
            else current.w_back.bulletid = RegBullet2(ref current.w_back.bullet,26);
            core_location = origion_location;

            current.wing.imageid = CreateImageA(def_transform, current.wing.image, current.wing.spt_id);
            current.wing.imageid2 = CreateImageA(def_transform, current.wing.image, current.wing.spt_id);

            current.wing.transform = buff_img[current.wing.imageid].transform;
            current.wing.transform2 = buff_img[current.wing.imageid2].transform;
            current.wing.location2 = current.wing.location = origion_location;

        }
        static void ClearWarPlane()
        {
            ClearImage(coreid);
            ClearImage(current.warplane.imageid);
            ClearImage(hpid);
            ClearImage(hpid2);
            ClearImage(epid);
            ClearImage(epid2);
            ClearImage(spid);
            ClearImage(spid2);
            ClearImage(current.wing.imageid);
            ClearImage(current.wing.imageid2);
            if (current.warplane.specital)
                current.warplane.sp_bullet.dispose();
            if (current.B_second.specital)
                current.B_second.sp_bullet.dispose();
            if (current.wing.specital)
                current.wing.sp_bullet.dispose();
            if (current.bs_back.specital)
                current.bs_back.sp_bullet.dispose();
            if (current.w_back.specital)
                current.w_back.sp_bullet.dispose();
        }
        static void UpdateWingLocation()//and blood
        {
            if (current.warplane.specital)
                current.warplane.sp_bullet.updatemian();
            if(bs_back)
            {
                if (current.bs_back.specital)
                    current.bs_back.sp_bullet.updatemian();
            }else if (current.B_second.specital)
                current.B_second.sp_bullet.updatemian();
            if(w_back)
            {
                if (current.w_back.specital)
                    current.w_back.sp_bullet.updatemian();
            }else
            if (current.wing.specital)
                current.wing.sp_bullet.updatemian();
            current.wing.transform.localPosition = current.wing.location;
            current.wing.transform2.localPosition = current.wing.location2;
            buff_img[hpid2].spriterender.material.SetFloat("_Progress", current.warplane.currentblood / current.warplane.blood);
            buff_img[spid2].spriterender.material.SetFloat("_Progress", current.shiled.current / current.shiled.max);
            buff_img[epid2].spriterender.material.SetFloat("_Progress", current.skill.energy_c / current.skill.energy_max);
            buff_img[buff_button[4]].spriterender.material.SetFloat("_Progress", Big_boomaready / 1000);
        }
        static void WingMove()//x 0.8-1.2 y-0.3
        {
            if (Big_boomaready > 0)
                Big_boomaready--;
            float x = core_location.x - current.wing.location.x;
            float y = core_location.y - current.wing.location.y;
            if (x < 0.98f | x > 1.02f)
            {
                if (x > 1.3f)
                    current.wing.location.x = core_location.x - 1.3f;
                else
                if (x < 0.7f)
                    current.wing.location.x = core_location.x - 0.7f;
                else if (x > 1)
                    current.wing.location.x += 0.02f;
                else
                    current.wing.location.x -= 0.02f;
                current.wing.location2.x = current.wing.location.x + 2;
            }
            if (y > 0.32f | y < 0.28f)
            {
                if (y > 0.5f)
                    current.wing.location.y = core_location.y - 0.5f;
                else
                if (y < 0f)
                    current.wing.location.y = core_location.y;
                else if (y > 0.3f)
                    current.wing.location.y += 0.02f;
                else
                    current.wing.location.y -= 0.02f;
                current.wing.location2.y = current.wing.location.y;
            }
        }
        static void CalculateSkill()
        {
            if (big_boom.active)
            {
                big_boom.during--;
                if (big_boom.during < 0)
                    big_boom.active = false;
            }
            if (current.skill.active)
            {
                //current.skill.during--;
                //if (current.skill.during < 0)
                //    current.skill.active = false;
                current.skill.energy_c -= current.skill.energy_ec;
                if (current.skill.energy_c < 0)
                {
                    current.skill.active = false;
                    Effect_Stop(current.skill.effcetid);
                }
                else if (current.skill.follow)
                {
                    current.skill.location = core_location;
                    buff_effect[current.skill.effcetid].location = core_location;
                }
            }
            if (shiled_resume == 0)
            {
                current.shiled.current += current.shiled.resume;
                if (current.shiled.current > current.shiled.max)
                {
                    current.shiled.current = current.shiled.max;
                    current.skill.energy_c += current.skill.energy_re;
                    if (current.skill.energy_c > current.skill.energy_max)
                        current.skill.energy_c = current.skill.energy_max;
                }
            }
            else shiled_resume--;
            if (current.level == 3)
            {
                level4_during--;
                if (level4_during <= 0)
                    DeGrade();
            }
        }

        static int shiled_resume;
        static void Prop_Collider(int style)
        {
            if (style == 0)
                UpGrade();
            else
            if (style == 1)
            {
                current.warplane.currentblood += current.warplane.blood * 0.1f;
                if (current.warplane.currentblood > current.warplane.blood)
                    current.warplane.currentblood = current.warplane.blood;
            }
        }
        #endregion

        #region demage
        static void DamageCalculateA(int bulletid)
        {
            float harm = 0;
            if (current.shiled.current > 10)
            {
                harm = buff_bullet[bulletid].attack - current.shiled.defence;
                if (harm > 0)
                {
                    current.shiled.current -= harm;
                    if (current.shiled.current < 0)
                    {
                        current.warplane.currentblood += current.shiled.current;
                        current.shiled.current = 0;
                    }
                    shiled_resume = 100;
                }
            }
            else
            {
                current.warplane.currentblood -= buff_bullet[bulletid].attack;
                DeGrade();
            }
            if (current.warplane.currentblood <= 0)
                gameover = true;
        }
        static void DamageCalculateB(int bulletid, int enemyid)
        {
            if (buff_enemy[enemyid].currentblood < 0)
                return;
            float harm = buff_bullet[bulletid].attack - buff_enemy[enemyid].defance;
            if (harm > 0)
            {
                buff_enemy[enemyid].currentblood -= harm;
                buff_enemy[enemyid].bloodpercent = buff_enemy[enemyid].currentblood / buff_enemy[enemyid].blood;
                if (buff_enemy[enemyid].currentblood < 0)
                {
                    buff_enemy[enemyid].die = true;
                    destory_enemy++;
                    if (bomb_count < 15)
                    {
                        bomb_queue[bomb_count] = buff_enemy[enemyid].location;
                        bomb_count++;
                    }
                    generateA(10 + currentwave, buff_enemy[enemyid].location);
                }
            }
        }
        static void DamageCalculateC(int enemyid)
        {
            current.warplane.currentblood -= 100;
            DeGrade();
            if (current.warplane.currentblood <= 0)
                gameover = true;
        }
        #endregion

        #region gamerun button
        static float Big_boomaready;
        static int[] buff_button = new int[7];
        private static int bigboom_effcetid;
        static void loadgamebutton()
        {
            buff_button[0] = CreateCircleButton(def_transform, skill_button);
            change_weapon.imagebase.location.y = -2.6f;
            change_weapon.property.click = ChangeSecondWeapon;
            change_weapon.text.text = change_bs[CanvasControl.Language];
            buff_button[1] = CreateEdgeButton(def_transform, change_weapon);
            change_weapon.imagebase.location.y = -3.6f;
            change_weapon.property.click = ChangeWing;
            change_weapon.text.text = change_wing[CanvasControl.Language];
            buff_button[2] = CreateEdgeButton(def_transform, change_weapon);
            buff_button[3] = CreateImage(def_transform, big_boom_b);
            buff_button[4] = CreateImage(def_transform, big_boom_f);
            buff_img[buff_button[4]].spriterender.material = Mat_fan;
            big_boom_button.transform = buff_img[buff_button[4]].transform;
            buff_button[5] = RegCircleButton(big_boom_button);
        }
        static void unloadgamebutton()
        {
            DeleteCircleButton(buff_button[0]);
            DeleteEdgeButton(buff_button[1]);
            DeleteEdgeButton(buff_button[2]);
            ClearImage(buff_button[3]);
            ClearImage(buff_button[4]);
            UnRegCircleButton(buff_button[5]);
        }
        static void Skill_Click()
        {
            if (current.skill.active == false & current.skill.energy_c / current.skill.energy_max > 0.2f)
            {
                current.skill.active = true;
                current.skill.energy_c -= current.skill.energy_max * 0.2f;
                Effect_Active(current.skill.effcetid);
            }
        }
        static void BigBoom()//核弹攻击在此处改
        {
            if (Big_boomaready <= 0)
            {
                Effect_Active(bigboom_effcetid);
                big_boom.active = true;
                big_boom.attack = 1;
                big_boom.during = 150;
                Big_boomaready = 1000;
            }
        }
        static void ChangeSecondWeapon()
        {
            if(bs_back)
            {
                bs_back = false;
            }
            else
            {
                bs_back = true;
            }
        }
        static void ChangeWing()
        {
            if(w_back)
            {
                w_back = false;
                int id = current.wing.imageid;
                int index = current.wing.spt_id;
                buff_img[id].spriterender.sprite = buff_spriteex[index].sprites[0];
                buff_img[id].transform.localEulerAngles = Vector3.zero;
                id = current.wing.imageid2;
                buff_img[id].spriterender.sprite = buff_spriteex[index].sprites[0];
                buff_img[id].transform.localEulerAngles = Vector3.zero;
            }
            else
            {
                w_back = true;
                int id = current.wing.imageid;
                int index = current.w_back.spt_id;
                if (buff_spriteex[index].sprites == null)
                    CreateSpriteGA(index,new Grid(1,1));
                buff_img[id].spriterender.sprite = buff_spriteex[index].sprites[0];
                buff_img[id].transform.localEulerAngles = Vector3.zero;
                id = current.wing.imageid2;
                buff_img[id].spriterender.sprite = buff_spriteex[index].sprites[0];
                buff_img[id].transform.localEulerAngles = Vector3.zero;
            }
        }
        #endregion

        #region bomb
        static AnimatBase buff_bomb = new AnimatBase();
        static int bomb_count;
        static Vector3[] bomb_queue = new Vector3[16];
        static void Bomb_Initial()
        {
            buff_bomb.sptex_id = CreateSpriteG(DataSource.explode_fire.path, DataSource.explode_fire.grid);
            buff_bomb.son = new AnimatObject[16];
        }
        static void Bomb_Update2(int img_id,int extra)
        {
            if (buff_bomb.son[extra].active)
            {
                if (buff_bomb.son[extra].extra % 3 == 0)
                {
                    buff_img2[img_id].spriterender.sprite =
                        buff_spriteex[buff_bomb.sptex_id].sprites[buff_bomb.son[extra].extra / 3];
                }
                buff_bomb.son[extra].extra++;
                if (buff_bomb.son[extra].extra > 45)
                {
                    buff_bomb.son[extra].extra = 0;
                    buff_bomb.son[extra].active = false;
                    buff_img2[img_id].gameobject.SetActive(false);
                    buff_bomb.act_count--;
                }
            }
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
                    buff_img2[imgid].gameobject.SetActive(true);
                    buff_img2[imgid].transform.localPosition = bomb_queue[i];
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
        static void Bomb_Clear()
        {
            for (int i = 0; i < 10; i++)
            {
                buff_bomb.son[i].created = false;
            }
        }
        #endregion

        #region laser cut
        static LaserProject lp;
        static int laser_id;
        static BulletPropertyEX b_laser;
        public static void Laser_Initial(BulletPropertyEX bullet)
        {
            b_laser = bullet;
            lp = new LaserProject(0.01f);
            Material mat = Resources.Load("Shader/SliceStream") as Material;
            laser_id = CreateImageNull(def_transform);
            int spt_id =// b_laser.spt_id;
            CreateSpriteS(b_laser.image.imagepath,b_laser.image.sprite);
            SpriteRenderer sp= buff_img[laser_id].spriterender;
            sp.sprite = buff_spriteex[spt_id].sprites[0];
            sp.sortingOrder = 4;
            sp.material = mat;
            sp.material.SetFloat("_Speed", 40);
            sp.material.SetFloat("_Minx", 0.24f);
            sp.material.SetFloat("_Maxx", 0.41f);
            sp.material.SetFloat("_Miny", 0.06f);
            sp.material.SetFloat("_Maxy", 0.86f);
            buff_img[laser_id].transform.localPosition = new Vector3(0f, 0f,1);
            buff_img[laser_id].transform.localScale = new Vector3(1.5f, 2.85f);
        }
        public static void Laser_Calculate()
        {
            if (!mousedown)
                return;
            if (lp.GetState != 0)
                return;
            float distance = b_laser.maxrange;
            Point2 location = new Point2(core_location.x,core_location.y);
            lp.SetLaser(b_laser.edgepoints,location);
            for(int i=0;i<20;i++)
            {
                if(buff_enemy[i].move!=null)
                {
                    if(buff_enemy[i].boss & buff_enemy[i].offset != null)
                    {
                        lp.ProjectRect(ref buff_enemy[i].offset, i);
                    }
                    else
                    {
                        float d = location.x - buff_enemy[i].location.x;
                        if (d < 0)
                            d = -d;
                        if(d<distance & buff_enemy[i].offset!=null)
                        {
                            lp.ProjectRect(ref buff_enemy[i].offset,i);
                        }
                    }
                }
            }
            int count = 0;
            Point2T[] buff;
            count= lp.Complete(out buff);
            float len = b_laser.edgepoints[2].x - b_laser.edgepoints[0].x;
            float attck = b_laser.attack;
            attck *= 5 + current.level;
            attck /= 5;
            for (int i=0;i<count;i++)
            {
                int tag = buff[i].tag;
                if (buff_enemy[tag].currentblood > 0)
                {
                    float at = buff[i].x / len * attck;
                    at -= buff_enemy[tag].defance;
                    if (at > 0)
                    {
                        buff_enemy[tag].currentblood -= at;
                        buff_enemy[tag].bloodpercent = buff_enemy[tag].currentblood / buff_enemy[tag].blood;
                        if (buff_enemy[tag].currentblood < 0)
                        {
                            buff_enemy[tag].die = true;
                            destory_enemy++;
                            if (bomb_count < 15)
                            {
                                bomb_queue[bomb_count] = buff_enemy[tag].location;
                                bomb_count++;
                            }
                            generateA(10 + currentwave, buff_enemy[tag].location);
                        }
                    }
                } 
            }
        }
        public static void Laser_Update()
        {
            if (mousedown)
            {
                buff_img[laser_id].gameobject.SetActive(true);
                Vector3 location = core_location;
                //location.y += 0.1f;
                buff_img[laser_id].transform.localPosition = location;
                buff_img[laser_id].spriterender.material.SetTexture("_SliceTex", lp.GetTexture());
            }
            else
            {
                buff_img[laser_id].gameobject.SetActive(false);
            }
        }
        public static void Laser_Dispose()
        {
            lp.Dispose();
            ClearImage(laser_id);
        }
        #endregion

        #region chain spore
        static int[] chain_id;
        static float chain_at;
        public static void Chain_Shot()
        {
            int c = 0;
            int id = chain_id[0];
            for(int i=0;i<10;i++)
            {
                if (!buff_bullet[id].bulletstate[i].active)
                {
                    buff_bullet[id].bulletstate[i].location = core_location;
                    buff_bullet[id].bulletstate[i].active = true;
                    buff_bullet[id].bulletstate[i].extra = 0;
                    if (c == 0)
                    {
                        c++;
                        buff_bullet[id].bulletstate[i].location.x -= 0.1f;
                        buff_bullet[id].bulletstate[i].angle.z = 60;
                    }
                    else
                    {
                        buff_bullet[id].bulletstate[i].location.x += 0.1f;
                        buff_bullet[id].bulletstate[i].angle.z = 300;
                        buff_bullet[id].s_count = i++;
                        break;
                    }
                }
            }
        }
        static void Chain_Multi(int bulletid, int enemyid)
        {
            int index = 0;
            float x = buff_enemy[enemyid].location.x;
            float y = buff_enemy[enemyid].location.y;
            for(int i=0;i<20;i++)
            {
                if (buff_enemy[i].move != null)
                    if (i != enemyid)
                    {
                        float x1 = buff_enemy[i].location.x - x;
                        float y1 = buff_enemy[i].location.y - y;
                        float d = x1 * x1 + y1 * y1;
                        if (d < 10)
                        {
                            for (int c = index; c < 128; c++)
                            {
                                unsafe
                                {
                                    fixed (BulletState* bs=&buff_bullet[bulletid].bulletstate[c])
                                    {
                                        if (!bs->active)
                                        {
                                            bs->active = true;
                                            float z = Aim(ref buff_enemy[enemyid].location, ref buff_enemy[i].location);
                                            bs->location = buff_enemy[enemyid].location;
                                            bs->angle.z = z;
                                            bs->movexyz.x = angle_table[(int)z].x * buff_bullet[bulletid].speed;
                                            bs->movexyz.y = angle_table[(int)z].y * buff_bullet[bulletid].speed;
                                            bs->extra = 25;
                                            bs->extra2 = i;
                                            index = c;
                                            break;
                                        }
                                    }
                                } 
                            }
                        }
                    }
            }
            buff_bullet[bulletid].s_count = index;
        }
        public static void Chain_Mov()
        {
            float at = chain_at;
            at *= 5 + current.level;
            at /= 5;
            for (int c=0;c<3;c++)
            {
                int id = chain_id[c];
                buff_bullet[id].attack = at;
                for(int i=0;i<buff_bullet[id].max;i++)
                {
                    if(buff_bullet[id].bulletstate[i].active)
                    {
                        float x = buff_bullet[id].bulletstate[i].location.x;
                        float y = buff_bullet[id].bulletstate[i].location.y;
                        if (y > 5.5f | y < -5.5f | x > 3f | x < -3f)
                        {
                            buff_bullet[id].bulletstate[i].active = false;
                            buff_bullet[id].bulletstate[i].extra = 0;
                            buff_bullet[id].bulletstate[i].extra2 = -1;
                            goto label1;
                        }
                        if (buff_bullet[id].bulletstate[i].extra == 0)
                        {
                           int u=  buff_bullet[id].bulletstate[i].extra2 = BulletMove.LockEnemy(buff_bullet[id].bulletstate[i].location);
                            if(u<0)
                            {
                                int z =(int) buff_bullet[id].bulletstate[i].angle.z;
                                buff_bullet[id].bulletstate[i].movexyz.x = angle_table[z].x * buff_bullet[id].speed;
                                buff_bullet[id].bulletstate[i].movexyz.y = angle_table[z].y * buff_bullet[id].speed;
                            }
                        }
                        int index = buff_bullet[id].bulletstate[i].extra2;
                        if (index >= 0)
                        {
                            if (buff_enemy[index].die)
                            {
                                buff_bullet[id].bulletstate[i].extra2 = -1;
                                goto label2;
                            }
                            x -= buff_enemy[index].location.x;
                            y -= buff_enemy[index].location.y;
                            if(x*x+y*y<0.3f)
                            {
                                if (c < 2)
                                    Chain_Multi(chain_id[c+1],index);
                                buff_bullet[id].bulletstate[i].active = false;
                                buff_bullet[id].bulletstate[i].extra = 0;
                                buff_bullet[id].bulletstate[i].extra2 = -1;
                                goto label1;
                            }
                            float a = Aim(ref buff_bullet[id].bulletstate[i].location, ref buff_enemy[index].location);
                            float z = buff_bullet[id].bulletstate[i].angle.z;
                            float change = 5;
                            if (buff_bullet[id].bulletstate[i].extra > 25)
                                change = 10;
                            if (a > z)//顺时针
                            {
                                float b = a - z;//顺时针
                                if (b < change)
                                {
                                    z = a;
                                    goto label3;
                                }
                                float cc = 360 - a + z;
                                if (b > cc)
                                {
                                    z -= change;
                                }
                                else
                                {
                                    z += change;
                                }
                            }
                            else//逆时针
                            {
                                float b = z - a;//逆时针
                                if(b<change)
                                {
                                    z = a;
                                    goto label3;
                                }
                                float cc = 360 - z + a;
                                if (b > cc)
                                {
                                    z += change;
                                }
                                else
                                {
                                    z -= change;
                                }
                            }
                            if (z > 359)
                                z = 0;
                            if (z < 0)
                                z = 359;
                            label3:;
                            buff_bullet[id].bulletstate[i].angle.z = z;
                            buff_bullet[id].bulletstate[i].movexyz.x = angle_table[(int)z].x * buff_bullet[id].speed;
                            buff_bullet[id].bulletstate[i].movexyz.y = angle_table[(int)z].y * buff_bullet[id].speed;
                        }
                    label2:;
                        buff_bullet[id].bulletstate[i].location += buff_bullet[id].bulletstate[i].movexyz;
                        buff_bullet[id].bulletstate[i].extra++;
                    }
                label1:;
                }
            }
        }
        public static void Chain_Initial(BulletPropertyEX bullet)
        {
            chain_at = bullet.attack;
            chain_id = new int[3];
            chain_id[0] = RegBullet2(ref bullet, 38);
            chain_id[1] = RegBullet2(ref bullet, 38);
            chain_id[2] = RegBullet2(ref bullet, 38);
        }
        public static void Chain_Update()
        {
            for (int c = 0; c < 3; c++)
            {
                int id = chain_id[c];
                int s = buff_bullet[id].s_count;
                int max = buff_bullet[id].max;
                if (s > max)
                {
                    int extra = (id << 16) + max;
                    for (int i = max; i < s; i++)
                    {
                        int index = CreateImageNull2(UpdateBullet, extra);
                        buff_bullet[id].bulletstate[i].imageid = index;
                        buff_bullet[id].bulletstate[i].reg = true;
                        buff_img2[index].spriterender.sortingOrder = buff_bullet[id].image.sorting;
                        int spt_id = buff_bullet[id].spt_index;
                        buff_img2[index].spriterender.sprite = buff_spriteex[buff_bullet[id].spt_id].sprites[spt_id];
                        buff_img2[index].transform.localPosition = buff_bullet[id].bulletstate[i].location;
                        buff_img2[index].transform.localEulerAngles = buff_bullet[id].bulletstate[i].angle;
                        extra++;
                    }
                    buff_bullet[id].max = s;
                }
            }
        }
        public static void Chain_Dispose()
        {
            for (int i = 0; i < 3; i++)
            {
                int c = chain_id[i];
                for (int l = 0; l < buff_bullet[l].max; l++)
                    buff_bullet[c].bulletstate[l].reg = false;
                buff_bullet[c].active = false;
                buff_bullet[c].max = 0;
            }
        }
        #endregion

        #region red alert 2 laser
        static int ra2l_id;
        static float ra2l_len=2.28f,ra2l_at;
        static Material mat_alpha;
        static Vector3 left_a, right_a;
        static void ra2l_Demage(int enemyid,float attack)
        {
            if (buff_enemy[enemyid].currentblood < 1)
                return;
            float harm = attack - buff_enemy[enemyid].defance;
            if (harm > 0)
            {
                buff_enemy[enemyid].currentblood -= harm;
                buff_enemy[enemyid].bloodpercent = buff_enemy[enemyid].currentblood / buff_enemy[enemyid].blood;
                if (buff_enemy[enemyid].currentblood < 0)
                {
                    buff_enemy[enemyid].die = true;
                    destory_enemy++;
                    if (bomb_count < 15)
                    {
                        bomb_queue[bomb_count] = buff_enemy[enemyid].location;
                        bomb_count++;
                    }
                    generateA(10 + currentwave, buff_enemy[enemyid].location);
                }
            }
        }
        public static void ra2l_Initial(BulletPropertyEX bullet)
        {
            ra2l_at = bullet.attack;
            current.wing.shotfrequency = 100-(current.level*10);
            ra2l_id = RegBullet2(ref bullet,38);
            mat_alpha = Resources.Load("Shader/Alpha") as Material;
            left_a =right_a= Vector3.zero;
        }
        public static void ra2l_mov()
        {
            float x = current.wing.location.x;
            float y = current.wing.location.y;
            float x1 = current.wing.location2.x;
            float y1 = current.wing.location2.y;
            float d1 = 100, d2 = 100;
            int c1 = -1, c2 = -1;
            for (int i = 0; i < 20; i++)
            {
                if (buff_enemy[i].move != null & buff_enemy[i].die == false)
                {
                    float xx = buff_enemy[i].location.x - x;
                    float yy = buff_enemy[i].location.y - y;
                    float dd = xx * xx + yy * yy;
                    if (d1 > dd)
                    {
                        d1 = dd;
                        c1 = i;
                    }
                    xx = buff_enemy[i].location.x - x1;
                    yy = buff_enemy[i].location.y - y1;
                    dd = xx * xx + yy * yy;
                    if (d2 > dd)
                    {
                        d2 = dd;
                        c2 = i;
                    }
                }
            }
            if (c1 >= 0)
            {
                left_a.z = Aim(ref current.wing.location, ref buff_enemy[c1].location);
            }
            else left_a.z = 0;
            if (c2 >= 0)
            {
                right_a.z = Aim(ref current.wing.location2, ref buff_enemy[c2].location);
            }
            else right_a.z = 0;
            int c = buff_bullet[ra2l_id].extra;
            if (c > 31)
                return;
            if (c > 0 & c < 31)
            {
                buff_bullet[ra2l_id].speed = (float)c / 30;
            }
            buff_bullet[ra2l_id].extra++;
        }
        public static void ra2l_Update()
        {
            current.wing.transform.localEulerAngles =left_a;
            current.wing.transform2.localEulerAngles = right_a;
            int max = buff_bullet[ra2l_id].max;
            int s = buff_bullet[ra2l_id].s_count;
            if (s>max)
            {
                int extra = (ra2l_id << 16) + max;
                for (int i=max;i< s;i++)
                {
                    int index= CreateImageNull2(Updatera2l,extra);
                    buff_bullet[ra2l_id].bulletstate[i].reg = true;
                    buff_bullet[ra2l_id].bulletstate[i].imageid = index;
                    buff_img2[index].spriterender.material = mat_alpha;
                    buff_img2[index].spriterender.sortingOrder = buff_bullet[i].image.sorting;
                    int spt_id = buff_bullet[ra2l_id].bulletstate[i].sptindex;
                    buff_img2[index].spriterender.sprite = buff_spriteex[buff_bullet[ra2l_id].spt_id].sprites[spt_id];
                    buff_img2[index].transform.localScale = buff_bullet[ra2l_id].bulletstate[i].scale;
                    buff_img2[index].transform.localPosition = buff_bullet[ra2l_id].bulletstate[i].location;
                    buff_img2[index].transform.localEulerAngles = buff_bullet[ra2l_id].bulletstate[i].angle;
                    extra++;
                }
                buff_bullet[ra2l_id].max = s;
            }

        }
        public static void ra2l_Dispose()
        {
            int max = buff_bullet[ra2l_id].max;
            for (int i = 0; i < max; i++)
                buff_bullet[ra2l_id].bulletstate[i].reg = false;
            buff_bullet[ra2l_id].active = false;
            buff_bullet[ra2l_id].max = 0;
        }
        public static void ra2l_shot()
        {
            float at = ra2l_at;
            at *= 5 + current.level;
            at /= 5;
            buff_bullet[ra2l_id].attack = at;
            float x = current.wing.location.x;
            float y = current.wing.location.y;
            float x1 = current.wing.location2.x;
            float y1 = current.wing.location2.y;
            float d1 = 100, d2 = 100;
            int c1 = -1, c2 = -1, s = 0;
            for (int i = 0; i < 20; i++)
            {
                if (buff_enemy[i].move != null &buff_enemy[i].die==false)
                {
                    float xx = buff_enemy[i].location.x - x;
                    float yy = buff_enemy[i].location.y - y;
                    float dd = xx * xx + yy * yy;
                    if (d1 > dd)
                    {
                        d1 = dd;
                        c1 = i;
                    }
                    xx = buff_enemy[i].location.x - x1;
                    yy = buff_enemy[i].location.y - y1;
                    dd = xx * xx + yy * yy;
                    if (d2 > dd)
                    {
                        d2 = dd;
                        c2 = i;
                    }
                }
            }
            if (c1 >= 0)
            {
                ra2l_Demage(c1,buff_bullet[ra2l_id].attack);
                buff_bullet[ra2l_id].bulletstate[s].location = current.wing.location;
                left_a.z= buff_bullet[ra2l_id].bulletstate[s].angle.z = Aim(ref current.wing.location, ref buff_enemy[c1].location);
                buff_bullet[ra2l_id].bulletstate[s].scale.x = 1;
                buff_bullet[ra2l_id].bulletstate[s].scale.y = Mathf.Sqrt(d1) / ra2l_len;
                buff_bullet[ra2l_id].bulletstate[s].update = true;
                buff_bullet[ra2l_id].bulletstate[s].active = true;
                s++;
                s= ra2l_reflect(c1,s);
            }
            if (c2 >= 0)
            {
                ra2l_Demage(c2, at);
                buff_bullet[ra2l_id].bulletstate[s].location = current.wing.location2;
                right_a.z= buff_bullet[ra2l_id].bulletstate[s].angle.z = Aim(ref current.wing.location2, ref buff_enemy[c2].location);
                buff_bullet[ra2l_id].bulletstate[s].scale.x = 1;
                buff_bullet[ra2l_id].bulletstate[s].scale.y = Mathf.Sqrt(d2) / ra2l_len;
                buff_bullet[ra2l_id].bulletstate[s].update = true;
                buff_bullet[ra2l_id].bulletstate[s].active = true;
                s++;
                s= ra2l_reflect(c2,s);
            }
            buff_bullet[ra2l_id].s_count = s;
            buff_bullet[ra2l_id].extra = 0;
            buff_bullet[ra2l_id].speed = 0;
        }
        static int ra2l_reflect(int id ,int s)
        {
            float x = buff_enemy[id].location.x;
            float y = buff_enemy[id].location.y;
            for(int i=0;i<20;i++)
            {
                if(i!=id)
                {
                    if (buff_enemy[i].move != null &buff_enemy[i].die==false)
                    {
                        float xx = buff_enemy[i].location.x - x;
                        float yy = buff_enemy[i].location.y - y;
                        float dd = xx * xx + yy * yy;
                        if (dd<10)
                        {
                            ra2l_Demage(i, buff_bullet[ra2l_id].attack * 0.3f);
                            unsafe
                            {
                                fixed(BulletState* bs=&buff_bullet[ra2l_id].bulletstate[s])
                                {
                                    bs->location = buff_enemy[id].location;
                                    bs->angle.z = Aim(ref buff_enemy[id].location, ref buff_enemy[i].location);
                                    bs->scale.x = 1;
                                    bs->scale.y = Mathf.Sqrt(dd) / ra2l_len;
                                    bs->update = true;
                                    bs->active = true;
                                }
                            }
                            s++;
                        }
                    }
                }
            }
            return s;
        }
        static void Updatera2l(int img_id, int extra)
        {
            int id = extra >> 16;
            int index = extra & 0xffff;
            if (buff_bullet[id].bulletstate[index].imageid != img_id)
            {
                buff_img2[img_id].gameobject.SetActive(false);
                buff_img2[img_id].restore = true;
            }
            if (buff_bullet[id].bulletstate[index].reg)
            {
                if (buff_bullet[id].bulletstate[index].active)
                {
                    buff_img2[img_id].gameobject.SetActive(true);
                    buff_img2[img_id].transform.localPosition = buff_bullet[id].bulletstate[index].location;
                    buff_img2[img_id].transform.localEulerAngles = buff_bullet[id].bulletstate[index].angle;
                    buff_img2[img_id].spriterender.material.SetFloat("_Alpha",buff_bullet[id].speed);
                    if (buff_bullet[id].speed >= 1)
                        buff_bullet[id].bulletstate[index].active = false;
                    if (buff_bullet[id].bulletstate[index].update)
                    {
                        //int sptindex = buff_bullet[id].spt_index;
                        //buff_img2[img_id].spriterender.sprite = buff_spriteex[buff_bullet[id].spt_id].sprites[sptindex];
                        buff_img2[img_id].transform.localScale = buff_bullet[id].bulletstate[index].scale;
                        buff_bullet[id].bulletstate[index].update = false;
                    }
                }
                else
                {
                    buff_img2[img_id].gameobject.SetActive(false);
                }
            }
            else
            {
                buff_img2[img_id].gameobject.SetActive(false);
                buff_img2[img_id].restore = true;
            }
        }
        #endregion

        #region enemy
        static Vector3 boss_v = Vector3.zero;
        static int boss_state=0;
        static int a10_extra=0;
        public static bool M_Boss_a10(int id)
        {
            if (buff_enemy[id].animation.img_path != null)
            {
                buff_enemy[id].animation.location = buff_enemy[id].location;
                CalculAnimation(ref buff_enemy[id].animation, 0);
            }
            if (buff_enemy[id].extra_m < 100)
            {
                if (buff_enemy[id].play != null)
                    buff_enemy[id].play(id);
                buff_enemy[id].extra_m++;
                buff_enemy[id].location.y -= 0.03f;
                return true;
            }
            else//mov
            {
                if (buff_enemy[id].extra_m == 100)
                {
                    buff_enemy[id].extra_m++;
                    boss_v.x = -0.02f - (float)lucky.NextDouble() / 50;
                    boss_v.y = 0.02f + (float)lucky.NextDouble() / 50;
                }
                if (buff_enemy[id].location.x > 0.8f)
                {
                    boss_v.x = -0.02f - (float)lucky.NextDouble() / 50;
                }
                if (buff_enemy[id].location.x < -0.8f)
                {
                    boss_v.x = 0.02f + (float)lucky.NextDouble() / 50;
                }
                if (buff_enemy[id].location.y > 4f)
                {
                    boss_v.y = -0.02f - (float)lucky.NextDouble() / 50;
                    if (boss_v.x == 0)
                        boss_v.x = -0.04f + (float)lucky.NextDouble() / 12.5f;
                }
                if (buff_enemy[id].location.y < 2.6f)
                {
                    boss_v.y = 0.02f + (float)lucky.NextDouble() / 50;
                    if (boss_v.x == 0)
                        boss_v.x = -0.04f + (float)lucky.NextDouble() / 12.5f;
                }
            }
            int eid;
            switch (boss_state)
            {                
                case 0:
                    {                       
                        if (buff_enemy[id].extra_b > 2)
                        {
                            buff_enemy[id].extra_b = 0;
                            eid = buff_enemy[id].extra_a & 1;
                            eid = buff_enemy[id].bulletid[eid];
                            Vector3 site = buff_enemy[id].location;
                            site.y += 0.5f;
                            Vector3 temp = Vector3.zero;
                            temp.z = a10_extra;
                            buff_bullet[eid].s_count = 5;
                            buff_bullet[eid].shot = new StartPoint[5];
                            for (int i = 0; i < 5; i++)
                            {
                                buff_bullet[eid].shot[i] = new StartPoint() { location = site, angle = temp };
                                temp.z += 72;
                            }
                            if (buff_enemy[id].extra_a < 16)
                            {
                                a10_extra += 5;
                                if (buff_enemy[id].extra_a == 8)
                                    a10_extra = 0;
                                if (buff_enemy[id].extra_a == 15)
                                {
                                    //buff_enemy[id].extra_b = -10;
                                    a10_extra = 60;
                                }
                            }
                            else
                            {
                                a10_extra -= 5;
                                if (buff_enemy[id].extra_a ==24)
                                    a10_extra = 60;
                                if (buff_enemy[id].extra_a >= 31)
                                {
                                    a10_extra = 0;
                                    buff_enemy[id].extra_a = 0;
                                    buff_enemy[id].extra_b = -100;
                                    boss_state = 1;
                                    break;
                                }
                            }
                            buff_enemy[id].extra_a++;
                        }
                        break;
                    }
                case 1:
                    {
                        if (buff_enemy[id].extra_b > 20)
                        {
                            buff_enemy[id].extra_b = -50;
                            eid = buff_enemy[id].bulletid[2];
                            Vector3 site = buff_enemy[id].location;
                            site.x -= 0.3f;
                            site.y -= 0.5f;
                            Vector3 temp = Vector3.zero;                           
                            temp.z = 156;
                            buff_bullet[eid].s_count = 5;
                            buff_bullet[eid].shot = new StartPoint[5];
                            for (int i=0;i<5;i++)
                            {
                                buff_bullet[eid].shot[i] = new StartPoint() { location=site,angle=temp};
                                site.x += 0.1f;
                                temp.z += 12;
                            }
                            boss_state=2;
                        }
                        break;
                    }
                case 2:
                    {
                        buff_enemy[id].location += boss_v;
                        if (buff_enemy[id].extra_b>8)
                        {
                            eid = buff_enemy[id].bulletid[0];
                            float z = Aim(ref buff_enemy[id].location, ref core_location);
                            Vector3 site = buff_enemy[id].location;
                            site.y -= 1f;
                            z -= 30;
                            if (z < 0)
                                z += 360;
                            buff_bullet[eid].s_count = 6;
                            buff_bullet[eid].shot = new StartPoint[6];
                            for (int i = 0; i < 6; i++)
                            {
                                buff_bullet[eid].shot[i].location = site;
                                buff_bullet[eid].shot[i].angle.z = z;
                                z += 10;
                                if (z > 360)
                                    z -= 360;
                            }
                            buff_enemy[id].extra_b = 0;
                            buff_enemy[id].extra_a++;
                            if(buff_enemy[id].extra_a>12)
                            {
                                buff_enemy[id].extra_a = 0;
                                boss_state = 3;
                            }
                        }                       
                        break;
                    }
                case 3:
                    { 
                        if(buff_enemy[id].extra_b>2)
                        {
                            if(buff_enemy[id].extra_a==0)
                            {
                                a10_extra = 0;
                            }
                            if ((buff_enemy[id].extra_a & 8) == 0)
                                a10_extra += 5;
                            else a10_extra -= 5;
                            eid = buff_enemy[id].bulletid[1];
                            Vector3 site = buff_enemy[id].location;
                            site.y -= 1f;
                            buff_bullet[eid].s_count = 2;
                            buff_bullet[eid].shot = new StartPoint[2];
                            buff_bullet[eid].shot[0] = new StartPoint() { location=site,angle=new Vector3(0,0,180+a10_extra)};
                            buff_bullet[eid].shot[1] = new StartPoint() { location = site, angle = new Vector3(0, 0, 180 - a10_extra) };
                            buff_enemy[id].extra_b = 0;
                            buff_enemy[id].extra_a++;
                            if (buff_enemy[id].extra_a > 36)
                            {
                                buff_enemy[id].extra_a = 0;
                                boss_state = 4;
                            }
                        }
                        break;
                    }
                case 4:
                    {
                        if (buff_enemy[id].extra_b > 2)
                        {
                            buff_enemy[id].extra_b = 0;
                            eid = buff_enemy[id].bulletid[3];
                            Vector3 site = buff_enemy[id].location;
                            site.y += 0.5f;
                            Vector3 temp = Vector3.zero;
                            temp.z = a10_extra;
                            buff_bullet[eid].s_count = 6;
                            buff_bullet[eid].shot = new StartPoint[6];
                            for (int i = 0; i < 6; i++)
                            {
                                buff_bullet[eid].shot[i] = new StartPoint() { location = site, angle = temp };
                                temp.z += 60;
                            }
                            if (buff_enemy[id].extra_a < 12)
                            {
                                a10_extra += 5;
                                if(buff_enemy[id].extra_a==11)
                                {
                                    buff_enemy[id].extra_b = -5;
                                }
                            }
                            else
                            {
                                a10_extra -= 5;
                                if (buff_enemy[id].extra_a >= 23)
                                {
                                    buff_enemy[id].extra_b = -50;
                                    buff_enemy[id].extra_a = 0;
                                    boss_state = 0;
                                    break;
                                }
                            }
                            buff_enemy[id].extra_a++;
                        }
                        break;
                    }
            }           
            buff_enemy[id].extra_b++;
            return true;
        }
        public static bool M_Boss_Moth(int id)
        {
            if (buff_enemy[id].animation.img_path != null)
            {
                buff_enemy[id].animation.location = buff_enemy[id].location;
                CalculAnimation(ref buff_enemy[id].animation, 0);
            }
            if (buff_enemy[id].extra_m < 200)
            {
                if (buff_enemy[id].play != null)
                    buff_enemy[id].play(id);
                buff_enemy[id].extra_m++;
                buff_enemy[id].location.y -= 0.015f;
                return true;
            }
            else//mov
            {
                if (buff_enemy[id].extra_m == 200)
                {
                    buff_enemy[id].extra_m++;
                    boss_v.x = -0.02f - (float)lucky.NextDouble() / 50;
                    boss_v.y = 0.02f + (float)lucky.NextDouble() / 50;
                }
                if (buff_enemy[id].location.x > 0.8f)
                {
                    boss_v.x = -0.02f - (float)lucky.NextDouble() / 50;
                }
                if (buff_enemy[id].location.x < -0.8f)
                {
                    boss_v.x = 0.02f + (float)lucky.NextDouble() / 50;
                }
                if (buff_enemy[id].location.y > 4f)
                {
                    boss_v.y = -0.02f - (float)lucky.NextDouble() / 50;
                    if (boss_v.x == 0)
                        boss_v.x = -0.04f + (float)lucky.NextDouble() / 12.5f;
                }
                if (buff_enemy[id].location.y < 2.6f)
                {
                    boss_v.y = 0.02f + (float)lucky.NextDouble() / 50;
                    if (boss_v.x == 0)
                        boss_v.x = -0.04f + (float)lucky.NextDouble() / 12.5f;
                }
            }
            switch (buff_enemy[id].extra_a)
            {
                case 0:
                    if (buff_enemy[id].extra_b % 5 == 0)
                    {
                        Vector3 site = buff_enemy[id].location;
                        Vector3 temp = Vector3.zero;
                        int id0 = buff_enemy[id].bulletid[1];
                        site.x -= 1.6f;
                        temp.z = Aim(ref site, ref core_location);
                        buff_bullet[id0].s_count = 6;
                        buff_bullet[id0].shot = new StartPoint[6];
                        buff_bullet[id0].shot[0] = new StartPoint() { location = site, angle = temp };
                        site.y += 0.1f;
                        site.x += 0.4f;
                        temp.z = Aim(ref site, ref core_location);
                        buff_bullet[id0].shot[1] = new StartPoint() { location = site, angle = temp };
                        site.y += 0.1f;
                        site.x += 0.4f;
                        temp.z = Aim(ref site, ref core_location);
                        buff_bullet[id0].shot[2] = new StartPoint() { location = site, angle = temp };
                        site.x += 1;
                        site.x += 0.8f;
                        temp.z = Aim(ref site, ref core_location);
                        buff_bullet[id0].shot[3] = new StartPoint() { location = site, angle = temp };
                        site.y -= 0.1f;
                        site.x += 0.4f;
                        temp.z = Aim(ref site, ref core_location);
                        buff_bullet[id0].shot[4] = new StartPoint() { location = site, angle = temp };
                        site.y -= 0.1f;
                        site.x += 0.4f;
                        temp.z = Aim(ref site, ref core_location);
                        buff_bullet[id0].shot[5] = new StartPoint() { location = site, angle = temp };
                        if (buff_enemy[id].extra_b >= 40)
                        {
                            buff_enemy[id].extra_a = lucky.Next(0, 5);
                            buff_enemy[id].extra_b = 0;
                        }
                    }
                    break;
                case 1:
                    buff_enemy[id].location += boss_v;
                    if (buff_enemy[id].extra_b % 5 == 0)
                    {
                        Vector3 site1 = buff_enemy[id].location;
                        Vector3 z1 = buff_enemy[id].angle;
                        int id1 = buff_enemy[id].bulletid[0];
                        site1.x -= 0.5f;
                        buff_bullet[id1].s_count = 6;
                        z1.z = 150;
                        buff_bullet[id1].shot = new StartPoint[6];
                        buff_bullet[id1].shot[0] = new StartPoint() { location = site1, angle = z1 };
                        z1.z = 180;
                        buff_bullet[id1].shot[1] = new StartPoint() { location = site1, angle = z1 };
                        z1.z = 210;
                        buff_bullet[id1].shot[2] = new StartPoint() { location = site1, angle = z1 };
                        site1.x += 1;
                        buff_bullet[id1].shot[3] = new StartPoint() { location = site1, angle = z1 };
                        z1.z = 180;
                        buff_bullet[id1].shot[4] = new StartPoint() { location = site1, angle = z1 };
                        z1.z = 150;
                        buff_bullet[id1].shot[5] = new StartPoint() { location = site1, angle = z1 };
                        if (buff_enemy[id].extra_b > 100)
                        {
                            buff_enemy[id].extra_b = 0;
                            buff_enemy[id].extra_a = lucky.Next(0, 6);
                            if (buff_enemy[id].extra_a > 5)
                                buff_enemy[id].extra_a = 5;
                        }
                    }
                    break;
                case 2:
                    Vector3 site5 = buff_enemy[id].location;
                    Vector3 z5 = buff_enemy[id].angle;
                    int id5 = buff_enemy[id].bulletid[2];
                    buff_bullet[id5].s_count = 1;
                    z5.z = 180;
                    site5.x = (float)lucky.NextDouble() * 5;
                    site5.x -= 2.5f;
                    buff_bullet[id5].shot = new StartPoint[1];
                    buff_bullet[id5].shot[0] = new StartPoint() { location = site5, angle = z5 };
                    if (buff_enemy[id].extra_b > 100)
                    {
                        buff_enemy[id].extra_b = 0;
                        buff_enemy[id].extra_a = lucky.Next(0, 5);
                        if (buff_enemy[id].extra_a == 2)
                            buff_enemy[id].extra_a = 3;
                    }
                    break;
                case 3:
                    if (buff_enemy[id].extra_b == 1)
                    {
                        id5 = buff_enemy[id].bulletid[5];
                        Vector3 temp = buff_enemy[id].location;
                        temp.y -= 0.9218f;
                        buff_effect[buff_bullet[id5].eff_id].location = temp;
                        Effect_Active(buff_bullet[id5].eff_id);
                    }
                    if (buff_enemy[id].extra_b == 30)
                    {
                        id5 = buff_enemy[id].bulletid[5];
                        buff_bullet[id5].s_count = 1;
                        Vector3 temp = buff_enemy[id].location;
                        temp.y -= 0.9218f;
                        buff_bullet[id5].shot = new StartPoint[] { new StartPoint() { location = temp } };
                    }
                    animat_time = (float)buff_enemy[id].extra_b / 150;
                    if (buff_enemy[id].extra_b > 150)
                    {
                        buff_enemy[id].extra_b = 0;
                        buff_enemy[id].extra_a = lucky.Next(0, 5);
                        if (buff_enemy[id].extra_a == 3)
                            buff_enemy[id].extra_a = 4;
                    }
                    break;
                case 4:
                    if (buff_enemy[id].extra_b % 5 == 0)
                    {
                        Vector3 site1 = buff_enemy[id].location;
                        Vector3 z1 = buff_enemy[id].angle;
                        int id1 = buff_enemy[id].bulletid[4];
                        site1.x -= 0.5f;
                        buff_bullet[id1].s_count = 6;
                        z1.z = 80;
                        buff_bullet[id1].shot = new StartPoint[6];
                        buff_bullet[id1].shot[0] = new StartPoint() { location = site1, angle = z1 };
                        z1.z = 90;
                        buff_bullet[id1].shot[1] = new StartPoint() { location = site1, angle = z1 };
                        z1.z = 100;
                        buff_bullet[id1].shot[2] = new StartPoint() { location = site1, angle = z1 };
                        z1.z = 260;
                        site1.x += 1;
                        buff_bullet[id1].shot[3] = new StartPoint() { location = site1, angle = z1 };
                        z1.z = 270;
                        buff_bullet[id1].shot[4] = new StartPoint() { location = site1, angle = z1 };
                        z1.z = 280;
                        buff_bullet[id1].shot[5] = new StartPoint() { location = site1, angle = z1 };
                        if (buff_enemy[id].extra_b > 100)
                        {
                            buff_enemy[id].extra_b = 0;
                            buff_enemy[id].extra_a = lucky.Next(0, 6);
                            if (buff_enemy[id].extra_a > 5)
                                buff_enemy[id].extra_a = 5;
                        }
                    }
                    break;
                case 5:
                    buff_enemy[id].location += boss_v;
                    if (buff_enemy[id].extra_b % 10 == 0)
                    {
                        int id1 = buff_enemy[id].bulletid[3];
                        buff_bullet[id1].s_count = 72;
                        Vector3 temp = buff_enemy[id].location;
                        temp.x -= 1.5f;
                        buff_bullet[id1].shot = new StartPoint[72];
                        for (int i = 0; i < 36; i++)
                            buff_bullet[id1].shot[i] = new StartPoint() { location = temp, angle = new Vector3(0, 0, i * 10) };
                        temp.x += 3;
                        for (int i = 0; i < 36; i++)
                            buff_bullet[id1].shot[i + 36] = new StartPoint() { location = temp, angle = new Vector3(0, 0, i * 10) };
                        if (buff_enemy[id].extra_b > 10)
                        {
                            buff_enemy[id].extra_b = 0;
                            buff_enemy[id].extra_a = lucky.Next(0, 5);
                        }
                    }
                    break;
            }
            buff_enemy[id].extra_b++;
            return true;
        }
        #endregion
    }
}
