using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    const int MAX_ENEMIES = 10;
    const float MIN_DIST = 40.0f;
    const float MAX_DIST = 80.0f;
    const float DESTR_TIME = 0.5f;
    const float BULLET_SPEED = 80f;
    
    public Object enemy;
    public Material[] enemy_cols;
    public List<GameObject> tower_enemies;
    
    public static int current_enemies;
    static List<Object> enemy_list;
    public static List<Object> bullet_list;
    static Quaternion zero_q = new Quaternion(0, 0, 0, 0);
    
    public static EnemySpawner inst;
    
    void Awake()
    {
        inst = this;
        current_enemies = 0;
        enemy_list = new List<Object>();
        bullet_list = new List<Object>();
    }
    
    void FixedUpdate()
    {
        if(current_enemies >= MAX_ENEMIES) return;
        //
        Vector3 pos = GameControl.player_tr.position;
        Vector3 enemy_pos;
        float x = Random.Range(MIN_DIST, MAX_DIST);
        float z = Random.Range(-MAX_DIST, MAX_DIST);
        int mod = Random.Range(0, 2) == 0 ? 1 : -1;
        if(Random.Range(0, 2) == 0)
            enemy_pos = pos + new Vector3(x * mod, 0.0f, z);
        else
            enemy_pos = pos + new Vector3(z, 0.0f, x * mod);
        //Bounds class doesn't work normally for some reason
        if(enemy_pos.x >= -120.0f && enemy_pos.x <= 120.0f &&
            enemy_pos.z >= 30.0f && enemy_pos.z <= 870.0f &&
            !Physics.CheckSphere(enemy_pos, 4.0f))
            enemy_list.Add(GameObject.Instantiate(enemy, enemy_pos, zero_q));
    }
    
    public static void DisableSpawn()
    {
        inst.enabled = false;
        foreach(Object e in enemy_list)
            Destroy(e);
        enemy_list.Clear();
        //
        foreach(Object b in bullet_list)
            Destroy(b);
        bullet_list.Clear();
        foreach(GameObject e in inst.tower_enemies)
            e.SetActive(false);
        current_enemies = 0;
    }
    
    public static void EnableSpawn()
    {
        inst.enabled = true;
        foreach(GameObject e in inst.tower_enemies)
            e.SetActive(true);
    }
    
    public static void DestroyEnemy(Object obj)
    {
        enemy_list.Remove(obj);
        Destroy(obj);
        current_enemies--;
    }
}