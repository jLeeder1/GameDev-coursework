using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class InGameControllerMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject MenuComponentsRoot;

    [SerializeField]
    private GameObject PausePanel;

    [SerializeField]
    private Button ResumeButton;

    [SerializeField]
    private Button SaveButton;

    [SerializeField]
    private Button QuitToMainMenuButton;

    [SerializeField]
    private Button QuitGameButton;

    [SerializeField]
    private FirstPersonController firstPersonController;

    [SerializeField]
    private GameObject GunObject;

    [SerializeField]
    private Image PlayerCrosshair;

    [SerializeField]
    private SaveGameController SaveGameController;

    private bool isMenuActive;
    private bool areMenuDependenciesFound;
    private int mainMenuSceneIndex;

    void Start()
    {
        isMenuActive = false;
        areMenuDependenciesFound = false;
        mainMenuSceneIndex = 0;
        StartCoroutine(FindGunObjectAfterEntitiesHaveSpawned());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            isMenuActive = !isMenuActive;

        if (isMenuActive && areMenuDependenciesFound)
            PauseGame();

        // Constantly running this isn't very good?
        if (!isMenuActive && areMenuDependenciesFound)
            ResumeGame();

    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        SetActivityOfAllMenuComponents(true);
        Cursor.lockState = CursorLockMode.None;

    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        SetActivityOfAllMenuComponents(false);
        Cursor.lockState = CursorLockMode.Locked;
        isMenuActive = false;
    }

    public void SaveGame()
    {
        SaveGameController.SaveGame();
    }

    public void QuitToMainMenu()
    {
        StartCoroutine(QuitToMainMenuHandler());
    }

    private IEnumerator QuitToMainMenuHandler()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(mainMenuSceneIndex);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    private IEnumerator FindGunObjectAfterEntitiesHaveSpawned()
    {
        while (!areMenuDependenciesFound)
        {
            yield return null;
            GunObject = GameObject.FindGameObjectWithTag("gunObject");
            firstPersonController = GameObject.FindObjectOfType<FirstPersonController>();

            if(GunObject != null && firstPersonController != null)
            {
                PlayerCrosshair = GameObject.FindGameObjectWithTag("crossHair").GetComponent<Image>();
                areMenuDependenciesFound = true;
            }
        }
    }

    public void QuitGame()
    {

    }

    private void SetActivityOfAllMenuComponents(bool isActive)
    {
        MenuComponentsRoot.SetActive(isActive);
        GunObject.SetActive(!isActive);
        firstPersonController.enabled = !isActive;
        PlayerCrosshair.enabled = !isActive;
        Cursor.visible = isActive;
    }
}
