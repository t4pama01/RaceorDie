using UnityEngine;


public class UserInput : MonoBehaviour
{
    private CarControl m_Car;
    private CarLight m_Light;

    float h;
    float accel;
    float brake;
    float handbrake;
    float revears;
    bool toggleDriftSystem;
    bool toggleFLightsOn;
    bool activeNitro;
    bool toggleFLightsOff;

    const string Horizontal_KEY = "Horizontal";
    const string Vertical_KEY = "Vertical";
    const string NITRO_KEY = "Fire2";
    const string HANDBRAKE_KEY = "Fire1";
    //const string DRIFT_SYSTEM_KEY = KeyCode.A;


    
    void Start()
    {
    
        m_Car = GetComponent<CarControl>();
    }

    void Update()
    {
        toggleDriftSystem = Input.GetKeyUp(KeyCode.Q);
        if (toggleDriftSystem)
            m_Car.toggleDriftSystem();

        
        toggleFLightsOn = Input.GetKeyUp(KeyCode.E);
        if (toggleFLightsOn)
            m_Car.frontLightsOn();

        toggleFLightsOff = Input.GetKeyUp(KeyCode.R);
        if (toggleFLightsOff)
            m_Car.frontLightsOff();
    }

 

    void FixedUpdate()
    {

       
         h =  Input.GetAxis(Horizontal_KEY) ;
      
        accel = brake = Input.GetAxis(Vertical_KEY) ;
        handbrake = Input.GetAxis(HANDBRAKE_KEY);

        m_Car.Move(h, accel, brake, handbrake); // pass the input to the car!
        activeNitro = Input.GetButton(NITRO_KEY);
        m_Car.nitro(activeNitro);

       
    }
   
}

