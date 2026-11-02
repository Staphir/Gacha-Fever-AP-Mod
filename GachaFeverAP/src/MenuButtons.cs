using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GachaFeverAP;

public class MenuButtons
{
    // Chemin de base des boutons
    private const string BaseMenuPath = "Canvas_MainMenu/Graphic_MainMenu/AllButtonMainMenu/";

    public static bool menuLocked;

    private static readonly List<string> buttonNames = new()
    {
        "ButtonPlay",
        "ButtonSuccess",
        "ButtonTalentTree",
        "ButtonCodex"
    };

    // Méthode pour initialiser les références aux boutons
    private static Dictionary<string, GameObject> initializeButtonsDictionary()
    {
        Dictionary<string, GameObject> buttonsDictionnary = new();
        foreach (var buttonName in buttonNames)
        {
            var button = GameObject.Find(BaseMenuPath + buttonName);
            if (button)
                buttonsDictionnary[buttonName] = button;
            else
                Debug.LogError($"Bouton {buttonName} non trouvé.");
        }

        return buttonsDictionnary;
    }

    // Méthode pour verrouiller tous les boutons
    public static void LockAll()
    {
        var buttonReferences = initializeButtonsDictionary();
        foreach (var buttonReference in buttonReferences)
            Debug.Log($"Bouton {buttonReference.Key}, GameObject : {buttonReference.Value}");
        foreach (var button in buttonReferences.Values)
            if (button)
            {
                var buttonComponent = button.GetComponent<Button>();
                if (buttonComponent != null)
                {
                    buttonComponent.interactable = false; // Désactiver l'interactivité du bouton
                    var buttonBackgroundColor = buttonComponent.colors;
                    var newBackgroundColorBlock = new ColorBlock
                    {
                        normalColor = new Color(buttonBackgroundColor.normalColor.r,
                            buttonBackgroundColor.normalColor.g,
                            buttonBackgroundColor.normalColor.b, 0.5f),
                        highlightedColor = buttonBackgroundColor.highlightedColor,
                        pressedColor = buttonBackgroundColor.pressedColor,
                        selectedColor = buttonBackgroundColor.selectedColor,
                        disabledColor = buttonBackgroundColor.disabledColor,
                        colorMultiplier = buttonBackgroundColor.colorMultiplier,
                        fadeDuration = buttonBackgroundColor.fadeDuration
                    };
                    ChangeButtonStyle(buttonComponent, newBackgroundColorBlock);
                }
                else
                {
                    Debug.LogError($"Composant Button manquant sur {button.name}.");
                }
            }

        menuLocked = true;
    }

    // Méthode pour déverrouiller tous les boutons
    public static void UnlockAll()
    {
        var buttonReferences = initializeButtonsDictionary();
        foreach (var buttonReference in buttonReferences)
            Debug.Log($"Bouton {buttonReference.Key}, GameObject : {buttonReference.Value}");
        foreach (var button in buttonReferences.Values)
            if (button)
            {
                var buttonComponent = button.GetComponent<Button>();
                if (buttonComponent != null)
                {
                    buttonComponent.interactable = true; // Activer l'interactivité du bouton
                    var buttonBackgroundColor = buttonComponent.colors;
                    var newBackgroundColorBlock = new ColorBlock
                    {
                        normalColor = new Color(buttonBackgroundColor.normalColor.r,
                            buttonBackgroundColor.normalColor.g,
                            buttonBackgroundColor.normalColor.b, 1f),
                        highlightedColor = buttonBackgroundColor.highlightedColor,
                        pressedColor = buttonBackgroundColor.pressedColor,
                        selectedColor = buttonBackgroundColor.selectedColor,
                        disabledColor = buttonBackgroundColor.disabledColor,
                        colorMultiplier = buttonBackgroundColor.colorMultiplier,
                        fadeDuration = buttonBackgroundColor.fadeDuration
                    };
                    ChangeButtonStyle(buttonComponent, newBackgroundColorBlock);
                }
                else
                {
                    Debug.LogError($"Composant Button manquant sur {button.name}.");
                }
            }

        menuLocked = false;
    }

    // TODO more details changement color
    private static void ChangeButtonStyle(
        Button button,
        // Color textColor,
        // Color outlineColor1,
        // Color outlineColor2,
        // Color outlineColor3,
        ColorBlock backgroundColor
    )
    {
        button.colors = backgroundColor;
    }
}