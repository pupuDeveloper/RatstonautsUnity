using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class CameraViewportHandler : MonoBehaviour
{
    public enum Constraint { Landscape, Portrait }

    #region FIELDS
    public Color wireColor = Color.white;
    public float UnitsSize = 1; // size of your scene in unity units
    public Constraint constraint = Constraint.Portrait;
    public static CameraViewportHandler Instance;
    public new Camera camera;
    public int amountOfBackGrounds;

    public bool executeInUpdate;

    private float _width;
    private float _height;
    private float leftX, rightX, topY, bottomY;
    float cameraX, cameraY;
    //*** bottom screen
    private Vector3 _bl;
    private Vector3 _bc;
    private Vector3 _br;
    //*** middle screen
    private Vector3 _ml;
    private Vector3 _mc;
    private Vector3 _mr;
    //*** top screen
    private Vector3 _tl;
    private Vector3 _tc;
    private Vector3 _tr;
    #endregion

    #region PROPERTIES
    public float Width
    {
        get
        {
            return _width;
        }
    }
    public float Height
    {
        get
        {
            return _height;
        }
    }

    // helper points:
    public Vector3 BottomLeft(int whichRoom)
    {
        switch (whichRoom)
        {
            case 0:
                return _bl;
                break;
            case 1:
                return new Vector3(_bl.x + Mathf.Abs(_bl.x) * whichRoom, _bl.y, _bl.z);
                break;
            case 2:
                return new Vector3(_bl.x + Mathf.Abs(_bl.x) * whichRoom, _bl.y, _bl.z);
                break;
            default:
                return _bl;
                break;
        }
    }
    public Vector3 BottomCenter(int whichRoom)
    {
        switch (whichRoom)
        {
            case 0:
                return _bc;
                break;
            case 1:
                return new Vector3(_bc.x - ((_bc.x / amountOfBackGrounds) * whichRoom), _bc.y, _bc.z);
                break;
            case 2:
                return new Vector3(_bc.x - ((_bc.x / amountOfBackGrounds) * whichRoom), _bc.y, _bc.z);
                break;
            default:
                return _bc;
                break;
        }
    }
    public Vector3 BottomRight(int whichRoom)
    {
        switch (whichRoom)
        {
            case 0:
                return _br;
                break;
            case 1:
                return new Vector3(_br.x - ((_br.x / amountOfBackGrounds) * whichRoom), _br.y, _br.z);
                break;
            case 2:
                return new Vector3(_br.x - ((_br.x / amountOfBackGrounds) * whichRoom), _br.y, _br.z);
                break;
            default:
                return _br;
                break;
        }
    }
    public Vector3 MiddleLeft(int whichRoom)
    {
        switch (whichRoom)
        {
            case 0:
                return _ml;
                break;
            case 1:
                return new Vector3(_ml.x - ((_ml.x / amountOfBackGrounds) * whichRoom), _ml.y, _ml.z);
                break;
            case 2:
                return new Vector3(_ml.x - ((_ml.x / amountOfBackGrounds) * whichRoom), _ml.y, _ml.z);
                break;
            default:
                return _ml;
                break;
        }
    }
    public Vector3 MiddleCenter(int whichRoom)
    {
        switch (whichRoom)
        {
            case 0:
                return _mc;
                break;
            case 1:
            Debug.Log("here");
                return new Vector3(_mc.x + Mathf.Abs(_mc.x) * whichRoom, _mc.y, _mc.z);
                break;
            case 2:
                return new Vector3(_mc.x - ((_mc.x / amountOfBackGrounds) * whichRoom), _mc.y, _mc.z);
                break;
            default:
                return _mc;
                break;
        }
    }
    public Vector3 MiddleRight(int whichRoom)
    {
        switch (whichRoom)
        {
            case 0:
                return _mr;
                break;
            case 1:
                return new Vector3(_mr.x - ((_mr.x / amountOfBackGrounds) * whichRoom), _mr.y, _mr.z);
                break;
            case 2:
                return new Vector3(_mr.x - ((_mr.x / amountOfBackGrounds) * whichRoom), _mr.y, _mr.z);
                break;
            default:
                return _mr;
                break;
        }
    }
    public Vector3 TopLeft(int whichRoom)
    {
        switch (whichRoom)
        {
            case 0:
                return _tl;
                break;
            case 1:
                return new Vector3(_tl.x - ((_tl.x / amountOfBackGrounds) * whichRoom), _tl.y, _tl.z);
                break;
            case 2:
                return new Vector3(_tl.x - ((_tl.x / amountOfBackGrounds) * whichRoom), _tl.y, _tl.z);
                break;
            default:
                return _tl;
                break;
        }
    }
    public Vector3 TopCenter(int whichRoom)
    {
        switch (whichRoom)
        {
            case 0:
                return _tc;
                break;
            case 1:
                return new Vector3(_tc.x - ((_tc.x / amountOfBackGrounds) * whichRoom), _tc.y, _tc.z);
                break;
            case 2:
                return new Vector3(_tc.x - ((_tc.x / amountOfBackGrounds) * whichRoom), _tc.y, _tc.z);
                break;
            default:
                return _tc;
                break;
        }
    }
    public Vector3 TopRight(int whichRoom)
    {
        switch (whichRoom)
        {
            case 0:
                return _tr;
                break;
            case 1:
                return new Vector3(_tr.x - ((_tr.x / amountOfBackGrounds) * whichRoom), _tr.y, _tr.z);
                break;
            case 2:
                return new Vector3(_tr.x - ((_tr.x / amountOfBackGrounds) * whichRoom), _tr.y, _tr.z);
                break;
            default:
                return _tr;
                break;
        }
    }
    #endregion

    #region METHODS
    private void Awake()
    {
        camera = GetComponent<Camera>();
        Instance = this;
        ComputeResolution();
    }

    private void ComputeResolution()
    {
        if (constraint == Constraint.Landscape)
        {
            camera.orthographicSize = 1f / camera.aspect * UnitsSize / 2f;
        }
        else
        {
            camera.orthographicSize = UnitsSize / 2f;
        }

        _height = 2f * camera.orthographicSize;
        _width = _height * camera.aspect * amountOfBackGrounds;

        cameraX = camera.transform.position.x;
        cameraY = camera.transform.position.y;

        Debug.Log("cameraX is: " + cameraX);
        Debug.Log("cameraY is: " + cameraY);

        leftX = cameraX - _width / 2;
        rightX = cameraX + _width / 2;
        topY = cameraY + _height / 2;
        bottomY = cameraY - _height / 2;

        //*** bottom
        _bl = new Vector3(leftX, bottomY, 0);
        _bc = new Vector3(cameraX, bottomY, 0);
        _br = new Vector3(rightX, bottomY, 0);
        //*** middle
        _ml = new Vector3(leftX, cameraY, 0);
        _mc = new Vector3(cameraX, cameraY, 0);
        _mr = new Vector3(rightX, cameraY, 0);
        //*** top
        _tl = new Vector3(leftX, topY, 0);
        _tc = new Vector3(cameraX, topY, 0);
        _tr = new Vector3(rightX, topY, 0);
    }

    private void Update()
    {
#if UNITY_EDITOR

        if (executeInUpdate)
            ComputeResolution();

#endif
    }

    void OnDrawGizmos()
    {
        Gizmos.color = wireColor;

        Matrix4x4 temp = Gizmos.matrix;
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        if (camera.orthographic)
        {
            float spread = camera.farClipPlane - camera.nearClipPlane;
            float center = (camera.farClipPlane + camera.nearClipPlane) * 0.5f;
            Gizmos.DrawWireCube(new Vector3(0, 0, center), new Vector3(camera.orthographicSize * 2 * camera.aspect, camera.orthographicSize * 2, spread));
        }
        else
        {
            Gizmos.DrawFrustum(Vector3.zero, camera.fieldOfView, camera.farClipPlane, camera.nearClipPlane, camera.aspect);
        }
        Gizmos.matrix = temp;
    }
    #endregion

} // class
