using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;

public class HighScore : MonoBehaviour
{
    const string score_format = "000000";
	public TextMesh score;
    Color[] char_colors;
    int offset;
    
    void Awake()
    {
        int rank;
        for(rank = 0; rank < GameControl.scores.Length; rank++)
        {
           if(GameControl.scores[rank].score < GameControl.player_score) break;
        }
        rank++;
        score.text = (rank < 10 ? " " : "") + rank.ToString() + "." + GameControl.player_name + " " + GameControl.player_score.ToString(score_format);
        offset = 0;
        char_colors = new Color[GameControl.game_colors.Length];
        for(int i = 0; i < GameControl.game_colors.Length; i++)
        {
            char_colors[i] = GameControl.game_colors[i];
        }
        StartCoroutine(ShiftColors());
        if(rank > GameControl.scores.Length) return;
        try
        {
            rank--;
            using(StreamWriter sw = new StreamWriter(Path.Combine("Text", "scores.txt"), false, Encoding.Default))
            {
                for(int i = 0; i < rank; i++)
                {
                    WriteScore(sw, GameControl.scores[i].name, GameControl.scores[i].score);
                }
                WriteScore(sw, GameControl.player_name, GameControl.player_score);
                for(int i = rank; i < GameControl.scores.Length - 1; i++)
                {
                    WriteScore(sw, GameControl.scores[i].name, GameControl.scores[i].score);
                }
            }
        }
        catch
        {
        }
    }
    
    void WriteScore(StreamWriter sw, string name, int score)
    {
        sw.WriteLine(name+score.ToString(score_format));
    }
    
	IEnumerator ShiftColors()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.05f);
            score.color = char_colors[offset];
            offset = (offset + 1) % char_colors.Length;
        }
    }
}