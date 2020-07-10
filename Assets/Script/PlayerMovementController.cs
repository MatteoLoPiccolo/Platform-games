using UnityEngine;

[RequireComponent(typeof(CharacterGrounding))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementController : MonoBehaviour, IMove
{
    [SerializeField]
    private float moveSpeed = 2;
    [SerializeField]
    private float jumpForce = 400;

    private new Rigidbody2D rigidbody2D;
    private CharacterGrounding characterGrounding;

    private AudioSource audioSource;

    public float Speed { get; private set; }

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        characterGrounding = GetComponent<CharacterGrounding>();

        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && characterGrounding.IsGrounded)
        {
            rigidbody2D.AddForce(Vector2.up * jumpForce);
            //audioSource.Play();
        }
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        Speed = horizontal;

        Vector3 movement = new Vector3(horizontal, 0);

        transform.position += movement * Time.deltaTime * moveSpeed;
    }

    internal void Bounce()
    {
        rigidbody2D.AddForce(Vector2.up * jumpForce);
    }
}
