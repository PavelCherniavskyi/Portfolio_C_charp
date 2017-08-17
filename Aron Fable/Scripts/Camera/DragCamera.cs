using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragCamera : MonoBehaviour
{
    private LevelController _LevelController;

    public float xMax = -34.814f;
    public float xMin = -115.9948f;
    public float yMax = 21.138f;
    public float yMin = 46.06979f;
    public float orthZoomMaxSize;
    public float orthZoomMinSize;
    public float orthZoomStep = 0.1f;

    public float MouseSensitivity = 0.3f;
    private Vector3 _lastPosition;

    public static bool IsOtherObjectDragging { set; get; }
    public static bool LineActive { set; get; }

    private GameObject MainCamera;

    private void Start()
    {
        MainCamera = GameObject.Find("Main Camera");
        _LevelController = GameObject.Find("LevelControllerPref").GetComponent<LevelController>();
        _lastPosition = transform.position;
    }

    void Update()
    {

        if (IsOtherObjectDragging || LineActive)
            return;
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            return;
        if (!_LevelController.isLevelStarted)
            return;

        zoomCamera();

        if (CameraMoveToHero.mode)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _lastPosition = Input.mousePosition;

            }

            if (Input.GetMouseButton(0))
            {
                Vector3 delta = Input.mousePosition - _lastPosition;
                transform.Translate(delta.x * -MouseSensitivity * Time.deltaTime, delta.y * -MouseSensitivity * Time.deltaTime, -10);
                transform.position = new Vector3(Mathf.Clamp(transform.position.x, xMin, xMax), Mathf.Clamp(transform.position.y, yMin, yMax), -10);
            }
        }
    }

    void zoomCamera()
    {
        if (!IsWithinBorders())
            return;

        // zoom out
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (Camera.main.orthographicSize < orthZoomMaxSize)
            {
                Camera.main.orthographicSize += orthZoomStep;

                #region То что добавил
                transform.position = MainCamera.transform.position;
                MainCamera.GetComponent<Camera2DFollowTDS>().InstanteFollow();
                #endregion
            }
        }
        // zoom in
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (Camera.main.orthographicSize > orthZoomMinSize)
            {
                Camera.main.orthographicSize -= orthZoomStep;

                #region То что добавил
                transform.position = MainCamera.transform.position;
                #endregion
            }
        }
    }

    bool IsWithinBorders()
    {
        if (transform.position.x < xMin || transform.position.x > xMax)
            return false;
        if (transform.position.y < yMin || transform.position.y > yMax)
            return false;
        return true;
    }
}
