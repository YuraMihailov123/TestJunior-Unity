using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class MapCreation : MonoBehaviour
{
    [SerializeField]
    string path;
    [SerializeField]
    string jsonString;
    [SerializeField]
    MapObject[] mapObjects;
    // Start is called before the first frame update
    void Start()
    {
        path = Application.dataPath + "/Resources/maps/testing_views_settings_normal_level.json";
        jsonString = File.ReadAllText(path);

        mapObjects = JsonConverter.FromJson<MapObject>(jsonString);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
