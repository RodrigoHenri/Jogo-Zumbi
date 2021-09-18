using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemieFollow : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform Player;
    [SerializeField] private bool DetectouJogador;
    [SerializeField] private float Distance;
    [SerializeField] private float TestVariableDistance;

    [Header("Nav Mesh")]
    public NavMeshAgent nav;

    [Header("Zombie Speed")]
    [SerializeField] private int Speed;

    [Header("Zombie Attacks")]
    [SerializeField] private bool IsAttacking;
    [SerializeField] private GameObject HitboxAttack;

    [Header("Animations Variables")]
    [SerializeField] private Animator animator;
    [SerializeField] private bool Attacking;
    [SerializeField] public bool Died;
    
    [Header("Audio Variables")]
    [SerializeField] private AudioSource audio;

    [Header("Life Variables")]
    public int Life;
    public int MaxLife;

   // Start is called before the first frame update
   void Start()
   {
       Player = GameObject.Find("Player").transform;
       nav = GetComponent<NavMeshAgent>();

       nav.speed = Speed;
       Life = MaxLife;

        animator = GetComponent<Animator>();
       

    }
    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Attacking", Attacking);
        animator.SetBool("Died", Died);
        animator.SetInteger("SpeedMovement", Speed);

        float Distance = Vector3.Distance(Player.transform.position, this.transform.position);
       
        
        if (IsAttacking == false && Life > 0 && Player.GetComponent<PlayerController>().CanPlay == true)
        {
            transform.LookAt(Player);

            if (Distance >= TestVariableDistance)
            {
                Attacking = false;
                StopCoroutine(ZombieAttackThePlayer());
                Attacking = false;
                
                nav.speed = Speed;
               
                nav.SetDestination(Player.position);
            }
            
            else
            {
                StartCoroutine(ZombieAttackThePlayer());
            }
        }

    }


    public IEnumerator ZombieAttackThePlayer()
    {
        Attacking = true;
        nav.speed = 0f;
        IsAttacking = true;
        yield return new WaitForSeconds(1.36f);
        HitboxAttack.SetActive(true);
        yield return new WaitForSeconds(1.12f);
        HitboxAttack.SetActive(false);
        yield return new WaitForSeconds(1.15f);
        
        IsAttacking = false;
    }

    


}
