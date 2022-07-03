using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    public EnemyStats Estats;
    
    private float timerForShoot;
    private float timerForMove;
    private int move;
    private int waitingTimeToShoot;
    private int waitingTimeToMove = 2;
    public Player player;
    public Transform target;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public GameObject ammoBox;
    public GameObject medBox;
    public AudioClip fire;
    public GameObject bulletPosition;
    
    public GameObject secondWeapon;
    public GameObject firstWeapon;
    public GameObject dropGun;
    public Levels level;
    
    
    public void TakeDamage(float amount)
    {
        Estats.health -= amount;
        if (Estats.health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        GameObject newAmmoBox = Instantiate(ammoBox, gameObject.transform.position, Quaternion.identity);
        GameObject newMedBox = Instantiate(medBox, gameObject.transform.position, Quaternion.identity);
        if (Estats.currentWeapon == 1)
        { GameObject newGun = Instantiate(dropGun, gameObject.transform.position, Quaternion.identity); }
        if (gameObject.name != "EnemyOriginal")
        { Destroy(gameObject); }
        level.killcount++;
        level.spawnedEnemies--;
    }

    private void Start()
    {
        if (level.level > 3)
        { move = 1; }
        else move = -1;
        waitingTimeToShoot = 8-level.level/10;
        gameObject.SetActive(true);
    }

    public void Shoot()
    {
        AudioSource.PlayClipAtPoint(fire, transform.position, 1);
        muzzleFlash.Play();
        AudioSource.PlayClipAtPoint(fire, transform.position, 1);
        if (Physics.Raycast(bulletPosition.transform.position, bulletPosition.transform.forward, out RaycastHit hit, Estats.range))
        {

            //Debug.Log(this.name+ " a lovit pe "+hit.transform.name);

            Player player = hit.transform.GetComponent<Player>();
            GameObject GOimapct = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(GOimapct, 1);
            if (player != null)
            {
                if (hit.collider is SphereCollider)
                    player.TakeDamage(100f);
                player.TakeDamage(Estats.damage);
            }

           /* if (hit.rigidbody != null)
            {

                hit.rigidbody.AddForce(-hit.normal * Estats.impactForce);

            }*/
        }

    }
    void Dodge(int Pmove)
    {
        
        if (Pmove ==1)
        {
            transform.position += Vector3.left * Time.deltaTime * Estats.speed;
            //Debug.Log("trb sa ne mutam stanga");
        }
        else if (Pmove ==2)
        {
           // Debug.Log("trb sa ne mutam dreapta");
            transform.position += Vector3.right * Time.deltaTime * Estats.speed;

        }


    }
    void Update()
    {
        //m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX;
        // m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationZ;
        //m_Rigidbody.freezeRotation = true;
        //this.transform.position = Vector3.Lerp(transform.position,transform.position+plm, 0);

        /*float dist = Vector3.Distance(player.transform.position, transform.position);
         if (dist > range)
         {
             myNavMeshAgent.isStopped = false;
             myNavMeshAgent.SetDestination(player.transform.position);

         }
         if (dist < range)
         {
             myNavMeshAgent.isStopped = true;

         }*/
        if(level.reset ==1)
        {
            if (gameObject.name != "EnemyOriginal")
            { Destroy(gameObject); }
        }


        if(Estats.currentWeapon==1)
        {
            secondWeapon.SetActive(false);
            firstWeapon.SetActive(true);
            waitingTimeToShoot = 8 - level.level/10;
        }
        if(Estats.currentWeapon>=2)
        {
            secondWeapon.SetActive(true);
            firstWeapon.SetActive(false);
        }
        if (move == 1)
            Dodge(1);
        else if(move==2) Dodge(2);

        var targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
        targetRotation.x = 0f;
        targetRotation.z = 0f;
        this.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Estats.speed * Time.deltaTime);
         
        timerForShoot += Time.deltaTime;
         if (timerForShoot > waitingTimeToShoot)
         {
            if (level.level > 3)
            { Shoot(); }
             timerForShoot = 0;
         }
        timerForMove += Time.deltaTime;
        if (timerForMove > waitingTimeToMove)
        {
            if (level.level > 3)
            {
                if (move == 1)
                    move = 2;
                else if (move == 2) move = 1;
            }
            timerForMove = 0;
        }
    }
}
