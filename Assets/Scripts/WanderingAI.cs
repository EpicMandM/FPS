using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 3.0f;
    public const float baseSpeed = 3.0f;
    public float obstacleRange = 5.0f;
    [SerializeField] private GameObject fireballPrefab;
    private GameObject _fireball;

    private bool _alive;
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

    // Update is called once per frame
    void Start()
    {
        _alive = true;
    }
    void Update()
    {   if(_alive)
            transform.Translate(0, 0, speed * Time.deltaTime);

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if(Physics.SphereCast(ray, 0.75f, out hit)) // Бросаем луч с описанной вокруг него окружностью
        {
            GameObject hitObject = hit.transform.gameObject;
            if(hitObject.GetComponent<PlayerCharacter>())
            {
                if(_fireball == null)
                {
                    _fireball = Instantiate(fireballPrefab);
                    _fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f); // Помещаем fireball перед врагом и нацелим его в направлении его движения
                    _fireball.transform.rotation = transform.rotation;
                }

            }
            else if (hit.distance < obstacleRange)
            {
                float angle = UnityEngine.Random.Range(-110, 110); // Поворот с наполовину случайным выбором нового направления
                transform.Rotate(0, angle, 0);
            }

        }
        
    }
    public void SetAlive(bool alive)
    {
        _alive = alive;
    }
}
