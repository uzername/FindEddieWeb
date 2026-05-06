using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    public enum GameEndings
{
    FoundCharacter =0, LeftGate=1, Swimming =2, Falling=3
}
public enum KnownLanguages
{
    EN=0, UA=1, /*RU=2, NL=3, SK=4,*/ LAST
}
public enum TranslationsID
{
     LEFTMAP, SWIMMING, FOUND_CHARACTER, EXITED, RESTART, 
     INSTRUCTIONS, START,
     LAST
}
/// <summary>
/// used to store info about endings when unloading TerrainScene and loading EndGameScene
/// </summary>
    public class MySceneOptions
    {    
    public static GameEndings endingOption;
    public static string characterName;
    }

    public class TranslatorService
    {
    public static KnownLanguages currentLanguage = KnownLanguages.EN;
    /// <summary>
    /// here are static translations - for each known language and ID I set up a translation
    /// </summary>
    public static Dictionary<KnownLanguages, string[]> allTranslations = new Dictionary<KnownLanguages, string[]>();
    public static void SetupTranslations() { 
        if (allTranslations.Count==0)
        {
            // init languages
            for (int i = 0; i< (int) KnownLanguages.LAST; i++)
            {
                allTranslations[(KnownLanguages)i] = new string[(int)TranslationsID.LAST];
            }
            allTranslations[KnownLanguages.EN][(int)TranslationsID.LEFTMAP] = "Left area bounds, \n girl feels abandoned now";
            allTranslations[KnownLanguages.EN][(int)TranslationsID.SWIMMING] = "Fell into water, \n girl stands on a shore";
            allTranslations[KnownLanguages.EN][(int)TranslationsID.FOUND_CHARACTER] = "You have found ";
            allTranslations[KnownLanguages.EN][(int)TranslationsID.EXITED] = "Reached Town Exit, \n girl feels lonely";
            allTranslations[KnownLanguages.EN][(int)TranslationsID.RESTART] = "RESTART";
            allTranslations[KnownLanguages.EN][(int)TranslationsID.INSTRUCTIONS] = "There is a pink-haired girl walking around town. \n Try to find her. \n Or just explore around if you wish";
            allTranslations[KnownLanguages.EN][(int)TranslationsID.START] = "START";

            allTranslations[KnownLanguages.UA][(int)TranslationsID.LEFTMAP] = "Покинули карту \n і облишили дівчину";
            allTranslations[KnownLanguages.UA][(int)TranslationsID.SWIMMING] = "Впали у воду \n і дівчина стоїть на березі";
            allTranslations[KnownLanguages.UA][(int)TranslationsID.FOUND_CHARACTER] = "Ви зустріли ";
            allTranslations[KnownLanguages.UA][(int)TranslationsID.EXITED] = "Вийшли за межі міста \n і дівчина почувається покинутою";
            allTranslations[KnownLanguages.UA][(int)TranslationsID.RESTART] = "ЩЕ РАЗ";
            allTranslations[KnownLanguages.UA][(int)TranslationsID.INSTRUCTIONS] = "В місті гуляє дівчина з рожевим волоссям. \n Спробуйте знайти її \n Або просто подосліджуйте якщо хочете";
            allTranslations[KnownLanguages.UA][(int)TranslationsID.START] = "Розпочати";
        }
    }

    }

