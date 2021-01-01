using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 1;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other) // Вызывается при столкновении с другим объектом
    {
        PlayerCharacter player = other.GetComponent<PlayerCharacter>();
        if (player != null)
            player.Hurt(1);
        Destroy(gameObject);
    }
    // TODO: Update fireball's commit with OnTriggerEnter realization.
}

