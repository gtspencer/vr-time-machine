using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class PictureSaver : MonoBehaviour
{
    List<SerializedTrasnform> allTransforms;
    // Start is called before the first frame update
    void Start()
    {
        allTransforms = new List<SerializedTrasnform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SavePictures()
    {
        foreach (LocationSaver loc in FindObjectsOfType<LocationSaver>())
        {
            allTransforms.Add(loc.GetTransform());
        }
        
        List<string> outputString = new List<string>();
        foreach (SerializedTrasnform transform in allTransforms)
        {
            StringWriter writer = new StringWriter();
            XmlSerializer ser = new XmlSerializer(typeof(SerializedTrasnform));
            ser.Serialize(writer, transform);
            outputString.Add(writer.ToString());
            Debug.LogError(writer.ToString());
            writer.Close();
        }
    }
}
