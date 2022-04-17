using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;
using System;

public class UserInterfaceController : MonoBehaviour
{
    private VisualElement _menu;
    private List<VisualElement> _mainMenuOptions;
    private VisualElement _settingsMenuOptions;

    private const string POPUP_ANIMATION = "pop-animation-hide";
    private int _mainPopupIndex = -1;

    public bool _navTransitionActive;
    public bool _firstLoadDone = false;

    private void Awake()
    {
        _menu = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("Menu");

        _mainMenuOptions = _menu.Q<VisualElement>("MainNav").Children().ToList();
        _settingsMenuOptions = _menu.Q<VisualElement>("SettingsNav");

        //var settingsButton = _mainMenuOptions.Cast<Label>().FirstOrDefault(x => x.text.ToUpper().Equals("SETTINGS"));
        //var cancelButton = _settingsMenuOptions.Children().Last();

        _menu.RegisterCallback<TransitionEndEvent>(Menu_TransitionEnd);

        //settingsButton.RegisterCallback<MouseDownEvent>(Button_OnClick);
        //cancelButton.RegisterCallback<MouseDownEvent>(Button_OnClick);
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);

        _menu.ToggleInClassList(POPUP_ANIMATION);
    }

    private void Menu_TransitionEnd(TransitionEndEvent evt)
    {
        if (!evt.stylePropertyNames.Contains("opacity") || _mainPopupIndex == _mainMenuOptions.Count - 1) { return; }

        _mainPopupIndex++;

        _mainMenuOptions[_mainPopupIndex].ToggleInClassList(POPUP_ANIMATION);

    }


    //private void Menu_TransitionEnd(TransitionEndEvent evt)
    //{
    //    if (!evt.stylePropertyNames.Contains("opacity")) { return; }

    //    _firstLoadDone = _mainPopupIndex == _mainMenuOptions.Count - 1;

    //    if (!_firstLoadDone)
    //    {
    //        _mainPopupIndex++;

    //        _mainMenuOptions[_mainPopupIndex].ToggleInClassList(POPUP_ANIMATION);

    //    }
    //    else if (_navTransitionActive)
    //    {
    //        _mainMenuOptions[0].parent.style.display = _settingsMenuOptions.style.display == DisplayStyle.Flex ? DisplayStyle.Flex : DisplayStyle.None;
    //        _settingsMenuOptions.style.display = _settingsMenuOptions.style.display == DisplayStyle.Flex ? DisplayStyle.None : DisplayStyle.Flex;

    //        _navTransitionActive = false;

    //        _menu.ToggleInClassList(POPUP_ANIMATION);
    //    }
    //}

    private void Button_OnClick(MouseDownEvent evt)
    {
        //Cannot interact with the settings button during a transition
        if (_navTransitionActive)
        {
            return;
        }

        _navTransitionActive = true;

        _menu.ToggleInClassList(POPUP_ANIMATION);
    }
}
