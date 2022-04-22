using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private Camera _camera;
    private float _zoom;
    private float zoomFactor = 3f;
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        _zoom = _camera.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        float scrollData;
        scrollData = Input.GetAxis("Mouse ScrollWheel");
        _zoom -= scrollData * zoomFactor;
        _zoom = Mathf.Clamp(_zoom, 5f, 25f);
        _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, _zoom, Time.deltaTime);
    }
}
