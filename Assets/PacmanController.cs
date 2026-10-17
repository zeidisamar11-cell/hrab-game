using UnityEngine;
using UnityEngine.InputSystem;

public class PacmanController : MonoBehaviour
{
    public float speed = 5f;               // vitesse de Pacman
    public AudioClip coinSound;            // son quand Pacman mange une pièce
    public AudioClip teleportSound;        // son de téléportation

    private InputAction moveAction;        // l'action "Move" (flèches / ZQSD)
    private Rigidbody rb;
    private AudioSource audioSource;
    private Vector2 moveInput;
    private Vector3 startPosition;

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        startPosition = transform.position;
    }

    void Update()
    {
        // Lecture des entrées : à chaque frame
        moveInput = moveAction.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        // Physique : à intervalle fixe
        rb.linearVelocity = new Vector3(moveInput.x, 0f, moveInput.y) * speed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);   // cachée, pas détruite : elle peut revenir
            audioSource.pitch = Random.Range(0.95f, 1.05f);
            audioSource.PlayOneShot(coinSound, 0.4f);
            GameManager.Instance.CoinCollected();
        }
        else if (other.CompareTag("Enemy"))
        {
            GameManager.Instance.PacmanHit();
        }
    }

    // Retour à la position de départ (après une vie perdue)
    public void ResetPosition()
    {
        rb.linearVelocity = Vector3.zero;
        rb.position = startPosition;
        transform.position = startPosition;
    }

    public void PlayTeleportSound()
    {
        audioSource.pitch = 1f;
        audioSource.PlayOneShot(teleportSound, 0.7f);
    }
}