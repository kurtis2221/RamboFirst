using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour
{   
    void Awake()
    {
        AudioClip clip = SoundHandler.GetMus(GameMus.Lose);
        GetComponent<AudioSource>().PlayOneShot(clip);
        StartCoroutine(StartLevel(clip.length));
    }
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) LoadLevel();
    }
    
    void LoadLevel()
    {
        int rank;
        for(rank = 0; rank < GameControl.scores.Length; rank++)
        {
           if(GameControl.scores[rank].score < GameControl.player_score) break;
        }
        if(rank < GameControl.scores.Length)
            Application.LoadLevel((int)GameControl.GameLevels.HighSc);
        else
            Application.LoadLevel((int)GameControl.GameLevels.Menu);
    }
    
    IEnumerator StartLevel(float sec)
    {
        yield return new WaitForSeconds(sec);
        LoadLevel();
    }
}