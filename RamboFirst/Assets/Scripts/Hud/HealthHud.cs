using UnityEngine;
using System.Collections;

public class HealthHud : MonoBehaviour
{
    const float EFF_SPEED = 0.02f;
    
    public Texture tex1, tex2;
    public Color col_norm;
    public Color col_warn;
    
    private Rect hud_area, hud_area2;
    private Rect hud_health, hud_health2;
    private float max_width;
    private Color curr_col;
    private bool warning;
    private bool dir;
    
    public static HealthHud inst;
    
    void Awake()
    {
        float sc_h = Screen.height / GameControl.BASE_HEIGHT;
        inst = this;
        float height = Screen.height;
        max_width = 256 * sc_h;
        hud_area = new Rect(0 * sc_h, height - 96 * sc_h, max_width, 32 * sc_h);
        hud_area2 = new Rect(256 * sc_h, height - 96 * sc_h, 0 * sc_h, 32 * sc_h);
        hud_health = new Rect(0 * sc_h, 0 * sc_h, 256 * sc_h, 32 * sc_h);
        hud_health2 = new Rect(0 * sc_h, 0 * sc_h, 256 * sc_h, 32 * sc_h);
        SetValue(100);
    }
    
    void FixedUpdate()
    {
        if(warning)
        {
            if(dir)
            {
                curr_col.r += EFF_SPEED;
                curr_col.g += EFF_SPEED;
                curr_col.b += EFF_SPEED;
                if(curr_col.r >= 1) dir = false;
            }
            else
            {
                curr_col.r -= EFF_SPEED;
                curr_col.g -= EFF_SPEED;
                curr_col.b -= EFF_SPEED;
                if(curr_col.r <= 0) dir = true;
            }
        }
    }
    
    public void SetValue(int val)
    {
        bool warning = val <= 20;
        if(warning)
        {
            if(this.warning != warning)
            {
                dir = false;
                curr_col = col_warn;
            }
        }
        else curr_col = col_norm;
        this.warning = warning;
        float width = max_width * (val / 100.0f);
        hud_area.width = width;
        hud_area2.x = width;
        hud_area2.width = max_width - width;
        hud_health2.x = -width;
    }
    
    void OnGUI()
    {
        if(Event.current.type == EventType.Repaint)
        {
            GUI.BeginGroup(hud_area);
            GUI.color = curr_col;
            GUI.DrawTexture(hud_health, tex1);
            GUI.EndGroup();
            GUI.BeginGroup(hud_area2);
            GUI.color = curr_col;
            GUI.DrawTexture(hud_health2, tex2);
            GUI.EndGroup();
        }
    }
}