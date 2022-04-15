using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;
using System;

public class UserInterfaceController : MonoBehaviour
{
    private enum MenuState
    {
        Settings
    }

    private MenuState _currentMenuState;

    private VisualElement _root;
    private VisualElement _menu;
    private VisualElement _mainMenuOptions;
    private VisualElement _settingsMenuOptions;

    private bool expanded = true;
    private VisualElement _settingsButton;
    private VisualElement _cancelButton;

    private bool _navTransitionActive;

    private const string SETTINGS_STYLE = "menu-settings";

    private void Start()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;
        _menu = _root.Q<VisualElement>("Menu");

        _mainMenuOptions = _menu.Q<VisualElement>("MenuContent");
        _settingsMenuOptions = _menu.Q<VisualElement>("SettingsContent");

        _settingsButton = _mainMenuOptions.Children().Cast<Label>().FirstOrDefault(x => x.text.ToUpper().Equals("SETTINGS"));
        _cancelButton = _settingsMenuOptions.Children().Last();

        _settingsButton.RegisterCallback<MouseDownEvent>((evt) => 
        {
            //Cannot interact with the settings button during a transition
            if (_navTransitionActive)
            {
                return;
            }

            Debug.Log("Settings Clicked");

            _menu.EnableInClassList(SETTINGS_STYLE, true);
        });

        _cancelButton.RegisterCallback<MouseDownEvent>((evt) =>
        {
            //Cannot interact with the settings button during a transition
            if (_navTransitionActive)
            {
                return;
            }

            Debug.Log("Settings Clicked");

            _menu.EnableInClassList(SETTINGS_STYLE, false);
        });
    }


    // Start is called before the first frame update
    [ContextMenu("Animate")]
    public void AnimateTest()
    {
        _menu.EnableInClassList("menu-expanded", !expanded);
        _menu.EnableInClassList("menu-collapsed", expanded);

        expanded = !expanded;
    }
}
