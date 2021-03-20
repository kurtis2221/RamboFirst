using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameControl : MonoBehaviour
{
    [System.Serializable]
    public class DamageItem
    {
        public MeshFilter[] data;
    }
    
    public class ScoreItem
    {
        public string name;
        public int score;
    }
    
    public enum GameLevels
    {
        Load,
        Menu,
        Name,
        Ready,
        Game,
        GameO,
        End,
        HighSc,
        Trainer
    }
    
    public enum GameProg
    {
        None = 0,
        PowRes = 1,
        PrisonOpen = 2
    }
    
    public const float BASE_WIDTH = 1024.0f;
    public const float BASE_HEIGHT = 768.0f;
    
    public static GameProg GlobalProg;
    public static string player_name;
    public static bool unlimited_energy;
    public static int player_energy;
    public static int player_score;
    public static bool player_drain;
    public static ScoreItem[] scores;
    
    public static Color[] game_colors =
    {
        new Color(1.0f,1.0f,1.0f,1.0f), //White
        new Color(1.0f,1.0f,0.5f,1.0f), //Yellow
        new Color(0.375f,0.75f,0.75f,1.0f), //Blue
        new Color(0.5f,0.625f,0.3125f,1.0f), //Green
        new Color(0.5625f,0.3125f,0.6875f,1.0f), //Purple
        new Color(0.625f,0.25f,0.25f,1.0f), //Red
        new Color(0.1875f,0.25f,0.5625f,1.0f), //DarkBlue
        new Color(0.0f,0.0f,0.0f,1.0f) //Black
    };
    public static string[] game_colors_hexstr =
    {
        "ffffffff", //White
        "ffff80ff", //Yellow
        "60c0c0ff", //Blue
        "80a050ff", //Green
        "9050b0ff", //Purple
        "a04040ff", //Red
        "304090ff" //DarkBlue
    };
    
    public static Transform player_tr;
    
    public Transform playerp_tr;
    public Transform playerh_tr;
    public PlayerCtrl playerp_sc;
    public PlayerCtrl playerh_sc;
    
    public GameObject sound_obj;
    
    public Transform stream_parent;
    public GameObject expl_part;
    [SerializeField]
    public List<DamageItem> damage_mesh;
    public Transform[] teleports;
    public Object enemy_bullet;
    public Object enemy_rocket;
    
    public GameObject hud;
    
    /*public Quaternion[,] rot_dirs =
    {
        {Quaternion.Euler(0,225,0), Quaternion.Euler(0,180,0), Quaternion.Euler(0,135,0)},
        {Quaternion.Euler(0,270,0), Quaternion.Euler(0,0,0), Quaternion.Euler(0,90,0)},
        {Quaternion.Euler(0,315,0), Quaternion.Euler(0,0,0), Quaternion.Euler(0,45,0)},
    };*/
    
    public static GameControl inst;
    
    void Awake()
    {
        inst = this;
        player_tr = playerp_tr;
        player_energy = 100;
        ScoreHud.inst.SetValue(player_score);
        GlobalProg = GameProg.None;
    }
    
    public void SwitchControl(bool isplayer, Transform tr)
    {
        WeapHandler.MAX_BULLETS = isplayer ? 3 : 1;
        playerp_sc.EnableControl(isplayer);
        playerh_sc.EnableControl(!isplayer);
        PlayerCtrl.ChangeCamTo(player_tr);
    }
    
    public static void TeleportPlayer(int idx)
    {
        Vector3 pos = inst.teleports[idx].position;
        pos.y = player_tr.position.y;
        player_tr.position = pos;
    }
    
    public static void AddScore(int input)
    {
        player_score += input;
        ScoreHud.inst.SetValue(player_score);
    }
    
    public static void RestoreEnergy()
    {
        player_energy = 100;
        HealthHud.inst.SetValue(player_energy);
    }
    
    public static void Damage(int input)
    {
        if(unlimited_energy) return;
        player_energy -= input;
        if(player_energy <= 0)
        {
            Application.LoadLevel((int)GameLevels.GameO);
            return;
        }
        HealthHud.inst.SetValue(player_energy);
    }
    
    public static void SetEnergyDrain(int level)
    {
        if(level == 0) inst.StopAllCoroutines();
        else
        {
            float sec;
            if(level == 1) sec = 1.0f;
            else sec = 10.0f;
            inst.StartCoroutine(inst.DrainEnergy(sec));
        }
    }
    
    IEnumerator DrainEnergy(float sec)
    {
        while(true)
        {
            yield return new WaitForSeconds(sec);
            Damage(5);
        }
    }
}