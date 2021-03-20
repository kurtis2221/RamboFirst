using UnityEngine;
using System.Collections;

public class ProjScript : MonoBehaviour
{
    public GameSnd sound;
    public bool explosive;
    const float DESTR_TIME = 1.0f;
    const float BULLET_SPEED = 80f;
    Transform tran;
    
    const int RAYCAST_LAYER = 1 << 2;
    const int PLAYER_LAYER = 1 << 8;
    const int ENEMY_LAYER = 1 << 9;
    int check_layer;

    void Awake()
    {
        if(explosive) check_layer = ~(RAYCAST_LAYER | PLAYER_LAYER);
        else check_layer = ENEMY_LAYER;
        tran = GetComponent<Transform>();
        GetComponent<Rigidbody>().velocity = tran.forward * BULLET_SPEED;
        Destroy(gameObject, DESTR_TIME);
    }

	void FixedUpdate()
    {
        Collider[] cols = Physics.OverlapSphere(tran.position, 2.0f);
        foreach(Collider col in cols)
        {
            if((1 << col.gameObject.layer & check_layer) > 0)
            {
                IntObjBase tmp = col.GetComponent<IntObjBase>();
                if(tmp != null) tmp.Damage();
                if(explosive)
                {
                    Instantiate(GameControl.inst.expl_part,tran.position,tran.rotation);
                    ExplosiveDamage(col);
                }
                if(sound != GameSnd.None) SoundScript.CreateSound(tran, SoundHandler.GetSnd(sound));
                Destroy(gameObject);
                return;
            }
        }
	}
    
    void ExplosiveDamage(Collider input)
    {
        Collider[] cols = Physics.OverlapSphere(tran.position, 6.0f);
        foreach(Collider col in cols)
        {
            //Ignore self
            if(col == input) continue;
            if((1 << col.gameObject.layer & check_layer) > 0)
            {
                IntObjBase tmp = col.GetComponent<IntObjBase>();
                if(tmp != null) tmp.Damage();
            }
        }
    }
    
    void OnDestroy()
    {
        if(WeapHandler.bullets > 0) WeapHandler.bullets--;
    }
}