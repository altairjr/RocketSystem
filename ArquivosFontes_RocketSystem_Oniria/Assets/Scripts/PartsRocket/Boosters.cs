using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boosters : MonoBehaviour
{
    [Header("Info")]
    public string type_;
    public string name_;
    public string desciption_;

    [Header("TWR and FUEL")]
    public float thrust_;
    public float fuel_;

    [Header("Check State")]
    public bool disabled_;

    [Header("Particles System and Sound")]
    [SerializeField] private ParticleSystem particle_;
    [SerializeField] private AudioSource audio_;

    // Burn
    private float burningTime_ = 0f;
    private float efficiency_ = 1.5f;

    // Physics
    private float friction_ = 8f;
    private float frictionTime_ = 0f;
    private float rotX_;
    private float rotY_;
    private float rotZ_;

    // Random
    private float timeRand_ = 0f;

    // Components
    private Rocket rocket_;
    private WindForce windForce_;
    private Rigidbody rigidbody_;

    private void Start()
    {
        SetConfig();
    }

    private void Update()
    {
        CheckState();
    }

    private void SetConfig()
    {
        rocket_ = GameObject.FindGameObjectWithTag("Rocket").GetComponent<Rocket>();
        windForce_ = GameObject.FindGameObjectWithTag("GameManager").GetComponent<WindForce>();
        gameObject.name = type_;
    }

    public float Burn()
    {
        burningTime_ += Time.deltaTime;
        if (burningTime_ >= efficiency_)
        {
            burningTime_ = 0f;
            fuel_--;
        }

        if (fuel_ <= 0)
            fuel_ = 0;

        if (!audio_.isPlaying)
            audio_.Play();

        if (!particle_.isPlaying)
            particle_.Play();

        return (fuel_);
    }

    private void CheckState()
    {
        if (disabled_)
        {
            audio_.Stop();
            particle_.Stop();

            if (rocket_.stages_.Count > 1)
            {
                transform.parent = null;
                if (gameObject.GetComponent<Rigidbody>() == null)
                    gameObject.AddComponent<Rigidbody>();

                if (gameObject.GetComponent<Rigidbody>() != null)
                    rigidbody_ = GetComponent<Rigidbody>();

                rigidbody_.drag = 1f;
                RangeRot();
                Physics();
            }
        }
    }

    private void RangeRot()
    {
        timeRand_ += Time.deltaTime;
        if(timeRand_ >= 1.5f)
        {
            timeRand_ = 0f;
            rotX_ = Random.Range(0, 360);
            rotY_ = Random.Range(0, 360);
            rotZ_ = Random.Range(0, 360);
        }
    }

    private void Physics()
    {
        frictionTime_ += Time.deltaTime;
        if(frictionTime_ >= 1f)
        {
            frictionTime_ = 0f;
            friction_ -= 1.2f;
        }

        if (friction_ <= 0.5f)
            friction_ = 0.5f;

        rigidbody_.drag = friction_;
        rigidbody_.AddForce(new Vector3(windForce_.windX_, 0, windForce_.windZ_), ForceMode.Acceleration);
        rigidbody_.MoveRotation(Quaternion.Lerp(gameObject.transform.rotation, Quaternion.Euler(rotX_, rotY_, rotZ_), 1f * Time.deltaTime));
    }
}
