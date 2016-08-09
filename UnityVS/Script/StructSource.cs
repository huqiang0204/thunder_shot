using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Assets.UnityVS.Script
{
    #region delegate
    delegate bool ShaderEffect(ref Material mat, float timeratio);
    delegate void Sliding(Vector3 offset);
    delegate void ListBoxClick(int index);
    delegate void EffectUpdate(int id);
    delegate void BulletMoveEX(int mainid, ref BulletState state);
    delegate bool CollisonCheckA(int mianid, ref BulletState state);
    delegate void BulletShotEX(Transform parent, ref int count, int id);
    delegate bool EnemyMoveEX(int id);//return false is disappear
    delegate bool Shot(int id);
    delegate void ShotEX(int id, int enemyid);
    delegate void PlayerE(int enemyid);
    delegate void PlayerB(ref BulletState state);
    delegate void DamageCalculate(int index);
    delegate void DamageCalculate2(int index, int enemyid);
    delegate void GenerateProp(int max, Vector3 location);
    delegate void GameOverMod(bool pass);
    delegate void PropCollider(int style);
    delegate void SetBattelField(ref BattelField bf);
    delegate void UpdateImage(int img_id,int extra);
    #endregion

    struct AnimatBase
    {
        public int act_count;
        public int sptex_id;
        public AnimatObject[] son;
    }
    struct AnimatObject
    {
        public bool created;
        public bool active;
        public Vector3 location;
        public Vector3 scale;
        public Vector3 angle;
        public int sptindex;
        public int imageid;
        public int extra;
        public Point2[] spritecurve;
    }
    struct AnimatStruct
    {
        public Point2 pivot;//支点与父支点 x=角度 y=半径  
        public Vector3 location;//中心轴
        public Vector3 angle;//旋转角度
        public Point2[] anglecurve;//角度变化线-360到360
        public Point2[] stretchcurve;//伸缩轴变化线
        public AnimatStruct[] son;
        public Transform transform;
        //public ImageProperty image;
        public Vector3 scale;
        public SpriteInfo[] sprite;
        // public Rect[] rect;
        public string img_path;
        public int spt_id;
        public int img_sort;
        public int image_id;
        public int spt_index;
    }

    struct BattelField
    {
        public int mode;
        public int time;
        public int crt_bkg;
        public BackGround[] bk_groud;
        public List<EnemyWave> wave;
    }
    struct BackGround
    {
        public int dur_wave;
        public Vector3 location;
        public Vector3 mixoffset;
        public Vector3 angle;
        public Vector3 scale;
        public string imgpath;
    }
    struct BossBlood
    {
        public int enemy_id;
        public int back_spt_id;
        public int for_spt_id;
        public int back_img_id;
        public int for_img_id;
    }
    struct BulletPropertyEX
    {
        public bool active;
        public float speed;
        public float maxrange;//sqr save performance pre-judgment
        public float minrange;//sqr save performance pre-judgment
        public BulletMoveEX move;
        public float radius;
        public int max;
        public float attack;
        public bool penetrate;

       // public int sum;
        public int s_count;//shot count sub thread
        public int s_count2;//shot count main thread

        public PlayerB play;
        public int spt_id;
        public int spt_index;
        public int parentid;//enemy id
        public Point2[] edgepoints;
        public StartPoint[] shot;
        public ImageProperty image;
        public BulletState[] bulletstate;
        public EffcetBase eff;//only one control with enemy
        public int eff_id;
        public int extra;
    }
    struct BulletState
    {
        public int id;
        public int imageid;
        public bool reg;
        public bool active;
        public int extra;
        public int extra2;
        public Vector3 movexyz;
        public Vector3 location;
        public Vector3 angle;
        public Vector3 scale;

        public bool update;
        public int extra3;
        public int sptindex;
    }
    struct BloodProperty
    {
        public PercentSliser slider;
        public float value;
        public Color color;
    }

    struct ColliderEffect
    {
        public string path;
        public int timeout;
        public Vector3 offset;
    }
    struct CirclePropertyEx
    {
        public ImageProperty imagebase;
        public TextProperty text;
        public CircleButtonBase property;
    }
    struct CircleButtonBase
    {
        public int imageid;
        public int textid;
        public bool active;
        public bool ui;
        public Transform transform;
        public float r;
        public Action click;
        public Sliding sliding;
    }
    struct CurrentState
    {
        public Int32 warplane;
        public Int32 secodeweapon;
        public Int32 wingweapon;
        public Int32 blood;
        public Int32 energy;
        public Int32 mainpower;
        public Int32 secondpower;
        public Int32 wingpower;
        public Int32 shiledpower;
    }
    struct CurrentDispose
    {
        public int level;
        public int power;
        public WarPlane warplane;
        public Wing wing;
        public SecondBullet B_second;
        public Shiled shiled;
        public Skill skill;
        public SecondBullet bs_back;
        public Wing w_back;
        //public BulletProperty wingweapon;

        //shiled
        //armor
        //backup second weapon
        //backup wing weap
        //Energy storage weapon 
        //power dispose
    }

    struct DropDownStruct
    {
        public Action create;
        public Action destory;
    }

    struct EnemyBaseEX
    {
        public bool boss;
        public bool show_blood;
        public float blood;
        public float currentblood;
        public int bloodid;
        public float bloodpercent;
        public float defance;
        public bool die;
        public int imageid;
        public int extra_a;
        public int extra_b;//bullet recorder
        public int extra_m;
        public float radius;
        public float maxrange;
        public float minrange;
        public Vector3 location;
        public Vector3 olt;
        public Vector3 angle;
        public int points_style;
        public Point2[] points;
        public Point2[] offset;
        public int[] bulletid;
        public EnemyMoveEX move;
        public ShotEX shot;
        public int shotfrequency;

        public PlayerE play;//帧动画
        public bool update_spt;
        public int spt_index;//current display index
        public int dspt_id;//dynammic sprite id
                           // public Rect[] rect;//multi rect        
        public AnimatStruct animation;//组件动画

    }
    struct EnemyPropertyEX
    {
        public EnemyBaseEX enemy;
        public ImageProperty image;
        public BulletPropertyEX[] bullet;
        public BloodProperty bloodproperty;
    }
    struct EnemyWave
    {
        public int level;
        public int staytime;
        public int sum;
        public int interval;
        public int time_c;
        public StartPoint[] start;
        public EnemyPropertyEX enemyppt;
    }
    struct EdgeButtonBase
    {
        public int imageid;
        public int textid;
        public bool active;
        public bool ui;
        public Transform transform;
        public Point2[] points;
        public Action click;
        public Sliding sliding;
        //public Action LoseFocus; 
    }
    struct EdgePropertyEx
    {
        public ImageProperty imagebase;
        public TextProperty text;
        public EdgeButtonBase property;
    }
    struct EffectObject
    {
        public bool created;
        public bool active;
        public bool update_s;
        public string imgpath;
        public Rect[] rect;
        public SpriteInfo[] sprite;
        public int extra;
        public int imgid;
        public int sort;

        public float maxspt;
        public int spt_id;
        public int sptindex;
        public Vector3 offset;
        public Vector3 origina;//origin angle
        public Vector3 scale;
        public Color originHSVA;
        public Vector3 location;
        public Vector3 angle;
        public Color color;
        public Point2[] spritecurve;
        public Point2[] angleZcurve;
        public Point2[] scalecurve_x;
        public Point2[] scalecurve_y;
        public Point2[] colorH;
        public Point2[] colorS;
        public Point2[] colorV;
        public Point2[] colorA;

        public Material mat;
        public float matratio;
        public Point2[] matcurve;
    }
    struct EmissionProperty
    {
        public int sum;
        public bool random;//or average
        public Point2 starttime; //x=min scope y=max scope
        public Point2 scale;
        public Point2 angle;//x=z min scope y=z max scope
        public Point2 colorh;
        public Point2 colors;
        public Point2 colorv;
        public Point2 colora;
    }
    struct EffectProperty
    {
        public bool follow;//follow parent
        public bool update_s;
        public string matpath;
        public Material mat;
        public Point2[] matcurve;
        public string path;
        public Grid grid;
        public ShaderEffect shader_s;//shader
    }
    struct EffcetBase
    {
        public bool reg;
        public bool play;
        public bool sleep;
        public int alltime;
        public int currenttime;
        public float timeratio;
        public Vector3 location;
        public EffectObject[] son;
    }

    struct Grid
    {
        public int x;
        public int y;
        public Grid(int x1, int y1) { x = x1;y = y1; }
    }
    struct Goods
    {
        public Int32 id;//type and position
        public Int32 level;//store id and level
        public Int32 exp;
    }

    struct ImageBase
    {
        public bool reg;//只好用这个来比较
        public bool restore;
        public GameObject gameobject;//子线程中比较null都有时候报错，悲催的unity多线程
        public Transform transform;
        public SpriteRenderer spriterender;
        public int spriteid;
    }
    struct ImageBase2
    {
        public bool reg;//只好用这个来比较
        public bool restore;
        public GameObject gameobject;//子线程中比较null都有时候报错，悲催的unity多线程
        public Transform transform;
        public SpriteRenderer spriterender;
        public UpdateImage update;
        public int extra;
    }
    struct ImageProperty
    {
        public int spt_id;
        public int spt_index;
        public Vector3 location;
        public Vector3 angle;
        public Vector3 scale;
        // public Rect[] rect;
        public Grid grid;
        public string imagepath;
        public SpriteInfo[] sprite;
        public int sorting;
    }
    struct ItemBindData
    {
        public string[] imgpath;
        public string[] text;
        //public int[] data;
        //public bool slected;
    }

    struct ListboxItemID
    {
        public int[] img_id;
        public int[] txt_id;
    }
    struct ListBoxItemMod
    {
        public UIImageProperty[] img;
        public UITextProperty[] text;
    }
    struct ListBox
    {
        public bool reg;
        public bool landscape;
        public int current_item;
        public int full_item;
        public int s_index;
        public float len;
        public float per_len;
        public float start;
        public float over;
        public int bk_id;
        public UIImageProperty bk_ground;
        public int vp_id;
        public UIImageProperty vp_window;//view port window      
        public int[] it_id;
        public UIImageProperty item_bkg;//view port window
        public List<ItemBindData> data;
        public ListBoxItemMod item_mod;
        public ListboxItemID[] item_son_id;
        public int event_id;
        public EdgeButtonBase edgebutton;
        public ListBoxClick click;
    }

    struct Point2
    {
        public float x;
        public float y;
        public Point2(float x1, float y1) { x = x1; y = y1; }
    }
    struct Point3
    {
        public float x;
        public float y;
        public float z;
        public Point3(float x1, float y1, float z1) { x = x1; y = y1; z = z1; }
    }
    struct PropBase
    {
        public bool closely;
        public bool state;
        public bool created;
        public bool up_spt;
        public bool cycle;
        public int img_id;
        public int spt_index;
        public int style;
        public int extra;
        public Vector3 location;
        public Vector3 motion;
    }
    struct PercentSliser
    {
        public ImageProperty background;
        public ImageProperty forground;
    }
    struct Power
    {
        public int energy;
        public string[] explain;
        public ImageProperty image;
    }

    struct SpriteInfo
    {
        public Rect rect;
        public Vector2 pivot;
    }
    struct SecondBullet
    {
        public int gold;
        public int bulletid;
        public int shotfrequency;
        public ShotEX[] shot;
        public bool specital;
        public BulletPropertyEX bullet;
        public SpecitialBullet sp_bullet;
        public int extra_b;
    }
    struct Shiled
    {
        public int gold;
        public float defence;
        public float max;
        public float current;
        public float resume;
        public string imgpath;
    }
    struct SpriteBase
    {
        public ResourceRequest rr;
        public Texture2D texture;
        public Sprite sprite;
        public string path;
        public int count;
    }
    struct SpriteBaseEx
    {
        public int dataid;
        public ResourceRequest rr;
        public Texture2D texture;
        public Sprite[] sprites;
        public string path;
        public int count;
    }
    struct StartPoint
    {
        public Vector3 location;
        public Vector3 angle;
    }
    struct Skill
    {
        public int gold;
        public int during;
        public bool follow;
        public float energy_max;
        public float energy_c;//current energy
        public float energy_ec;//energy consumption
        public float energy_re;//resume
        public Vector3 location;
        public float r;
        public float attack;
        public bool active;
        public bool force_clear;
        public int effcetid;
        public EffcetBase eff_b;
    }
    struct SpecitialBullet
    {
        public Action<BulletPropertyEX> inital;//main thread;
        public Action updatemian;//main thread;
        public Action shot;//sub thread;//calculate
        public Action move;//sub thread
        public Action dispose;//main thread;
    }

    struct TextProperty
    {
        public Vector3 location;
        public Vector3 angle;
        public Vector3 scale;
        //public int fontid;
        public int fontsize;
        public Color color;
        public string text;
    }
    struct TextBase
    {
        public GameObject gameobject;
        public Transform transform;
        public MeshRenderer meshrender;
        public TextMesh textmesh;
    }

    struct UITextBase
    {
        public GameObject gameobject;
        public RectTransform transform;
        public Text text;
    }
    struct UIImageBase
    {
        public GameObject gameobject;
        public RectTransform transform;
        public Image image;
        public int spriteid;
    }
    struct UIImageProperty
    {
        public int spt_id;
        public int spt_index;
        public Vector3 location;
        public Vector3 angle;
        public Vector3 scale;
        public Vector2 size;
        public Grid grid;
        public string imagepath;
        public SpriteInfo[] sprite;
    }
    struct UITextProperty
    {
        public Vector3 location;
        public Vector3 angle;
        public Vector3 scale;
        public Vector2 size;
        public int fontid;
        public int fontsize;
        public Color color;
        public string text;
    }

    struct WarPlane
    {
        public int gold;
        public float blood;
        public float currentblood;
        public int spt_id;
        public int spt_index;
        public int imageid;
        public Action play;
        public ImageProperty image;
        //public Point2[] edgepoints;
        //public Transform transform;
        //public StartPoint[] startpoints;
        public bool specital;
        public BulletPropertyEX bullet;
        public SpecitialBullet sp_bullet;
        public ShotEX[] shot;
        public int shotfrequency;
        public int bulletid;
        public int extra_a;//move recorder
        public int extra_b;//bullet recorder
    }
    struct Wing
    {
        public int gold;
        public int imageid;
        public int imageid2;
        public int bulletid;
        public int spt_id;
        public int shotfrequency;
        public Action play;
        public ImageProperty image;
        public bool specital;
        public BulletPropertyEX bullet;
        public SpecitialBullet sp_bullet;
        public Transform transform;
        public ShotEX[] shot;
        public Vector3 location;
        public Vector3 location2;
        public Transform transform2;
        public int extra_b;
        //public int extra2;
    }
}
