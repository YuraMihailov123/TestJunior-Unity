using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapUI : MonoBehaviour
{
    [SerializeField]
    GameObject infoWindow;

    // Start is called before the first frame update
    void Start()
    {
        infoWindow = transform.GetChild(0).gameObject;
    }

    public void OnSettingsPressed()
    {
        MapCreation.Instance.FindLeftSidedObjectToCameraLeftCorner();
        infoWindow.SetActive(true);
    }
}
