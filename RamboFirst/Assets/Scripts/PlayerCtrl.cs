using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerCtrl : MonoBehaviour
{
    public float PLAYER_SPD;
    public static Transform cam_help;
    public static Camera cam1;
    public static Camera cam2;
    public static AudioSource cam_snd;
    public static AudioSource cam_snd2;
    public static WeapHandler weap;
    
    public bool player;
    public Transform cam_single;
    public Camera cam1_single;
    public Camera cam2_single;
    public Animator anims;
    
    public Transform axis_help;
    public Transform proj_point;
    public RotateScript[] rotors;
    public Transform prison_move;
    
    CharacterController ctrl;
    Transform tran;
    AudioSource snd;
    public bool locked;
    public bool needtomove;
    public static bool devmode;

    void Awake()
    {
        devmode = false;
        ctrl = GetComponent<CharacterController>();
        tran = GetComponent<Transform>();
        snd = GetComponent<AudioSource>();
        locked = false;
        needtomove = false;
        if(player)
        {
            anims.SetBool("move", false);
            cam_help = cam_single;
            cam1 = cam1_single;
            cam2 = cam2_single;
            cam_snd = cam1.GetComponent<AudioSource>();
            cam_snd.clip = SoundHandler.GetMus(GameMus.Main);
            cam_snd.Play();
            cam_snd2 = cam1.transform.GetChild(0).GetComponent<AudioSource>();
        }
        else
        {
            //Editor always re-enabled it
            //Needs to be disabled, otherwise an enemy can somehow move the helicopter and it will trigger
            ctrl.enabled = false;
            snd.clip = SoundHandler.GetSnd(GameSnd.Helicopter);
        }
    }
 
    void Update()
    {
        if(locked) return;
        if(Input.GetButtonDown("Weap")) weap.ChangeWeapon();
        else if(Input.GetButtonDown("Cam")) cam1.enabled = !(cam2.enabled = cam1.enabled);
        else if(Input.GetButtonDown("Fire") && weap.CanFire(player)) weap.CreateProj(proj_point, player, GetComponent<AudioSource>());
        if(devmode)
        {
            if(Input.GetKey(KeyCode.Backspace))
            {
                if(Input.GetKeyDown(KeyCode.F1)) GameControl.TeleportPlayer(0);
                else if(Input.GetKeyDown(KeyCode.F2)) GameControl.TeleportPlayer(1);
                else if(Input.GetKeyDown(KeyCode.F3)) GameControl.TeleportPlayer(2);
                else if(Input.GetKeyDown(KeyCode.F4)) GameControl.TeleportPlayer(3);
                else if(Input.GetKeyDown(KeyCode.F5)) GameControl.TeleportPlayer(4);
                else if(Input.GetKeyDown(KeyCode.F6)) GameControl.TeleportPlayer(5);
                else if(Input.GetKeyDown(KeyCode.F9)) GameControl.AddScore(1000);
                else if(Input.GetKeyDown(KeyCode.F10)) GameControl.AddScore(10000);
                else if(Input.GetKeyDown(KeyCode.F11)) GameControl.RestoreEnergy();
                else if(Input.GetKeyDown(KeyCode.F12)) GameControl.Damage(10);
            }
        }
        else
        {
            if(Input.GetKey(KeyCode.Backspace) &&
             Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.E) && Input.GetKeyDown(KeyCode.V))
            {
                devmode = true;
                snd.PlayOneShot(SoundHandler.GetSnd(GameSnd.Pickup));
            }
        }
    }
    
    void FixedUpdate()
    {
        //Prison rescue script
        if(locked)
        {
            if(needtomove)
            {
                Vector3 dir = (Vector3.forward * -1.0f);
                ctrl.Move(PLAYER_SPD * dir * Time.fixedDeltaTime);
                axis_help.LookAt(tran.position + dir);
                anims.SetBool("move", true);
                if(tran.position.z <= prison_move.position.z)
                {
                    needtomove = false;
                    POWSpawn.inst.Trigger();
                }
            }
            else
            {
                anims.SetBool("move", false);
            }
            return;
        }
        
        float ver = Input.GetAxisRaw("Vertical");
        float hor = Input.GetAxisRaw("Horizontal");
        float inputmod = hor != 0 && ver != 0 ? 0.7071f : 1.0f;
        if (ver != 0 || hor != 0)
        {
            Vector3 dir = (Vector3.forward * ver + Vector3.right * hor);
            ctrl.Move(PLAYER_SPD * dir * inputmod * Time.fixedDeltaTime);
            //int iver = Mathf.CeilToInt(Input.GetAxisRaw("Vertical"));
            //int ihor = Mathf.CeilToInt(Input.GetAxisRaw("Horizontal"));
            //axis_help.localRotation = GameControl.inst.rot_dirs[iver + 1, ihor + 1];
            axis_help.LookAt(tran.position + dir);
        }
        else ctrl.Move(Vector3.zero);
        if(player) anims.SetBool("move", ctrl.velocity.magnitude > 0.1f);
    }
    
    public void EnableControl(bool input)
    {
        enabled = input;
        ctrl.enabled = input;
        //Move heli up
        if(input)
        {
            if(!player)
            {
                //Brief script handles this later
                //snd.Play();
                Vector3 pos = tran.position;
                pos.y = 25.0f;
                tran.position = pos;
            }
            GameControl.player_tr = tran;
        }
        else
        {
            if(!player) snd.Stop();
        }
        if(!player)
        {
            foreach(RotateScript r in rotors) r.enabled = input;
        }
    }
    
    public static void ChangeCamTo(Transform tran)
    {
        cam_help.parent = tran;
        cam_help.localPosition = Vector3.zero;
    }
}