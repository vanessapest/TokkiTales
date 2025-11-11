using UnityEngine;
using UnityEngine.SceneManagement;

// steuert das Hauptmenu des Spiels
public class Menu : MonoBehaviour
{
    public void PlayGame() // erste Level Szene wird geladen
    {
        SceneManager.LoadSceneAsync($"1-1"); 
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
