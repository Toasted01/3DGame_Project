using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenuControl : MonoBehaviour
{
    public GameObject main;
    public GameObject settings;
    public AudioMixer AudioMixer;
    Resolution[] resolutions;
    public Dropdown resolutionDropdown;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        int current = 0;
        List<string> options = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                current = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = current;
        resolutionDropdown.RefreshShownValue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void start()
    {
        SceneManager.LoadScene("Main");
    }

    public void settingsButton()
    {
        main.SetActive(false);
        settings.SetActive(true);
    }

    public void mainButton()
    {
        main.SetActive(true);
        settings.SetActive(false);
    }

    public void SetVolume(float volume)
    {
        AudioMixer.SetFloat("Volume", volume);
    }

    public void SetGraphics(int level)
    {
        QualitySettings.SetQualityLevel(level);
    }

    public void setFullscreen(bool full)
    {
        Screen.fullScreen = full;
    }

    public void ApplyResolution(int i)
    {
        Resolution resolution = resolutions[i];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
