using UnityEngine;
using System.Collections;

public class TriggerScript : MonoBehaviour
{
    public TriggerScript sc_enable;
    public TriggerScript sc_enable2;
    public GameControl.GameProg req_stat;
    public GameControl.GameProg set_stat;
    public bool sw_control;
    public bool sw_player;
    public bool win;
    public Transform tr_player;
    public Transform tr_heli;
    public GameObject dest_obj;
    public GameSnd sound;
    public bool knife;
    public bool pausemus;
    public string brief_text;
    public GameMus brief_mus;
    public int score;
    public int drain_lvl;
    
	void OnTriggerStay(Collider col)
    {
	    if(col.gameObject.layer != 8)
            return;
        //
        GameControl.AddScore(score);
        //
        if(win)
        {
            Application.LoadLevel((int)GameControl.GameLevels.End);
            return;
        }
        //
        if(knife && WeapHandler.weapid != 0)
            return;
        //
        if(dest_obj != null) Destroy(dest_obj);
        SetState(this, false);
        //
        if(req_stat != GameControl.GameProg.None &&
            req_stat != GameControl.GlobalProg)
        {
            Application.LoadLevel((int)GameControl.GameLevels.GameO);
            return;
        }
        if(req_stat == GameControl.GameProg.PowRes)
        {
            PlayerCtrl.weap.EnableWeapon(5);
        }
        else if(set_stat == GameControl.GameProg.PrisonOpen)
        {
            EnemySpawner.DisableSpawn();
            GameControl.inst.playerp_sc.locked = true;
            GameControl.inst.playerp_sc.needtomove = true;
        }
        GameControl.GlobalProg = set_stat;
        //
        if(sound != GameSnd.None)
        {
            AudioClip clip = SoundHandler.GetSnd(sound);
            if(pausemus)
            {
                PlayerCtrl.cam_snd.Pause();
                StartCoroutine(ResumeMusic(clip.length));
            }
            PlayerCtrl.cam_snd2.PlayOneShot(clip);
        }
        //
        if(sc_enable != null) SetState(sc_enable, true);
        if(sc_enable2 != null) SetState(sc_enable2, true);
        //
        if(sw_control)
        {
            GameControl.RestoreEnergy();
            if(sw_player)
            {
                GameControl.SetEnergyDrain(0);
                EnemySpawner.EnableSpawn();
            }
            else EnemySpawner.DisableSpawn();
            GameControl.RestoreEnergy();
            GameControl.inst.SwitchControl(sw_player, tr_player);
            if(tr_player != null)
            {
                Transform helitr = GameControl.inst.playerh_tr;
                helitr.position = tr_heli.position;
                helitr.GetComponent<PlayerCtrl>().axis_help.rotation = tr_heli.rotation;
                GameControl.inst.playerp_tr.position = tr_player.position;
            }
            else
            {
                GameControl.inst.playerp_tr.position = new Vector3(0,0,-500);
            }
        }
        //
        if(brief_text != string.Empty)
        {
            BriefHud.inst.LoadBrief(brief_text, brief_mus, drain_lvl);
        }
	}
    
    static void SetState(TriggerScript sc, bool stat)
    {
        sc.enabled = stat;
        sc.GetComponent<Collider>().enabled = stat;
    }
    
    IEnumerator ResumeMusic(float sec)
    {
        yield return new WaitForSeconds(sec);
        PlayerCtrl.cam_snd.UnPause();
    }
}