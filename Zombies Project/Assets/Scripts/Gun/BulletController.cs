using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [Header("Bullet Hole Reference")]
    [SerializeField] private GameObject bulletDecal;

    [Header("Bullet Settings")]
    [SerializeField] private float speed = 50f;
    [SerializeField] private float timeToDestroy = 3f;

  
    public Vector3 target { get; set;}
    public bool hit { get; set; }

    private void OnEnable()
    {
        
        Destroy(gameObject, timeToDestroy);

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (!hit && Vector3.Distance(transform.position, target) < .01f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.gameObject.layer == LayerMask.NameToLayer("Environment"))
        {
            ContactPoint contact = other.GetContact(0);
            GameObject.Instantiate(bulletDecal, contact.point + contact.normal * 0.0001f, Quaternion.LookRotation(contact.normal));
        }

        if (other.gameObject.tag == "Player")
        {
            Physics.IgnoreCollision(other.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
        }
        
        Destroy(gameObject);
    }
}
