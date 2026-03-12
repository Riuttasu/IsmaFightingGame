using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
/// <summary>
/// Component which manages scene and game state changes, such as reseting the scene or leaving the game alltogether
/// </summary>
public class Scenemanager : MonoBehaviour
{
    private InputAction resetScene, escapeScene;
    private void Awake()
    {
        resetScene = InputSystem.actions.FindAction("Reset");
        escapeScene = InputSystem.actions.FindAction("Escape");
    }
    private void Update()
    {
        // ResetScene reloads the current scene
        if (resetScene.WasPressedThisFrame())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (escapeScene.WasPressedThisFrame()) 
        { 
        }
    }
    /// <summary>
    /// Changes a scene
    /// </summary>
    /// <param name="newscene"></param>
    public void ChangeScene(int newscene)
    {
        SceneManager.LoadScene(newscene);
    }
}
