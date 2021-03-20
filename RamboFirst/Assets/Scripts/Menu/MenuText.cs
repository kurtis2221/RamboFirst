using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;

public class MenuText : MonoBehaviour
{
    public string textfile;
    
    void Awake()
    {
        try
        {
            GetComponent<TextMesh>().text = File.ReadAllText(Path.Combine("Text", textfile + ".txt"), Encoding.Default);
        }
        catch
        {
        }
    }
}