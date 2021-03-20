using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;

public class BriefHud : MonoBehaviour
{
    public static BriefHud inst;
    public Camera cam;
    public TextMesh txt;
    
    void Awake()
    {
        inst = this;
    }
    
    public void LoadBrief(string textfile, GameMus music, int drain_lvl)
    {
        try
        {
            GameControl.inst.playerh_sc.enabled = false;
            GameControl.inst.hud.SetActive(false);
            cam.enabled = true;
            txt.text = File.ReadAllText(Path.Combine("Text", textfile + ".txt"), Encoding.Default);
            AudioClip clip = SoundHandler.GetMus(music);
            PlayerCtrl.cam_snd.Stop();
            PlayerCtrl.cam_snd.PlayOneShot(clip);
            StartCoroutine(ResumeGame(clip.length, drain_lvl));
        }
        catch
        {
        }
    }
    
    IEnumerator ResumeGame(float sec, int lvl)
    {
        yield return new WaitForSeconds(sec);
        cam.enabled = false;
        GameControl.inst.playerh_sc.enabled = true;
        GameControl.inst.hud.SetActive(true);
        txt.text = string.Empty;
        GameControl.inst.playerh_tr.GetComponent<AudioSource>().Play();
        PlayerCtrl.cam_snd.Play();
        GameControl.SetEnergyDrain(lvl);
        if(lvl == 2) EnemyHeli.inst.enabled = true;
    }
}