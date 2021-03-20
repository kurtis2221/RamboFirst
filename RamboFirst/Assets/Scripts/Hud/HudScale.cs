using UnityEngine;
using System.Collections;

public class HudScale : MonoBehaviour
{
	void Awake()
    {
        float sc_h = Screen.height / GameControl.BASE_HEIGHT;
        foreach(GUITexture g in transform.GetComponentsInChildren<GUITexture>())
        {
            Rect rec = g.pixelInset;
            rec.x *= sc_h;
            rec.y *= sc_h;
            rec.width *= sc_h;
            rec.height *= sc_h;
            g.pixelInset = rec;
        }
	}
}