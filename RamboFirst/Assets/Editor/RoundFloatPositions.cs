using UnityEngine;
using UnityEditor;

public class RoundFloatPositions : EditorWindow 
{
	[MenuItem("Window/RoundFloatPositions")]
    static void Init() 
    {
        foreach(Transform t in Selection.transforms)
        {
            Vector3 pos = t.transform.position;
            pos.x = Mathf.Round(pos.x);
            pos.y = Mathf.Round(pos.y);
            pos.z = Mathf.Round(pos.z);
            t.transform.position = pos;
        }
    }
}