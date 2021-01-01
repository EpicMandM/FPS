using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sensitivityHor = 9f;
    public float sensitivityVert = 9f;

    public float minimumVert = -45f;
    public float maximumVert = 45f;

    private float _rotationX = 0;
    public enum RotationAxes
    {
        MouseXAndY, 
        MouseX,
        MouseY
    }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        if (body != null)
            body.freezeRotation = true; // отключаем влияние модели физической модели на игрока

    }

    // Update is called once per frame
    void Update()
    {
        if (axes == RotationAxes.MouseX) // по горизонтали
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);
        }
        else if (axes == RotationAxes.MouseY) // по вертикали
        {
            _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);
            float rotationY = transform.localEulerAngles.y;
            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }
        else // все дружно
        {
            _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);

            float delta = Input.GetAxis("Mouse X") * sensitivityHor; // величина изменения
            float rotationY = transform.localEulerAngles.y + delta;

            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }
    }
}
