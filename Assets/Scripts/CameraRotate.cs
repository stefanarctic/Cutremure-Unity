using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{

    public Transform targetTransform;
    //public Transform target1;
    //public Transform target2;

    //public Transform cameraPosition1;
    //public Transform cameraPosition2;
    public Vector3 TargetPosition
    {
        get { return target; }
        set { target = value; }
    }

    private Vector3 target;
    public float amp = 2.0f;

    void Awake()
    {
        //targetTransform = target1;
        if (!targetTransform)
        {
            target = TargetPosition;
        }
        else
            target = targetTransform.position;
        transform.LookAt(target);
    }

    void FixedUpdate()
    {
        transform.LookAt(target);
        //transform.position = cameraPosition1.position;

        if (Input.GetKey(KeyCode.RightArrow))
            transform.Translate(Vector3.right * amp * Time.fixedDeltaTime);
        else if (Input.GetKey(KeyCode.LeftArrow))
            transform.Translate(Vector3.left * amp * Time.fixedDeltaTime);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.instance.NextSceneAsync();
            //print("Pressed enter");
            //target = target2.position;
            //transform.position = cameraPosition2.position;
        }
    }
}
