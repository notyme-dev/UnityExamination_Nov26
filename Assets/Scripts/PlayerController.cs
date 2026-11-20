using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;
using UnityEngine.Animations;
using UnityEngine.Rendering.Universal;

public class PlayerControllerNew : MonoBehaviour
{
    // These variables hold the key-bind reference. They are then to be initialized in void Start.
    InputAction moveAction;
    InputAction jumpAction;
    InputAction interactAction;
    // Serialized referenses.
    [Header("References")]
    [SerializeField] InputActionAsset inputActionAsset;
    [SerializeField] private Transform avatarRoot;
    [SerializeField] private Animator animator;

    [Header("Ray points")]
    [SerializeField] private GameObject foot;
    // Customizable settings.
    [Header("Settings")]
    [SerializeField] private float maxSpeed = 7f;
    [SerializeField] private float jumpForce = 20f;
    [SerializeField] private float gravityMultiplier = 6f;
    [SerializeField] private float terminal_y = 50f;
    [SerializeField] private float gravityMultiplierWall = 1f;
    [SerializeField] private float jump_hold_deacceleration = 7f;
    [SerializeField] private float jump_deacceleration = 30f;
    [SerializeField] private float falling_anim_delay = 0.01f;

    private Vector2 velocity;
    private Vector2 moveInput;
    private PlayerState currentPlayerState = PlayerState.FALLING;
    private PlayerState previousPlayerState = PlayerState.GROUNDED;
    private Rigidbody2D rb;
    private float ground_ray_dist = 1f;
    private Vector2 current_grounded_forward = Vector2.right;
    private float ground_hit_distance = 0.25f;
    private float started_falling = 0.0f;
    private bool jumped = false;
    private float coyote = 0.1f;
    private bool has_coyoted = false;

    
    enum PlayerState
    {
        GROUNDED,
        FALLING
    }   
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (inputActionAsset != null)
        {
            inputActionAsset.Enable();
            moveAction = inputActionAsset.FindAction("Move");
            jumpAction = inputActionAsset.FindAction("Jump");
            interactAction = inputActionAsset.FindAction("Interact");
            if (moveAction != null) moveAction.Enable();
            if (jumpAction != null) jumpAction.Enable();
        }
        else
        {
            Debug.LogWarning("InputActionAsset not assigned on " + gameObject.name);
        }

