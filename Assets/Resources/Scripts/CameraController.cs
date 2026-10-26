using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public Camera camera;
    public Transform target;
    public LayerMask groundLayer;


    public bool movewithKeyboard;
    public bool movewithMouseDrag;
    public bool movewithEdgeScroll;
    private bool isFollowing;
    private Vector3 preMousePos=Vector3.zero;
    private CursorState cursorState;

    public float normalSpeed=10f;
    public float fastSpeed=20;
    public float minFov=10f;
    public float maxFov=20f;


    public Texture2D cursorArrow;
    public Texture2D cursorArrowUp;
    public Texture2D cursorArrowDown;
    public Texture2D cursorArrowLeft;
    public Texture2D cursorArrowRight;

    enum CursorState
    {
        Left,
        Right,
        Up,
        Down,
        Default
    }

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        camera = Camera.main;
        AdjustCameraPosition();
        groundLayer = LayerMask.GetMask("Ground");
        ChangeCursorState(CursorState.Default);
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update()
    {
        if(Input.mouseScrollDelta.y!=0)
        {
            Debug.Log(Input.mouseScrollDelta.y);
            camera.fieldOfView = Mathf.Min(Mathf.Max(camera.fieldOfView-Input.mouseScrollDelta.y, minFov), maxFov);
        }
        if(isFollowing)
        {
            if(target!=null||Input.GetKeyDown(KeyCode.Escape))
            {
                isFollowing = false;
                return;
            }
            transform.position = target.position;
        }
        else
        {
            HandleCameraMovement();
        }
    }

    private void HandleCameraMovement()
    {
        float speed = Input.GetKey(KeyCode.LeftShift) ? fastSpeed : normalSpeed;
        Vector3 movement = Vector3.zero;

        if (movewithKeyboard)
        {
            if (Input.GetKey(KeyCode.W)) movement += Vector3.forward;
            if (Input.GetKey(KeyCode.S)) movement += Vector3.back;
            if (Input.GetKey(KeyCode.A)) movement += Vector3.left;
            if (Input.GetKey(KeyCode.D)) movement += Vector3.right;
        }

        if (movewithEdgeScroll)
        {
            Vector3 mousePosition = Input.mousePosition;
            float edgeThreshold = Screen.height * 0.04f;
            if (mousePosition.x <= edgeThreshold)
            {
                movement += Vector3.left;
                ChangeCursorState(CursorState.Left);
            }
            else if (mousePosition.x >= Screen.width - edgeThreshold)
            {
                movement += Vector3.right;
                ChangeCursorState(CursorState.Right);
            }
            else if (mousePosition.y <= edgeThreshold)
            {
                movement += Vector3.back;
                ChangeCursorState(CursorState.Down);
            }
            else if (mousePosition.y >= Screen.height - edgeThreshold)
            {
                movement += Vector3.forward;
                ChangeCursorState(CursorState.Up);
            }
            else
            {
                ChangeCursorState(CursorState.Default);
            }
        }

        movement= Quaternion.AngleAxis(camera.transform.eulerAngles.y, Vector3.up) *movement.normalized * speed * Time.deltaTime;
        if (movewithMouseDrag && Input.GetMouseButton(2))
        {
            Ray newRay = camera.ScreenPointToRay(Input.mousePosition);
            Ray preRay = camera.ScreenPointToRay(preMousePos);
            RaycastHit newHit, preHit;
            if (Physics.Raycast(newRay, out newHit,Mathf.Infinity,groundLayer) && Physics.Raycast(preRay, out preHit,Mathf.Infinity,groundLayer))
            {
                movement += preHit.point-newHit.point;
            }
        }
        transform.position += movement;
        preMousePos = Input.mousePosition;
    }

    private void AdjustCameraPosition()
    {
        float height = camera.transform.position.y;
        float offset=height/Mathf.Tan(camera.transform.eulerAngles.x*Mathf.Deg2Rad);
        float offsetX=-offset*Mathf.Cos(camera.transform.eulerAngles.y*Mathf.Deg2Rad);
        float offsetZ=-offset*Mathf.Sin(camera.transform.eulerAngles.y*Mathf.Deg2Rad);
        camera.transform.localPosition = new Vector3(offsetX, height, offsetZ);
    }

    private void ChangeCursorState(CursorState newState)
    {
        if (cursorState == newState) return;

        cursorState = newState;
        switch (cursorState)
        {
            case CursorState.Default:
                Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.Auto);
                break;
            case CursorState.Left:
                Cursor.SetCursor(cursorArrowLeft, Vector2.zero, CursorMode.Auto);
                break;
            case CursorState.Right:
                Cursor.SetCursor(cursorArrowRight, new Vector2(cursorArrowRight.width,0), CursorMode.Auto);
                break;
            case CursorState.Up:
                Cursor.SetCursor(cursorArrowUp, Vector2.zero, CursorMode.Auto);
                break;
            case CursorState.Down:
                Cursor.SetCursor(cursorArrowDown, new Vector2(0,cursorArrowDown.height), CursorMode.Auto);
                break;
        }
    }

}
