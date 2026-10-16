using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class PacmanController : MonoBehaviour
{
    public float speed = 5f;          // vitesse de Pacman

    private InputAction moveAction;   // l'action "Move" (flèches / ZQSD)
    private Rigidbody rb;
    private Vector2 moveInput;
    private int score = 0;
    public AudioClip coinSound;       // son quand Pacman mange une pièce
    public AudioClip teleportSound;   // son de téléportation
    private AudioSource audioSource;
        private bool gameOver = false;    // vrai quand toutes les pièces sont mangées
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody>();
            audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Lecture des entrées : à chaque frame
        moveInput = moveAction.ReadValue<Vector2>();
            // Rejouer avec la touche R après le Game Over
        if (gameOver && Keyboard.current.rKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    
    }

    void FixedUpdate()

    {
                if (gameOver) { rb.linearVelocity = Vector3.zero; return; }   // Pacman s'arrête
        // Physique : à intervalle fixe
        Vector3 velocity = new Vector3(moveInput.x, 0f, moveInput.y) * speed;
        rb.linearVelocity = velocity;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            score++;
            Debug.Log("Score : " + score);
            audioSource.pitch = Random.Range(0.95f, 1.05f);  // petite variation
            audioSource.PlayOneShot(coinSound, 0.4f);        // volume 40 %
                        // La pièce détruite compte encore pendant cette frame, d'où "<= 1"
            if (GameObject.FindGameObjectsWithTag("Coin").Length <= 1)
            {
                gameOver = true;
                Debug.Log("GAME OVER - Score final : " + score);
            }
        }
    }
        public void PlayTeleportSound()
    {
        audioSource.pitch = 1f;
        audioSource.PlayOneShot(teleportSound, 0.7f);    // volume 70 %
    }
        void OnGUI()
    {
        if (!gameOver) return;

        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.fontSize = 60;
        style.fontStyle = FontStyle.Bold;
        style.alignment = TextAnchor.MiddleCenter;
        style.normal.textColor = Color.yellow;

        GUI.Label(new Rect(0, 0, Screen.width, Screen.height),
                  "GAME OVER\nScore : " + score + "\n\nAppuie sur R pour rejouer", style);
    }
}