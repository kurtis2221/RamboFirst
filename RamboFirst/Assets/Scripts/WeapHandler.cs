using UnityEngine;
using System.Collections;

public class WeapHandler : MonoBehaviour
{
	[System.Serializable]
    public class Weapon
    {
        public GameSnd sound;
        public Object obj;
        public bool active;
        public bool onlyplayer;
    }
    
    const float FIRE_WAIT = 0.15f;
    const int MAX_WEAPONS = 6;
    const float COLOR_SPEED = 0.05f;
    
    public static int MAX_BULLETS;
    public Weapon[] weapons;
    public GUITexture[] hud_weapons;
    Weapon curr_weap;
    GUITexture curr_hud;
    static bool canfire = true;
    public static int weapid = 0;
    public static int bullets = 0;
    Color col_normal = new Color(0.5f,0.5f,0.5f,0.5f);
    Color col_curr = new Color(0.5f,0.5f,0.5f,0.5f);
    bool col_dir;
    
    void Awake()
    {
        MAX_BULLETS = 3;
        PlayerCtrl.weap = this;
        weapid = 0;
        bullets = 0;
        curr_weap = weapons[weapid];
        curr_hud = hud_weapons[weapid];
    }
    
    void FixedUpdate()
    {
        if(col_dir)
        {
            col_curr.r += COLOR_SPEED;
            col_curr.g += COLOR_SPEED;
            col_curr.b += COLOR_SPEED;
            if(col_curr.r >= 1.0f) col_dir = false;
        }
        else
        {
            col_curr.r -= COLOR_SPEED;
            col_curr.g -= COLOR_SPEED;
            col_curr.b -= COLOR_SPEED;
            if(col_curr.r <= 0.0f) col_dir = true;
        }
        curr_hud.color = col_curr;
    }
    
    public bool CanFire(bool player)
    {
        return canfire && (player || player == curr_weap.onlyplayer) && bullets < MAX_BULLETS;
    }
    
    public void ChangeWeapon()
    {
        do
        {
            weapid--;
            if(weapid < 0) weapid = MAX_WEAPONS - 1;
        }
        while(!weapons[weapid].active);
        curr_weap = weapons[weapid];
        curr_hud.color = col_normal;
        curr_hud = hud_weapons[weapid];
    }
    
    public void EnableWeapon(int weapid)
    {
        weapons[weapid].active = true;
        hud_weapons[weapid].enabled = true;
    }
    
    public void CreateProj(Transform proj_point, bool player, AudioSource audio)
    {
        canfire = false;
        bullets++;
        if(curr_weap.sound != GameSnd.None) audio.PlayOneShot(SoundHandler.GetSnd(curr_weap.sound));
        Instantiate(curr_weap.obj, proj_point.position, proj_point.rotation);
        StartCoroutine(FireSleep());
    }
    
    IEnumerator FireSleep()
    {
        yield return new WaitForSeconds(FIRE_WAIT);
        canfire = true;
    }
}