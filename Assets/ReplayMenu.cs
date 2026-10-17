using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class ReplayMenu : MonoBehaviour
{
    [SerializeField] private string gameScene = "SampleScene";   // la scène du jeu

    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        Button replay = root.Q<Button>("replay-button");
        replay.clicked += () => SceneManager.LoadScene(gameScene);
    }
}
