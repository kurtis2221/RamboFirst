using UnityEngine;
using System.Collections;

public class MenuName : MonoBehaviour
{
    const float COLOR_SPEED = 0.07f;
    //const float COLOR2_SPEED = 0.04f;
    static Vector3 BOX_FIX = new Vector3(0.0f, 0.005f, 0.0f);
    
    Color col = new Color(1.0f,1.0f,1.0f,1.0f);
    bool dir;
    public TextMesh title;
    public TextMesh box;
    public TextMesh cursor;
    public Transform[] chars;
    TextMesh[] char_meshes;
    Color[] char_colors;
    //bool[] char_coldir;
    int boxidx = 0;
    int offset = 0;
    
    void Awake()
    {
        AudioClip clip = SoundHandler.GetMus(GameMus.Name);
        GetComponent<AudioSource>().PlayOneShot(clip);
        char_colors = new Color[GameControl.game_colors.Length];
        //char_coldir = new bool[GameControl.game_colors.Length];
        for(int i = 0; i < GameControl.game_colors.Length; i++)
        {
            char_colors[i] = GameControl.game_colors[i];
            //char_colors[i].a -= i * 0.2f;
        }
        char_meshes = new TextMesh[chars.Length];
        for(int i = 0; i < chars.Length; i++)
            char_meshes[i] = chars[i].GetComponent<TextMesh>();
        StartCoroutine(ShiftColors());
    }
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            boxidx--;
            if(boxidx < 0) boxidx = chars.Length - 1;
            box.transform.position = chars[boxidx].position + BOX_FIX;
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            boxidx++;
            if(boxidx >= chars.Length) boxidx = 0;
            box.transform.position = chars[boxidx].position + BOX_FIX;
        }
        else if(Input.GetKeyDown(KeyCode.Space))
        {
            if(boxidx == 0)
            {
                GameControl.player_name = cursor.text.Remove(cursor.text.Length - 1).PadRight(10,' ');
                Application.LoadLevel((int)GameControl.GameLevels.Ready);
            }
            else if(boxidx == 28)
            {
                if(cursor.text.Length > 1)
                    cursor.text = cursor.text.Remove(cursor.text.Length - 2) + "|";
            }
            else if(cursor.text.Length <= 10)
            {
                if(boxidx == 27) cursor.text = InsertChars(cursor.text, " ");
                else cursor.text = InsertChars(cursor.text, chars[boxidx].GetComponent<TextMesh>().text);
            }
        }
    }
    
    string InsertChars(string txt, string ch)
    {
        return txt.Insert(txt.Length - 1, ch);
    }
    
    void FixedUpdate()
    {
        if(dir)
        {
            col.a += COLOR_SPEED;
            if(col.a >= 1.0f) dir = false;
        }
        else
        {   col.a -= COLOR_SPEED;
            if(col.a <= 0.0f) dir = true;
        }
        title.color = col;
        box.color = col;
        cursor.color = col;
        //Char colors
        /*for(int i = 0; i < char_colors.Length; i++)
        {
            if(char_coldir[i])
            {
                char_colors[i].a += COLOR2_SPEED;
                if(char_colors[i].a >= 1.0f) char_coldir[i] = false;
            }
            else
            {   char_colors[i].a -= COLOR2_SPEED;
                if(char_colors[i].a <= 0.0f) char_coldir[i] = true;
            }
        }*/
        for(int i = 0; i < char_meshes.Length; i++)
        {
            char_meshes[i].color = char_colors[(char_meshes.Length - i + offset) % char_colors.Length];
        }
    }
    
    IEnumerator ShiftColors()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.1f);
            offset = (offset + 1) % char_colors.Length;
        }
    }
}