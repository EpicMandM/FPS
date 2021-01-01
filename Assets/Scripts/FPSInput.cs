using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPSInput")]
public class FPSInput : MonoBehaviour
{
    public float speed = 6.0f;
    public const float baseSpeed = 6.0f;
    public float gravity = -9.8f;
    private CharacterController _charController;

    private void Awake()
    {
        Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }

    private void OnDestroy()
    {
        Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }
    private void OnSpeedChanged(float value)
    {
        speed = baseSpeed * value;
    }
    // Start is called before the first frame update
    void Start()
    {
        _charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed; // A/D
        float deltaZ = Input.GetAxis("Vertical") * speed;  //W/S
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);  // Ограничим движение по диагонали той же скоростью, 
        movement = Vector3.ClampMagnitude(movement, speed); // что и движение параллельно осям.   
        movement.y = gravity;
        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement); // Преобразуем вектор движения от локальных к глобальным кординатам
        _charController.Move(movement); // Заставим этот вектор перемещать компонент CharacterController
        //transform.Translate(deltaX * Time.deltaTime, 0, deltaZ * Time.deltaTime);
    }
}
