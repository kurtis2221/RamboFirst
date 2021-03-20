using UnityEngine;
using System.Collections;

public class WaterMove : MonoBehaviour
{
    public Material water_mat;
    public Vector2 move_offs;
    Vector2 offset = Vector2.zero;
    
    void FixedUpdate()
    {
        offset += move_offs;
        offset.x %= 1;
        offset.y %= 1;
        water_mat.mainTextureOffset = offset;
    }
}