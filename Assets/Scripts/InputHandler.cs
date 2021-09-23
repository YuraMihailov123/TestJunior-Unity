using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    Vector3 mouseTouch;

    void HandleMouseWheel()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        CameraController.Instance.InputZoomHandler(scroll);

    }

    void HandleMousePressed()
    {

        if (Input.GetMouseButtonDown(0))
        {
            mouseTouch = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(0)) return;
        CameraController.Instance.InputMoveHandler(mouseTouch);


    }
    // Update is called once per frame
    void Update()
    {
        if (!CameraController.Instance.isLockedMovement)
        {
            HandleMouseWheel();
            HandleMousePressed();
        }

    }
}
