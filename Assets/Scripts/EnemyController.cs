using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int health = 3;
    public float ShootingFrequency = 0.5f;
    public float BulletSpeed = 75f;
    public float maxDistanceToRecognize = 3000f;
    public float maxAngleToRecognize = 45f;
    public float curDist;
    public float angleToPlayer;
    public HealthBar healthBar;
    public GameObject bullet;
    public Transform hand;    

    private bool Shooting = false;
    private Animator animator;
    private GameObject tank;
    private Quaternion initialRotation;
    private Vector3 initialPosition;
    private Vector3 initialForward;

   // private bool checking;
    private void Start()
    {
        health = 3;
        healthBar.SetMaxHealth(health);
        animator = gameObject.GetComponent<Animator>();
        tank = GameObject.Find("tank");
        initialRotation = transform.rotation;
        initialPosition = transform.position;
        initialForward = transform.forward;
        Shooting = false;
    }
    private void Update()
    {
        Vector3 targetDir = tank.transform.position - initialPosition;
        curDist = targetDir.sqrMagnitude;
        angleToPlayer = (Vector3.Angle(targetDir, initialForward));

        if(angleToPlayer >= -maxAngleToRecognize && angleToPlayer <= maxAngleToRecognize && curDist <= maxDistanceToRecognize)
        {
            LookatPlayer();
            animator.SetBool("Shoot", true);
            if(!Shooting)
                ShootBullet();
            //Instantiate Bullets
            
        }
        else
        {
            ResetLook();
            animator.SetBool("Shoot", false);
        }
    }

    /// <summary>
    /// LookatPlayer: this allows the enemy to pin point the Player and follow their moves to shoot at them.
    /// </summary>
    public void LookatPlayer()
    {
        Vector3 LookDir = tank.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(LookDir, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 8 * Time.deltaTime);
    }

    /// <summary>
    /// ResetLook: Once the player moves out of sight, this is called to set the enemy back to it's initial position.
    /// </summary>
    public void ResetLook()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, initialRotation, 8 * Time.deltaTime);
    }

    /// <summary>
    /// ShootBullet: Creates the Bullet instance and calls it's Setup function to move the Bullet with a specified force.
    /// </summary>
    public void ShootBullet()
    {
        Shooting = true;
        GameObject BulletObject = Instantiate(bullet, hand.transform.position, Quaternion.identity);
        Vector3 shootDir = transform.forward;
        BulletObject.transform.GetComponent<BulletController>().Setup(shootDir, BulletSpeed);
        StartCoroutine(WaitingToShoot());
    }

    /// <summary>
    /// IEnumerator WaitingToShoot: waits for a certain amount of seconds before firing the next bullet at the player.
    /// </summary>
    /// <returns>N/A</returns>
    IEnumerator WaitingToShoot()
    {
        yield return new WaitForSeconds(ShootingFrequency);
        Shooting = false;
    }

    /// <summary>
    /// Hit: If the Enemy is hit, this is called and the enemy loses a health point, by default the enemy only has 3.
    /// </summary>
    public void Hit()
    {
        if (health <= 1)
        {
            health--;
            healthBar.SetHealth(health);
            tank.GetComponent<TankController>().TankAddLives(2);
            Destroy(gameObject);
            //Debug.Log("Enemy eliminated");
            return;
        }
        health--;
        healthBar.SetHealth(health);
        //Debug.Log("Enemy Hit! Health at: " + health);
        return;
    }
}
