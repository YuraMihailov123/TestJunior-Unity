using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementByMouse : MonoBehaviour
{
    public float dragSpeed = 0.01f;
    private Vector3 dragOrigin;

    float zoomSpeed = 1;
    float targetOrtho;
    float smoothSpeed = 2.0f;
    float minOrtho = 1.0f;
    float maxOrtho = 20.0f;

    Vector2 minXY;
    Vector2 maxXY;

    void Start()
    {
        targetOrtho = Camera.main.orthographicSize;
    }

    public void Test()
    {
        Vector3 pos = MapCreation.Instance.mapGameobjects[0].transform.localPosition;
        Vector3 pos2 = MapCreation.Instance.mapGameobjects[MapCreation.Instance.mapGameobjects.Count - 1].transform.localPosition;
        var first = MapCreation.Instance.mapGameobjects[0].GetComponent<UISprite>();
        var last = MapCreation.Instance.mapGameobjects[MapCreation.Instance.mapGameobjects.Count-1].GetComponent<UISprite>();
        minXY.x = pos.x + first.width + first.width / 2;
        maxXY.x = pos2.x + last.width/2 - Camera.main.pixelWidth/2;

        maxXY.y = pos.y - first.height / 2;
        minXY.y = pos2.y + last.height + last.height/2 - Camera.main.pixelHeight / 2;
    }

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {
            targetOrtho -= scroll * zoomSpeed;
            targetOrtho = Mathf.Clamp(targetOrtho, minOrtho, maxOrtho);
        }

        Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, targetOrtho, smoothSpeed * Time.deltaTime);


        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(0)) return;

        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(pos.x * dragSpeed, pos.y * dragSpeed,0);
        transform.Translate(-move, Space.World);

        if (transform.localPosition.x < minXY.x)
        {
            transform.localPosition = new Vector3(minXY.x+1, transform.localPosition.y, transform.localPosition.z);
        }
        if (transform.localPosition.x > maxXY.x)
        {
            transform.localPosition = new Vector3(maxXY.x-1, transform.localPosition.y, transform.localPosition.z);
        }
        if (transform.localPosition.y < minXY.y)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, minXY.y + 1, transform.localPosition.z);
        }
        if (transform.localPosition.y > maxXY.y)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, maxXY.y - 1, transform.localPosition.z);
        }
    }
}
