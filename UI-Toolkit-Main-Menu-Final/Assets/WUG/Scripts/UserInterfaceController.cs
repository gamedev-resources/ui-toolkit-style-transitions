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
        Collapsed,
        Expanded
    }

    private MenuState _currentMenuState;

    private VisualElement _root;
    private VisualElement _menu;

    private bool expanded = true;
    private VisualElement _settingsButton;
    private VisualElement _cancelButton;

    private bool _navTransitionActive;

    private const string EXPANDED = "menu-expanded";
    private const string COLLAPSED = "menu-collapsed";

    private void Start()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;
        _menu = _root.Q<VisualElement>("Menu");

        var mainOptions = _menu.Q<VisualElement>("MenuContent").Children().ToList();

        _settingsButton = mainOptions[mainOptions.Count - 2];
        _cancelButton = _menu.Q<VisualElement>("SettingsContent").Children().Last();

        _menu.RegisterCallback<TransitionEndEvent>((evt) => 
        {
            Debug.Log("Transition End Fired");
            if (_currentMenuState == MenuState.Collapsed)
            {
               _menu.EnableInClassList(COLLAPSED, false);

                _currentMenuState = MenuState.Expanded;
            }
            else
            {
                _navTransitionActive = false;
            }

        });
        
        
        _settingsButton.RegisterCallback<MouseDownEvent>((evt) => 
        {
            //Cannot interact with the settings button during a transition
            if (_navTransitionActive)
            {
                return;
            }

            Debug.Log("Settings Clicked");

            _currentMenuState = MenuState.Collapsed;
            _navTransitionActive = true;

            _menu.EnableInClassList(COLLAPSED, true);
        });

        _cancelButton.RegisterCallback<MouseDownEvent>((evt) =>
        {
            //Cannot interact with the settings button during a transition
            if (_navTransitionActive)
            {
                return;
            }

            Debug.Log("Settings Clicked");

            _currentMenuState = MenuState.Collapsed;
            _navTransitionActive = true;

            _menu.EnableInClassList(COLLAPSED, true);
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