        rb = GetComponent<Rigidbody2D>();
        currentPlayerState = PlayerState.FALLING;
    }

    private RaycastHit2D CheckGrounded()
    {
        RaycastHit2D hit;
        int numrays = 5;
        Vector2 start_pos = new Vector2(foot.transform.position.x - 0.3f, foot.transform.position.y);
        start_pos = new Vector2(foot.transform.position.x, foot.transform.position.y) - current_grounded_forward * 0.3f;
        hit = Physics2D.Raycast(start_pos, -Vector2.Perpendicular(current_grounded_forward), ground_ray_dist, LayerMask.GetMask("Ground"));
        
        for(int i = 1; i < numrays; i++)
        {
            // weird early exit because we begin check before loop starts T_T STUPID BRAIN IDIOT THING
            if (hit.collider != null && hit.distance < ground_hit_distance) break;
            Vector2 ray_origin = new Vector2(foot.transform.position.x, foot.transform.position.y) - current_grounded_forward * 0.3f + current_grounded_forward * 0.5f / (numrays - 1) * i;
            Debug.DrawRay(ray_origin, -Vector2.Perpendicular(current_grounded_forward) * ground_ray_dist, Color.red);
            hit = Physics2D.Raycast(ray_origin, -Vector2.Perpendicular(current_grounded_forward), ground_ray_dist, LayerMask.GetMask("Ground"));
        }
        Vector2 normal = hit.normal;
        if (hit && -Vector2.Perpendicular(normal).y < 0.4f){ 
            current_grounded_forward = -Vector2.Perpendicular(normal);
        } else
        {
            current_grounded_forward = Vector2.right;
        }
        Debug.DrawRay(foot.transform.position, current_grounded_forward * 0.5f, Color.green);
        //Debug.Log("Grounded forward: " + current_grounded_forward.ToString());
        return hit;
    }


    void ChangeState(PlayerState newState)
    {
        previousPlayerState = currentPlayerState;
        currentPlayerState = newState;

        if (newState == PlayerState.FALLING)
        {
            started_falling = Time.time;
            AudioSource audioSource = GetComponent<AudioSource>();
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
        if (newState == PlayerState.GROUNDED)
        {
            has_coyoted = false;
        }
    }

    void UpdateGroundedState()
    {
        velocity.x = moveInput.x * maxSpeed;

       if (jumpAction.triggered)
        {
            velocity.y = jumpForce;
            jumped = true;
            ChangeState(PlayerState.FALLING);
            has_coyoted = true;
        }

        if (interactAction.triggered)
        {
           // sda
        }

        RaycastHit2D hit = CheckGrounded();
        if(hit.collider) transform.position = new Vector2(transform.position.x, Vector2.Distance(transform.position, foot.transform.position) + hit.point.y); // Adjust the player's position to be on top of the ground
        if ((hit.collider == null) || hit.distance > ground_hit_distance)
        {
            ChangeState(PlayerState.FALLING);
        }

        AudioSource audioSource = GetComponent<AudioSource>();
        if (Mathf.Abs(rb.linearVelocityX) > 0.1)
        {
            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }

    private void UpdateFallingState()
    { 
        velocity.x = moveInput.x * maxSpeed;
        if (jumpAction.triggered && started_falling + coyote > Time.time && previousPlayerState == PlayerState.GROUNDED && !has_coyoted)
        {
            velocity.y = jumpForce;
            jumped = true;
            has_coyoted = true;
        }
        // Check for ground
        RaycastHit2D hit = CheckGrounded();
        //Debug.Log(hit.distance);
        
        if (velocity.y <= 0 && hit.collider != null && hit.distance < ground_hit_distance)
        {
            ChangeState(PlayerState.GROUNDED);
            velocity.y = 0.0f;
            transform.position = new Vector2(transform.position.x, Vector2.Distance(transform.position, foot.transform.position) + hit.point.y); // Adjust the player's position to be on top of the ground
        }

        if (velocity.y <= -terminal_y && rb.linearVelocityY >= -0.1f){
            velocity.y = 0f;
            ChangeState(PlayerState.GROUNDED);
        }
    }

    private void SetAnimationTrigger(string triggerName)
    {
        if (animator != null)
        {
            animator.SetTrigger(triggerName);
        }
    }

    private void UpdateAnimationState() {
        if (animator == null) return;
        animator.SetBool("running", Mathf.Abs(moveInput.x) > 0.01f);
        animator.SetBool("falling", currentPlayerState == PlayerState.FALLING && ((started_falling + falling_anim_delay < Time.time) || jumped));
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = moveAction != null ? moveAction.ReadValue<Vector2>().normalized : Vector2.zero;
        if (currentPlayerState == PlayerState.GROUNDED)
        {
            UpdateGroundedState();
        }
        else if (currentPlayerState == PlayerState.FALLING)
        {
            UpdateFallingState();
        }

        if (avatarRoot)
        {
            if (moveInput.x > 0.01f)
            {
                avatarRoot.transform.localScale = new Vector3(1, 1, 1);
            }
            else if (moveInput.x < -0.01f)
            {
                avatarRoot.transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        UpdateAnimationState();
        jumped = false;

        GlobalPlayerInfo.GetInstance().position = transform.position;
    }

    void FixedUpdate() 
    {
        if (currentPlayerState == PlayerState.GROUNDED && velocity.y < 0.0f)
        {
            velocity.y = 1.0f;
        }
        if (currentPlayerState == PlayerState.FALLING)
        {
            velocity.y += Physics2D.gravity.y * gravityMultiplier * Time.deltaTime;
        } 
        Mathf.Clamp(velocity.y, -terminal_y, terminal_y);
        if (rb != null)
        {
            rb.linearVelocity = velocity;
        }
    }
}
