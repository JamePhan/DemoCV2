using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : Singleton<SettingsPanel>
{
    public Button btn_settings;
    public Button btn_resume;
    public Button btn_exit;

    public GameObject settings;
    public GameObject settingsPanel;

    private bool isPaused = false;

    protected override void InAwake()
    {
        Time.timeScale = 1f;
        btn_settings.onClick.AddListener(ToggleSettingsPanel);
        btn_resume.onClick.AddListener(ResumeGame);
        btn_exit.onClick.AddListener(ExitGame);

        settingsPanel.SetActive(false);
    }

    public void ToggleSettingsPanel()
    {
        bool isActive = settingsPanel.activeSelf;
        PauseGame();
        settings.SetActive(false);
        settingsPanel.SetActive(!isActive);
    }

    public void PauseGame()
    {
        if (!isPaused)
        {
            Time.timeScale = 0f;
            isPaused = true;
        }
    }

    public void ResumeGame()
    {
        if (isPaused)
        {
            Time.timeScale = 1f;
            isPaused = false;
            settings.SetActive(true);
            settingsPanel.SetActive(false);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
