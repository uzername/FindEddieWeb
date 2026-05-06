using TMPro;
using UnityEngine;

public class StartgameSceneHandler : MonoBehaviour
{
    public TextMeshProUGUI targetTextExplanation;
    public TextMeshProUGUI targetTextButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TranslatorService.SetupTranslations();
        assignTexts();
    }
    void assignTexts()
    {
        if (targetTextExplanation == null || targetTextButton == null) { return; }
        targetTextButton.text = TranslatorService.allTranslations[TranslatorService.currentLanguage][(int)TranslationsID.START];
        targetTextExplanation.text = TranslatorService.allTranslations[TranslatorService.currentLanguage][(int)TranslationsID.INSTRUCTIONS];
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
