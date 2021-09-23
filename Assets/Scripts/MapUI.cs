using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapUI : MonoBehaviour
{
    [SerializeField]
    GameObject infoWindow;

    UILabel infoWindowLabel;

    // Start is called before the first frame update
    void Start()
    {
        infoWindow = transform.GetChild(0).gameObject;
        infoWindowLabel = infoWindow.transform.GetChild(0).GetComponent<UILabel>();
    }

    public void OnSettingsPressed()
    {
        CameraMovementByMouse.Instance.LockUnlockMovement();
        var leftSidedSprite = MapCreation.Instance.FindLeftSidedObjectToCameraLeftCorner();
        infoWindowLabel.text = leftSidedSprite;
        infoWindow.SetActive(true);
    }

    public void OnOkPressed()
    {
        CameraMovementByMouse.Instance.LockUnlockMovement();
        infoWindow.SetActive(false);
    }
}
