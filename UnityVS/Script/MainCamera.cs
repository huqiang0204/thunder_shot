#define Phone
#undef Phone
#define run
#undef run
#define desktop
#undef desktop
#define PC
#undef PC
using System;
using UnityEngine;
using Assets.UnityVS.Script;

 class MainCamera : MonoBehaviour {
    // Use this for initialization
    //720/1280=0.5625
    Transform tf;
    static int cycle;
    static void Buff()
    {
#if PC
        Screen.SetResolution(720,1280,false);
#endif
        for(int i=0;i<10;i++)
           QueueSourceEX.CreateImagePreBuff();
        cycle--;
        if (cycle <= 0)
            update = null;
    }
    void  Start () {
        tf = transform;
#if Phone
        Vector3 scale = tf.localScale;
        scale.x = (float)Screen.width / (float)Screen.height / 0.5625f;
        tf.localScale = scale;  
#endif
        QueueSourceEX.def_font = Resources.Load("Fonts/STXINGKA") as Font;                 
        CanvasControl.Language = 0;       
        CanvasControl.Initial();
        FileManage.LoadDisposeFile();
        GameObject temp = new GameObject();
        
        Canvas c= temp.AddComponent<Canvas>();      
        c.renderMode = RenderMode.ScreenSpaceOverlay;
        //c.worldCamera = Camera.main;
        //c.planeDistance = -1;
        QueueSourceEX.main_canvas = c.transform;
        c.transform.localScale = new Vector3(1,1);
#if PC
        AsyncManage.Inital();
#endif
#if run
        cycle=100;
        update=Buff;
#endif
        GameControl.LoadBaseComponent();
    }
    // Update is called once per frame
    static int clicktime;
    void Update()
    {
#if desktop
        Vector3 scale = tf.localScale;
        scale.x = (float)Screen.width / (float)Screen.height / 0.5625f;
        tf.localScale = scale;
#endif
        if (clicktime > 0)
            clicktime--;
        if (Input.GetMouseButtonDown(0))
        {
            QueueSourceEX.Mouse_Down(Input.mousePosition);
            clicktime = 20;
        }           
        else if (Input.GetMouseButtonUp(0))
        {
            if (clicktime > 0)
                QueueSourceEX.Mouse_Click(Input.mousePosition);
            QueueSourceEX.Mouse_Up(Input.mousePosition);
        }            
        else
            QueueSourceEX.Mouse_Move(Input.mousePosition);
#if PC || desktop
        if (Input.GetKeyDown(KeyCode.Escape))
            if (CanvasControl.BackUp() == false)
                Application.Quit();
#endif
#if Phone
        if (CanvasControl.Quit)
        {
            if (CanvasControl.BackUp() == false)
            {
                Application.Quit();
                CanvasControl.AppQuit();
            }
            else CanvasControl.Quit = false;
        }                         
#endif
    }
    public static Action update; 
    void FixedUpdate()
    {
        if (update != null)
            update();
    }
    void OnApplicationQuit()
    { 
        if(CanvasControl.BackUp())
           Application.CancelQuit();
        FileManage.SaveDisposeFile();
    }
}
