using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private PacmanController pacman;
    [SerializeField] private EnemyChase[] enemies;
    [SerializeField] private UIDocument hud;
    [SerializeField] private int maxLives = 3;
    [SerializeField] private float hitCooldown = 1f;   // évite de perdre 2 vies d'un coup

    private int score;
    private int lives;
    private GameObject[] coins;
    private Label scoreLabel;
    private VisualElement[] lifeIcons;
    private float lastHitTime = -10f;

    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        if (Keyboard.current == null) return;

        if (Keyboard.current.escapeKey.wasPressedThisFrame || Keyboard.current.mKey.wasPressedThisFrame)
        {
            Debug.Log("Retour au menu");
            SceneManager.LoadScene("MainMenu");
        }
    }
    private IEnumerator Start()
    {
        lives = maxLives;

        // Récupérer les éléments du HUD par leur name (UXML)
        VisualElement root = hud.rootVisualElement;
        scoreLabel = root.Q<Label>("score-value");
        lifeIcons = new[] { root.Q("life-1"), root.Q("life-2"), root.Q("life-3") };
        UpdateHUD();

        // Attendre une frame : CoinSpawner crée les pièces dans son Start()
        yield return null;
        coins = GameObject.FindGameObjectsWithTag("Coin");
    }

    // Appelé par Pacman quand il touche une pièce
    public void CoinCollected()
    {
        score++;
        Debug.Log("Score : " + score);
        UpdateHUD();

        if (coins == null) return;
        foreach (GameObject coin in coins)
        {
            if (coin.activeSelf) return;   // il reste au moins une pièce
        }
        SceneManager.LoadScene("GameWin"); // toutes les pièces collectées
    }

    // Appelé par Pacman quand un ennemi le touche
    public void PacmanHit()
    {
        if (Time.time - lastHitTime < hitCooldown) return;
        lastHitTime = Time.time;

        lives--;
        UpdateHUD();

        if (lives <= 0)
        {
            SceneManager.LoadScene("GameOver");
            return;
        }
        ResetRound();
    }

    // Les pièces reviennent, Pacman et les ennemis retournent à leur départ
    private void ResetRound()
    {
        foreach (GameObject coin in coins) coin.SetActive(true);
        score = 0;
        UpdateHUD();

        pacman.ResetPosition();
        foreach (EnemyChase enemy in enemies) enemy.ResetPosition();
    }

    private void UpdateHUD()
    {
        scoreLabel.text = score.ToString();
        for (int i = 0; i < lifeIcons.Length; i++)
        {
            lifeIcons[i].style.visibility = i < lives ? Visibility.Visible : Visibility.Hidden;
        }
    }
}
