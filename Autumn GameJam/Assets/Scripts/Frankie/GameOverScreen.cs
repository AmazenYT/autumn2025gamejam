using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;
public class GameOverScreen : MonoBehaviour
{
   public TextMeshProUGUI scoreText;

   public void Setup(int score)
   {
        gameObject.SetActive(true);
        scoreText.text = score.ToString() + " Enemies Purified";
      Cursor.lockState = CursorLockMode.None;
      Cursor.visible = true;
   }

   public void RestartButton()
   {
        SceneManager.LoadScene("FrankieScene");

   }

   public void QuitButton()
   {
        Application.Quit();
   }
}
