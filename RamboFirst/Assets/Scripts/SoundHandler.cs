using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Globalization;

public enum GameMus
{
    None = -1,
    Brief,
    Brief2,
    Intro,
    End,
    Lose,
    Main,
    Menu,
    Name,
    Ready,
    HighSc
}

public enum GameSnd
{
    None = -1,
    Arrow,
    ArrowEx,
    Death,
    Grenade,
    GrenadeEx,
    Helicopter,
    Helicopter2,
    Hit,
    Machinegun,
    Pickup,
    Prison,
    RPG,
    RPGEx,
    Trigger
}

public class SoundHandler : MonoBehaviour
{
    const string CFG_FILE = "RamboFirst.ini";
    
    private static string[] mus_list =
    {
        "brief1.ogg",
        "brief2.ogg",
        "intro.ogg",
        "end.ogg",
        "lose.ogg",
        "main.ogg",
        "menu.ogg",
        "name.ogg",
        "ready.ogg",
        "highscore.ogg"
    };
    
    private static string[] snd_list =
    {
        "arrow.ogg",
        "arrow_ex.ogg",
        "death.ogg",
        "grenade.ogg",
        "grenade_ex.ogg",
        "helicopter.ogg",
        "helicopter2.ogg",
        "hit.ogg",
        "machinegun.ogg",
        "pickup.ogg",
        "prison.ogg",
        "rpg.ogg",
        "rpg_ex.ogg",
        "trigger.ogg"
    };
    
    static AudioClip[] musics;
    static AudioClip[] snds;
    static int snd_loaded;
    static int snd_total;
    
    public TextMesh tm_loaded;
    public C64Load c64sc;
    
    void Awake()
    {
        //Little more than SoundLoading...
        CultureInfo ci = CultureInfo.InvariantCulture;
        float vol = 0.25f;
        int fps = 60;
        bool fps_lim = false;
        try
        {
            using (StreamReader sr = new StreamReader(CFG_FILE, Encoding.Default))
            {
                if(!float.TryParse(sr.ReadLine(), NumberStyles.Float, ci, out vol))  vol = 0.25f;
                if(!int.TryParse(sr.ReadLine(), out fps)) fps = 60;
                if(!bool.TryParse(sr.ReadLine(), out fps_lim)) fps_lim = false;
            }
        }
        catch
        {
        }
        Cursor.visible = false;
        Screen.lockCursor = true;
        if(fps_lim)
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = fps;
        }
        AudioListener.volume = vol;
        //Sound loading
        snd_loaded = 0;
        snd_total = mus_list.Length + snd_list.Length;
        //
        string path = Path.Combine(Application.dataPath, "..");
        musics = new AudioClip[mus_list.Length];
        for(int i = 0; i < mus_list.Length; i++)
            StartCoroutine(LoadClip(path, "Music", i, mus_list, musics));
        snds = new AudioClip[snd_list.Length];
        for(int i = 0; i < snd_list.Length; i++)
            StartCoroutine(LoadClip(path, "Sound", i, snd_list, snds));
    }
    
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            tm_loaded.text = "Loading menu...";
            Application.LoadLevel((int)GameControl.GameLevels.Trainer);
        }
    }

    public static AudioClip GetMus(GameMus input)
    {
        return musics[(int)input];
    }
    
    public static AudioClip GetSnd(GameSnd input)
    {
        return snds[(int)input];
    }
    
    IEnumerator LoadClip(string path, string folder, int i, string[] snd_list, AudioClip[] snds)
    {
        path = Path.Combine(path, folder + "\\" + snd_list[i]);
        using(WWW data = new WWW("file://" + path))
        {
            yield return data;
            snds[i] = data.GetAudioClip(true, false, AudioType.OGGVORBIS);
            snd_loaded++;
        }
        if(snd_loaded >= snd_total)
        {
            GetComponent<AudioSource>().clip = GetMus(GameMus.Intro);
            GetComponent<AudioSource>().Play();
            tm_loaded.text = "Press space to continue...";
            c64sc.enabled = true;
        }
    }
}