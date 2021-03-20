using UnityEngine;
using System.Collections;

public class ProjEnScript : MonoBehaviour
{
    public GameSnd sound;
    public bool explosive;
    const float DESTR_TIME = 1.0f;
    public float BULLET_SPEED;
    Transform tran;
    
    const int RAYCAST_LAYER = 1 << 2;
    const int PLAYER_LAYER = 1 << 8;
    const int ENEMY_LAYER = 1 << 9;
    int check_layer;

    void Awake()
    {
        EnemySpawner.bullet_list.Add(gameObject);
        if(explosive) check_layer = ~(RAYCAST_LAYER | ENEMY_LAYER);
        else check_layer = PLAYER_LAYER;
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
                PlayerCtrl tmp = col.GetComponent<PlayerCtrl>();
                if(tmp != null)
                {
                    GameControl.Damage(10);
                    if(sound != GameSnd.None) SoundScript.CreateSound(tran, SoundHandler.GetSnd(sound));
                }
                if(explosive) Instantiate(GameControl.inst.expl_part,tran.position,tran.rotation);
                Destroy(gameObject);
                return;
            }
        }
    }
    
    void OnDestroy()
    {
        EnemySpawner.bullet_list.Remove(gameObject);
    }
}