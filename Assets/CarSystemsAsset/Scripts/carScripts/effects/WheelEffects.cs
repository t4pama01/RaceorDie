using System.Collections;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class WheelEffects : MonoBehaviour
{
    public Transform SkidTrailPrefab;
    public Transform[] SkidTrailPrefabPool;
    public int maxSkidMarks = 10;
    private static Transform skidTrailsDetachedParent;
    private GameObject skidParticles;
    public bool skidding { get; private set; }
    public bool PlayingAudio { get; private set; }
    public Accessories a;
    public WheelCollider collider;
    float speedKMH = 0.0f;


    AudioSource m_AudioSource;
    Transform m_SkidTrail;
    WheelCollider m_WheelCollider;

    /*public void speedometer()
    {
        speedKMH = PlayerPrefs.GetFloat("speedKMH");

        // Debug.Log(speedKMH);

        float tempSlip = 1f;

        if (speedKMH > 30) { tempSlip = 1.4f; }
        if (speedKMH > 50) { tempSlip = 1.6f; }
        if (speedKMH > 70) { tempSlip = 1.8f; }
        if (speedKMH > 80) { tempSlip = 2.0f; }
        if (speedKMH > 110) { tempSlip = 2.2f; }
        else tempSlip = 1.0f;
        //Debug.Log(tempSlip);

        WheelFrictionCurve frictionCurve = collider.sidewaysFriction;
        //frictionCurve.stiffness = a.car.sideStifftness;//whatver you want
        frictionCurve.extremumSlip = tempSlip;
        collider.sidewaysFriction = frictionCurve;

    }


    void Update()
    {
        speedometer();
    }*/


    void Start()
    {
        skidParticles = Instantiate(GameObject.Find("ParticleBurnoutSmoke"));//GetComponentInChildren<ParticleSystem>());
        skidParticles.transform.SetParent(transform.gameObject.GetComponentInParent<Car>().transform);
        if (skidParticles == null)
        {
            Debug.LogWarning(" no particle system found on car to generate smoke particles", gameObject);
        }
        else
        {
            skidParticles.GetComponent<ParticleSystem>().Stop();
        }

        m_WheelCollider = GetComponent<WheelCollider>();
        m_AudioSource = GetComponent<AudioSource>();
        PlayingAudio = false;

        if (skidTrailsDetachedParent == null)
        {
            skidTrailsDetachedParent = new GameObject("Skid Trails - Detached").transform;
        }
        initSkids();
    }

    public void initSkids()
    {
        SkidTrailPrefabPool = new Transform[maxSkidMarks];
        for(int i=0; i < maxSkidMarks; i++)
        {
            SkidTrailPrefabPool[i]= Instantiate(SkidTrailPrefab);
        }
    }
    int currentSkidIndex = 0;
    public Transform getNextSkid()
    {
        if (currentSkidIndex >= maxSkidMarks)
            currentSkidIndex = 0;
        
        return SkidTrailPrefabPool[currentSkidIndex++];
    }

    public void EmitTyreSmoke()
    {
        if (!m_WheelCollider.isGrounded)
        {
            EndSkidTrail();
            return;

        }
        if (skidParticles != null)
        {
            skidParticles.transform.position = transform.position - transform.up * m_WheelCollider.radius;
            skidParticles.GetComponent<ParticleSystem>().Emit(1);
        }
        if (!skidding)
        {
            StartCoroutine(StartSkidTrail());
        }
    }


    public void PlayAudio()
    {
        m_AudioSource.Play();
        PlayingAudio = true;
    }


    public void StopAudio()
    {
        m_AudioSource.Stop();
        PlayingAudio = false;
    }

    WheelHit wheelHit;
    public IEnumerator StartSkidTrail()
    {
        skidding = true;
        m_SkidTrail = getNextSkid();// Instantiate(SkidTrailPrefab);
        m_WheelCollider.GetGroundHit(out wheelHit);
        
        while (m_SkidTrail == null)
        {
            yield return null;
        }
        m_SkidTrail.parent = transform;
        m_SkidTrail.GetComponent<TrailRenderer>().Clear();
      m_SkidTrail.position = wheelHit.point;
     


    }


    public void EndSkidTrail()
    {
        StopAudio();
         if (!skidding)
         {
            // return;
         }
        skidding = false;
        if (m_SkidTrail == null)
            return;
        m_SkidTrail.parent = skidTrailsDetachedParent;
        m_SkidTrail.GetComponent<TrailRenderer>().Clear();
        m_SkidTrail.localPosition = -Vector3.up * 100000;
        //  Destroy(m_SkidTrail.gameObject, 10);
    }
}

