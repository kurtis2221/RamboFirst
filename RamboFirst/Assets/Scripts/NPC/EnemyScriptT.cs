using UnityEngine;
using System.Collections;

public class EnemyScriptT : IntObjBase
{
    //Ignore enemy and player
    const int LAYER_CHECK = ~(1 << 9 | 1 << 8);
    Transform tran;
    static Vector3 look_pos = new Vector3(0.0f, 12.0f, 0.0f);
    static Vector3 corr_pos = new Vector3(0.0f, 6.0f, 0.0f);
    public Animator anims;
    public Transform axis_help;
    public Transform proj_point;
    bool pl_near;
    
	void Awake()
    {
	    tran = GetComponent<Transform>();
        pl_near = false;
	}
    
    void OnEnable()
    {
        StartCoroutine(FireBullet());
    }
    
	void FixedUpdate()
    {
        Vector3 pos = tran.position;
        Vector3 pl_pos = GameControl.player_tr.position;
        pl_near = Vector3.Distance(pos, pl_pos) < 50.0f;
        if(pl_near)
        {
            axis_help.LookAt(pl_pos + look_pos);
        }
	}
    
    public override void Damage()
    {
        StopAllCoroutines();
        enabled = false;
        tran.position -= look_pos;
        GameControl.AddScore(100);
        SoundScript.CreateSound(tran, SoundHandler.GetSnd(GameSnd.Death));
        anims.Play("Death");
        StartCoroutine(DelayDestroy());
    }
    
    IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(0.5f);
        EnemySpawner.inst.tower_enemies.Remove(gameObject);
        Destroy(gameObject);
    }
    
    IEnumerator FireBullet()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(2, 7));
            if(!pl_near) continue;
            Vector3 pos = GameControl.player_tr.position + corr_pos;
            proj_point.LookAt(pos);
            Instantiate(GameControl.inst.enemy_bullet, proj_point.position, proj_point.rotation);
        }
    }
}