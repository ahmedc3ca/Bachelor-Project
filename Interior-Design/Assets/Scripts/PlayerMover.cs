using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerMover : NetworkBehaviour
{
    // Fields for sensitivity to mouse and keyboard input
    [SerializeField] private float translateSpeed = 3f; //Serializable to see it in Unity Inspector
    [SerializeField] private float rotationSpeed = 8f; //Serializable to see it in Unity Inspector
    
    // Get player camera
    [SerializeField] private Camera cam;

    // Get player animator and rigidbody
    [SerializeField] private Animator m_animator = null;
    [SerializeField] private Rigidbody m_rigidBody = null;


    private float m_currentV = 0;

    private readonly float m_interpolation = 10;
    private readonly float m_walkScale = 0.33f;

    public bool gameIsPaused = false;
    private bool m_isGrounded;

    private List<Collider> m_collisions = new List<Collider>();

    private void Awake()
    {
        if(isLocalPlayer){
            if (!m_animator) { gameObject.GetComponent<Animator>(); }
            if (!m_rigidBody) { gameObject.GetComponent<Animator>(); }
        }  
    }

    // Executed when player collides with another collider for first time
    private void OnCollisionEnter(Collision collision)
    {
        // Get all contact points
        if(isLocalPlayer){
            ContactPoint[] contactPoints = collision.contacts;
            for (int i = 0; i < contactPoints.Length; i++)
            {
                if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
                {
                    if (!m_collisions.Contains(collision.collider))
                    {
                        m_collisions.Add(collision.collider);
                    }
                    m_isGrounded = true;
                }
            }
        }
        
    }

    // Executed when player collides with same collider for multiples times
    private void OnCollisionStay(Collision collision)
    {
        // Keep only normal surfaces (grounds)
        if(isLocalPlayer){
            ContactPoint[] contactPoints = collision.contacts;
            bool validSurfaceNormal = false;
            for (int i = 0; i < contactPoints.Length; i++)
            {
                if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
                {
                    validSurfaceNormal = true; break;
                }
            }

            if (validSurfaceNormal)
            {
                m_isGrounded = true;
                if (!m_collisions.Contains(collision.collider))
                {
                    m_collisions.Add(collision.collider); // List of all collider with collision on player
                }
            }
            else
            {
                if (m_collisions.Contains(collision.collider))
                {
                    m_collisions.Remove(collision.collider);
                }
                if (m_collisions.Count == 0) { m_isGrounded = false; } // If no normal surfaces -> we are not on the ground
            }
        }
        
    }

    // Executed when player stops colliding with another collider
    private void OnCollisionExit(Collision collision)
    {
        // Remove collider we "leave"
        if(isLocalPlayer){
            if (m_collisions.Contains(collision.collider))
            {
                m_collisions.Remove(collision.collider);
            }
            if (m_collisions.Count == 0) { m_isGrounded = false; }
        }
    }

    private void Update()
    {
        if(isLocalPlayer){
            m_animator.SetBool("Grounded", m_isGrounded); // Update animator according to collisions
            Movements(); // Get inputs from mouse and keyboard to move around
        }
        else
            cam.enabled = false; // Disable camera if not local player
    }

    // Compute speed + rotation of character according to keyboard and mouse input
    private void Movements()
    {
        if (isLocalPlayer && !gameIsPaused)
        {
            // Get input from keyboard and mouse
            float horizontalT = Input.GetAxis("Horizontal");
            float verticalT = Input.GetAxis("Vertical");
            float horizontalR = Input.GetAxis("Mouse X");
            float verticalR = Input.GetAxis("Mouse Y");

            // Create vector from input (decompose horizontal to movement of complete player and vertical to movement of camera only)
            Vector3 rotationCharacter = new Vector3(0f, horizontalR, 0f);
            Vector3 rotationCamera = new Vector3(-verticalR, 0f, 0f); 
            
            // Change position of character
            transform.position += transform.Find("Camera").forward * verticalT * Time.deltaTime * translateSpeed; 
            transform.position += transform.Find("Camera").right * horizontalT * Time.deltaTime * translateSpeed;

            // Change orientation of camera and player
            transform.Find("Camera").Rotate(rotationCamera * rotationSpeed); 
            transform.Rotate(rotationCharacter * rotationSpeed);

            // Constraint vertical movements of camera 
            Quaternion currentRot = transform.Find("Camera").rotation;
            if (currentRot.eulerAngles.x > 55 && currentRot.eulerAngles.x < 100)
                currentRot.eulerAngles = new Vector3(55, currentRot.eulerAngles.y, 0);
            else if (currentRot.eulerAngles.x < 305 && currentRot.eulerAngles.x > 260)
                currentRot.eulerAngles = new Vector3(315, currentRot.eulerAngles.y, 0);
            else
                currentRot.eulerAngles = new Vector3(currentRot.eulerAngles.x, currentRot.eulerAngles.y, 0);

            transform.Find("Camera").rotation = currentRot; // Apply constraints to camera
            
            // Compute speed and update animator
            m_currentV = Mathf.Lerp(m_currentV, verticalT *= m_walkScale, Time.deltaTime * m_interpolation);
            m_animator.SetFloat("MoveSpeed", m_currentV);
        }
    }
}
