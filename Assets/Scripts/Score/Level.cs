using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

[Serializable]
public class Level
{
    [XmlElement("LevelName")]
    public string Name { get; set; }

    [XmlElement("IsCompleted")]
    public bool IsCompleted { get; set; }

    [XmlElement("Score")]
    public int Score { get; set; }
}

[Serializable]
public class GameManager
{
    [XmlArray("Levels")]
    [XmlArrayItem("Level")]
    public List<Level> Levels { get; set; } = new List<Level>();

    public void SaveToFile(string filePath)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(GameManager));
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            serializer.Serialize(writer, this);
        }
    }

    public static GameManager LoadFromFile(string filePath)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(GameManager));
        using (StreamReader reader = new StreamReader(filePath))
        {
            return (GameManager)serializer.Deserialize(reader);
        }
    }
}
