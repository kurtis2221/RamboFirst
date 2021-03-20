using UnityEngine;
using System.Collections;

public class ScoreHud : MonoBehaviour
{
    const int SCORE_NUMS = 6;
    
    public Texture score;
    public Texture[] numbers;
    public Color col_norm;
    private Rect hud_score;
    private Rect[] hud_numbers;
    private int[] num_val;
    
    public static ScoreHud inst;
    
    void Awake()
    {
        float sc_h = Screen.height / GameControl.BASE_HEIGHT;
        inst = this;
        float height = Screen.height;
        float max_width = 256 * sc_h;
        hud_score = new Rect(0 * sc_h, height - 64 * sc_h, max_width, 32 * sc_h);
        hud_numbers = new Rect[SCORE_NUMS];
        num_val = new int[SCORE_NUMS];
        for(int i = 0; i < SCORE_NUMS; i++)
            hud_numbers[i] = new Rect((i * 44) * sc_h, height - 32 * sc_h, 44 * sc_h, 32 * sc_h);
        SetValue(0);
    }
    
    public void SetValue(int number)
    {
        for(int i = SCORE_NUMS - 1; i >= 0; i--)
        {
            num_val[i] = number % 10;
            number /= 10;
        }
    }
    
	void OnGUI()
    {
        GUI.color = col_norm;
        GUI.DrawTexture(hud_score, score);
        for(int i = 0; i < SCORE_NUMS; i++)
            GUI.DrawTexture(hud_numbers[i], numbers[num_val[i]]);
	}
}