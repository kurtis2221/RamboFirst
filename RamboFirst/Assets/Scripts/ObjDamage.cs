using UnityEngine;
using System.Collections;

public class ObjDamage : IntObjBase
{
    public enum DamageType
    {
        None = -1,
        Palm,
        Tree,
        Bush,
        Tower,
        Barr,
        Wareh,
        Post
    }

    public DamageType dam_typ;
    public MeshFilter[] meshes;
    int dam_idx = 0;

    public override void Damage()
    {
        if (dam_typ != DamageType.None)
        {
            var dam_mesh = GameControl.inst.damage_mesh[(int)dam_typ].data;
            for (int i = 0; i < meshes.Length; i++)
            {
                meshes[i].sharedMesh = dam_mesh[dam_idx].sharedMesh;
            }
            dam_idx++;
            if (dam_idx == dam_mesh.Length)
            {
                GetComponent<Collider>().enabled = false;
            }
            if(dam_typ == DamageType.Tower)
            {
                transform.GetChild(0).GetComponent<IntObjBase>().Damage();
            }
        }
        else
        {
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
        }
    }
}