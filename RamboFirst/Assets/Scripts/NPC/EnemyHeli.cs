using UnityEngine;
using System.Collections;

public class EnemyHeli : IntObjBase
{
    const int PLAYER_LAYER = 1 << 8;
    public int COOLDOWN_TIME;
    public float CHASE_SPD;
    public float HELI_SPD;
    public float DIE_SPD;
    public int DELAY_TIME;
    
    public Transform axis_help;
    public Transform proj_point;
    public RotateScript rotsc;
    public RotateScript[] rotors;
    
    Transform tran;
    Rigidbody rig;
    Collider col;
    AudioSource snd;
    Vector3 resp_pos;
    
    public static EnemyHeli inst;
    int cooldown;
    int delay;
    int health;
    bool dying;
    
    void Awake()
    {
        inst = this;
        tran = GetComponent<Transform>();
        rig = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        snd = GetComponent<AudioSource>();
        snd.clip = SoundHandler.GetSnd(GameSnd.Helicopter2);
        resp_pos = tran.position;
        cooldown = 0;
        health = 100;
    }
    
    void Start()
    {
        snd.Play();
        foreach(RotateScript r in rotors) r.enabled = true;
    }
    
    void FixedUpdate()
    {
        if(dying)
        {
            rig.velocity = Vector3.right * DIE_SPD;
            if(tran.position.x <= -350.0f)
            {
                snd.Play();
                col.enabled = true;
                rotsc.enabled = false;
                tran.position = resp_pos;
                dying = false;
                health = 100;
            }
        }
        if(delay <= 0)
        {
            Vector3 pos = tran.position;
            Vector3 pl_pos = GameControl.player_tr.position;
            float speed = Vector3.Distance(pos, pl_pos) < 40.0f ? HELI_SPD : CHASE_SPD;
            delay = DELAY_TIME;
            Vector3 dir = (GameControl.player_tr.position - pos).normalized;
            rig.velocity = dir * speed;
            axis_help.LookAt(pos + dir);
        }
        else delay--;
        if(cooldown <= 0)
        {
            if(Physics.Raycast(proj_point.position, proj_point.forward, 40.0f, PLAYER_LAYER))
            {
                Instantiate(GameControl.inst.enemy_rocket, proj_point.position, proj_point.rotation);
                cooldown = COOLDOWN_TIME;
            }
        }
        else cooldown--;
    }
    
    public override void Damage()
    {
        health -= 20;
        GameControl.AddScore(2000);
        if(health <= 0)
        {
            snd.Stop();
            col.enabled = false;
            rotsc.enabled = true;
            dying = true;
        }
    }
}