using System;
using System.Collections;
using TMPro;
using UnityEngine;
namespace Pathfinding
{
   
    [UniqueComponent(tag = "ai.destination")]
    public class PlayerTarget : VersionedMonoBehaviour
    {
       // enemytag to find enemies
        public string enemyTag;

        //parent of players
        Transform players;

        //Enemy to follow and attack
         Transform target;

        IAstarAI ai;

        //animator of the player
        Animator anim;

        //playerHealth script
        PlayerHealth playerHealthScript;

        //time interval for the next attack
        public float timeIntervalToAttack = 1f;

        
        bool isAttackable = true;
        bool isAttacking=false;
        bool isMoving;

        [SerializeField] private float playerHealth=100;
        [SerializeField] private int enemyDamage=10;
        private void Awake()
        {
            anim= GetComponent<Animator>();
            playerHealthScript= GetComponent<PlayerHealth>();
        }
        void OnEnable()
        {
            ai = GetComponent<IAstarAI>();
            if (ai != null) ai.onSearchPath += Update;

            players = GameObject.Find("Players").transform;
            
        }

        void OnDisable()
        {
            if (ai != null) ai.onSearchPath -= Update;
        }

        /// <summary>Updates the AI's destination every frame</summary>
        void Update()
        {
            TargetFind();
            if (target != null && ai != null) ai.destination = target.position;
            isMoving=ai.velocity.magnitude> 0.5;
            Debug.Log(ai.velocity.magnitude);
        }
        void TargetFind()
        {

            foreach(Transform player in players)
            {
                if(player.tag== enemyTag)
                {
                    if(target==null)
                    {
                        target= player;
                    }
                    else if(Vector3.Distance(gameObject.transform.position,target.position)>
                         Vector3.Distance(gameObject.transform.position, player.position))
                    {
                            target= player;

                    }

                    if (!ai.reachedDestination)
                    {
                        anim.SetBool("Walk", true)
;
                    }
                   

                }
                if (!isMoving)
                {
                    anim.SetBool("Walk", false);
                }
            }
            

        }

        private void OnTriggerStay(Collider other)
        {
            if(other.CompareTag(enemyTag)&&!isAttacking&&isAttackable)
            {
                transform.LookAt(other.gameObject.transform);
                isAttacking=true;
                StartCoroutine("AttackEnemy");
            }
            
        }
        IEnumerator AttackEnemy()
        {
            
            PlayerTarget enemyHealth=target.GetComponent<PlayerTarget>();
            if(enemyHealth != null&&enemyHealth.isAttackable)
            {
                anim.SetBool("Attack", true);
                enemyHealth.TakeDamage(enemyDamage);
            }
            
            yield return new WaitForSeconds(1);
            anim.SetBool("Attack", false);
            isAttacking=false;
        }

        public void TakeDamage(int damage)
        {
            
            playerHealth-=damage;
            playerHealthScript.HealthUpdate(playerHealth);
            if (playerHealth <= 0)
            {
                isAttackable = false;
                anim.SetTrigger("Death");
                StartCoroutine("PlayerDied");
            }
            Debug.Log(enemyTag + damage.ToString() + "  " + playerHealth.ToString());
        }


        IEnumerator PlayerDied()
        {
            yield return new WaitForSeconds(3.5f);
            Destroy(gameObject);
        }
    }

}