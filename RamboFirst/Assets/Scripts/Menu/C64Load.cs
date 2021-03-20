using UnityEngine;
using System.Collections;

public class C64Load : MonoBehaviour
{
    public Transform box, box2;
    Vector3 hor_move = new Vector3(0.005f,0f,0f);
    Vector3 ver_move = new Vector3(0f,-0.1f,0f);
    Vector3 pos;
    
    void Awake()
    {
        pos = box2.localPosition;
    }
    
	void FixedUpdate()
    {
        pos += hor_move;
	    box2.localPosition = pos;
        if(box2.localPosition.x >= 1.0f)
        {
            pos.x = 0;
            box2.localPosition = pos;
            box.localPosition += ver_move;
            if(box.localPosition.y <= -1.1f)
            {
                enabled = false;
            }
        }
	}
}