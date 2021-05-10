using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TankController : MonoBehaviour
{
    private float m_horizontalInput;
    private float m_verticalInput;
    private float m_steerAngle;
    private float m_turretHorizontal;
    private float m_turretVertical;
    private Rigidbody m_Rigidbody;
    private Vector3 m_EulerAngleVelocity; 

    public int health = 20;
    public float RocketForce = 75f;
    public float maxSteerAngle = 45f;
    public float maxCanonAngle = 90f;
    public float m_Speed = 10f;
    public Camera thirdPerson;
    public Camera overHead;
    public GameObject FLWheel, FRWheel, RLWheel, RRWheel; // front left, front right, rear left, rear right.  Looking at it from the back of the tank to the front..
    public GameObject LeftSteerGeo, RightSteerGeo;
    public GameObject turretGeo, canonGeo;
    public GameObject RocketPreFab;
    public HealthBar healthBar;

    private void Start()
    {
        //start off with third person view
        overHead.gameObject.SetActive(false);
        thirdPerson.gameObject.SetActive(true);
        m_Rigidbody = GetComponent<Rigidbody>();
        health = 20;
        healthBar.SetMaxHealth(health);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))               { Application.Quit(); } //quit application
        if (Vector3.Dot(transform.up, Vector3.down) > 0)    {   RestartGame();  }//tank is upside down
        if (Input.GetKeyDown(KeyCode.Space))                {   FireRocket();   }//shoot the rocket
        if (Input.GetKeyDown(KeyCode.R))                    {   RestartGame();  }// restart

        //switches between overhead view and third person view.
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (thirdPerson.gameObject.activeSelf)
            {
                thirdPerson.gameObject.SetActive(false);
                overHead.gameObject.SetActive(true);
            }
            else
            {
                overHead.gameObject.SetActive(false);
                thirdPerson.gameObject.SetActive(true);
            }
        }
    }

    private void FixedUpdate()
    {
        GetInput();
        Steer();
        Move();
        RotateWheels();
    }

    /// <summary>
    /// GetInput: Saves the Horizontal and Vertical Inputs for the Tank Movement and the Turret Movement
    /// </summary>
    public void GetInput()
    {
        m_horizontalInput = Input.GetAxis("Horizontal");
        m_verticalInput = Input.GetAxis("Vertical");
        m_turretHorizontal = Input.GetAxis("TurretHorizontal") * 3f;
        m_turretVertical = Input.GetAxis("TurretVertical") * 3f;
        
    }

    /// <summary>
    /// Steer: Sets the SteerAngle, Resets the front wheels if no horizontal input is noticed and rotates the turret and canon.
    /// </summary>
    private void Steer()
     {
        m_steerAngle = maxSteerAngle * m_horizontalInput;

        if(m_horizontalInput == 0)
        {
            ResetRotation(LeftSteerGeo);
            ResetRotation(RightSteerGeo);
        }

        AdjustRotation(turretGeo, m_turretHorizontal, 'y', false);
        AdjustRotation(canonGeo, m_turretVertical, 'x', false);
    }

    /// <summary>
    /// Move: Moves the Tank by multiplying the rigidbody by the speed using MovePosition. Also, rotates the front wheels for turning and rotates the whole tank if moving.
    /// </summary>
    private void Move()
    {

        //Store user input as a movement vector
        Vector3 m_Input = new Vector3(0, 0, m_verticalInput);

        //Apply the movement vector to the current position, which is
        //multiplied by deltaTime and speed for a smooth MovePosition
        m_Rigidbody.MovePosition(m_Rigidbody.position + transform.forward * m_Input.normalized.z * Time.deltaTime * m_Speed);

        if (m_steerAngle != 0)
        {
            AdjustRotation(LeftSteerGeo, m_steerAngle, 'y', true);
            AdjustRotation(RightSteerGeo, m_steerAngle, 'y', true);
            
            if (m_verticalInput != 0)
            {
                m_EulerAngleVelocity = new Vector3(0, m_steerAngle * m_verticalInput, 0);
                Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.fixedDeltaTime);
                m_Rigidbody.MoveRotation(m_Rigidbody.rotation * deltaRotation);
            }
        }
    }

    /// <summary>
    /// RotateWheels: Calls the Function AdjustWheelSpin for each tire and gives a tire speed.
    /// </summary>
    private void RotateWheels()
    {
        AdjustWheelSpin(FLWheel, 65f);
        AdjustWheelSpin(FRWheel, 65f);
        AdjustWheelSpin(RLWheel, 80f);
        AdjustWheelSpin(RRWheel, 80f);
    }

    /// <summary>
    /// AdjustWheelSpin: Rotates the wheel, forward and backwards, by a speed passed in the parameters.
    /// </summary>
    /// <param name="g">g: GameObject that needs to be rotated (in this case the wheels).</param>
    /// <param name="speed">Speed: the speed at which you want that wheel to spin.</param>
    public void AdjustWheelSpin(GameObject g, float speed)
    {
        g.transform.Rotate(new Vector3(m_Speed * Time.deltaTime * m_verticalInput * speed, 0, 0));
    }
    /// <summary>
    /// AdjustRotation: This is used to rotate the Wheels left and right, The turret left and right and the canon up and down
    /// </summary>
    /// <param name="g">g: GameObject that needs to be rotated.</param>
    /// <param name="rotationValue">rotationValue: the value at which to rotate the GameObject</param>
    /// <param name="XYorZ">XYorZ: The plane that needs to be changed: x, y or z plane.</param>
    /// <param name="forWheels">forWheels: specifies if this iteration is for the wheels or not.</param>
    public void AdjustRotation(GameObject g, float rotationValue, char XYorZ, bool forWheels)
    {
        /*if(HorV == 'v')
        {
            if(isContinuous)
            {
                Quaternion angle = Quaternion.Euler(g.transform.eulerAngles.x + rotationValue, 0, 0);
                g.transform.rotation = Quaternion.Lerp(g.transform.rotation, angle, 8 * Time.deltaTime);
            }
            else
            {
                Quaternion angle = Quaternion.AngleAxis(rotationValue, Vector3.right);
                g.transform.rotation = Quaternion.Lerp(g.transform.rotation, angle, 8 * Time.deltaTime);
            }
        }    
        if(HorV == 'h')
        {
            if(isContinuous)
            {
                Quaternion angle = Quaternion.Euler(0, g.transform.eulerAngles.y + rotationValue, 0);
                g.transform.rotation = Quaternion.Lerp(g.transform.rotation, angle, 8 * Time.deltaTime);
            }
            else
            {
                Quaternion angle = Quaternion.AngleAxis(rotationValue, Vector3.up);
                g.transform.rotation = Quaternion.Lerp(g.transform.rotation, angle, 8 * Time.deltaTime);
            }
        }*/
        Quaternion quat = g.transform.rotation;
        Vector3 vec = quat.eulerAngles;
        switch (XYorZ)
        {
            case 'x':
                if (forWheels)
                    vec.x = rotationValue;
                else
                    vec.x += rotationValue;
                break;
            case 'y':
                if (forWheels)
                    vec.y = rotationValue;
                else
                    vec.y += rotationValue;
                break;
            case 'z':
                if (forWheels)
                    vec.z = rotationValue;
                else
                    vec.z += rotationValue;
                break;
        }
        quat.eulerAngles = vec;
        g.transform.rotation = Quaternion.Lerp(g.transform.rotation, quat, 8 * Time.deltaTime);
    }

    /// <summary>
    /// ResetRotation: resets the front wheels rotation so that they are always pointing forward when not being rotated.
    /// </summary>
    /// <param name="g">g: GameObject to be reset.</param>
    public void ResetRotation (GameObject g)
    {
        Quaternion rotation = Quaternion.LookRotation(transform.forward, Vector3.up);
        g.transform.rotation = Quaternion.Lerp(g.transform.rotation, rotation, 8 * Time.deltaTime);
    }

    /// <summary>
    /// FireRocket: Creates the Rocket instance and calls it's Setup function to move the Rocket with a specified force.
    /// </summary>
    public void FireRocket()
    {
        GameObject Rocket = Instantiate(RocketPreFab, canonGeo.transform.position, Quaternion.identity);
        Vector3 shootDir = canonGeo.transform.forward;
        Rocket.transform.GetComponent<RocketController>().Setup(shootDir, RocketForce);
    }

    /// <summary>
    /// TankHit: If the tank is hit by an enemy, this is called and the Tank loses health.
    /// If it loses too much health, by default it has 20 lives, then it is destroyed and the game restarts.
    /// </summary>
    public void TankHit()
    {
        if(health <= 1)
        {
            health--;
            healthBar.SetHealth(health);
            RestartGame();
            Destroy(this.gameObject);
            return;
        }
        health--;
        healthBar.SetHealth(health);
    }

    /// <summary>
    /// TankAddLives: To give the user a way to gain health, this is called when the user eliminates an enemy. This adds as much health as specified, to the tank.
    /// </summary>
    /// <param name="livesToAdd">livesToAdd: The specific number of lives to add to the tank.</param>
    public void TankAddLives(int livesToAdd)
    {
        health += livesToAdd;
        if (health > 20) health = 20;
        healthBar.SetHealth(health);
    }

    /// <summary>
    /// RestartGame: Re-Loads the Scene so that everything is fresh and new.
    /// </summary>
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}
