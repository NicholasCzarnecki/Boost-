using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource rocketSound;
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationSpeed = 50f;
    [SerializeField] AudioClip thruster;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rocketSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust() {
        // Input.GetKey("Space")
        if (Input.GetKey(KeyCode.Space)){
            if (!rocketSound.isPlaying)
            {
                rocketSound.PlayOneShot(thruster);
            }
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);   
        }
        else
        {
             rocketSound.Stop();
        }
    }

    void ProcessRotation(){
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationSpeed);
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation so that we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing rotation so that physics system can take over
    }
}
