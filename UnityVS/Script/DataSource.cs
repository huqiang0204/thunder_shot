using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.UnityVS.Script
{        
    class DataSource:StaticParameter
    {
        #region string resouce
        static string img_zhihui = "Picture/p-06d";
        static string img_nouhuo = "Picture/p-03c";
        static string img_jifeng = "Picture/p-05d";
        static string img_thunderboll = "Picture/m-04a";
        static string img_mijia = "Picture/w-02a";
        static string img_p02b = "Picture/p-02b";
        static string img_p04b = "Picture/p-b04";
        static string img_circle = "Picture/magic_shiled";
        static string bigboom_s = "Picture/BigBoom";
        static string enemy_a = "Picture/enemy_a";
        static string enemy_a10 = "Picture/a-10";
        static string enemy_b = "Picture/enemy_b";
        #endregion

        #region edge points
        static readonly Point2[] points_01 = new Point2[]{ new Point2(71f,0.58f), new Point2(0f,0.4832685f),
                 new Point2(289f,0.58f) ,new Point2(217f,0.44877f),new Point2(143f,0.44877f)};
        static readonly Point2[] points_02 = new Point2[]{ new Point2(180f,0.65f), new Point2(63f,0.863133f),
                 new Point2(0f,0.67f) ,new Point2(296f,0.863133f)};
        static readonly Point2[] points_meteor = new Point2[] { new Point2(-0.546423f, -0.09401575f), new Point2(0f, 0.792303f),
                    new Point2(0.5756082f, -0.1933437f),
                    new Point2(0.4163094f, -0.4766243f), new Point2(-0.02686294f, -0.8191688f),
                    new Point2(-0.2146185f, -0.8054901f),new Point2(-0.4541725f,-0.4700786f)};
        #endregion

        #region sprite info
        static readonly SpriteInfo[] Sprite_Moth =new SpriteInfo[] { new SpriteInfo() {rect=new Rect(3,307,145,205),pivot=pivot_center},//main
                new SpriteInfo() {rect=new Rect(152,169,251,208),pivot=pivot_center},//minleg
                new SpriteInfo() {rect=new Rect(154,424,75,81),pivot=new Vector2(0.1208f,0.0877f)},//minleg
                new SpriteInfo() {rect=new Rect(278,382,36,88),pivot=new Vector2(0.5f,0.8595f)},//bigleg1
                new SpriteInfo() {rect=new Rect(235,417,42,86),pivot=new Vector2(0.62463f,0.8553f)},//bigleg2
                new SpriteInfo() {rect=new Rect(408,420,41,68),pivot=new Vector2(0.66322f,0.8726f)},//bigleg1            
        };

        #endregion

        #region goods image plane
        
        public static string[] img_all_plane = new string[] { img_nouhuo, img_jifeng, img_zhihui };
        #endregion

        #region goods image second ewapon
                
        public static string[] img_all_second = new string[] { img_thunderboll,img_p04b };
        #endregion

        #region goods iamge wing
        public static string[] img_all_wing = new string[] { img_mijia,img_p02b };
        #endregion

        #region goods image skill        
        public static string[] img_all_skill = new string[] { img_circle };
        #endregion

        #region effect Big boom
        [Obsolete]
        static void SetEmission(int alltime, ref EffectObject[] eo, int offset, EmissionProperty ep)
        {
            if (ep.random)
            {
                System.Random temp = new System.Random();
                for (int i = 0; i < ep.sum; i++)
                {
                    float a = (float)temp.NextDouble();
                    int c = i + offset;
                    eo[c].scale.x = eo[c].scale.y = (ep.scale.y - ep.scale.x) * a + ep.scale.x;
                    eo[c].origina.z = (ep.angle.y - ep.angle.x) * a + ep.angle.x;
                    eo[c].originHSVA.r = (ep.colorh.y - ep.colorh.x) * a + ep.colorh.x;
                    eo[c].originHSVA.g = (ep.colors.y - ep.colors.x) * a + ep.colors.x;
                    eo[c].originHSVA.b = (ep.colorv.y - ep.colorv.x) * a + ep.colorv.x;
                    eo[c].originHSVA.a = (ep.colora.y - ep.colora.x) * a + ep.colora.x;
                }
            }
            else
            {
                float t = 1 / (float)ep.sum;
                for (int i = 0; i < ep.sum; i++)
                {
                    float a = i * t;
                    int c = i + offset;
                    eo[c].scale.x = eo[c].scale.y = (ep.scale.y - ep.scale.x) * a + ep.scale.x;
                    eo[c].origina.z = (ep.angle.y - ep.angle.x) * a + ep.angle.x;
                    eo[c].originHSVA.r = (ep.colorh.y - ep.colorh.x) * a + ep.colorh.x;
                    eo[c].originHSVA.g = (ep.colors.y - ep.colors.x) * a + ep.colors.x;
                    eo[c].originHSVA.b = (ep.colorv.y - ep.colorv.x) * a + ep.colorv.x;
                    eo[c].originHSVA.a = (ep.colora.y - ep.colora.x) * a + ep.colora.x;
                }
            }
        }
        public static EffectProperty explode_fire = new EffectProperty()
        {
            path = "Picture/Explode",
            grid = def_4x4
        };
       
        public static EffcetBase eff_explode_big = new EffcetBase()
        {
            location = new Vector3(0, 2, 1),
            alltime = 150,
            son = new EffectObject[] {
            new EffectObject() {imgpath=bigboom_s,sptindex=0,scale=def_scale,offset=def_location,sprite=def_sprite1280x512_5x2,
                mat=Mat_effect,sort=9,
                originHSVA=new Color(0.223f,0.8f,0,0.5f),
                angleZcurve=new Point2[] { new Point2(0,0),new Point2(1,90f)},
                scalecurve_x =new Point2[] { new Point2(0,0f),new Point2(0.02f,3f),new Point2(1,3f)},
                scalecurve_y =new Point2[] { new Point2(0,0f),new Point2(0.02f,3f),new Point2(1,3f)},
                colorH =new Point2[] { new Point2(0, 0.223f), new Point2(0.3f, 0.0722f), new Point2(1,0.0722f)},
                colorV =new Point2[] { new Point2(0,0),new Point2(0.05f,1), new Point2(0.3f, 0.4f), new Point2(1,0)}
                },
            new EffectObject() {imgpath=bigboom_s,sptindex=1,origina=def_angle90 ,scale=def_scale,
                mat=Mat_effect,sort=9,
                offset =def_location,
                originHSVA=new Color(0.223f,0.8f,0,0.5f),
                angleZcurve=new Point2[] { new Point2(0,90f),new Point2(1,0)},
                scalecurve_x =new Point2[] { new Point2(0,0f),new Point2(0.02f,3f),new Point2(1,3f)},
                scalecurve_y =new Point2[] { new Point2(0,0f),new Point2(0.02f,3f),new Point2(1,3f)},
                colorH =new Point2[] { new Point2(0, 0.223f), new Point2(0.3f, 0.0722f), new Point2(1,0.0722f)},
                colorV =new Point2[] { new Point2(0,0),new Point2(0.05f,1), new Point2(0.3f, 0.4f), new Point2(1,0)}
            },

            new EffectObject() {imgpath=bigboom_s,sptindex=2,origina=def_angle180,scale=def_scale,
                mat=Mat_effect,sort=9,
                offset =def_location,
                originHSVA=new Color(0.223f,0.8f,0,0.5f),
            angleZcurve=new Point2[] { new Point2(0,180f),new Point2(1,270f)},
                scalecurve_x =new Point2[] { new Point2(0,0f),new Point2(0.02f,3f),new Point2(1,3f)},
                scalecurve_y =new Point2[] { new Point2(0,0f),new Point2(0.02f,3f),new Point2(1,3f)},
                colorH =new Point2[] { new Point2(0, 0.223f), new Point2(0.3f, 0.0722f), new Point2(1,0.0722f)},
                colorV =new Point2[] { new Point2(0,0),new Point2(0.05f,1), new Point2(0.3f, 0.4f), new Point2(1,0)}
            },

            new EffectObject() {imgpath=bigboom_s ,sptindex=3,origina=def_angle90,scale=def_scale,
                mat=Mat_effect,sort=9,
                offset =def_location,originHSVA=new Color(0.223f,0,0,0.5f),
            angleZcurve=new Point2[] { new Point2(0,270f),new Point2(1,90f)},
            scalecurve_x =new Point2[] { new Point2(0,0f),new Point2(0.02f,3f),new Point2(1,3f)},
            scalecurve_y =new Point2[] { new Point2(0,0f),new Point2(0.02f,3f),new Point2(1,3f)},
            colorH =new Point2[] { new Point2(0, 0.223f), new Point2(0.3f, 0.0722f), new Point2(1,0.0722f)},
                colorV =new Point2[] { new Point2(0,0),new Point2(0.05f,1), new Point2(0.3f, 0.4f), new Point2(1,0)}
            },

            new EffectObject() {imgpath=bigboom_s,sptindex=4,scale=new Vector3(0.2f,0.2f),offset=def_location,
                mat=Mat_effect,sort=10,
            originHSVA=new Color(0.223f,0.8f,0,0.5f),
                scalecurve_x =new Point2[] { new Point2(0,0.2f),new Point2(0.05f,4f),new Point2(1,4f)},
                scalecurve_y =new Point2[] { new Point2(0,0.2f),new Point2(0.05f,4f),new Point2(1,4f)},
                colorH =new Point2[] { new Point2(0, 0.223f), new Point2(0.3f, 0.0722f), new Point2(1,0.0722f)},
                colorV =new Point2[] { new Point2(0,0),new Point2(0.02f,1), new Point2(0.3f, 0.3f), new Point2(1,0)}
            },

            new EffectObject() {imgpath=bigboom_s,sptindex=5,scale=new Vector3(0.2f,0.2f),offset=def_location,
                mat=Mat_effect,sort=10,
            originHSVA=new Color(0.223f,0.8f,0,0.5f),
                scalecurve_x =new Point2[] { new Point2(0,0.2f),new Point2(0.05f,4f),new Point2(1,4f)},
                scalecurve_y =new Point2[] { new Point2(0,0.2f),new Point2(0.05f,4f),new Point2(1,4f)},
                colorV =new Point2[] { new Point2(0,0),new Point2(0.02f,1), new Point2(0.3f, 0.3f), new Point2(1,0)}
            },

            new EffectObject() {imgpath=bigboom_s,sptindex=6,scale=new Vector3(0.2f,0.2f),offset=def_location,
                mat=Mat_effect,sort=10,
            originHSVA=new Color(0.223f,0.8f,0,0.5f),
                scalecurve_x =new Point2[] { new Point2(0,0.2f),new Point2(0.05f,4f),new Point2(1,4f)},
                scalecurve_y =new Point2[] { new Point2(0,0.2f),new Point2(0.05f,4f),new Point2(1,4f)},
                colorV =new Point2[] { new Point2(0,0),new Point2(0.02f,1), new Point2(0.3f, 0.3f), new Point2(0.7f,0),new Point2(1, 0) }
            },

            new EffectObject() {imgpath=bigboom_s,sptindex=7,scale=new Vector3(3f,3f),offset=def_location,
                mat=Mat_effect,sort=10,
                originHSVA=new Color(0.223f,0.8f,0,0.5f),
            angleZcurve=new Point2[] { new Point2(0,0),new Point2(1,90f)},
            colorH =new Point2[] { new Point2(0, 0.223f), new Point2(0.3f, 0.0722f), new Point2(1,0.0722f)},
                colorV =new Point2[] { new Point2(0,0),new Point2(0.05f,0), new Point2(0.1f, 1f), new Point2(1,0)}},

            new EffectObject() {imgpath=bigboom_s,sptindex=8,scale=new Vector3(3f,3f),offset=def_location,
                mat=Mat_effect,sort=10,
                originHSVA=new Color(0.223f,0.8f,0,0.5f),
                angleZcurve=new Point2[] { new Point2(0,0f),new Point2(1,90f)},
                colorV =new Point2[] { new Point2(0,0),new Point2(0.05f,0), new Point2(0.1f, 1f), new Point2(0.7f, 0), new Point2(1,0)}
            },

            new EffectObject() {imgpath=bigboom_s,sptindex=9,scale=new Vector3(3f,3f),offset=def_location,
                mat=Mat_effect,sort=10,
                originHSVA=new Color(0.223f,0.8f,0,0.5f),
            angleZcurve=new Point2[] { new Point2(0,360f),new Point2(1,270f)},
                colorV =new Point2[] { new Point2(0,0),new Point2(0.05f,0), new Point2(0.1f, 1f), new Point2(0.7f, 0), new Point2(1,0)}}
            }
        };
        #endregion

        #region skill resource
        public static EffcetBase eff_shiled = new EffcetBase() {
            location = def_location,
            alltime =360, son=new EffectObject[] { new EffectObject() {
            mat=Mat_effect,sort=9,scale=def_scale,
         imgpath="Picture/magic099",originHSVA=new Color(0,0,1,1),angleZcurve=new Point2[] {
         new Point2(0,0),new Point2(0.5f,360),new Point2(0.5f,0),new Point2(1,360)} } } };
        static Skill skill_shiled = new Skill() {gold=10000, force_clear=true, follow=true, r=1.4f, energy_ec=4,energy_max=1000,eff_b=eff_shiled,energy_re=1f};
        public static Skill[] all_skill = new Skill[] { skill_shiled};
        #endregion

        #region down red laser
        static SpriteInfo[] laser_sprite = new SpriteInfo[] {
            new SpriteInfo { rect=new Rect(3,299,33,34),pivot=pivot_center},
            new SpriteInfo { rect=new Rect(49,268,53,64),pivot=pivot_center},
            new SpriteInfo { rect=new Rect(113,275,53,48),pivot=pivot_center},
            new SpriteInfo { rect=new Rect(11,145,47,101),pivot=new Vector2(0.5f,0.996f)},
            new SpriteInfo { rect=new Rect(60,17,47,228),pivot=new Vector2(0.5f,0.996f)},
            new SpriteInfo { rect=new Rect(175,15,51,228),pivot=new Vector2(0.5f,0.996f)}
        };
        public static EffcetBase eff_laser_red = new EffcetBase()
        {
            alltime=150,
            son=new EffectObject[] {
            new EffectObject() {imgpath="Picture/bullet_laser_red", sprite=laser_sprite,sort=5,scale=def_scale,
                originHSVA=new Color(0,0,1,0),
                colorA =new Point2[] {new Point2(0,1),new Point2(0.12f,0),new Point2(1,0)
            } },
            new EffectObject() {imgpath="Picture/bullet_laser_red", sptindex =1,sort=6,scale=def_scale,
                originHSVA=new Color(0,0,1,0),
                colorA =new Point2[] {new Point2(0,0),new Point2(0.12f,1),new Point2(0.8f,0),new Point2(1,0) 
            } },
            new EffectObject() {imgpath="Picture/bullet_laser_red", sptindex =2,sort=5,scale=new Vector3(2.7f,1),
                originHSVA=new Color(0,0,1,0),
                 colorA =new Point2[] {new Point2(0,0),new Point2(0.12f,1),new Point2(0.9f,0),new Point2(1,0)
            } },
            new EffectObject() {imgpath="Picture/bullet_laser_red", sptindex =2,sort=6,mat=Mat_effect,scale=new Vector3(2.7f,1),
                originHSVA=new Color(0,0,0,1),
                 colorV =new Point2[] { new Point2(0,0),new Point2(0.2f,0),new Point2(0.3f,1),new Point2(0.6f,0),new Point2(1,0)
            } },
            new EffectObject() {imgpath="Picture/bullet_laser_red", sptindex =3,sort=7,scale=new Vector3(3,0.2f),
                originHSVA=new Color(0,0,1,0),
                scalecurve_y=new Point2[] {new Point2(0,0f),new Point2(0.01f,0.2f),new Point2(0.12f,12), new Point2(0.9f, 12),new Point2(1,0.2f) },
                colorA=new Point2[] { new Point2(0,0),new Point2(0.12f,0),new Point2(0.04f,1),new Point2(0.6f,1),new Point2(0.8f,0), new Point2(1,0)
            } },
            new EffectObject() {imgpath="Picture/bullet_laser_red", sptindex =4,sort=8,mat=Mat_effect,scale=new Vector3(3.5f,3.8f),
                originHSVA=new Color(0,0,0,1),
                colorV =new Point2[] { new Point2(0,0),new Point2(0.2f,0),new Point2(0.3f,1),new Point2(0.6f,0),new Point2(1,0)
            } },
            new EffectObject() {imgpath="Picture/bullet_laser_red", sptindex =5,sort=5,scale=new Vector3(3f,3.8f),
                originHSVA=new Color(0,0,1,0),
                colorA=new Point2 [] { new Point2(0,0),new Point2(0.6f,0),new Point2(0.7f,1) ,new Point2(1,0)} }
            }

        };
        #endregion

        #region Animation Moth
        static AnimatStruct Moth_left_minleg = new AnimatStruct()
        {
            spt_index = 2,
            pivot = new Point2(87.70938f, 0.1882263f),
            scale = def_scale_l,
            location = def_location,
            img_sort = 6,anglecurve=new Point2[] {new Point2(0,0),new Point2(0.1f,30),new Point2(0.9f,30),new Point2(1,0) }
            
        };
        static AnimatStruct Moth_right_minleg = new AnimatStruct()
        {
            spt_index = 2,
            pivot = new Point2(272.2906f, 0.1882263f),
            scale = def_scale,
            location = def_location,
            img_sort = 6,
            anglecurve = new Point2[] { new Point2(0, 0), new Point2(0.1f, -30), new Point2(0.9f, -30), new Point2(1, 0) }
        };
        static AnimatStruct Moth_left_minleg1 = new AnimatStruct()
        {
            spt_index = 2,
            angle = new Vector3(0, 0, 10),
            pivot = new Point2(142.9449f, 0.3141848f),
            scale = def_scale_l,
            location = def_location,
            img_sort = 6,
            anglecurve = new Point2[] { new Point2(0, 10), new Point2(0.1f, 40), new Point2(0.9f, 40), new Point2(1, 10) }
        };        
        static AnimatStruct Moth_right_minleg1 = new AnimatStruct()
        {
            spt_index = 2,
            angle = new Vector3(0, 0, 350),
            pivot = new Point2(217.0551f, 0.3141848f),
            scale = def_scale,
            location = def_location,
            img_sort = 6,
            anglecurve = new Point2[] { new Point2(0, -10), new Point2(0.1f, -40), new Point2(0.9f, -40), new Point2(1, -10) }
        };

        static AnimatStruct Moth_left_bigleg_01 = new AnimatStruct()
        {
            spt_index = 5,
            angle = new Vector3(0, 0, 20),
            pivot = new Point2(188.3202f, 0.6124921f),
            scale = def_scale_l,
            location = def_location,
            img_sort = 6,
            anglecurve = new Point2[] { new Point2(0, 20), new Point2(0.1f, 50), new Point2(0.9f, 50), new Point2(1, 20) }
        };
        static AnimatStruct Moth_right_bigleg_01 = new AnimatStruct()
        {
            spt_index = 5,
            angle = new Vector3(0, 0,340),
            pivot= new Point2(171.6798f, 0.6124921f),
            scale = def_scale,
            location = def_location,
            img_sort = 6,
            anglecurve = new Point2[] { new Point2(0, -20), new Point2(0.1f, -50), new Point2(0.9f, -50), new Point2(1, -20) }
        };
        static AnimatStruct Moth_left_bigleg1_01 = new AnimatStruct()
        {
            spt_index = 5,
            pivot = new Point2(191.6888f, 0.6376349f),
                      
            scale = def_scale,
            location = def_location,
            img_sort = 6,
            anglecurve = new Point2[] { new Point2(0, 0), new Point2(0.1f, -30), new Point2(0.9f, -30), new Point2(1, 0) }
        };
        static AnimatStruct Moth_right_bigleg1_01 = new AnimatStruct()
        {
            spt_index = 5,
            pivot = new Point2(168.3112f, 0.6376349f),
            scale = def_scale_l,
            location = def_location,
            img_sort = 6,           
            anglecurve = new Point2[] { new Point2(0, 0), new Point2(0.1f, 30), new Point2(0.9f, 30), new Point2(1, 0) }
        };

        static AnimatStruct Moth_left_bigleg = new AnimatStruct()
        {
            spt_index = 3,
            pivot = new Point2(76.74481f, 0.4961952f),
            scale = def_scale_l,
            location = def_location,
            img_sort = 5,
            son = new AnimatStruct[] { Moth_left_bigleg_01 },
            anglecurve = new Point2[] { new Point2(0, 0), new Point2(0.1f, -60), new Point2(0.9f, -60), new Point2(1, 0) }
        };
        static AnimatStruct Moth_right_bigleg = new AnimatStruct()
        {
            spt_index = 3,
            pivot = new Point2(283.2552f, 0.4961952f),
            scale = def_scale,
            location = def_location,
            img_sort = 5,
            son = new AnimatStruct[] { Moth_right_bigleg_01 },
            anglecurve = new Point2[] { new Point2(0, 0), new Point2(0.1f, 60), new Point2(0.9f, 60), new Point2(1, 0) }
        };
        static AnimatStruct Moth_left_bigleg1 = new AnimatStruct()
        {
            spt_index = 4,
            pivot = new Point2(248.2084f, 0.4485087f),          
            scale = def_scale_l,
            location = def_location,
            img_sort = 5,
            son = new AnimatStruct[] { Moth_left_bigleg1_01 },
            anglecurve = new Point2[] { new Point2(0, 0), new Point2(0.1f, 30), new Point2(0.9f, 30), new Point2(1, 0) }
        };
        static AnimatStruct Moth_right_bigleg1 = new AnimatStruct()
        {
            spt_index = 4,            
            pivot = new Point2(111.7916f, 0.4485087f),
            scale = def_scale,
            location = def_location,
            img_sort = 5,
            son = new AnimatStruct[] { Moth_right_bigleg1_01 },
            anglecurve = new Point2[] { new Point2(0, 0), new Point2(0.1f, -30), new Point2(0.9f, -30), new Point2(1, 0) }
        };

        static AnimatStruct Moth_left_wing = new AnimatStruct()
        {
            spt_index = 1,
            pivot = new Point2(93f, 1.454427f),
            scale = def_scale_l,
            location = new Vector3(-1.452f, -0.084f, 1),
            img_sort = 3
        };
        static AnimatStruct Moth_right_wing = new AnimatStruct()
        {
            spt_index = 1,
            pivot = new Point2(266f, 1.454427f),
            scale = def_scale,
            location = new Vector3(1.452f, -0.084f, 1),
            img_sort = 3
        };

        public static AnimatStruct Moth_Ani = new AnimatStruct()
        {
            spt_index = 0,
            scale = def_scale,
            location = def_location,
            img_sort = 4,
            sprite=Sprite_Moth,
            img_path = "Picture/boss-05b",
            son = new AnimatStruct[] { Moth_left_wing ,Moth_right_wing,Moth_left_bigleg,Moth_left_bigleg1,
               Moth_right_bigleg,Moth_right_bigleg1, Moth_right_minleg1,
            Moth_left_minleg,Moth_left_minleg1,Moth_right_minleg}
        };
        #endregion

        #region power dispose
        public readonly static Power[] allpower = new Power[]
        {
            new Power() {energy=100 }
        };
        #endregion

        #region bullet
        public static BulletPropertyEX b_def_redlaser = new BulletPropertyEX()
        {
            penetrate=true,
            maxrange =120f,
            minrange = 0.6f,
            edgepoints=new Point2[] { new Point2(90,0.556f),new Point2(270,0.556f),new Point2(183.6735f,8.67783f) , new Point2(176.3264f, 8.67783f) },
            attack = 100,eff=eff_laser_red,move=BulletMove.B_Fixd_110,
        };
        public static BulletPropertyEX b_def_b3 = new BulletPropertyEX()
        {
            image = new ImageProperty()
            {grid=def_4x2, scale = def_scale, imagepath = "Picture/bullet3", sorting = 6 },
            radius = 0.1f,
            maxrange = 0.0182f,
            minrange = 0.0182f,
            attack = 30,
            speed = 0.03f
        };
        public static BulletPropertyEX b_def_b03b = new BulletPropertyEX()
        {
            image = new ImageProperty()
            { scale = def_scale, imagepath = "Picture/bullet-03", sorting = 6 },
            maxrange = 0.0835f,
            minrange = 0.03f,
            attack=32,
            edgepoints = new Point2[]{
               new Point2(21.6f,0.22508f), new Point2(158f,0.2487211f), 
               new Point2(202f,0.2487211f), new Point2(338.4f,0.22508f) },
            speed = 0.04f
        };
        public static BulletPropertyEX b_def_b04y = new BulletPropertyEX()
        {
            image = new ImageProperty()
            { scale = def_scale, imagepath = "Picture/bullet-04-y", sorting = 6 },
            maxrange = 0.0835f,
            minrange = 0.03f,
            attack = 33,
            edgepoints = new Point2[]{
               new Point2(21.6f,0.22508f), new Point2(158f,0.2487211f), 
               new Point2(202f,0.2487211f), new Point2(338.4f,0.22508f) },
            speed = 0.2f
        };
        public static BulletPropertyEX b_bigblueboll = new BulletPropertyEX()
        {
            penetrate = true,
            image = new ImageProperty()
            { scale = def_scale, imagepath = "Picture/boll01", sorting = 6 ,grid=def_3x2},
            radius = 0.5f,play=BulletMove.Play_6_1,
            maxrange = 0.2581f,
            minrange = 0.2581f,
            attack = 300,
            speed = 0.03f
        };
        #endregion

        #region plane bullet
        public static BulletPropertyEX b_chain = new BulletPropertyEX()
        {
            image = new ImageProperty()
            {
                imagepath = "Picture/p-b04",
                scale = new Vector3(1f, 1f),
                sorting = 4,sprite=new SpriteInfo[] { new SpriteInfo() {rect=new Rect(0,0,66,133), pivot=new Vector2(0.5f,1)} },
            },
            attack = 30,
            speed=0.3f
        };
        public static BulletPropertyEX b_thunder = new BulletPropertyEX()
        {
            image = new ImageProperty() {grid=new Grid(4,4),
                imagepath = "Picture/thunderboll" ,sorting=2},
            play=BulletMove.Play_Def16,        
            radius = 0.3f,
            maxrange = 1.2f,
            minrange = 0.1f,
            attack = 50,
            speed = 0.2f,
            move = BulletMove.B_LockEnemy
        };
        public static BulletPropertyEX b_laser = new BulletPropertyEX()
        {
            image = new ImageProperty() {grid=new Grid(7,1),
                imagepath = "Picture/wing-t05" ,scale=new Vector3(1,2.5f),sorting=7},
            play=BulletMove.Play_7_1,
            edgepoints = new Point2[]{
               new Point2(358.08f,4.9327f), new Point2(1.92f,4.9327f),
               new Point2(178.08f,-4.93f), new Point2(181.92f,4.9327f) },
            maxrange = 20f,
            minrange = 0.8f,
            attack = 20,penetrate=true,
            move = BulletMove.B_Laser_level1
        };
        public static BulletPropertyEX b_z_zhzd = new BulletPropertyEX()
        {
            attack = 20,
            image = new ImageProperty() { imagepath = "Picture/zhzd", scale = new Vector3(1f, 1f) ,sorting=2},
            edgepoints = new Point2[]{
               new Point2(33.46f,0.3700994f), new Point2(0f,1.0988618f),
               new Point2(328f,0.362133f), new Point2(183f,1.030481f)},
            maxrange = 5,
            minrange = 0.5f,
            speed = 0.4f
        };
        public static BulletPropertyEX b_laser_r = new BulletPropertyEX()
        {
            image = new ImageProperty()
            {
                sprite = new SpriteInfo[] { new SpriteInfo() { rect = new Rect(124, 9, 78, 352), pivot = new Vector2(0.5f, 0) } },
                imagepath = "Picture/laser-01c",
                scale = new Vector3(1.5f, 2.85f),
                sorting = 4
            },
            edgepoints = new Point2[]{
               new Point2(-0.645f,0f), new Point2(-0.645f,10f),
               new Point2(0.645f,10f), new Point2(0.645f,0f) }, 
            maxrange = 1.6f,
            attack = 80,
        };
        public static BulletPropertyEX b_laser_w= new BulletPropertyEX()
        {
            image = new ImageProperty()
            {
                sprite = new SpriteInfo[] {new SpriteInfo() { rect = new Rect(102,5,26,228), pivot = new Vector2(0.5f, 0f) },
                    new SpriteInfo() { rect = new Rect(58,236,53,67), pivot = new Vector2(0.5f, 0.5f) },
                  },
                imagepath = "Picture/wing-laser03",
                scale = new Vector3(1f, 1f),
                sorting = 4
            },
            maxrange = 1.6f,
            attack = 300,
            penetrate = true,
            move = BulletMove.B_Laser_level1
        };

        public static BulletPropertyEX b_zhuji= new BulletPropertyEX()
        {
            image = new ImageProperty()
            { sprite=new SpriteInfo[] { new SpriteInfo() { rect = new Rect(1,42,43,184), pivot = new Vector2(0.5f, 0.5f)},
                new SpriteInfo() { rect = new Rect(45,37,44,188), pivot = new Vector2(0.5f, 0.5f)},
                new SpriteInfo() { rect = new Rect(90,0,48,228), pivot = new Vector2(0.5f, 0.5f)},
                new SpriteInfo() { rect = new Rect(139,42,90,186), pivot = new Vector2(0.5f, 0.5f)},
                new SpriteInfo() { rect = new Rect(230,1,94,227), pivot = new Vector2(0.5f, 0.5f)},
                new SpriteInfo() { rect = new Rect(324,0,86,228), pivot = new Vector2(0.5f, 0.5f)},
            }, scale = def_scale, imagepath = "Picture/zhujizidan-07", sorting = 4 },
            edgepoints=new Point2[] { new Point2(0,0.8277818f),new Point2(348,0.57f),new Point2(180,0.707f),new Point2(12,0.57f)},
            radius = 0.1f,
            maxrange = 5f,
            minrange = 0.5f,
            attack = 30,
            speed = 0.4f
        };
        #endregion

        #region warplane dispose
        public static WarPlane plane_nuhuo = new WarPlane()
        {
            gold = 100,
            blood = 1000,
            currentblood = 1000,
            bullet = b_zhuji,
            image = new ImageProperty()
            {
                location = new Vector3(0f, 0, 1),
                imagepath = "Picture/i-p-03c",
                sorting = 5,
                scale = new Vector3(1.6f, 1.6f)
            },//shotfrequency=1,
            shot = new ShotEX[] { ShotBullet.Shot_zhuji1, ShotBullet.Shot_zhuji1,
                    ShotBullet.Shot_zhuji1,ShotBullet.Shot_zhuji1 },
        };
        public static WarPlane plane_zhihui = new WarPlane()
        {
            gold =100,
            blood=1000,currentblood=1000, bullet=b_z_zhzd ,image=new ImageProperty() {location=new Vector3(-0.05f,0,1),
                imagepath ="Picture/p-07d-1",sorting=5,scale=new Vector3(1,1)},
                shot= new ShotEX[] { ShotBullet.Shot_Scatter_level2, ShotBullet.Shot_Scatter_level2,
                    ShotBullet.Shot_Scatter_level3,ShotBullet.Shot_Scatter_level4 },shotfrequency=4
        };
        public static WarPlane plane_jifeng = new WarPlane()
        {
            gold = 100,
            blood = 1000,
            currentblood = 1000,
            bullet = b_laser_r,
            image = new ImageProperty()
            {
                location = def_location,
                imagepath = "Picture/i-p-05d",
                sorting = 5,
                scale = new Vector3(1.6f, 1.6f)
            },
            specital=true,
            sp_bullet =new SpecitialBullet { inital=ThunderMod.Laser_Initial,
            updatemian=ThunderMod.Laser_Update,shot=ThunderMod.Laser_Calculate,
            dispose=ThunderMod.Laser_Dispose}
        };
        public static WarPlane[] Plane_all = new WarPlane[] { plane_nuhuo, plane_jifeng, plane_zhihui };
        #endregion

        #region  second weapon dispose
        public static SecondBullet SB_Chain = new SecondBullet()
        {
            gold=100,
            specital=true,shotfrequency=5,
            sp_bullet=new SpecitialBullet() {
            inital=ThunderMod.Chain_Initial,updatemian=ThunderMod.Chain_Update,
            shot=ThunderMod.Chain_Shot,dispose=ThunderMod.Chain_Dispose,
            move=ThunderMod.Chain_Mov},
            bullet = b_chain
        };
        public static SecondBullet SB_Thunder = new SecondBullet()
        {
            gold = 100,shotfrequency=35,
            shot = new ShotEX[] { ShotBullet.Shot_Thunder_level1,ShotBullet.Shot_Thunder_level2,
            ShotBullet.Shot_Thunder_level3,ShotBullet.Shot_Thunder_level4},
            bullet = b_thunder
        };
        public static SecondBullet[] SB_all = new SecondBullet[] { SB_Thunder, SB_Chain, };
        #endregion

        #region wing dispose
        public static Wing W_laser = new Wing()
        {gold=100,
            image =new ImageProperty() { imagepath= "Picture/wing-02d" ,sorting=4,scale=new Vector3(1,1f)},
            shot=new ShotEX[] { ShotBullet.Shot_Laser_level1, ShotBullet.Shot_Laser_level1 ,ShotBullet.Shot_Laser_level1,
            ShotBullet.Shot_Laser_level4},bullet=b_laser,shotfrequency=100
        };
        public static Wing W_laserra2 = new Wing()
        {
            gold = 10000,shotfrequency=60,
            image = new ImageProperty() { imagepath = "Picture/i-p-02b", sorting = 4, scale = new Vector3(1, 1f) },
            specital=true,sp_bullet=new SpecitialBullet {inital=ThunderMod.ra2l_Initial,move=ThunderMod.ra2l_mov,
            updatemian=ThunderMod.ra2l_Update,shot=ThunderMod.ra2l_shot,dispose=ThunderMod.ra2l_Dispose},
            bullet = b_laser_w
        };
        public readonly static Wing[] Wing_all = new Wing[] { W_laser, W_laserra2, };
        #endregion

        #region shiled dispose
        static Shiled S_d = new Shiled {gold=1000, max = 100, defence = 5, resume = 0.3f ,imgpath = "Picture/d" };
        static Shiled S_c = new Shiled {gold=2000, max = 110, defence = 6, resume = 0.32f, imgpath = "Picture/c" };
        static Shiled S_b = new Shiled {gold=3000, max = 120, defence = 7, resume = 0.34f, imgpath = "Picture/b" };
        static Shiled S_a = new Shiled {gold=4000, max = 130, defence = 8, resume = 0.36f, imgpath = "Picture/a" };
        static Shiled S_s = new Shiled {gold=5000, max = 150, defence = 10, resume = 0.4f, imgpath = "Picture/s" };
        public readonly static Shiled[] Shiled_all = new Shiled[] {S_d,S_c,S_b,S_a,S_s};
        #endregion

        #region enemy_a dispose
        public static EnemyPropertyEX Boss_Moth = new EnemyPropertyEX()
        {        
            enemy = new EnemyBaseEX()
            {   
                boss=true,            
                blood = 50000,
                defance = 10,
                minrange = 1f,
                maxrange = 3,
                points = new Point2[]{ new Point2(129.7f,1.691885f), new Point2(91.37f,2.6231956f),new Point2 (0,1),
                 new Point2 (268.63f,2.6231956f) , new Point2(230.3f,1.691885f)},
                animation=Moth_Ani
            }

        };
        public static EnemyPropertyEX e_a10 = new EnemyPropertyEX()
        {
            image = new ImageProperty() { scale = def_scale, imagepath = enemy_a10 },
            enemy = new EnemyBaseEX()
            {
                blood = 30000,
                boss = true,
                defance = 9,
                minrange = 1f,
                maxrange = 3,
                points = new Point2[]{ new Point2(180f,1.15f), new Point2(80f,1.61f),
                 new Point2 (0f,1.13f) ,new Point2(280f,1.61f)}
            }
        };
        public static EnemyPropertyEX e_def_a00 = new EnemyPropertyEX()
        {
            image = new ImageProperty() { scale = def_scale, imagepath = enemy_a ,grid=def_4x2},
            bullet = new BulletPropertyEX[] { b_def_b03b },
            enemy = new EnemyBaseEX()
            {
                blood = 1500,show_blood=true,
                defance = 5,
                minrange = 0.3f,
                maxrange = 2,
                points = points_01
            }
        };
        public static EnemyPropertyEX e_meteor = new EnemyPropertyEX()
        {
            image = new ImageProperty() { scale = def_scale, imagepath = "Picture/meteor" },
            enemy = new EnemyBaseEX()
            {
                blood = 3000,show_blood=true,
                defance = 10,
                minrange = 0.3f,
                maxrange = 2,points_style=1,
                points = points_meteor
            }
        };
        #endregion

        #region enemy_b dispose
        public static EnemyPropertyEX e_def_b00 = new EnemyPropertyEX()
        {
            image = new ImageProperty() { scale = def_scale, imagepath = enemy_b, grid = def_4x3 },
            bullet = new BulletPropertyEX[] { b_def_b03b },
            enemy = new EnemyBaseEX()
            {
                blood = 1000,
                show_blood = true,
                defance = 5,
                minrange = 0.3f,
                maxrange = 2, 
                points = points_01
            }
        };
        #endregion

        #region Battelfiled

        static SetBattelField[] SetLeveMod = new SetBattelField[] { SetLevel1, SetLevel2, SetLevel3, SetLevel4, SetLevel5, SetLevel6, SetLevel7,
        SetLevel8,SetLevel9,SetLevel10,};
        static int index_s;
        public static void GetLevel(int index)
        {
            index_s = index;
            AsyncManage.AsyncDelegate(()=> {
                GameControl.SetLevel(SetLeveMod[index_s]);
            });                    
        }
        static EnemyPropertyEX InitialEnemy(int level, EnemyPropertyEX enemy)
        {
            float d = 1 + (float)level / 10;
            enemy.enemy.defance*=d;
            enemy.enemy.blood *= d;
            return enemy;       
        }
        static void SetLevel1(ref BattelField bf)
        {
            bf = new BattelField();
            bf.bk_groud = new BackGround[] { new BackGround() { dur_wave = 999999, location= new Vector3(0, 0f, 1),
               mixoffset= new Vector3(0, 11.47f, 0),angle=new Vector3(0,0,90), scale=new Vector3(2,2), imgpath="Picture/cosmos" } };            
            bf.wave = new List<EnemyWave>();
            //b_def_b03b, b_def_b04y, b_def_reddiamond 注册需要的资源让主线程去加载
            GameControl.RegE(ref e_meteor);
            GameControl.RegE(ref e_def_b00);
            GameControl.RegB(ref b_def_b03b);
            //
            BulletPropertyEX[] bp1=new BulletPropertyEX[] { b_def_b03b };
            BulletPropertyEX[] bp2 =new BulletPropertyEX[] { b_def_b03b };
            bp2[0].speed = 0.08f;
            BulletPropertyEX[] bp_diamond =new BulletPropertyEX[] { b_def_b03b };
            bp_diamond[0].move = BulletMove.B_Diamond;
            EnemyPropertyEX e1= InitialEnemy(0, e_def_b00);
            EnemyPropertyEX e2= InitialEnemy(1, e_meteor);

            EnemyWave ew = new EnemyWave();
            ew.enemyppt = e1;
            ew.enemyppt.enemy.move = EnemyMove.M_Dock_200;
            ew.enemyppt.bullet = bp_diamond;
            ew.enemyppt.enemy.shot = ShotBullet.Shot_Diamond;
            ew.enemyppt.enemy.shotfrequency = 20;
            ew.enemyppt.bullet[0].move = BulletMove.B_Diamond;
            ew.start = S_Dwon_2;
            ew.sum = 2;
            ew.staytime = 50;
            bf.wave.Add(ew);
            ew.enemyppt.bullet = bp1;
            ew.enemyppt.enemy.move = EnemyMove.M_Dock_180;
            ew.enemyppt.enemy.shot = ShotBullet.Shot_ThreeBeline;
            ew.enemyppt.enemy.shotfrequency = 5;
            ew.start = S_Up_3;
            ew.sum = 1;
            ew.staytime = 200;
            bf.wave.Add(ew);
            ew.enemyppt.enemy.move = EnemyMove.M_Dock_200;
            ew.enemyppt.enemy.shot = ShotBullet.Shot_Down_Arc3;
            ew.enemyppt.enemy.shotfrequency = 20;
            ew.enemyppt.bullet =bp2;
            bf.wave.Add(ew);
            ew= bf.wave[0];
            bf.wave.Add(ew);
            ew.enemyppt.enemy.spt_index = 2;
            ew.enemyppt.enemy.points = points_02;
            ew.enemyppt.enemy.shot = ShotBullet.Shot_Down_Arc3;
            ew.enemyppt.bullet = bp1;
            ew.start = S_Dwon_3;
            ew.sum = 3;
            ew.staytime = 500;
            bf.wave.Add(ew);
            ew.enemyppt.bullet = bp2;
            ew.enemyppt.enemy.shot = ShotBullet.Shot_Aim_12;
            ew.enemyppt.enemy.move = EnemyMove.M_Downward_NoStop;
            ew.enemyppt.enemy.shotfrequency = 20;
            ew.staytime = 500;
            bf.wave.Add(ew);
            ew.enemyppt = e2;
            ew.enemyppt.enemy.move = EnemyMove.M_Downward_NoStop;
            ew.start = S_Up_3;
            ew.sum = 1;
            ew.staytime = 0;
            bf.wave.Add(ew);
            ew = bf.wave[2];
            ew.enemyppt.enemy.move = EnemyMove.M_Downward_NoStop;
            ew.enemyppt.enemy.shot = ShotBullet.Shot_Down_Arc3;
            ew.enemyppt.bullet = bp2;
            ew.start = S_RandomDown_1();
            ew.sum = 1;
            ew.staytime = 50;
            bf.wave.Add(ew);
            ew.start = S_RandomDown_1();
            bf.wave.Add(ew);
            ew.start = S_RandomDown_1();
            bf.wave.Add(ew);            
            ew.start = S_RandomDown_1();
            bf.wave.Add(ew);
            ew.enemyppt.enemy.move = EnemyMove.M_LeftArc;
            ew.enemyppt.enemy.shot = ShotBullet.Shot_Aim_12;
            ew.start = S_Left_1;
            ew.sum = 1;
            ew.staytime = 50;
            bf.wave.Add(ew);
            ew.enemyppt.enemy.move = EnemyMove.M_RightArc;
            ew.enemyppt.enemy.shot = ShotBullet.Shot_Aim_12;
            ew.start = S_Right_1;
            ew.sum = 1;
            ew.staytime = 50;
            bf.wave.Add(ew);
            ew.enemyppt =  e2;
            ew.enemyppt.enemy.move = EnemyMove.M_Downward_NoStop;
            ew.start = S_Dwon_3;
            ew.sum = 3;
            ew.staytime = 0;
            bf.wave.Add(ew);
            ew = bf.wave[1];
            ew.enemyppt.bullet = bp2;
            ew.enemyppt.enemy.move = EnemyMove.M_Dock_180;
            ew.enemyppt.enemy.shot = ShotBullet.Shot_Down_Arc3;
            ew.start = S_Up_3;
            ew.sum = 1;
            ew.staytime = 200;
            bf.wave.Add(ew);
            ew.enemyppt= e2;
            ew.enemyppt.enemy.move = EnemyMove.M_Downward_NoStop;
            ew.start = S_Up_1;
            ew.sum = 1;
            ew.staytime = 300;
            bf.wave.Add(ew);
            bf.wave.Add(bf.wave[0]);
            bf.wave.Add(bf.wave[1]);
            bf.wave.Add(bf.wave[3]);
            bf.wave.Add(bf.wave[5]);
            bf.wave.Add(bf.wave[11]);
            bf.wave.Add(bf.wave[9]);
            bf.wave.Add(bf.wave[2]);
            bf.wave.Add(bf.wave[12]);
            bf.wave.Add(bf.wave[7]);
            bf.wave.Add(bf.wave[8]);
            bf.wave.Add(bf.wave[4]);
            bf.wave.Add(bf.wave[11]);
            bf.wave.Add(bf.wave[14]);
            bf.wave.Add(bf.wave[3]);
            bf.wave.Add(bf.wave[7]);
            bf.wave.Add(bf.wave[8]);
            bf.wave.Add(bf.wave[9]);
            bf.wave.Add(bf.wave[10]);
            bf.wave.Add(bf.wave[2]);
            bf.wave.Add(bf.wave[12]);
            bf.wave.Add(bf.wave[6]);
            ew.staytime = 2000;
            bf.wave.Add(ew);
        }
        static void SetLevel2(ref BattelField bf)
        {
            bf = new BattelField();
            bf.bk_groud = new BackGround[] { new BackGround() { dur_wave = 999999, location= new Vector3(0, 0f, 1),
               mixoffset= new Vector3(0, 11.47f, 0),angle=new Vector3(0,0,90), scale=new Vector3(2,2), imgpath="Picture/cosmos" } };
            bf.wave = new List<EnemyWave>();
            //b_def_b03b, b_def_b04y, b_def_reddiamond 注册需要的资源让主线程去加载
            GameControl.RegE(ref e_meteor);
            GameControl.RegE(ref e_a10);
            GameControl.RegE(ref e_def_b00);
            GameControl.RegB(ref b_def_b04y);
            GameControl.RegB(ref b_def_b03b);
            GameControl.RegB(ref b_bigblueboll);
            //
            BulletPropertyEX[] bp1 = new BulletPropertyEX[] { b_def_b03b };
            BulletPropertyEX[] bp2 = new BulletPropertyEX[] { b_def_b03b };
            BulletPropertyEX[] bp_diamond = new BulletPropertyEX[] { b_def_b03b };
            bp_diamond[0].move = BulletMove.B_Diamond;
            BulletPropertyEX[] bboss = new BulletPropertyEX[] { b_def_b03b , b_def_b03b, b_bigblueboll,b_def_b04y};
            bboss[0].speed = 0.04f;
            bboss[1].speed = 0.04f;
            bp2[0].speed = 0.08f;
           
            EnemyPropertyEX e1= InitialEnemy(1, e_def_b00);
            EnemyPropertyEX e2 = InitialEnemy(1, e_meteor);
            EnemyPropertyEX e3 = InitialEnemy(1, e_a10);

            EnemyWave ew =new EnemyWave();
            ew.enemyppt = e1;
            ew.enemyppt.bullet = bp1;
            ew.enemyppt.enemy.move = EnemyMove.M_Dock_180;
            ew.enemyppt.enemy.shot = ShotBullet.Shot_ThreeBeline;
            ew.enemyppt.enemy.shotfrequency = 5;
            ew.start = S_Up_3;
            ew.sum = 1;
            ew.staytime = 200;
            bf.wave.Add(ew);
            ew.enemyppt.enemy.move = EnemyMove.M_Dock_200;
            ew.enemyppt.enemy.shot = ShotBullet.Shot_Down_Arc3;
            ew.enemyppt.enemy.shotfrequency = 20;
            ew.enemyppt.bullet = bp2;
            bf.wave.Add(ew);
            ew = bf.wave[0];
            bf.wave.Add(ew);
            ew.enemyppt.enemy.spt_index = 2;
            ew.enemyppt.enemy.shot = ShotBullet.Shot_Down_Arc3;
            ew.enemyppt.bullet = bp1;
            ew.start = S_Dwon_3;
            ew.sum = 3;
            ew.staytime = 500;
            bf.wave.Add(ew);
            ew.enemyppt.bullet = bp2;
            ew.enemyppt.enemy.shot = ShotBullet.Shot_Aim_12;
            ew.enemyppt.enemy.move = EnemyMove.M_Downward_NoStop;
            ew.enemyppt.enemy.shotfrequency = 20;
            ew.staytime = 500;
            bf.wave.Add(ew);
            ew.enemyppt = e2;
            ew.enemyppt.enemy.move = EnemyMove.M_Downward_NoStop;
            ew.start = S_Up_3;
            ew.sum = 1;
            ew.staytime = 0;
            bf.wave.Add(ew);
            ew = bf.wave[2];
            ew.enemyppt.enemy.move = EnemyMove.M_Downward_NoStop;
            ew.enemyppt.enemy.shot = ShotBullet.Shot_Down_Arc3;
            ew.enemyppt.bullet = bp2;
            ew.start = S_RandomDown_1();
            ew.sum = 1;
            ew.staytime = 50;
            bf.wave.Add(ew);
            ew.start = S_RandomDown_1();
            bf.wave.Add(ew);
            ew.start = S_RandomDown_1();
            bf.wave.Add(ew);
            ew.start = S_RandomDown_1();
            bf.wave.Add(ew);
            ew.enemyppt.enemy.move = EnemyMove.M_LeftArc;
            ew.enemyppt.enemy.shot = ShotBullet.Shot_Aim_12;
            ew.start = S_Left_1;
            ew.sum = 1;
            ew.staytime = 50;
            bf.wave.Add(ew);
            ew.enemyppt.enemy.move = EnemyMove.M_RightArc;
            ew.enemyppt.enemy.shot = ShotBullet.Shot_Aim_12;
            ew.start = S_Right_1;
            ew.sum = 1;
            ew.staytime = 50;
            bf.wave.Add(ew);
            ew.enemyppt= e2;
            ew.enemyppt.enemy.move = EnemyMove.M_Downward_NoStop;
            ew.start = S_Dwon_3;
            ew.sum = 3;
            ew.staytime = 0;
            bf.wave.Add(ew);
            ew = bf.wave[1];
            ew.enemyppt.bullet = bp2;
            ew.enemyppt.enemy.move = EnemyMove.M_Dock_180;
            ew.enemyppt.enemy.shot = ShotBullet.Shot_Down_Arc3;
            ew.start = S_Up_3;
            ew.sum = 1;
            ew.staytime = 200;
            bf.wave.Add(ew);
            ew.enemyppt= e2;
            ew.enemyppt.enemy.move = EnemyMove.M_Downward_NoStop;
            ew.start = S_Up_1;
            ew.sum = 1;
            ew.staytime = 300;
            bf.wave.Add(ew);
            bf.wave.Add(bf.wave[8]);
            bf.wave.Add(bf.wave[4]);
            bf.wave.Add(bf.wave[11]);
            bf.wave.Add(bf.wave[14]);
            bf.wave.Add(bf.wave[3]);
            bf.wave.Add(bf.wave[7]);
            bf.wave.Add(bf.wave[9]);
            bf.wave.Add(bf.wave[2]);
            bf.wave.Add(bf.wave[12]);
            bf.wave.Add(bf.wave[7]);
            bf.wave.Add(bf.wave[8]);
            bf.wave.Add(bf.wave[9]);
            bf.wave.Add(bf.wave[10]);          
            bf.wave.Add(bf.wave[0]);
            bf.wave.Add(bf.wave[1]);
            bf.wave.Add(bf.wave[3]);
            bf.wave.Add(bf.wave[5]);
            bf.wave.Add(bf.wave[11]);
            bf.wave.Add(bf.wave[2]);
            bf.wave.Add(bf.wave[12]);
            bf.wave.Add(bf.wave[6]);
            ew.staytime = 200;
            bf.wave.Add(ew);
            ew.enemyppt=e3;
            ew.enemyppt.enemy.move = ThunderMod.M_Boss_a10;
            ew.enemyppt.bullet = bboss;
            ew.start = S_Up_3;
            ew.sum = 1;
            ew.staytime = 50;
            bf.wave.Add(ew);

        }
        static void SetLevel3(ref BattelField bf)
        {
            bf = new BattelField();
            bf.bk_groud = new BackGround[] { new BackGround() { dur_wave = 999999, location= new Vector3(0, 0f, 1),
               mixoffset= new Vector3(0, 11.47f, 0),angle=new Vector3(0,0,90), scale=new Vector3(2,2), imgpath="Picture/cosmos" } };
            bf.wave = new List<EnemyWave>();
            //b_def_b03b, b_def_b04y, b_def_reddiamond 注册需要的资源让主线程去加载
            GameControl.RegE(ref e_meteor);
            GameControl.RegE(ref e_def_b00);
            GameControl.RegB(ref b_def_b03b);
            //
            BulletPropertyEX[] bp1 = new BulletPropertyEX[] { b_def_b03b };
            BulletPropertyEX[] bp2 = new BulletPropertyEX[] { b_def_b03b };
            bp2[0].speed = 0.08f;
            BulletPropertyEX[] bp_diamond = new BulletPropertyEX[] { b_def_b03b };
            bp_diamond[0].move = BulletMove.B_Diamond;
            EnemyPropertyEX e1 = InitialEnemy(2, e_def_b00);
            EnemyPropertyEX e2 = InitialEnemy(2, e_meteor);

            EnemyWave ew =new EnemyWave();
            ew.enemyppt = e1;
            ew.enemyppt.enemy.spt_index = 4;
            ew.enemyppt.enemy.move = EnemyMove.M_Dock_200;
            ew.enemyppt.bullet = bp_diamond;
            ew.enemyppt.enemy.shot = ShotBullet.Shot_Diamond;
            ew.enemyppt.enemy.shotfrequency = 20;
            ew.enemyppt.bullet[0].move = BulletMove.B_Diamond;
            ew.start = S_Dwon_2;
            ew.sum = 2;
            ew.staytime = 50;
            bf.wave.Add(ew);
            ew.enemyppt.bullet = bp1;
            ew.enemyppt.enemy.move = EnemyMove.M_Dock_180;
            ew.enemyppt.enemy.shot = ShotBullet.Shot_ThreeBeline;
            ew.enemyppt.enemy.shotfrequency = 5;
            ew.start = S_Up_3;
            ew.sum = 1;
            ew.staytime = 200;
            bf.wave.Add(ew);
            ew.enemyppt.enemy.move = EnemyMove.M_Dock_200;
            ew.enemyppt.enemy.shot = ShotBullet.Shot_Down_Arc3;
            ew.enemyppt.enemy.shotfrequency = 20;
            ew.enemyppt.bullet = bp2;
            bf.wave.Add(ew);
            ew = bf.wave[0];
            bf.wave.Add(ew);
            ew.enemyppt.enemy.spt_index = 5;
            ew.enemyppt.enemy.shot = ShotBullet.Shot_Circle36A;
            ew.enemyppt.enemy.shotfrequency = 60;
            ew.enemyppt.bullet = bp1;
            ew.start = S_Dwon_3;
            ew.sum = 3;
            ew.staytime = 500;
            bf.wave.Add(ew);
            ew.enemyppt.bullet = bp2;
            ew.enemyppt.enemy.shot = ShotBullet.Shot_Aim_12;
            ew.enemyppt.enemy.move = EnemyMove.M_Downward_NoStop;
            ew.enemyppt.enemy.shotfrequency = 20;
            ew.staytime = 500;
            bf.wave.Add(ew);
            ew.enemyppt =e2 ;
            ew.enemyppt.enemy.move = EnemyMove.M_Downward_NoStop;
            ew.start = S_Up_3;
            ew.sum = 1;
            ew.staytime = 0;
            bf.wave.Add(ew);
            ew = bf.wave[2];
            ew.enemyppt.enemy.move = EnemyMove.M_Downward_NoStop;
            ew.enemyppt.enemy.shot = ShotBullet.Shot_Downflowers;
            ew.enemyppt.bullet = bp2;
            ew.start = S_RandomDown_1();
            ew.sum = 1;
            ew.staytime = 50;
            bf.wave.Add(ew);
            ew.start = S_RandomDown_1();
            bf.wave.Add(ew);
            ew.start = S_RandomDown_1();
            bf.wave.Add(ew);
            ew.start = S_RandomDown_1();
            bf.wave.Add(ew);
            ew.enemyppt.enemy.spt_index = 6;
            ew.enemyppt.enemy.move = EnemyMove.M_LeftArc;
            ew.start = S_Left_1;
            ew.sum = 1;
            ew.staytime = 50;
            bf.wave.Add(ew);
            ew.enemyppt.enemy.move = EnemyMove.M_RightArc;
            ew.start = S_Right_1;
            ew.sum = 1;
            ew.staytime = 50;
            bf.wave.Add(ew);
            ew.enemyppt = e2;
            ew.enemyppt.enemy.move = EnemyMove.M_Downward_NoStop;
            ew.start = S_Dwon_3;
            ew.sum = 3;
            ew.staytime = 0;
            bf.wave.Add(ew);
            ew = bf.wave[1];
            ew.enemyppt.bullet = bp2;
            ew.enemyppt.enemy.spt_index = 7;
            ew.enemyppt.enemy.move = EnemyMove.M_Dock_180;
            ew.enemyppt.enemy.shot = ShotBullet.Shot_Down_Arc3;
            ew.start = S_Up_3;
            ew.sum = 1;
            ew.staytime = 200;
            bf.wave.Add(ew);
            ew.enemyppt = e2;
            ew.enemyppt.enemy.move = EnemyMove.M_Downward_NoStop;
            ew.start = S_Up_1;
            ew.sum = 1;
            ew.staytime = 300;
            bf.wave.Add(ew);
            bf.wave.Add(bf.wave[7]);
            bf.wave.Add(bf.wave[8]);
            bf.wave.Add(bf.wave[4]);
            bf.wave.Add(bf.wave[11]);
            bf.wave.Add(bf.wave[14]);
            bf.wave.Add(bf.wave[3]);
            bf.wave.Add(bf.wave[7]);
            bf.wave.Add(bf.wave[8]);
            bf.wave.Add(bf.wave[9]);
            bf.wave.Add(bf.wave[10]);
            bf.wave.Add(bf.wave[0]);
            bf.wave.Add(bf.wave[1]);
            bf.wave.Add(bf.wave[3]);
            bf.wave.Add(bf.wave[5]);
            bf.wave.Add(bf.wave[11]);
            bf.wave.Add(bf.wave[9]);
            bf.wave.Add(bf.wave[2]);
            bf.wave.Add(bf.wave[12]);
            bf.wave.Add(bf.wave[2]);
            bf.wave.Add(bf.wave[12]);
            bf.wave.Add(bf.wave[6]);
            ew.staytime = 2000;
            bf.wave.Add(ew);
        }
        static void SetLevel4(ref BattelField bf)
        {
            bf = new BattelField();
            bf.bk_groud = new BackGround[] { new BackGround() { dur_wave = 999999, location= new Vector3(0, 0f, 1),
               mixoffset= new Vector3(0, 11.47f, 0),angle=new Vector3(0,0,90), scale=new Vector3(2,2), imgpath="Picture/cosmos" } };
            bf.wave = new List<EnemyWave>();
            //b_def_b03b, b_def_b04y, b_def_reddiamond, b_def_redlaser 注册需要的资源让主线程去加载
            GameControl.RegE(ref e_def_a00);
            GameControl.RegE(ref e_meteor);
            GameControl.RegE(ref e_def_b00);
            GameControl.RegE(ref Boss_Moth);
            GameControl.RegB(ref b_def_b03b);
            GameControl.RegB(ref b_def_b04y);
            GameControl.RegB(ref b_def_b3);
            GameControl.RegB(ref b_def_redlaser);
            //
            EnemyPropertyEX e1 = InitialEnemy(3, e_def_a00);
            EnemyPropertyEX e2 = InitialEnemy(3, e_meteor);
            EnemyPropertyEX e3 = InitialEnemy(3, e_def_b00);
            e3.enemy.spt_index = 7;
            EnemyPropertyEX e4 = InitialEnemy(3, Boss_Moth);
            BulletPropertyEX[] bp1 = new BulletPropertyEX[] { b_def_b03b };
            BulletPropertyEX[] bp2 = new BulletPropertyEX[] { b_def_b03b };
            bp2[0].speed = 0.08f;
            EnemyWave ew = new EnemyWave();
            ew.enemyppt = e1;
            ew.enemyppt.bullet = new BulletPropertyEX[] { b_def_b03b };
            ew.enemyppt.enemy.move = EnemyMove.M_Dock_200;
            ew.enemyppt.enemy.shot = ShotBullet.Shot_Diamond;
            ew.enemyppt.bullet[0].move = BulletMove.B_Diamond;
            ew.enemyppt.enemy.shotfrequency = 20;
            ew.start = S_Dwon_2;
            ew.sum = 2;
            ew.staytime = 50;
            bf.wave.Add(ew);
            ew.enemyppt.bullet = bp1;
            ew.enemyppt.enemy.move = EnemyMove.M_Dock_180;
            ew.enemyppt.enemy.shot = ShotBullet.Shot_ThreeBeline;
            ew.enemyppt.enemy.shotfrequency = 5;
            ew.start = S_Up_3;
            ew.sum = 1;
            ew.staytime = 200;
            bf.wave.Add(ew);
            ew.enemyppt.enemy.move = EnemyMove.M_Dock_200;
            ew.enemyppt.enemy.shot = ShotBullet.Shot_Down_Arc3;
            ew.enemyppt.bullet = bp2;
            ew.enemyppt.enemy.shotfrequency = 5;
            bf.wave.Add(ew);
            ew = bf.wave[1];
            bf.wave.Add(ew);
            ew = bf.wave[2];
            ew.enemyppt.enemy.move = EnemyMove.M_Dock_200;
            ew.enemyppt.enemy.shot = ShotBullet.Shot_Down_Arc3;
            ew.start = S_Dwon_3;
            ew.sum = 3;
            ew.staytime = 500;
            bf.wave.Add(ew);
            ew.enemyppt.enemy.move = EnemyMove.M_Downward_NoStop;
            ew.staytime = 500;
            bf.wave.Add(ew);
            ew.enemyppt=e2;
            ew.enemyppt.enemy.move = EnemyMove.M_Downward_NoStop;
            ew.start = S_Up_3;
            ew.sum = 1;
            ew.staytime = 50;
            bf.wave.Add(ew);
            ew.enemyppt = e1;
            ew.enemyppt.bullet = bp1;
            ew.enemyppt.enemy.move = EnemyMove.M_Downward_NoStop;
            ew.enemyppt.enemy.shot = ShotBullet.Shot_Angle6_Rotate;
            ew.enemyppt.enemy.shotfrequency = 5;
            ew.start = S_RandomDown_1();
            ew.sum = 1;
            ew.staytime = 50;
            bf.wave.Add(ew);
            ew.start = S_RandomDown_1();
            bf.wave.Add(ew);
            ew.start = S_RandomDown_1();
            bf.wave.Add(ew);
            ew.start = S_RandomDown_1();
            bf.wave.Add(ew);
            ew.enemyppt=e3;
            ew.enemyppt.bullet = bp2;
            ew.enemyppt.enemy.move = EnemyMove.M_LeftArc;
            ew.enemyppt.enemy.shot = ShotBullet.Shot_Aim_12;
            ew.enemyppt.enemy.shotfrequency = 15;
            ew.start = S_Left_1;
            ew.sum = 1;
            ew.staytime = 50;
            bf.wave.Add(ew);
            ew.enemyppt.enemy.move = EnemyMove.M_RightArc;
            ew.enemyppt.enemy.shot = ShotBullet.Shot_Aim_12;
            ew.start = S_Right_1;
            ew.sum = 1;
            ew.staytime = 50;
            bf.wave.Add(ew);
            ew.enemyppt=e2;
            ew.enemyppt.enemy.move = EnemyMove.M_Downward_NoStop;
            ew.start = S_Dwon_3;
            ew.sum = 3;
            ew.staytime = 0;
            bf.wave.Add(ew);
            ew.enemyppt=e1;
            ew.enemyppt.bullet = bp2;
            ew.enemyppt.enemy.move = EnemyMove.M_Dock_180;
            ew.enemyppt.enemy.shot = ShotBullet.Shot_Down_Arc3;
            ew.enemyppt.enemy.shotfrequency = 5;
            ew.start = S_Up_3;
            ew.sum = 1;
            ew.staytime = 200;
            bf.wave.Add(ew);
            ew.enemyppt=e2;
            ew.enemyppt.enemy.move = EnemyMove.M_Downward_NoStop;
            ew.start = S_Up_1;
            ew.sum = 1;
            ew.staytime = 300;
            bf.wave.Add(ew);
            bf.wave.Add(bf.wave[0]);
            bf.wave.Add(bf.wave[1]);
            bf.wave.Add(bf.wave[3]);
            bf.wave.Add(bf.wave[5]);
            bf.wave.Add(bf.wave[11]);
            bf.wave.Add(bf.wave[9]);
            bf.wave.Add(bf.wave[2]);
            bf.wave.Add(bf.wave[12]);
            bf.wave.Add(bf.wave[7]);
            bf.wave.Add(bf.wave[8]);
            bf.wave.Add(bf.wave[4]);
            bf.wave.Add(bf.wave[11]);
            bf.wave.Add(bf.wave[14]);
            bf.wave.Add(bf.wave[3]);
            bf.wave.Add(bf.wave[7]);
            bf.wave.Add(bf.wave[8]);
            bf.wave.Add(bf.wave[9]);
            bf.wave.Add(bf.wave[10]);
            bf.wave.Add(bf.wave[2]);
            bf.wave.Add(bf.wave[12]);
            bf.wave.Add(bf.wave[6]);
            ew.staytime = 500;
            bf.wave.Add(ew);
            ew.enemyppt=e4;//
            ew.enemyppt.bullet = new BulletPropertyEX[] { b_def_b03b, b_def_b04y, b_def_b3, b_def_b03b, b_def_b04y, b_def_redlaser };
            ew.enemyppt.bullet[2].spt_index = 4;
            ew.enemyppt.bullet[3].move = BulletMove.B_Pentagram;
            ew.enemyppt.bullet[4].move = BulletMove.B_Boss_Tick;
            ew.enemyppt.enemy.move = ThunderMod.M_Boss_Moth;
            ew.enemyppt.bullet[0].speed = 0.08f;
            ew.start = S_Up_3;
            ew.sum = 1;
            ew.staytime = 2000;
            bf.wave.Add(ew);
        }
        static void SetLevel5(ref BattelField bf)
        {
            bf = new BattelField();
            bf.bk_groud = new BackGround[] { new BackGround() { dur_wave = 999999, location= new Vector3(0, 0f, 1),
               mixoffset= new Vector3(0, 11.47f, 0),angle=new Vector3(0,0,90), scale=new Vector3(2,2), imgpath="Picture/cosmos" } };
            bf.wave = new List<EnemyWave>();
            //b_def_b03b, b_def_b04y, b_def_reddiamond, b_def_redlaser 注册需要的资源让主线程去加载
            GameControl.RegE(ref e_def_a00);
            GameControl.RegE(ref e_meteor);
            GameControl.RegE(ref e_def_b00);
            GameControl.RegE(ref Boss_Moth);
            GameControl.RegB(ref b_def_b03b);
            GameControl.RegB(ref b_def_b04y);
            GameControl.RegB(ref b_def_b3);
            GameControl.RegB(ref b_def_redlaser);
            //
            EnemyPropertyEX e1 = InitialEnemy(4, e_def_a00);
            EnemyPropertyEX e2 = InitialEnemy(4, e_meteor);
            EnemyPropertyEX e3 = InitialEnemy(4, e_def_b00);
            e3.enemy.spt_index = 8;
            BulletPropertyEX[] bp1 = new BulletPropertyEX[] { b_def_b03b };
            BulletPropertyEX[] bp2 = new BulletPropertyEX[] { b_def_b03b };
            BulletPropertyEX[] bp3 = new BulletPropertyEX[] { b_def_b3};
            BulletPropertyEX[] bp4 = new BulletPropertyEX[] { b_def_b04y };
            bp2[0].speed = 0.08f;
            EnemyWave ew = new EnemyWave();
            ew.enemyppt = e1; 
            ew.sum = 3;
            ew.staytime = 200;
            ew.enemyppt.enemy.move = EnemyMove.M_Dock_200;
            ew.enemyppt.enemy.shot = ShotBullet.Shot_Down_Arc3;
            ew.enemyppt.bullet = bp2;
            ew.enemyppt.enemy.shotfrequency = 5;
            ew.start = S_Dwon_3;
            bf.wave.Add(ew);
            bf.wave.Add(ew);
            ew.enemyppt = e1;
            ew.enemyppt.bullet = bp1;
            ew.enemyppt.enemy.move = EnemyMove.M_Downward_NoStop;
            ew.enemyppt.enemy.shot = ShotBullet.Shot_Angle6_Rotate;
            ew.enemyppt.enemy.shotfrequency = 5;
            ew.start = S_RandomDown_1();
            ew.sum = 1;
            ew.staytime = 50;
            bf.wave.Add(ew);
            ew.enemyppt.enemy.move = EnemyMove.M_Dock_200;
            ew.start = S_Dwon_3;
            ew.sum = 3;
            ew.staytime = 500;
            bf.wave.Add(ew);
            ew.enemyppt.bullet = bp2;
            ew.enemyppt.enemy.move = EnemyMove.M_Downward_NoStop;
            ew.staytime = 500;
            bf.wave.Add(ew);
            ew.enemyppt = e2;
            ew.enemyppt.enemy.move = EnemyMove.M_Downward_NoStop;
            ew.start = S_Up_3;
            ew.sum = 1;
            ew.staytime = 50;
            bf.wave.Add(ew);
            ew.enemyppt = e1;
            ew.enemyppt.bullet = bp1;
            ew.enemyppt.enemy.move = EnemyMove.M_Downward_NoStop;
            ew.enemyppt.enemy.shot = ShotBullet.Shot_Angle6_Rotate;
            ew.enemyppt.enemy.shotfrequency = 5;
            ew.start = S_RandomDown_1();
            ew.sum = 1;
            ew.staytime = 50;
            bf.wave.Add(ew);
            ew.enemyppt.bullet = new BulletPropertyEX[] { b_def_b03b };
            ew.enemyppt.enemy.move = EnemyMove.M_Dock_200;
            ew.enemyppt.enemy.shot = ShotBullet.Shot_Diamond;
            ew.enemyppt.bullet[0].move = BulletMove.B_Diamond;
            ew.enemyppt.enemy.shotfrequency = 20;
            ew.start = S_Dwon_2;
            ew.sum = 2;
            ew.staytime = 50;
            bf.wave.Add(ew);
            ew.enemyppt.bullet = bp1;
            ew.enemyppt.enemy.move = EnemyMove.M_Dock_180;
            ew.enemyppt.enemy.shot = ShotBullet.Shot_ThreeBeline;
            ew.enemyppt.enemy.shotfrequency = 5;
            ew.start = S_Up_3;
            bf.wave.Add(ew);
            ew.start = S_RandomDown_1();
            bf.wave.Add(ew);
            ew.start = S_RandomDown_1();
            bf.wave.Add(ew);
            ew.enemyppt = e3;
            ew.enemyppt.bullet = bp4;
            ew.enemyppt.enemy.move = EnemyMove.M_Dock_200;
            ew.enemyppt.enemy.shot = ShotBullet.Shot_Angle6_RotateA;
            ew.enemyppt.enemy.shotfrequency = 2;
            ew.start = S_Up_3;
            ew.sum = 1;
            ew.staytime = 50;
            bf.wave.Add(ew);
            ew.enemyppt = e3;
            ew.enemyppt.bullet = bp2;
            ew.enemyppt.enemy.move = EnemyMove.M_RightArc;
            ew.enemyppt.enemy.shot = ShotBullet.Shot_Aim_12;
            ew.enemyppt.enemy.shotfrequency = 8;
            ew.start = S_Right_1;
            ew.sum = 1;
            ew.staytime = 50;
            bf.wave.Add(ew);
            ew.enemyppt = e2;
            ew.enemyppt.enemy.move = EnemyMove.M_Downward_NoStop;
            ew.start = S_Dwon_3;
            ew.sum = 3;
            ew.staytime = 0;
            bf.wave.Add(ew);
            ew.enemyppt = e1;
            ew.enemyppt.bullet = bp4;
            ew.enemyppt.enemy.move = EnemyMove.M_Dock_180;
            ew.enemyppt.enemy.shot = ShotBullet.Shot_Angle6_RotateA;
            ew.enemyppt.enemy.shotfrequency = 2;
            ew.start = S_Up_3;
            ew.sum = 1;
            ew.staytime = 200;
            bf.wave.Add(ew);
            ew.enemyppt = e2;
            ew.enemyppt.enemy.move = EnemyMove.M_Downward_NoStop;
            ew.start = S_Up_1;
            ew.sum = 1;
            ew.staytime = 300;
            bf.wave.Add(ew);
            bf.wave.Add(bf.wave[0]);
            bf.wave.Add(bf.wave[1]);
            bf.wave.Add(bf.wave[3]);
            bf.wave.Add(bf.wave[5]);
            bf.wave.Add(bf.wave[11]);
            bf.wave.Add(bf.wave[9]);
            bf.wave.Add(bf.wave[2]);
            bf.wave.Add(bf.wave[12]);
            bf.wave.Add(bf.wave[7]);
            bf.wave.Add(bf.wave[8]);
            bf.wave.Add(bf.wave[4]);
            bf.wave.Add(bf.wave[11]);
            bf.wave.Add(bf.wave[14]);
            bf.wave.Add(bf.wave[3]);
            bf.wave.Add(bf.wave[7]);
            bf.wave.Add(bf.wave[8]);
            bf.wave.Add(bf.wave[9]);
            bf.wave.Add(bf.wave[10]);
            bf.wave.Add(bf.wave[2]);
            bf.wave.Add(bf.wave[12]);
            bf.wave.Add(bf.wave[6]);
            ew.staytime = 500;
        }
        static void SetLevel6(ref BattelField bf)
        {
           
        }
        static void SetLevel7(ref BattelField bf)
        {
            
        }
        static void SetLevel8(ref BattelField bf)
        {
            
        }
        static void SetLevel9(ref BattelField bf)
        {
           
        }
        static void SetLevel10(ref BattelField bf)
        {
            
        }
        #endregion
    }
}