using UnityEngine;
using System.Collections;

public class POWScript : MonoBehaviour
{
    const float MOVE_SPEED = 0.3f;
    Transform tran;
    int idx;
    
    void Awake()
    {
        tran = GetComponent<Transform>();
        idx = 0;
    }
    
	void FixedUpdate()
    {
        Vector3 trg = POWSpawn.inst.locations[idx].position;
        tran.position = Vector3.MoveTowards(tran.position, trg, MOVE_SPEED);
        if(Vector3.Distance(tran.position, trg) < 0.1f)
        {
            tran.rotation = POWSpawn.inst.locations[idx].rotation;
            idx++;
            if(idx == POWSpawn.inst.locations.Length)
            {
                POWSpawn.rescued_pow++;
                if(POWSpawn.rescued_pow >= POWSpawn.SPAWN_NUMBER)
                {
                    GameControl.inst.playerp_sc.locked = false;
                    EnemySpawner.EnableSpawn();
                }
                Destroy(gameObject);
            }
        }
	}
}