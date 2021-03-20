using UnityEngine;
using System.Collections;

public class EnemyScript : IntObjBase
{
    //Ignore enemy and player
    const int LAYER_CHECK = ~(1 << 9 | 1 << 8);
    const float MIN_DIST = 2.5f;
    const float MAX_DIST = 100.0f;
    const float ANGLE_DIR_FIX = 0.7071f;
    const int MAX_DIRS = 8;
    
    public float ENEMY_SPD;
    
    public Animator anims;
    public Renderer rend;
    public Transform axis_help;
    public Transform proj_point;
    static Vector3 corr_pos = new Vector3(0.0f, 6.0f, 0.0f);
    
    static Vector3[] dirs =
    {
        Vector3.forward,
        (Vector3.forward + Vector3.right) * ANGLE_DIR_FIX,
        Vector3.right,
        (Vector3.forward * -1 + Vector3.right) * ANGLE_DIR_FIX,
        Vector3.forward * -1,
        (Vector3.forward * -1 + Vector3.right * -1) * ANGLE_DIR_FIX,
        Vector3.right * -1,
        (Vector3.forward + Vector3.right * -1) * ANGLE_DIR_FIX
    };
    
    Collider col;
    Transform tran;
    Rigidbody rig;
    Vector3 dir;
    
    void Awake()
    {
        col = GetComponent<Collider>();
        tran = GetComponent<Transform>();
        rig = GetComponent<Rigidbody>();
        rend.material = EnemySpawner.inst.enemy_cols[Random.Range(0, EnemySpawner.inst.enemy_cols.Length)];
        EnemySpawner.current_enemies++;
        Move(tran.position);
        StartCoroutine(ChangeDir());
        StartCoroutine(FireBullet());
    }
    
    void FixedUpdate()
    {
        Vector3 plpos = GameControl.player_tr.position;
        Vector3 pos = tran.position;
        float dist = Vector3.Distance(plpos, pos);
        if(dist <= MIN_DIST)
        {
            Damage();
            GameControl.Damage(10);
        }
        else if(dist >= MAX_DIST)
        {
            EnemySpawner.DestroyEnemy(gameObject);
        }
        if(Physics.CheckSphere(pos + dir, 2.0f, LAYER_CHECK))
        {
            Move(pos);
        }
    }
    
    void Move(Vector3 pos)
    {
        dir = dirs[Random.Range(0, MAX_DIRS)];
        for(int i = 0; i < MAX_DIRS; i++)
        {
            if(!Physics.CheckSphere(pos + dir, 2.0f, LAYER_CHECK)) break;
            dir = dirs[i];
        }
        rig.velocity = dir * ENEMY_SPD;
        axis_help.LookAt(pos + dir);
    }
    
    public override void Damage()
    {
        //Prevent any movement when playing death animation
        StopAllCoroutines();
        enabled = false;
        col.enabled = false;
        rig.velocity = Vector3.zero;
        GameControl.AddScore(100);
        SoundScript.CreateSound(tran, SoundHandler.GetSnd(GameSnd.Death));
        anims.Play("Death");
        StartCoroutine(DelayDestroy());
    }
    
    IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(0.5f);
        EnemySpawner.DestroyEnemy(gameObject);
    }
    
    IEnumerator ChangeDir()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(2, 7));
            Move(tran.position);
        }
    }
    
    IEnumerator FireBullet()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(2, 7));
            Vector3 pos = GameControl.player_tr.position + corr_pos;
            proj_point.LookAt(pos);
            Instantiate(GameControl.inst.enemy_bullet, proj_point.position, proj_point.rotation);
        }
    }
}