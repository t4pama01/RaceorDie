using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System.Collections.Generic;

/*
public enum CarDriveType
{
    FWD,
    RWD,
    AWD
}
*/
[System.Serializable]
public class Car : MonoBehaviour
{
    [SerializeField]
    // private CarDriveType m_CarDriveType = CarDriveType.AWD;
    // public CarDriveType carDriveType { get { return m_CarDriveType; } }
    // public List<Wheel> wheels;
    public GameObject destroyedLobsterPrefab;
    public Wheel[] wheels = new Wheel[4];
    public float mass = 1700; // Mass of the car
    public float drag = 0.01f; // drag to apply to the car
    public CarEngine engine; // Cars Engine
    [SerializeField]
    public Transmission transmission = new Transmission();

    public float frontBrakeTorque = 2500; // Brake torque to apply for each front wheel when Braking
    public float backBrakeTorque = 2000; // Brake torque to apply for each back wheel when Braking

  
    public HandBraking handBrake = new HandBraking(); // Class to handle Hand Braking

    public float forwardStifftness = 1f;// Cars wheels forward stifftness
    public float sideStifftness = 1f;// Cars wheels sideward stifftness

    public Vector3 centerOfMass; //vector to optimize the ceneter of mass of the car so it didnt flip over easly


    public float MaxSpeed = 310;//car's Top speed//{ get { return m_Topspeed; } }

   

    public float hp = 0;//Car's horsepower
    public AnimationCurve RPM_Curve;//engine rmp->torque curve, if hp>0 this will be ignored
    public AnimationCurve GearRation_Curve;// cars transmission's gear's ratio

    public Accessories accessories = new Accessories();

    //private Wheel motorWheel;
    private Transform wheelColiders_trans;
    public Wheel[] motorWheels; // motor wheels will be saved in this array for faster access
    public int motorWheelsCount = 0;
    public Wheel[] frontWheels = new Wheel[2];
    public Wheel[] backWheels = new Wheel[2];
    public Wheel[] leftWheels = new Wheel[2];
    public Wheel[] rightWheels = new Wheel[2];

    public Steering steering;

    private float resetTimer;
    private float resetTime = 5f;
    public float maxlifetime = 15.0f;
    public float checkpointtime = 15.0f;
    public float bestTime = 0.0f;
    public float score = 0.0f;
    public float bestscore = 0.0f;
    public int counter = 0;
    public float delayTimer = 0.0f;
    public Text DataText;
    public Text BestScoreText;
    public Image pauseImage;
    public Image descriptionImage;
    public Button PauseRetryButton;
    public Button PauseQuitButton;
    //public GameObject car;

    void Awake()
    {
        descriptionImage.gameObject.SetActive(true);
        pauseImage.gameObject.SetActive(false);
        PauseRetryButton.gameObject.SetActive(false);
        PauseQuitButton.gameObject.SetActive(false);
        Cursor.visible = true;
        bestscore = PlayerPrefs.GetFloat("bestscore");
        score = PlayerPrefs.GetFloat("score");

        wheelColiders_trans = transform.FindChild("WheelColiders");
        steering = GetComponent<Steering>();
        //if no wheels added to the car, create RWD Drivetype wheels system.
        if (wheels == null || wheels.Length == 0)
        {
            wheels = new Wheel[4];
            wheels[0] = buildWheel("wheelFR", true, false);
            wheels[1] = buildWheel("wheelFL", true, false);
            wheels[2] = buildWheel("wheelRR", false, true);
            wheels[3] = buildWheel("wheelRL", false, true);
        }

        foreach (Wheel wheel in wheels)
        {
            wheel.car = this;
        }
        setupMotorWheels();// find and saves motor wheels
        setupFrontAndBackWheels();

    }

    void Start()
    {
        accessories.car = this;
        accessories.OnEnabled();
       // GetComponent<PlayerCar>().enabled = true;
       
    }

