using UnityEngine;
using UnityEngine.UI;

public class EndgameSceneHandler : MonoBehaviour
{
    public Image targetPicture;
    public Sprite PlayerSwimmedAway;
    public Sprite PlayerFellOff;
    public Sprite PlayerFoundEddie;
    public Sprite PlayerFoundGirl;
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
                default:
                    {
                        break;
                    }
            }
        }
    }


}
