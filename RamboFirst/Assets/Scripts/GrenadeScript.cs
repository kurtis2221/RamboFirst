using UnityEngine;
using System.Collections;

public class GrenadeScript : MonoBehaviour
{
    const int CHECK_LAYER = ~(1 << 2 | 1 << 8);
    
	public Transform tran;
    
	void Awake()
    {
	    StartCoroutine(Destroy());
	}
    
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1.0f);
        Collider[] cols = Physics.OverlapSphere(tran.position, 8.0f);
        foreach(Collider col in cols)
        {
            if((1 << col.gameObject.layer & CHECK_LAYER) > 0)
            {
                IntObjBase tmp = col.GetComponent<IntObjBase>();
                if(tmp != null) tmp.Damage();
            }
        }
        Instantiate(GameControl.inst.expl_part,tran.position,tran.rotation);
        SoundScript.CreateSound(tran, SoundHandler.GetSnd(GameSnd.GrenadeEx));
        Destroy(gameObject);
    }
    
    void OnDestroy()
    {
        if(WeapHandler.bullets > 0) WeapHandler.bullets--;
    }
}