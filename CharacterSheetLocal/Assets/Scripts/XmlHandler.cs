using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.IO;
using System;

public class XmlHandler {

    private static XmlHandler instance = new XmlHandler();
    public static XmlHandler Instance { get { return instance; } }

    public void Save(string fileName, Type type, object item)
    {
        Debug.Log("Saving xml");
        XmlSerializer xmls = new XmlSerializer(type);
        using (FileStream stream = new FileStream(fileName, FileMode.Create))
        {
            xmls.Serialize(stream, item);
        }
        Debug.Log("Saved xml");
    }
    public object Load(string fileName, Type type)
    {
        object obj = null;
        XmlSerializer serializer = new XmlSerializer(type);
        if (File.Exists(fileName))
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Open))
            {
                obj = serializer.Deserialize(stream) as object;
            }
            Debug.Log("XML Loaded");
        }
        else
        {
            Debug.Log("File does not exist");
        }

        return obj;
    }
}
