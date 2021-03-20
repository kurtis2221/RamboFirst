using UnityEngine;
using System.Collections;
using System.IO;

public class TrainerScript : MonoBehaviour
{
    public Renderer q1_txt;
    public Renderer q2_txt;
    
    int question = 0;
    
	void Update()
    {
	    if(question == 0)
        {
            if(Input.GetKeyDown(KeyCode.H)) GameControl.unlimited_energy = false;
            else if(Input.GetKeyDown(KeyCode.U)) GameControl.unlimited_energy = true;
            else return;
            q1_txt.enabled = false;
            q2_txt.enabled = true;
            question++;
        }
        else if(question == 1)
        {
            if(Input.GetKeyDown(KeyCode.R)) LoadScore(true);
            else if(Input.GetKeyDown(KeyCode.L)) LoadScore(false);
            else return;
            Application.LoadLevel((int)GameControl.GameLevels.Menu);
        }
	}
    
    void LoadScore(bool restore)
    {
        try
        {
            if(restore || !File.Exists(Path.Combine("Text", "scores.txt")))
                File.Copy(Path.Combine("Text", "rscores.txt"), Path.Combine("Text", "scores.txt"), true);
        }
        catch
        {
        }
    }
}