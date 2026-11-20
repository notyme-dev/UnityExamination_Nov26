using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class BooController : MonoBehaviour
{
    [SerializeField] InputActionAsset inputActionAsset;
    [SerializeField] GameObject avatar;
    [SerializeField] float moveSpeed = 4f;
    [SerializeField] float maxSpeed = 3f;
    private InputAction moveAction;
    private InputAction MenuAction;
    private Animator animator;
    private Vector3 og_scale;

    private Rigidbody2D rb;
    private float targetAngle;

    public bool hasKey = false;

    public static Vector2 rotate(Vector2 v, float delta) {
        return new Vector2(
            v.x * Mathf.Cos(delta) - v.y * Mathf.Sin(delta),
            v.x * Mathf.Sin(delta) + v.y * Mathf.Cos(delta)
    );
}
    void Start()
    {
        if (inputActionAsset != null)
        {
            inputActionAsset.Enable();
            MenuAction = inputActionAsset.FindAction("Menu");
            moveAction = inputActionAsset.FindAction("Move");
            if (moveAction != null) moveAction.Enable();
            //if (jumpAction != null) jumpAction.Enable();
        }
        rb = GetComponent<Rigidbody2D>();

        if(avatar == null) return;
        animator = avatar.GetComponent<Animator>();
        og_scale = avatar.transform.localScale;
    }   

    // Update is called once per frame
    void Update()
    {
        if (rb == null) return;

        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        
        Vector2 rotated = moveInput;//rotate(moveInput, (torso.rotation.eulerAngles.z + 90f) * Mathf.Deg2Rad);
        rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity + rotated * moveSpeed * Time.deltaTime, maxSpeed);
        rb.linearVelocity = Vector2.MoveTowards(rb.linearVelocity, Vector2.zero, moveSpeed * 0.5f * Time.deltaTime);

        if (avatar == null) return;

        if (rb.linearVelocity.x < -0.2f)
        {
            Vector3 scale = og_scale;
            scale.x *= -1f;
            avatar.transform.localScale = scale;
        } 
        else if (rb.linearVelocity.x > 0.2f && avatar.transform.localScale.x < 0f)
        {
            avatar.transform.localScale = og_scale;
        }

        if (MenuAction.triggered)
        {
            SceneManager.LoadScene(2);
        }

        if (animator == null) return;
        animator.SetFloat("velocityX", Mathf.Abs(rb.linearVelocity.x / maxSpeed));
        
        
    }
}
