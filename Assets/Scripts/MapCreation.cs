using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class MapCreation : MonoBehaviour
{
    #region Singleton
    private static MapCreation _instance;
    public static MapCreation Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

    }

    #endregion

    [SerializeField]
    string path;

    [SerializeField]
    string jsonString;

    [SerializeField]
    GameObject mapPrefab;

    MapObject[] mapObjects;

    List<Texture2D> texturesMap2Ds;

    List<GameObject> mapGameobjects;

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

        mapGameobjects = new List<GameObject>();

        GenerateMap();
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
            
            var currentCell = transform.AddChild(mapPrefab);
            currentCell.transform.localPosition = new Vector3((float)x * 100, (float)y * 100, 0);
            var uiSprite = currentCell.GetComponent<UISprite>();
            uiSprite.spriteName = id;
            uiSprite.height = (int)(height * 100);
            uiSprite.width = (int)(width * 100);
            mapGameobjects.Add(currentCell);
        }
    }

    public UISprite FindLeftSidedObjectToCameraLeftCorner()
    {
        UISprite result = mapGameobjects[0].GetComponent<UISprite>();
        var minDist = int.MaxValue;
        var index = 0;
        for(int i = 0; i < mapGameobjects.Count; i++)
        {
            Vector3 pos = Camera.main.WorldToViewportPoint(mapGameobjects[i].transform.position);
            pos = new Vector3(pos.x, 1 - pos.y, pos.z);
            var dist = pos.magnitude;
            if (dist < minDist)
                minDist = dist;
            Debug.Log(i + " - " + pos + " - " + pos.magnitude);
        }

        return result;
    }
}
