using UnityEngine;
using System.Collections;

public class POWSpawn : MonoBehaviour
{
    public const int SPAWN_NUMBER = 10;
    
    public Object pow;
    public Transform spawn_point;
    public Transform[] locations;
    
    public static POWSpawn inst;
    public static int rescued_pow;
    int spawned;
    
    void Awake()
    {
        inst = this;
        rescued_pow = 0;
        spawned = 0;
    }
    
    public void Trigger()
    {
        StartCoroutine(SpawnTimer());
    }
    
    IEnumerator SpawnTimer()
    {
        while(spawned < SPAWN_NUMBER)
        {
            yield return new WaitForSeconds(1.0f);
            spawned++;
            GameControl.AddScore(2000);
            GameObject.Instantiate(pow, spawn_point.position, spawn_point.rotation);
        }
    }
}