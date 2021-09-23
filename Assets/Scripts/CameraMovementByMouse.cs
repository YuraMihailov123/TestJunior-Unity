using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementByMouse : MonoBehaviour
{
    #region Singleton
    private static CameraMovementByMouse _instance;
    public static CameraMovementByMouse Instance { get { return _instance; } }

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


    public float dragSpeed = 0.01f;
    private Vector3 dragOrigin;

    float zoomSpeed = 1;
    float targetOrtho;
    float smoothSpeed = 2.0f;
    float minOrtho = 1f;
    float maxOrtho = 1.3f;

    [SerializeField]
    Vector2 minXY;
    [SerializeField]
    Vector2 maxXY;

    bool isLockedMovement = false;

    void Start()
    {
        isLockedMovement = false;
        targetOrtho = Camera.main.orthographicSize;
    }

    public void LockUnlockMovement()
    {
        isLockedMovement = !isLockedMovement;
    }

    public void CalculateBorders()
    {
        Vector3 pos = MapCreation.Instance.mapGameobjects[0].transform.localPosition;
        Vector3 pos2 = MapCreation.Instance.mapGameobjects[MapCreation.Instance.mapGameobjects.Count - 1].transform.localPosition;

        var first = MapCreation.Instance.mapGameobjects[0].GetComponent<UISprite>();
        var last = MapCreation.Instance.mapGameobjects[MapCreation.Instance.mapGameobjects.Count-1].GetComponent<UISprite>();

        minXY.x = pos.x + first.width + first.width / 2;
        maxXY.x = pos2.x + last.width/2 - Camera.main.pixelWidth/2;

        maxXY.y = pos.y - first.height / 2 - 30;
        minXY.y = pos2.y + last.height + last.height/2 - Camera.main.pixelHeight / 2 + 50;
        HandleBorders();
    }

    public void HandleBorders()
    {
        if (transform.localPosition.x < minXY.x * Camera.main.orthographicSize)
        {
            transform.localPosition = new Vector3(minXY.x * Camera.main.orthographicSize + 1, transform.localPosition.y, transform.localPosition.z);
        }
        if (transform.localPosition.x > maxXY.x / Camera.main.orthographicSize)
        {
            transform.localPosition = new Vector3(maxXY.x / Camera.main.orthographicSize - 1, transform.localPosition.y, transform.localPosition.z);
        }
        if (transform.localPosition.y < minXY.y / Camera.main.orthographicSize)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, minXY.y / Camera.main.orthographicSize + 1, transform.localPosition.z);
        }
        if (transform.localPosition.y > maxXY.y * Camera.main.orthographicSize)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, maxXY.y * Camera.main.orthographicSize  - 1, transform.localPosition.z);
        }
    }

    public void InputMoveHandler()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(0)) return;

        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(pos.x * dragSpeed, pos.y * dragSpeed, 0);
        transform.Translate(-move, Space.World);
        HandleBorders();
    }

    public void InputZoomHandler()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {
            targetOrtho -= scroll * zoomSpeed;
            targetOrtho = Mathf.Clamp(targetOrtho, minOrtho, maxOrtho);
        }

        Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, targetOrtho, smoothSpeed * Time.deltaTime);
        HandleBorders();
    }

    void Update()
    {
        if (!isLockedMovement)
        {
            InputZoomHandler();
            InputMoveHandler();
        }
    }
}
