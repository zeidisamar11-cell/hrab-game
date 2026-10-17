using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Slider volumeSlider;

    private void Start()
    {
        settingsPanel.SetActive(false);
        volumeSlider.value = AudioListener.volume;
        volumeSlider.onValueChanged.AddListener(v => AudioListener.volume = v);
    }

    public void Play()
    {
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
        SceneManager.LoadScene("SampleScene");
    }
    public void OpenSettings() => settingsPanel.SetActive(true);
    public void CloseSettings() => settingsPanel.SetActive(false);

      public void Exit()
    {
        Debug.Log("Quitter le jeu");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}