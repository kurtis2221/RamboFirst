using UnityEngine;
using System.Collections;

public class RotateScript : MonoBehaviour
{
    public Vector3 dir;
    public float speed;
    Transform tran;
    
    void Awake()
    {
        tran = GetComponent<Transform>();
    }
    
	void FixedUpdate()
    {
        tran.Rotate(dir, speed);
	}
}