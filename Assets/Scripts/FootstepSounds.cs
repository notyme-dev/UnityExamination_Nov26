using UnityEngine;
using UnityEngine.Audio;

public class FootstepSounds : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public enum FootstepType
    {
        GRASS,
        WOOD
    }

    [SerializeField] public AudioResource leaves;
    [SerializeField] public AudioResource wood;

    private FootstepType currentFootstepType = FootstepType.GRASS;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckGroundType();
    }

    void CheckGroundType()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, LayerMask.GetMask("Ground"));
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Grass"))
            {
                SetFootstepType(FootstepType.GRASS);
            }
            else if (hit.collider.CompareTag("Wood"))
            {
                SetFootstepType(FootstepType.WOOD);
            }
        }
    }

    void SetFootstepType(FootstepType type)
    {
        if (audioSource == null) return;
        currentFootstepType = type;
        if (type == FootstepType.GRASS)
        {
            audioSource.resource = leaves;
        }
        else if (type == FootstepType.WOOD)
        {
            audioSource.resource = wood;
        }
    }
}