    void Update()
    {
       
        bestscore = PlayerPrefs.GetFloat("bestscore");
        CheckIfCarIsFlipped();
        //maxlifetime += Time.deltaTime;

        DataText.text = "Checkpoints: " + counter.ToString() + " / 14" + "\n" + "Time: " + maxlifetime.ToString();
        BestScoreText.text = "Best Time: " + bestscore.ToString();
        if(Input.GetKeyDown(KeyCode.W))
        {
            descriptionImage.gameObject.SetActive(false);
        }
        if (maxlifetime > 0 && counter >= 1)
        {
            
            maxlifetime -= Time.deltaTime;
        }
        if (maxlifetime <= 0)
        {

            GameObject destroyedCarGameObject = (GameObject)Instantiate(destroyedLobsterPrefab, gameObject.transform.position, gameObject.transform.rotation);

            this.gameObject.SetActive(false);
            
        }
/*        if (delayTimer > 5.0f)
        {
            SceneManager.LoadScene("gameOver");
        }*/

/*        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }*/
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SceneManager.LoadScene("CarDemo");
        }
        if (Input.GetKeyDown(KeyCode.End))
        {
            PlayerPrefs.SetFloat("bestscore", 0);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            transform.rotation = Quaternion.LookRotation(transform.forward);
            transform.position += Vector3.up * 0.5f;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
        if (Time.timeScale == 1)
        {
            AudioListener.pause = false;
            AudioListener.volume = 1;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                pauseImage.gameObject.SetActive(true);
                PauseRetryButton.gameObject.SetActive(true);
                PauseQuitButton.gameObject.SetActive(true);
                Time.timeScale = 0;
                Cursor.visible = true;
            }
        }
        else if (Time.timeScale == 0)
        {
            AudioListener.pause = true;
            AudioListener.volume = 0;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                pauseImage.gameObject.SetActive(false);
                PauseRetryButton.gameObject.SetActive(false);
                PauseQuitButton.gameObject.SetActive(false);
                Time.timeScale = 1;
                Cursor.visible = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "checkpoint")
        {
            Debug.Log("Yay!");
            other.gameObject.SetActive(false);
            maxlifetime = checkpointtime + maxlifetime;
            counter++;
        }
        else if (other.gameObject.tag == "goal")
        {
            other.gameObject.SetActive(false);
            counter++;
        }
        else if (other.gameObject.tag == "start")
        {
            other.gameObject.SetActive(false);
            counter++;
        }
        if (counter == 14)
        {
            if (bestscore == 0)
            {
                PlayerPrefs.SetFloat("bestscore", maxlifetime);
                PlayerPrefs.SetFloat("score", maxlifetime);
                SceneManager.LoadScene("WinScreen");
            }
            else
            {
                if (maxlifetime > bestscore)
                {
                    PlayerPrefs.SetFloat("bestscore", maxlifetime);
                    PlayerPrefs.SetFloat("score", maxlifetime);
                    SceneManager.LoadScene("WinScreen");
                }
                else if (maxlifetime < bestscore)
                {
                    PlayerPrefs.SetFloat("score", maxlifetime);
                    SceneManager.LoadScene("WinScreen");
                }
            }
        }
    }

    public void CheckIfCarIsFlipped()
    {
        if (transform.localEulerAngles.z > 80 && transform.localEulerAngles.z < 280)
        {
            resetTimer += Time.deltaTime;
        }
        else
        {
            resetTimer = 0;
        }
        if (resetTimer > resetTime)
        {
            transform.rotation = Quaternion.LookRotation(transform.forward);
            transform.position += Vector3.up * 0.5f;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            resetTimer = 0;
        }

    }

    void setupFrontAndBackWheels()
    {
        Wheel wheel;
        int tmp_front_i = 0;
        int tmp_back_i = 0;
        int tmp_left_i = 0;
        int tmp_right_i = 0;
        for (int i = 0; i < 4; i++)
        {
            wheel = wheels[i];
            if (wheel.isFront)
            {
                wheel.onStart(frontBrakeTorque);
                frontWheels[tmp_front_i++] = wheel;
            }
            else
            {
                wheel.onStart(backBrakeTorque);
                backWheels[tmp_back_i++] = wheel;
            }

            if (wheel.wheelCollider.transform.position.x < transform.position.x)
            {
                leftWheels[tmp_left_i++] = wheel;
            }
            else
            {
                rightWheels[tmp_right_i++] = wheel;
            }
        }
    }
    Wheel buildWheel(string wheelName, bool isFront, bool isMotor)
    {
        Wheel wheel = new Wheel();
        Transform wheelTrans = transform.FindChild(wheelName);//search for wheel mesh that has "wheelName" name
        WheelCollider wheelCollider = wheelColiders_trans.FindChild(wheelName).GetComponent<WheelCollider>();// find a wheel colider with the same name
        wheel.wheelCollider = wheelCollider;
        wheel.wheelTransform = wheelTrans;
        wheel.isFront = isFront;
        wheel.isMotor = isMotor;
        return wheel;

    }

    public void setupMotorWheels()
    {
        motorWheelsCount = 0;
        Wheel wheel;
        for (int i = 0; i < 4; i++)
        {
            wheel = wheels[i];
            if (wheel.isMotor)
                motorWheelsCount++;
        }
        motorWheels = new Wheel[motorWheelsCount];
        int tmp_i = 0;
        for (int i = 0; i < 4; i++)
        {
            wheel = wheels[i];
            if (wheel.isMotor)
                motorWheels[tmp_i++] = wheel;

        }
    }
}
