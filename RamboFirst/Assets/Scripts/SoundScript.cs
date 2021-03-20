using UnityEngine;
using System.Collections;

public class SoundScript : MonoBehaviour
{
    public static void CreateSound(Transform tran, AudioClip clip)
    {
        GameObject obj = (GameObject)GameObject.Instantiate(GameControl.inst.sound_obj,tran.position,tran.rotation);
        obj.GetComponent<AudioSource>().PlayOneShot(clip);
        obj.GetComponent<SoundScript>().InitDestroy(clip.length);
    }
    
    public void InitDestroy(float sec)
    {
        StartCoroutine(AutoDestroy(sec));
    }
    
    public IEnumerator AutoDestroy(float sec)
    {
        yield return new WaitForSeconds(sec);
        Destroy(gameObject);
    }
}