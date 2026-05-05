using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndgameSceneHandler : MonoBehaviour
{
    public Image targetPicture;
    public Sprite PlayerSwimmedAway;
    public Sprite PlayerFellOff;
    public Sprite PlayerFoundEddie;
    public Sprite PlayerFoundGirl;
    public Sprite PlayerLeft;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (targetPicture != null)
        {
            switch (MySceneOptions.endingOption)
            {
                case GameEndings.FoundCharacter:
                    {
                        if (MySceneOptions.characterName == "Eddie")
                        {
                            targetPicture.sprite = PlayerFoundEddie;
                        } else
                        {
                            targetPicture.sprite = PlayerFoundGirl;
                        }
                            break;
                    }
                case GameEndings.Swimming:
                    {
                        targetPicture.sprite = PlayerSwimmedAway;
                        break;
                    }
                case GameEndings.Falling:
                    {
                        targetPicture.sprite = PlayerFellOff;
                        break;
                    }
                case GameEndings.LeftGate:
                    {
                        targetPicture.sprite = PlayerLeft;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
    }

    public void OnGetBackButtonClick()
    {
        SceneManager.LoadScene(0);
    }

}
