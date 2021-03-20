using UnityEngine;
using System.Collections;

public class SceneScript : MonoBehaviour
{
    public GameMus music;
    public GameControl.GameLevels level;
    
    void Awake()
    {
        AudioClip clip = SoundHandler.GetMus(music);
        GetComponent<AudioSource>().PlayOneShot(clip);
        StartCoroutine(StartLevel(clip.length));
    }
    
	void Update()
    {
	    if(Input.GetKeyUp(KeyCode.Space)) LoadLevel();
	}
    
    void LoadLevel()
    {
        Application.LoadLevel((int)level);
    }
    
    IEnumerator StartLevel(float sec)
    {
        yield return new WaitForSeconds(sec);
        LoadLevel();
    }
}