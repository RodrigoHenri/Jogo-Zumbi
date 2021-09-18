using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieHP : MonoBehaviour
{
    [Header("Zombie Parts")]
    [SerializeField] private bool ZombieHead;
    [SerializeField] private bool ZombieBody;
    [SerializeField] private int ZombiePoints;

    [Header("Zombie Parent Refence")]
    [SerializeField] private Transform Zombie;

    [Header("Audio Variables")]
    [SerializeField] private AudioSource audio;
    
    
    [Header("Player Score")]
    [SerializeField] private Transform PlayerScore;

    [Header("Spawn System Reference")]
    [SerializeField] private Transform SpawnReference;


    // Start is called before the first frame update
    void Start()
    {
        SpawnReference = GameObject.Find("SpawnControlls").transform;
        PlayerScore = GameObject.Find("Player").transform;
    }

    public void TakeDamage(int DamageTaken)
    {
        Zombie.GetComponent<EnemieFollow>().Life = Zombie.GetComponent<EnemieFollow>().Life - DamageTaken;

        if (Zombie.GetComponent<EnemieFollow>().Life <= 0)
        {
            StopAllCoroutines();
            Die();
        }
    }

    public void Die()
    {
        StartCoroutine(TimeToDie());
    }

    public IEnumerator TimeToDie()
    {
        Zombie.GetComponent<AudioSource>().Pause();
        Zombie.GetComponent<EnemieFollow>().nav.speed = 0f;
        audio.PlayDelayed(2f);
        Zombie.GetComponent<EnemieFollow>().Died = true;
        SpawnReference.GetComponent<SpawnSystem>().ZombieDied(1);

        Zombie.GetComponent<CapsuleCollider>().enabled = false;
        this.GetComponent<BoxCollider>().enabled = false;

        yield return new WaitForSeconds(3f);
        PlayerScore.GetComponent<PlayerScore>().UpdateActualScore(ZombiePoints);
        Destroy(Zombie.gameObject);
    }

    private void OnTriggerEnter(Collider Col)
    {
        if (Col.tag == "PistolBullet")
        {
            if (ZombieBody == true)
            {
                TakeDamage(3);
            }
            if (ZombieHead == true)
            {
                TakeDamage(6);

            }
        }
    }
}
