using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public Transform targetTransform;

    public Vector3 TargetPosition
    {
        get { return target; }
        set { target = value; }
    }

    private Vector3 target;
    public float amp = 5.0f;
    public float zoomAmp = 10.0f;
    public float minDistance = 2.0f;
    public float maxDistance = 50.0f;

    private Camera cameraComponent;
    //private Transform lastTransform;
    private Vector3 lastPosition;
    private Quaternion lastRotation;

    void Awake()
    {
        if (!targetTransform)
        {
            target = TargetPosition;
        }
        else
        {
            target = targetTransform.position;
        }

        transform.LookAt(target);

        if (!cameraComponent)
            cameraComponent = GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        transform.LookAt(target);

        if (Input.GetKey(KeyCode.RightArrow))
            transform.Translate(Vector3.right * amp * Time.fixedDeltaTime);
        else if (Input.GetKey(KeyCode.LeftArrow))
            transform.Translate(Vector3.left * amp * Time.fixedDeltaTime);
        else if (Input.GetKey(KeyCode.UpArrow))
            transform.Translate(Vector3.up * amp * Time.fixedDeltaTime);
        else if (Input.GetKey(KeyCode.DownArrow))
            transform.Translate(Vector3.down * amp * Time.fixedDeltaTime);

        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            transform.RotateAround(target, Vector3.up, mouseX * amp);
            transform.RotateAround(target, transform.right, -mouseY * amp);
        }

        // Zoom with scroll wheel
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) > 0.01f)
        {
            Vector3 direction = (transform.position - target).normalized;
            float distance = Vector3.Distance(transform.position, target);
            float zoomDelta = -scroll * zoomAmp;

            // Apply zoom only if within limits
            if ((distance > minDistance || zoomDelta < 0) && (distance < maxDistance || zoomDelta > 0))
            {
                transform.position += direction * zoomDelta;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
            Toggle2DCamera();


        if(Input.GetKey(KeyCode.LeftControl))
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
                SceneManager.instance.NextScene();
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
                SceneManager.instance.PreviousScene();
        }
        //if (Input.GetKeyDown(KeyCode.Return))
        //    SceneManager.instance.NextSceneAsync();

        //if (Input.GetKeyDown(KeyCode.Backspace))
        //    SceneManager.instance.PreviousSceneAsync();

        // Arrows with ctrl
    }

    public void Enable2DCamera()
    {
        cameraComponent.orthographic = true;
        lastPosition = transform.position;
        lastRotation = transform.rotation;
        //transform.position = new Vector3(0, 0, -10); // Looks at XY plane
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        transform.rotation = Quaternion.identity;
    }

    public void Disable2DCamera()
    {
        cameraComponent.orthographic = false;
        transform.position = lastPosition;
        transform.rotation = lastRotation;
    }

    public void Toggle2DCamera()
    {
        bool enabled = cameraComponent.orthographic;
        if (enabled)
            Disable2DCamera();
        else
            Enable2DCamera();
    }
}
