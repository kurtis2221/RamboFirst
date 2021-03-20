using UnityEngine;
using System.Collections;

public class WeaponPickup : MonoBehaviour
{
    public int weapid;
    
	void OnTriggerStay(Collider col)
    {
        GameControl.AddScore(3000);
        AudioClip clip = SoundHandler.GetSnd(GameSnd.Pickup);
        PlayerCtrl.cam_snd.PlayOneShot(clip);
        PlayerCtrl.weap.EnableWeapon(weapid);
	    Destroy(gameObject);
	}
}