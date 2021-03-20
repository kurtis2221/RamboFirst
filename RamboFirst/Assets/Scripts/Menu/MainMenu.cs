using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;

public class MainMenu : MonoBehaviour
{
    const int MAX_SCORE_ITEMS = 80;
    
    public TextMesh mesh;
    public Renderer txt_sc;
    public Renderer txt_in;
    Transform tr_mesh;
    Vector3 speed = new Vector3(0.0f,0.008f,0.0f);
    int state = 0;
    bool scroll_lock;
    
	void Awake()
    {
        GameControl.player_score = 0;
        GameControl.scores = new GameControl.ScoreItem[MAX_SCORE_ITEMS];
        scroll_lock = false;
        AudioClip clip = SoundHandler.GetMus(GameMus.Menu);
        GetComponent<AudioSource>().PlayOneShot(clip);
	    try
        {
            tr_mesh = mesh.transform;
            string score = Path.Combine("Text", "rscores.txt");
            string altscore = Path.Combine("Text", "scores.txt");
            if(File.Exists(altscore)) score = altscore;
            int rank = 1;
            int idx = 0;
            bool dir = false;
            using(StreamReader sr = new StreamReader(score, Encoding.Default))
            {
                while(sr.Peek() > -1)
                {
                    string line = sr.ReadLine();
                    string nm = line.Substring(0, 10);
                    string sc = line.Substring(10);
                    mesh.text += "<color=#" + GameControl.game_colors_hexstr[idx] + ">" +
                        (rank < 10 ? " " : "") + rank.ToString() + "." +
                        nm + " " + sc +
                        "</color>\r\n";
                    GameControl.ScoreItem score_item = new GameControl.ScoreItem();
                    score_item.name = nm;
                    score_item.score = int.Parse(sc);
                    GameControl.scores[rank - 1] = score_item;
                    rank++;
                    if(dir)
                    {
                        idx--;
                        if(idx <= 0) dir = false;
                    }
                    else
                    {
                        idx++;
                        if(idx >= GameControl.game_colors_hexstr.Length - 1) dir = true;
                    }
                }
            }
            mesh.text += "\r\n" +
            File.ReadAllText(Path.Combine("Text", "menu.txt"), Encoding.Default);
        }
        catch
        {
        }
	}
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) Application.LoadLevel((int)GameControl.GameLevels.Name);
    }
    
    void FixedUpdate()
    {
        if(scroll_lock) return;
        
        tr_mesh.position += speed;
        if(state == 0 && tr_mesh.position.y >= 13.8f)
        {
            state = 1;
            txt_sc.enabled = false;
            txt_in.enabled = true;
            scroll_lock = true;
            StartCoroutine(PauseRoll());
        }
        else if(state == 1 && tr_mesh.position.y >= 14.7f)
        {
            state = 2;
            scroll_lock = true;
            StartCoroutine(PauseRoll());
        }
        else if(state == 2 && tr_mesh.position.y >= 15.7f)
        {
            state = 0;
            txt_sc.enabled = true;
            txt_in.enabled = false;
            tr_mesh.position = new Vector3(0.0f, -0.9f, 1.0f);
        }
    }
    
    IEnumerator PauseRoll()
    {
        yield return new WaitForSeconds(30.0f);
        scroll_lock = false;
    }
}