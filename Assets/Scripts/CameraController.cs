// Based on the code provided here: https://forum.unity.com/threads/rts-top-down-map-constrained-camera.93956/

using UnityEngine;

public class CameraController : MonoBehaviour
{

    public bool MoveEnabled = true;
    public bool ZoomEnabled = true;

    public float horizontalScrollSpeed = 10f;
    public float verticalScrollSpeed = 10f;
    public float zoomSpeed = 10f;

    private readonly float _horizontalScrollArea = 0;
    private readonly float _verticalScrollArea = 0;

    private Vector2 _mousePosition;
    private float x;
    private float y;
    private float z;

    // Update is called once per frame
    void Update()
    {
        _mousePosition = Input.mousePosition;

        if (MoveEnabled)
        {
            if (_mousePosition.x <= _horizontalScrollArea)
            {
                x = -1;
            } 
            else if (_mousePosition.x >= Screen.width - _horizontalScrollArea) 
            {
                x = 1;
            }
            else
            {
                x = 0;
            }

            if (_mousePosition.y <= _verticalScrollArea)
            {
                y = -1;
            }
            else if (_mousePosition.y >= Screen.height - _verticalScrollArea)
            {
                y = 1;
            }
            else
            {
                y = 0;
            }
        }
        else
        {
            x = 0;
            y = 0;
        }

        if (ZoomEnabled)
        {
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                z = 1;
            }
            else if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                z = -1;
            }
            else
            {
                z = 0;
            }

        }
        else
        {
            z = 0;
        }

        Vector3 _moveVector = (new Vector3(x * horizontalScrollSpeed, y * verticalScrollSpeed, z * zoomSpeed) * Time.deltaTime);
        transform.Translate(_moveVector, Space.Self);
    }
}
