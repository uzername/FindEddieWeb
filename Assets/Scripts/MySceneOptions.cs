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
    EN=0, UA=1, RU=2, NL=3, SK=4, LAST
}
public enum TranslationsID
{
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
    /// <summary>
    /// here are static translations - for each known language and ID I set up a translation
    /// </summary>
    public static Dictionary<KnownLanguages, string[]> allTranslations = new Dictionary<KnownLanguages, string[]>();
    public static void SetupTranslations() { 
        if (allTranslations.Count==0)
        {
            for (int i = 0; i< (int) KnownLanguages.LAST; i++)
            {
                allTranslations[(KnownLanguages)i] = new string[(int)TranslationsID.LAST];
            }
        }
    }

    }

