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
    List<Texture2D> texturesMap2Ds;
    [SerializeField]
    GameObject mapPrefab;

    MapObject[] mapObjects;

    string map1 = "testing_views_settings_normal_level";
    string map2 = "testing_views_settings_hard_level";


    // Start is called before the first frame update
    void Start()
    {
        texturesMap2Ds = new List<Texture2D>(Resources.LoadAll<Texture2D>("sprites"));

        mapPrefab = Resources.Load<GameObject>("prefabs/mapPrefab");

        path = Application.dataPath + "/Resources/maps/" + map1 + ".json";
        jsonString = File.ReadAllText(path);

        mapObjects = JsonConverter.FromJson<MapObject>(jsonString);

        GenerateMap();
    }

    Texture2D FindSpriteById(string id)
    {
        for (int i = 0; i < texturesMap2Ds.Count; i++)
        {
            if (texturesMap2Ds[i].name == id)
            {
                return texturesMap2Ds[i];
            }
        }
        return null;
    }

    void GenerateMap()
    {
        for(int i = 0; i < mapObjects.Length; i++)
        {
            var id = mapObjects[i].Id;
            var type = mapObjects[i].Type;
            var x = mapObjects[i].X;
            var y = mapObjects[i].Y;
            var width = mapObjects[i].Width;
            var height = mapObjects[i].Height;
            Debug.Log(x);
            
            var currentCell = transform.AddChild(mapPrefab);//, new Vector3((float)x * 100, (float)y * 100, 0), Quaternion.identity, transform);
            currentCell.transform.localPosition = new Vector3((float)x * 100, (float)y * 100, 0);
            var uiSprite = currentCell.GetComponent<UISprite>();
            uiSprite.spriteName = id;
            uiSprite.height = (int)(height * 100);
            uiSprite.width = (int)(width * 100);
        }
    }
}
