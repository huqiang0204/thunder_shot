using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.UnityVS.Script
{
 
    class GameControl:QueueSourceEX
    {
        protected static CurrentDispose current;       
        protected static DamageCalculate damageA, damageC;
        protected static DamageCalculate2 damageB;
        protected static GenerateProp generateA;
        protected static GameOverMod D_gameover;
        protected static int currentwave = 0, destory_enemy;
        protected static BattelField buff_wave;
        protected static bool gameover = true, plane_move;
        protected static Action update;
        protected static PropCollider propcollid;
        protected static int coreid;
        static EnemyWave crt_wave;

        #region data source
        protected static Skill big_boom = new Skill() { location = new Vector3(0, 2, 1), eff_b = DataSource.eff_explode_big, force_clear = true, r = 2f };
        protected static ImageProperty core = new ImageProperty() { imagepath = "Picture/core", sorting = 7, location = new Vector3(0, 0, 1), scale = new Vector3(1, 1) };
        protected static ImageProperty blood_back = new ImageProperty()
        {
            imagepath = "Picture/bloodt",
            scale = new Vector3(0.7f, 0.8f),
            location = new Vector3(-1.34f, 4.83f, 1),
            sorting = 6
        };
        protected static ImageProperty blood_g = new ImageProperty()
        {
            imagepath = "Picture/bloodg",
            scale = origion_scale,
            location = origion_location,
            sorting = 7
        };
        protected static ImageProperty blood_b = new ImageProperty()
        {
            imagepath = "Picture/bloodb",
            scale = origion_scale,
            location = origion_location,
            sorting = 7
        };
        protected static ImageProperty blood_y = new ImageProperty()
        {
            imagepath = "Picture/bloody",
            scale = origion_scale,
            location = origion_location,
            sorting = 7
        };

        #endregion

        public static void LoadBaseComponent()
        {
            InitialBlood("Picture/hp2");
            blood_back.spt_id = CreateSprite(blood_back.imagepath);
            blood_b.spt_id = CreateSprite(blood_b.imagepath);
            blood_g.spt_id = CreateSprite(blood_g.imagepath);
            blood_y.spt_id = CreateSprite(blood_y.imagepath);
            bossblood.back_spt_id = CreateSprite("Picture/bossxue");
            bossblood.for_spt_id = CreateSprite("Picture/bossxue03");
        }

        #region move
        internal static void Plane_Move(Vector3 dot)
        {
            if (mousedown)
            {
                if (plane_move)
                {
                    Vector3 temp = PixToCameraOffset(dot - mouse_position);
                    mouse_position = dot;
                    temp += core_location;
                    if (temp.y < 4.8f & temp.y > -4.8f & temp.x < 2.5f & temp.x > -2.5f)
                    {
                        buff_img[coreid].transform.localPosition = core_location = temp;
                    }
                }
            }
            mouse_position = dot;
        }
        #endregion

        #region  process
        public static int RegB(ref BulletPropertyEX bullet)
        {
            return bullet.spt_id = QueueSourceEX.RegSpriteS(bullet.image.imagepath);
        }
        public static int RegE(ref EnemyPropertyEX enemy)
        {
             if (enemy.enemy.animation.img_path != null)
                return enemy.enemy.animation.spt_id = QueueSourceEX.RegSpriteS(enemy.enemy.animation.img_path);
             else return enemy.enemy.dspt_id = QueueSourceEX.RegSpriteS(enemy.image.imagepath);
        }
        public static void SuspendGame()
        {
            MainCamera.update = null;
            plane_move = false;
        }
        public static void ContinueGame()
        {
            MainCamera.update = update;
            plane_move = true;
        }
        public static void GameOver(bool pass)
        {
            D_gameover(pass);
        }
        static void LoadEnemyPicture()//pc sub thread
        {
            FileManage.Run();
        }
        protected static void LoadEnemyResource()//u main thread
        {
            for (int i = 128; i < 256; i++)
            {
                if (buff_spriteex[i].path != null)
                   if (buff_spriteex[i].texture == null)
                      buff_spriteex[i].rr = Resources.LoadAsync(buff_spriteex[i].path);
            }
        }
        public static void SetLevel(SetBattelField set)//u sub thread
        {
            set(ref buff_wave);
        }
        #endregion
  
        #region warplane dispose
        public static void SetDispose(CurrentDispose d)
        {
            current = d;
            RegSpriteA(current.warplane.image.imagepath, ref current.warplane.spt_id);
            RegSpriteA(current.warplane.bullet.image.imagepath, ref current.warplane.bullet.spt_id);
            RegSpriteA(current.B_second.bullet.image.imagepath, ref current.B_second.bullet.spt_id);
            RegSpriteA(current.bs_back.bullet.image.imagepath, ref current.bs_back.bullet.spt_id);
            RegSpriteA(current.wing.image.imagepath, ref current.wing.spt_id);
            RegSpriteA(current.wing.bullet.image.imagepath, ref current.wing.bullet.spt_id);
            RegSpriteA(current.w_back.image.imagepath, ref current.w_back.spt_id);
            RegSpriteA(current.w_back.bullet.image.imagepath, ref current.w_back.bullet.spt_id);

        }
        public static void SetMainPlane(WarPlane p)
        {
            current.warplane = p;
            RegSpriteA(p.image.imagepath,ref current.warplane.spt_id);
            RegSpriteA(p.bullet.image.imagepath, ref current.warplane.bullet.spt_id);
        }
        public static void SetSecondWeapon(SecondBullet b)
        {         
            current.B_second = b;
            RegSpriteA(b.bullet.image.imagepath, ref current.B_second.bullet.spt_id);
        }
        public static void SetWing(Wing w)
        {            
            current.wing = w;
            RegSpriteA(w.image.imagepath, ref current.wing.spt_id);
            RegSpriteA(w.bullet.image.imagepath,ref current.wing.bullet.spt_id);
        }
        public static void SetPower(int p)
        {
            current.power = p;
        }
        public static void SetShiled(Shiled s)
        {
            current.shiled = s;
        }
        #endregion

        #region animation control
        static void CreateAnimatComponent2(ref AnimatStruct a, int spt_id, UpdateImage validate,int extra)
        {
            int id = a.image_id = CreateImageNull2(validate,extra);
            //buff_img[id].transform.localPosition = a.location;
            buff_img2[id].transform.localEulerAngles = a.angle;
            buff_img2[id].transform.localScale = a.scale;//need repair 
            buff_img2[id].spriterender.sprite = buff_spriteex[spt_id].sprites[a.spt_index];
            buff_img2[id].spriterender.sortingOrder = a.img_sort;
            a.transform = buff_img2[id].transform;
            if (a.son != null)
                for (int i = 0; i < a.son.Length; i++)
                    CreateAnimatComponent2(ref a.son[i], spt_id,validate,extra);
        }
        public static void Create_animation2(ref AnimatStruct a ,UpdateImage validate ,int extra)//test
        {
            CreateSpriteS(a.spt_id, a.sprite);
            int id = a.image_id = CreateImageNull2(validate,extra);
            buff_img2[id].transform.localPosition = a.location;
            buff_img2[id].transform.localEulerAngles = a.angle;
            buff_img2[id].transform.localScale = a.scale;
            buff_img2[id].spriterender.sprite = buff_spriteex[a.spt_id].sprites[a.spt_index];
            buff_img2[id].spriterender.sortingOrder = a.img_sort;
            a.transform = buff_img2[id].transform;
            if (a.son != null)
                for (int i = 0; i < a.son.Length; i++)
                    CreateAnimatComponent2(ref a.son[i], a.spt_id ,validate,extra);
            CalculAnimation(ref a, 0);
        }

        static void Delete_animation(ref AnimatStruct a)
        {
            RecycleImageA(a.image_id);
            if (a.son != null)
                for (int i = 0; i < a.son.Length; i++)
                    Delete_animation(ref a.son[i]);
        }
        static void UpdateAnimation(ref AnimatStruct a)
        {
            a.transform.localPosition = a.location;
            a.transform.localEulerAngles = a.angle;
            if (a.son != null)
                for (int i = 0; i < a.son.Length; i++)
                    UpdateAnimation(ref a.son[i]);
        }
        protected static float animat_time;
        protected static void CalculAnimation(ref AnimatStruct a, float fa)
        {
            if (a.anglecurve != null)
            {
                a.angle.z = CalculateCurve(ref a.anglecurve, animat_time);
                if (a.angle.z < 0)
                    a.angle.z += 360;
            }
            if (a.stretchcurve != null)
                a.pivot.y = CalculateCurve(ref a.stretchcurve, animat_time);
            if (a.son != null)
                for (int i = 0; i < a.son.Length; i++)
                {
                    a.son[i].location = RotateVector3(a.son[i].pivot, ref a.location, a.angle.z + fa);//子对象绕轴位置
                    CalculAnimation(ref a.son[i], a.angle.z + fa);//计算子对象
                }
        }
        #endregion

        #region enemy control
        protected static EnemyBaseEX[] buff_enemy = new EnemyBaseEX[20];
        static void AnimationValidate2(int img_id, int extra)
        {
            if (buff_enemy[extra].die|buff_enemy[extra].animation.img_path==null)
            {
                buff_img2[img_id].gameobject.SetActive(false);
                buff_img2[img_id].restore = true;
            }
        }
        static void UpdateEnemy2(int img_id, int eid)
        {
            if (buff_enemy[eid].die)
            {
                buff_img2[img_id].gameobject.SetActive(false);
                buff_img2[img_id].restore = true;
            }
            else
            {
                buff_img2[img_id].transform.localPosition = buff_enemy[eid].location;
                buff_img2[img_id].transform.localEulerAngles = buff_enemy[eid].angle;
                if (buff_enemy[eid].boss)
                    buff_img[bossblood.for_img_id].spriterender.material.SetFloat("_Progress", buff_enemy[eid].bloodpercent);
                else if (buff_enemy[eid].show_blood)
                    ShowBlood(buff_enemy[eid].bloodid, buff_enemy[eid].bloodpercent);
            }
            if (buff_enemy[eid].play != null & buff_enemy[eid].update_spt)
            {
                buff_enemy[eid].update_spt = false;
                int tempid = buff_enemy[eid].dspt_id;
                buff_img2[img_id].spriterender.sprite = buff_spriteex[tempid].sprites[buff_enemy[eid].spt_index];
            }
        }
        protected static void UpdateEnemy2A()
        {
            for (int i = 0; i < 20; i++)
            {
                if (buff_enemy[i].move != null)
                {
                    if (buff_enemy[i].die)
                    {
                        buff_enemy[i].move = null;
                        RecyleEnemy2(i);
                    }
                    else
                    {
                        if (buff_enemy[i].animation.img_path != null)
                        {
                            UpdateAnimation(ref buff_enemy[i].animation);
                            if (buff_enemy[i].boss)
                                buff_img[bossblood.for_img_id].spriterender.material.SetFloat("_Progress", buff_enemy[i].bloodpercent);
                            else if (buff_enemy[i].show_blood)
                                ShowBlood(buff_enemy[i].bloodid, buff_enemy[i].bloodpercent);
                        }
                    }
                }
            }
            if (crt_wave.sum > 0)
            {
                if (crt_wave.interval == 0)
                {
                    int index, c = crt_wave.start.Length;
                    for (int i = 0; i < crt_wave.sum; i++)
                    {
                        index = i % c;
                        CreateEnemy2(crt_wave.enemyppt, crt_wave.start[index]);
                    }
                    crt_wave.sum = 0;
                }
                else
                {
                    if (crt_wave.time_c > crt_wave.interval)
                    {
                        crt_wave.sum--;
                        int index = crt_wave.sum, c = crt_wave.start.Length;
                        index = index % c;
                        CreateEnemy2(crt_wave.enemyppt, crt_wave.start[index]);
                        crt_wave.time_c = 0;
                    }
                    crt_wave.time_c++;
                }
            }
        }
        static int CreateEnemy2(EnemyPropertyEX target, StartPoint start)
        {
            int eid = 0;
            for(int i=0;i<20;i++)
            {
                if (buff_enemy[i].move == null)
                {
                    eid = i;
                    break;
                }
            }
            buff_enemy[eid].die = false;
            buff_enemy[eid] = target.enemy;
            int id;
            if (target.enemy.animation.img_path!=null)
            {
                buff_enemy[eid].location = start.location;
                buff_enemy[eid].angle = start.angle;
                Create_animation2(ref buff_enemy[eid].animation,AnimationValidate2,eid);
                id = target.enemy.animation.image_id;
            }
            else
            {
                id = CreateImageNull2(UpdateEnemy2, eid);
                if (buff_spriteex[buff_enemy[eid].dspt_id].sprites == null)
                    CreateSpriteGA(target.enemy.dspt_id, target.image.grid);
                buff_enemy[eid].imageid = id;
                buff_enemy[eid].angle = start.angle;
                buff_enemy[eid].location = start.location;
                buff_img2[id].transform.localPosition = start.location;
                int i = target.enemy.spt_index;
                buff_img2[id].spriterender.sprite = buff_spriteex[target.enemy.dspt_id].sprites[i];
                buff_img2[id].spriterender.sortingOrder = 3;
            }
            if (buff_enemy[eid].boss)
            {
                bossblood.enemy_id = eid;
                CreateBossBlood();
            }
            else if (target.enemy.show_blood)
                buff_enemy[eid].bloodid = RegBlood(buff_img2[id].transform);
            buff_enemy[eid].currentblood = buff_enemy[eid].blood;
            buff_enemy[eid].extra_a = 0;
            buff_enemy[eid].extra_b = 0;
            buff_enemy[eid].extra_m = 0;
            if (target.bullet != null)
            {
                int l = target.bullet.Length;
                buff_enemy[eid].bulletid = new int[l];
                for (int i = 0; i < l; i++)
                {
                    target.bullet[i].parentid = eid;
                    buff_enemy[eid].bulletid[i] = RegBullet2(ref target.bullet[i],0);
                }
            }
            return eid;
        }
        static void RecyleEnemy2(int id)
        {
            if (buff_enemy[id].animation.img_path != null)
            {
                Delete_animation(ref buff_enemy[id].animation);
            }
            else
            {
                int iid = buff_enemy[id].imageid;
                buff_img2[iid].gameobject.SetActive(false);
                buff_img2[iid].restore = true;
            }
            if (buff_enemy[id].boss)
                RecycleBossBlood();
            else if (buff_enemy[id].show_blood)
                RecycleBlood(buff_enemy[id].bloodid);
            if (buff_enemy[id].shot != null)
                for (int i = 0; i < buff_enemy[id].bulletid.Length; i++)
                    UnRegBullet2(buff_enemy[id].bulletid[i]);
            buff_enemy[id].move = null;
        }
        protected static void ClearEnemyAll()
        {
            for (int i = 0; i < 20; i++)
            {
                buff_enemy[i].move = null;
                buff_enemy[i].animation.img_path = null;
            }
        }
        #endregion

        #region Aim    
        internal static float Aim(ref Vector3 self, ref Vector3 v)
        {
            float x = self.x - v.x;
            float y = self.y - v.y;
            float ox = 0, a;
            if (x < 0)
            {
                x = 0 - x;
                if (y < 0)
                {
                    y = 0 - y;
                    ox = 270;
                    a = Mathf.Atan2(y, x) * 57.29577951f;
                }
                else
                {
                    ox = 180;
                    a = Mathf.Atan2(x, y) * 57.29577951f;
                }
            }
            else
            {
                if (y < 0)
                {
                    y = 0 - y;
                    a = Mathf.Atan2(x, y) * 57.29577951f;
                }
                else
                {
                    ox = 90;
                    a = Mathf.Atan2(y, x) * 57.29577951f;
                }
            }
            return a + ox;
        }
        internal static void Aim1(ref Vector3 self, ref Vector3 target, float speed)
        {
            float x = target.x - self.x;
            float y = target.y - self.y;
            if (x == 0)
            {
                if (y > 0)
                    self.y += speed;
                else
                    self.y -= speed;
                return;
            }
            if (y == 0)
            {
                if (x > 0)
                    self.x += speed;
                else
                    self.x -= speed;
                return;
            }
            float ratio = x / y;
            if (ratio < 0)
                ratio = 0 - ratio;
            float c = ratio * ratio;
            float y1 = Mathf.Sqrt(speed * speed / (c + 1));
            if (x > 0)
                self.x += ratio * y1;
            else
                self.x -= ratio * y1;
            if (y > 0)
                self.y += y1;
            else
                self.y -= y1;
        }
        internal static void Aim1(ref Vector3 self, ref Vector3 target, float speed, ref Vector3 up)
        {
            float x = target.x - self.x;
            float y = target.y - self.y;
            if (x == 0)
            {
                if (y > 0)
                {
                    self.y += speed;
                    up.x = 0;
                    up.y = -speed;
                }
                else
                {
                    self.y -= speed;
                    up.x = 0;
                    up.y = speed;
                }
                return;
            }
            if (y == 0)
            {
                if (x > 0)
                {
                    self.x += speed;
                    up.x = -speed;
                    up.y = 0;
                }
                else
                {
                    self.x -= speed;
                    up.x = speed;
                    up.y = 0;
                }
                return;
            }
            float ratio = x / y;
            if (ratio < 0)
                ratio = 0 - ratio;
            float c = ratio * ratio;
            float y1 = Mathf.Sqrt(speed * speed / (c + 1));
            if (x > 0)
            {
                self.x += ratio * y1;
                up.x = ratio * y1;
            }
            else
            {
                self.x -= ratio * y1;
                up.x = -ratio * y1;
            }
            if (y > 0)
            {
                self.y += y1;
                up.y = y1;
            }
            else
            {
                self.y -= y1;
                up.y = -y1;
            }
        }
        #endregion

        #region  bullet control
        static readonly int  b_max=128;
        protected static BulletPropertyEX[] buff_bullet = new BulletPropertyEX[48];
        protected static void UpdateBullet(int img_id, int extra)
        {
            int id = extra >> 16;
            int index = extra & 0xffff;
            if (buff_bullet[id].bulletstate[index].imageid != img_id)
            {
                buff_img2[img_id].gameobject.SetActive(false);
                buff_img2[img_id].restore = true;
            }
            if (index< buff_bullet[id].max)
            {
                if (buff_bullet[id].bulletstate[index].active)
                {
                    buff_img2[img_id].gameobject.SetActive(true);
                    buff_img2[img_id].transform.localPosition = buff_bullet[id].bulletstate[index].location;
                    buff_img2[img_id].transform.localEulerAngles = buff_bullet[id].bulletstate[index].angle;
                    if (buff_bullet[id].bulletstate[index].update)
                    {
                        int sptindex;
                        if (buff_bullet[id].play != null)
                            sptindex = buff_bullet[id].bulletstate[index].sptindex;
                        else
                            sptindex = buff_bullet[id].spt_index;
                        buff_img2[img_id].spriterender.sprite = buff_spriteex[buff_bullet[id].spt_id].sprites[sptindex];
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
        protected static void UpdateBulletA()
        {
            for (int i = 0; i < 38; i++)
            {
                if (buff_bullet[i].active)
                {
                    int max = buff_bullet[i].max;
                    int sc = buff_bullet[i].s_count2;
                    if(sc>max)
                    {
                        for(int c=max;c<sc;c++)
                        {
                            int index, spt_id = buff_bullet[i].spt_index;
                            int extra = i << 16 | c;
                            if (buff_bullet[i].image.imagepath != null)
                            {
                                index = CreateImageNull2(UpdateBullet, extra);
                                buff_bullet[i].bulletstate[c].imageid = index;
                                buff_img2[index].transform.localScale = origion_scale;
                                buff_img2[index].spriterender.sortingOrder = buff_bullet[i].image.sorting;
                                buff_img2[index].spriterender.sprite = buff_spriteex[buff_bullet[i].spt_id].sprites[spt_id];
                                buff_img2[index].transform.localPosition = buff_bullet[i].bulletstate[c].location;
                                buff_img2[index].transform.localEulerAngles = buff_bullet[i].bulletstate[c].angle;
                            }
                        }
                        buff_bullet[i].max = sc;
                    }
                }
            }
        }
        protected static int RegBullet2(ref BulletPropertyEX target,int start)
        {
            int i = start;
            for (int c = start; c < 48; c++)
            {
                if(!buff_bullet[c].active & buff_bullet[c].max < 1)
                {
                    i = c;
                    break;
                }
            }
            buff_bullet[i] = target;
            if (buff_bullet[i].bulletstate == null)
            {
                buff_bullet[i].bulletstate = new BulletState[b_max];
            }
            buff_bullet[i].active = true;
            buff_bullet[i].max = 0;
            buff_bullet[i].s_count2 = 0;
            buff_bullet[i].s_count = 0;
            if (target.image.imagepath != null)
            {
                if (buff_spriteex[buff_bullet[i].spt_id].sprites == null)
                {
                    if (target.image.sprite != null)
                        CreateSpriteS(buff_bullet[i].spt_id, target.image.sprite);
                    else
                        CreateSpriteGA(buff_bullet[i].spt_id, target.image.grid);
                } 
            }
            else if (buff_bullet[i].eff.son != null)
                buff_bullet[i].eff_id = RegEffet(buff_bullet[i].eff);
            return i;
        }
        protected static void ReSetBullet2(ref BulletPropertyEX target, int id)//only use to no eff bullet
        {
            buff_bullet[id].attack = target.attack;
            buff_bullet[id].edgepoints = target.edgepoints;
            buff_bullet[id].radius = target.radius;
            buff_bullet[id].spt_id = target.spt_id;
            buff_bullet[id].spt_index = target.spt_index;
            buff_bullet[id].move = target.move;
        }
        protected static void ReSetBullet2A(int sid ,int tid)//only use to no eff bullet
        {
            buff_bullet[tid].attack = buff_bullet[sid].attack;
            buff_bullet[tid].edgepoints =buff_bullet[sid].edgepoints;
            buff_bullet[tid].radius = buff_bullet[sid].radius;
            buff_bullet[tid].spt_id = buff_bullet[sid].spt_id;
            buff_bullet[tid].spt_index = buff_bullet[sid].spt_index;
            buff_bullet[tid].move = buff_bullet[sid].move;
        }
        protected static int RegBullet2P(BulletPropertyEX target, int start)
        {
            int i = start;
            while (buff_bullet[i].active | buff_bullet[i].max > 0)
                i++;
            buff_bullet[i] = target;
            if (buff_bullet[i].bulletstate == null)
            {
                buff_bullet[i].bulletstate = new BulletState[128];
            }
            buff_bullet[i].active = true;
            buff_bullet[i].max = 0;
            buff_bullet[i].s_count = 0;
            if (target.image.imagepath != null)
            {
                if(target.image.sprite!=null)
                    buff_bullet[i].spt_id = CreateSpriteS(buff_bullet[i].image.imagepath, target.image.sprite);
                else
                    buff_bullet[i].spt_id= CreateSpriteG(buff_bullet[i].image.imagepath, target.image.grid);
            }
            else if (buff_bullet[i].eff.son != null)
                buff_bullet[i].eff_id = RegEffet(buff_bullet[i].eff);
            return i;
        }
        static void UnRegBullet2(int id)
        {
            buff_bullet[id].active = false;
             if (buff_bullet[id].eff.son != null)
                  DeleteEffect(buff_bullet[id].eff_id);
        }
        protected static void ClearBullet(int id)
        {
            buff_bullet[id].active = false;
            if (buff_bullet[id].image.imagepath != null)
            {
                for (int c = 0; c < buff_bullet[id].max; c++)
                    buff_bullet[id].bulletstate[c].reg = false;
            }
            else
                if (buff_bullet[id].eff.son != null)
            {
                DeleteEffect(buff_bullet[id].eff_id);
                buff_bullet[id].eff.son = null;
            }
            buff_bullet[id].max = 0;
            buff_bullet[id].s_count = 0;
            buff_bullet[id].s_count2 = 0;
        }
        protected static void ClearBulletAll()
        {
            for (int i = 0; i < 48; i++)
            {
                if (buff_bullet[i].eff.son != null)
                {
                    DeleteEffect(buff_bullet[i].eff_id);
                    buff_bullet[i].eff.son = null;
                }
                buff_bullet[i].active = false;
                buff_bullet[i].move = null;
                buff_bullet[i].s_count = 0;
                buff_bullet[i].s_count2 = 0;
                buff_bullet[i].max = 0;
            }
        }
        #endregion

        #region blood manage
        static int[] buff_blood = new int[32];
        static int cycleblood = 16;
        static Vector3 b_def_p = new Vector3(0, -0.66f, 1);//blood default position
        static int arc_sptid;
        public static void InitialBlood(string path)
        {
            arc_sptid = CreateSprite(path);
        }
        static void RecycleBlood(int id)
        {
            buff_img[buff_blood[id]].transform.SetParent(def_transform);
            buff_img[buff_blood[id]].gameobject.SetActive(false);
            buff_blood[cycleblood] = buff_blood[id];
            cycleblood++;
            buff_blood[id] = -1;
        }
        static int RegBlood(Transform parent)
        {
            int c = 0;
            while (buff_blood[c] > 0)
                c++;
            if (cycleblood > 16)
            {
                cycleblood--;
                buff_blood[c] = buff_blood[cycleblood];
                buff_img[buff_blood[c]].transform.SetParent(parent);
                buff_img[buff_blood[c]].gameobject.SetActive(true);
                buff_img[buff_blood[c]].transform.localPosition = b_def_p;
                buff_img[buff_blood[c]].transform.localScale = origion_scale;
                buff_img[buff_blood[c]].transform.localEulerAngles = Vector3.zero;
            }
            else
            {
                int id = CreateImageNull(def_transform);
                buff_img[id].transform.SetParent(parent);
                buff_img[id].transform.localPosition = b_def_p;
                buff_img[id].transform.localEulerAngles = Vector3.zero;
                buff_img[id].spriterender.sortingOrder = 5;
                buff_img[id].spriterender.sprite = buff_spriteex[arc_sptid].sprites[0];
                buff_blood[c] = id;
                buff_img[id].spriterender.material = Mat_Blood;
            }
            return c;
        }
        static void ShowBlood(int id, float percent)
        {
            buff_img[buff_blood[id]].spriterender.material.SetFloat("_Progress", percent);
        }
        protected static BossBlood bossblood = new BossBlood();
        static void CreateBossBlood()//blood img_id
        {
            if (bossblood.for_img_id > 0)
            {
                buff_img[bossblood.back_img_id].gameobject.SetActive(true);
                buff_img[bossblood.for_img_id].gameobject.SetActive(true);
                return;
            }
            int id = bossblood.back_img_id = CreateImageNull(def_transform);
            buff_img[id].transform.localEulerAngles = Vector3.zero;
            buff_img[id].transform.localPosition = new Vector3(0, 4, 1);
            buff_img[id].transform.localScale = origion_scale;
            buff_img[id].spriterender.sortingOrder = 7;
            buff_img[id].spriterender.sprite = buff_spriteex[bossblood.back_spt_id].sprites[0];
            id = bossblood.for_img_id = CreateImageNull(def_transform);
            buff_img[id].transform.localEulerAngles = Vector3.zero;
            buff_img[id].transform.localPosition = new Vector3(0, 4, 1);
            buff_img[id].transform.localScale = origion_scale;
            buff_img[id].spriterender.sortingOrder = 8;
            buff_img[id].spriterender.sprite = buff_spriteex[bossblood.for_spt_id].sprites[0];
            buff_img[id].spriterender.material = Mat_Blood;
        }
        static void RecycleBossBlood()
        {
            buff_img[bossblood.back_img_id].gameobject.SetActive(false);
            buff_img[bossblood.for_img_id].gameobject.SetActive(false);
        }
        protected static void ClearBloodAll()
        {
            for (int i = 0; i < 32; i++)
            {
                if (buff_blood[i] > 0)
                {
                    ClearImage(buff_blood[i]);
                    buff_blood[i] = -1;
                }
            }
            cycleblood = 16;
            if (bossblood.for_img_id > 0)
            {
                ClearImage(bossblood.back_img_id);
                ClearImage(bossblood.for_img_id);
                bossblood.back_img_id = 0;
                bossblood.for_img_id = 0;
            }
        }
        #endregion

        #region pre calculate
        protected static Vector3 oldloaction = Vector3.zero;
        protected static Point2[] areapoints = new Point2[4];
        protected static Point2 xy = new Point2(0, 0);           
       
        protected static bool CircleMoveArea(Vector3 A, Vector3 B, float r, ref Point2[] temp)
        {
            float x = A.x - B.x;
            float y = A.y - B.y;
            float d = x * x + y * y;
            if (d < r)
                return false;
            float c = Mathf.Sqrt(d);
            x = r * x / c;
            y = r * y / c;
            temp[3].x = A.x + y;
            temp[3].y = A.y - x;
            temp[2].x = A.x - y;
            temp[2].y = A.y + x;
            temp[1].x = B.x - y;
            temp[1].y = B.y + x;
            temp[0].x = B.x + y;
            temp[0].y = B.y - x;
            return true;
        }
        protected static bool CollisionCheck(int id, ref BulletState state)
        {
            float x, y, d;
            float z = state.angle.z;
            //if (buff_bullet[id].penetrate == false)
            //{
            //    if (big_boom.active)
            //    {
            //        x = big_boom.location.x - state.location.x;
            //        y = big_boom.location.y - state.location.y;
            //        d = x * x + y * y;
            //        float d1 = big_boom.r * big_boom.r + buff_bullet[id].minrange +
            //            2 * big_boom.r * Mathf.Sqrt(buff_bullet[id].minrange);
            //        if (d < d1)
            //        {
            //            state.active = false;
            //            return true;
            //        }
            //    }
            //    if (current.skill.active & current.skill.force_clear)
            //    {
            //        x = current.skill.location.x - state.location.x;
            //        y = current.skill.location.y - state.location.y;
            //        d = x * x + y * y;
            //        float d1 = current.skill.r * current.skill.r + buff_bullet[id].minrange +
            //            2 * current.skill.r * Mathf.Sqrt(buff_bullet[id].minrange);
            //        if (d < d1)
            //        {
            //            int h = (int)(buff_bullet[id].attack / 5);
            //            current.skill.during -=h;
            //            state.active = false;
            //            return true;
            //        }
            //    }
            //}
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
            if (buff_bullet[id].radius>0)
            {                
                if (area & d<1)
                {
                   collid= CircleToPolygon(new Point2(state.location.x, state.location.y)
                      , buff_bullet[id].radius, areapoints);
                }
            }else
            {
                if (area & d < 1)
                {
                    collid = PToP2(areapoints, RotatePoint2(ref buff_bullet[id].edgepoints, new Point2(state.location.x, state.location.y), z));
                }else
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
        protected static bool CollisionCheck1(int id, ref BulletState state)
        {
            float x = state.location.x;
            float y = state.location.y;
            float z = state.angle.z;
            Point2 temp = new Point2(x, y);
            Point2[] P = new Point2[0];
            if (buff_bullet[id].radius == 0)
                P = RotatePoint2(ref buff_bullet[id].edgepoints, temp, z);
            for (int cc = 0; cc < 20; cc++)
            {
                if (buff_enemy[cc].move != null)
                {
                    float x1 = buff_enemy[cc].location.x - x;
                    float y1 = buff_enemy[cc].location.y - y;
                    float d = x1 * x1 + y1 * y1;
                    if (d < buff_bullet[id].minrange)
                    {
                        if (buff_bullet[id].penetrate == false)
                            state.active = false;
                        damageB(id, cc);
                        return true;
                    }
                    if (d < buff_bullet[id].maxrange)
                    {
                        bool collid = false;
                        if (buff_bullet[id].radius > 0)
                        {
                            if(buff_enemy[cc].radius>0)
                            {
                                if(d<=buff_enemy[cc].minrange+buff_bullet[id].minrange)
                                {
                                    if (buff_bullet[id].penetrate == false)
                                        state.active = false;
                                    damageB(id, cc);
                                    return true;
                                }
                            }
                            else if(buff_enemy[cc].offset!=null)
                                collid = CircleToPolygon(temp, buff_bullet[id].radius, buff_enemy[cc].offset);
                        }                            
                        else
                        {
                            if(buff_enemy[cc].radius>0)
                                collid = CircleToPolygon(buff_enemy[cc].location, buff_enemy[cc].radius, P);
                            else if(buff_enemy[cc].offset!=null)
                                collid = PToP2(buff_enemy[cc].offset, P);
                        }                            
                        if (collid)
                        {
                            damageB(id, cc);
                            if (buff_bullet[id].penetrate == false)
                            {
                                state.active = false;
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        protected static bool area;
        protected static void Calcul_Bullet2(int start, int end, int interval, CollisonCheckA collider)
        {
            for (int i = start; i < end; i += interval)
            {
                int max = buff_bullet[i].max;
                if (buff_bullet[i].active)
                {
                    int sc = buff_bullet[i].s_count;
                    if (buff_bullet[i].shot == null)
                        sc = 0;
                    else if (sc > buff_bullet[i].shot.Length)
                        sc = buff_bullet[i].shot.Length;
                    if (max > 0)
                    {
                        for (int c = 0; c < max; c++)
                        {
                            unsafe
                            {
                                fixed (BulletState* bs = &buff_bullet[i].bulletstate[c])
                                {
                                    if (bs->active)
                                    {
                                        if (buff_bullet[i].move != null)
                                            buff_bullet[i].move(i, ref buff_bullet[i].bulletstate[c]);
                                        else
                                        {
                                            float x = bs->location.x;
                                            float y = bs->location.y;
                                            if (y > 5.5f | y < -5.5f | x > 3f | x < -3f)
                                            {
                                                bs->active = false;
                                                goto label1;
                                            }
                                            else
                                            {
                                                if (bs->extra == 0)
                                                {
                                                    bs->extra++;
                                                    int z = (int)bs->angle.z;
                                                    if (z < 360)
                                                        z += 360;
                                                    if (z > 360)
                                                        z -= 360;
                                                    float s = buff_bullet[i].speed;
                                                    bs->movexyz.x = angle_table[z].x * s;
                                                    bs->movexyz.y = angle_table[z].y * s;
                                                }
                                                bs->location.x += bs->movexyz.x;
                                                bs->location.y += bs->movexyz.y;
                                            }
                                        }
                                        if (bs->active)
                                        {
                                            collider(i, ref *bs);
                                            if (!bs->active)
                                                goto label1;
                                        }
                                        else goto label1;
                                        if (buff_bullet[i].play != null)
                                            buff_bullet[i].play(ref *bs);
                                        goto label2;
                                    }
                                label1:;
                                    if (sc > 0)
                                    {
                                        sc--;
                                        bs->active = true;
                                        bs->angle = buff_bullet[i].shot[sc].angle;
                                        bs->location = buff_bullet[i].shot[sc].location;
                                        bs->extra = 0;
                                        bs->extra2 = 0;
                                        bs->extra3 = 0;
                                        bs->update = true;
                                    }
                                label2:;
                                }
                            }
                        }
                    }  
                    if(sc>0)
                    {
                        for(int c=max;c<128;c++)
                        {
                            sc--;
                            unsafe
                            {
                                fixed (BulletState* bs = &buff_bullet[i].bulletstate[c])
                                {
                                    bs->active = true;
                                    bs->id = c;
                                    bs->angle = buff_bullet[i].shot[sc].angle;
                                    bs->location = buff_bullet[i].shot[sc].location;
                                    bs->extra = 0;
                                    bs->extra2 = 0;
                                    bs->extra3 = 0;
                                    bs->update = true;
                                }
                            }
                            if (sc <= 0)
                            {
                                buff_bullet[i].s_count2 = c;
                                buff_bullet[i].s_count2++;
                                break;
                            }
                        }
                    }
                }
                else if(max>0)
                {
                    int top = 0;
                    for (int c = 0; c < max; c++)
                    {
                        unsafe
                        {
                            fixed (BulletState* bs = &buff_bullet[i].bulletstate[c])
                            {
                                if (bs->active)
                                {
                                    top = c;
                                    top++;
                                    if (buff_bullet[i].move != null)
                                        buff_bullet[i].move(i, ref buff_bullet[i].bulletstate[c]);
                                    else
                                    {
                                        float x = bs->location.x;
                                        float y = bs->location.y;
                                        if (y > 5.5f | y < -5.5f | x > 3f | x < -3f)
                                        {
                                            bs->active = false;
                                            goto label2;
                                        }
                                        else
                                        {
                                            if (bs->extra == 0)
                                            {
                                                bs->extra++;
                                                int z = (int)bs->angle.z;
                                                if (z < 360)
                                                    z += 360;
                                                if (z > 360)
                                                    z -= 360;
                                                float s = buff_bullet[i].speed;
                                                bs->movexyz.x = angle_table[z].x * s;
                                                bs->movexyz.y = angle_table[z].y * s;
                                            }
                                            bs->location.x += bs->movexyz.x;
                                            bs->location.y += bs->movexyz.y;
                                        }
                                    }
                                    if (bs->active)
                                    {
                                        collider(i, ref buff_bullet[i].bulletstate[c]);
                                        if (!bs->active)
                                            goto label2;
                                    }
                                    else goto label2;
                                    if (buff_bullet[i].play != null)
                                        buff_bullet[i].play(ref buff_bullet[i].bulletstate[c]);
                                }
                            label2:;
                            }
                        }
                    }
                    buff_bullet[i].max = top;
                }
                buff_bullet[i].s_count = 0;
            }
        }
        protected static void CaculateEnemy()
        {
            area = CircleMoveArea(oldloaction, core_location, 0.09f, ref areapoints);
            oldloaction = core_location;
            xy.x = core_location.x;
            xy.y = core_location.y;
            int c = 0;
            for (int i = 0; i < 20; i++)
            {
                if (buff_enemy[i].move != null)
                {
                    c++;
                    if (buff_enemy[i].move(i))
                    {
                        if (buff_enemy[i].play != null)
                            buff_enemy[i].play(i);                        
                        float x = buff_enemy[i].location.x;
                        float y = buff_enemy[i].location.y;
                        Point2 temp = new Point2(x, y);
                        if (buff_enemy[i].radius == 0)
                        {
                            if (buff_enemy[i].points_style > 0)
                                buff_enemy[i].offset = GetPointsOffset(buff_enemy[i].location,ref buff_enemy[i].points);
                            else buff_enemy[i].offset = RotatePoint2(ref buff_enemy[i].points, temp, buff_enemy[i].angle.z);
                        }      
                        float x1 = core_location.x - x;
                        float y1 = core_location.y - y;
                        float d = x1 * x1 + y1 * y1;
                        if (d < buff_enemy[i].minrange)
                        {
                            damageC(i);
                        }else
                        if (buff_enemy[i].radius>0)
                        {
                            if(area)
                            {
                                if(d<1)
                                {
                                    if (CircleToPolygon(buff_enemy[i].location, buff_enemy[i].radius, areapoints))
                                        damageC(i);
                                }
                            }                            
                        }
                        else
                        {
                            if(d < buff_enemy[i].maxrange)
                            {                               
                                if (area)
                                {
                                    if(PToP2(areapoints,buff_enemy[i].offset))
                                        damageC(i);
                                }
                                else
                                {
                                    if (CircleToPolygon(core_location, 0.09f, buff_enemy[i].offset))
                                        damageC(i);
                                }
                            }                           
                        }                        
                        if (big_boom.active)
                        {
                            x = big_boom.location.x - buff_enemy[i].location.x;
                            y = big_boom.location.y - buff_enemy[i].location.y;
                            d = x * x + y * y;
                            float d1 = big_boom.r * big_boom.r + buff_enemy[i].minrange +
                                2 * big_boom.r * Mathf.Sqrt(buff_enemy[i].minrange);
                            if (d < d1)
                            {
                                buff_enemy[i].currentblood -= big_boom.attack * big_boom.during;
                                if (buff_enemy[i].currentblood < 0)
                                {
                                    buff_enemy[i].die = true;
                                    generateA(10 + currentwave, buff_enemy[i].location);
                                }
                            }
                        }
                        if (current.skill.active)
                        {
                            x = current.skill.location.x - buff_enemy[i].location.x;
                            y = current.skill.location.y - buff_enemy[i].location.y;
                            d = x * x + y * y;
                            float d1 = current.skill.r * current.skill.r + buff_enemy[i].minrange +
                                2 * current.skill.r * Mathf.Sqrt(buff_enemy[i].minrange);
                            if (d < d1)
                            {
                                buff_enemy[i].currentblood -= current.skill.attack;
                                if (buff_enemy[i].currentblood < 0)
                                {
                                    buff_enemy[i].die = true;
                                    generateA(10 + currentwave, buff_enemy[i].location);
                                }
                            }
                        }
                    }
                }
            }
            if (c == 0)
            {
                if (crt_wave.sum == 0)
                {
                    if (currentwave >= buff_wave.wave.Count)
                    {
                        gameover = true;
                    }
                    else
                    {
                        crt_wave = buff_wave.wave[currentwave];
                        currentwave++;
                    }                   
                }                                
                return;
            }
            if (crt_wave.enemyppt.enemy.boss == false)
                crt_wave.staytime--;
            if (crt_wave.staytime < 0)
            {
                if (currentwave >= buff_wave.wave.Count)
                {
                    gameover = true;
                }else
                {
                    crt_wave = buff_wave.wave[currentwave];
                    currentwave++;
                }
            }
        }
        #endregion

        #region prop contorl
        protected static System.Random lucky = new System.Random();
        protected static PropBase[] buff_prop = new PropBase[8];
        protected static int propsum;
        protected static bool generate;
        protected static Vector3 build_position;
        protected static int build_type;
        static readonly string[] prop_all = { "Picture/prop_u", "Picture/prop_b" };
        static Grid prop_grid = new Grid(3, 2);
        static int[] pro_sptex_id;
        protected static void Prop_Inital()
        {
            pro_sptex_id = new int[2];
            pro_sptex_id[0] = CreateSpriteG(prop_all[0], prop_grid);
            pro_sptex_id[1] = CreateSpriteG(prop_all[1], prop_grid);
        }
        protected static void Prop_Destory()
        {
            for (int i = 0; i < 8; i++)
                if (buff_prop[i].created)
                {
                    buff_prop[i].created = false;
                    buff_prop[i].state = false;
                }
            propsum = 0;
        }
        protected static void Prop_Move()
        {
            int c = propsum;
            for (int i = 0; i < 8; i++)
            {
                if (buff_prop[i].state)
                {
                    if (buff_prop[i].closely)
                    {
                        Aim1(ref buff_prop[i].location, ref core_location, 0.1f, ref buff_prop[i].motion);
                        float x = buff_prop[i].location.x;
                        float y = buff_prop[i].location.y;
                        x -= core_location.x; y -= core_location.y;
                        float d = x * x + y * y;
                        if (d < 0.01)
                        {
                            buff_prop[i].closely = false;
                            buff_prop[i].state = false;
                            buff_prop[i].cycle = true;
                            propcollid(buff_prop[i].style);
                        }
                    }
                    else
                    {
                        if (buff_prop[i].location.x > 2.6f)
                        {
                            buff_prop[i].motion.x = -0.02f - (float)lucky.NextDouble() / 50;
                        }
                        if (buff_prop[i].location.x < -2.6f)
                        {
                            buff_prop[i].motion.x = 0.02f + (float)lucky.NextDouble() / 50;
                        }
                        if (buff_prop[i].location.y > 4.78f)
                        {
                            buff_prop[i].motion.y = -0.02f - (float)lucky.NextDouble() / 50;
                            if (buff_prop[i].motion.x == 0)
                                buff_prop[i].motion.x = -0.04f + (float)lucky.NextDouble() / 12.5f;
                        }
                        if (buff_prop[i].location.y < -4.78f)
                        {
                            buff_prop[i].motion.y = 0.02f + (float)lucky.NextDouble() / 50;
                            if (buff_prop[i].motion.x == 0)
                                buff_prop[i].motion.x = -0.04f + (float)lucky.NextDouble() / 12.5f;
                        }
                        float x = buff_prop[i].location.x += buff_prop[i].motion.x;
                        float y = buff_prop[i].location.y += buff_prop[i].motion.y;
                        x -= core_location.x; y -= core_location.y;
                        float d = x * x + y * y;
                        if (d < 1)
                            buff_prop[i].closely = true;
                    }
                    buff_prop[i].extra++;
                    if (buff_prop[i].extra > 59)
                        buff_prop[i].extra = 0;
                    if (buff_prop[i].extra % 10 == 0)
                    {
                        buff_prop[i].spt_index = buff_prop[i].extra / 10;
                        buff_prop[i].up_spt = true;
                    }
                    c--;
                    if (c < 1)
                        break;
                }
            }

        }
        static void UpdateProp2(int img_id,int extra)
        {
            if (buff_prop[extra].state)
            {
                buff_img2[img_id].transform.localPosition = buff_prop[extra].location;
                if (buff_prop[extra].up_spt)
                {
                    buff_img2[img_id].gameobject.SetActive(true);
                    buff_img2[img_id].spriterender.sprite = buff_spriteex[pro_sptex_id[buff_prop[extra].style]].sprites[buff_prop[extra].spt_index];
                    buff_prop[extra].up_spt = false;
                }
            }
            else
                if (buff_prop[extra].cycle)
            {
                buff_img2[img_id].gameobject.SetActive(false);
                buff_prop[extra].cycle = false;
            }
        }
        protected static void UpdateProp2A()
        {
            if (generate)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (buff_prop[i].state == false)
                    {
                        int id;
                        if (buff_prop[i].created)
                        {
                            id = buff_prop[i].img_id;
                            buff_img2[id].gameobject.SetActive(true);
                        }
                        else
                        {
                            id = buff_prop[i].img_id = CreateImageNull2(UpdateProp2,i);
                            buff_img2[id].spriterender.sortingOrder = 7;
                            buff_img2[id].transform.localScale = origion_scale;
                            buff_prop[i].style = build_type;
                            propsum++;
                        }
                        buff_img2[id].spriterender.sprite = buff_spriteex[pro_sptex_id[build_type]].sprites[0];
                        buff_prop[i].location = build_position;
                        buff_prop[i].motion.x = 0;
                        buff_prop[i].motion.y = -0.03f;
                        buff_prop[i].style = build_type;
                        buff_prop[i].state = true;
                        buff_prop[i].created = true;
                        generate = false;
                        return;
                    }
                }
                generate = false;
            }
        }
        #endregion

    }
}
