using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;
using System;

public class UserInterfaceController : MonoBehaviour
{
    private VisualElement _menu;
    private VisualElement[] _mainMenuOptions;
    //private VisualElement _settingsMenuOptions;
    private List<VisualElement> _widgets;

    private const string POPUP_ANIMATION = "pop-animation-hide";
    private int _mainPopupIndex = -1;

    // public bool _navTransitionActive;
    //  public bool _firstLoadDone = false;

    private void Awake()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        _menu = root.Q<VisualElement>("Menu");

        _mainMenuOptions = _menu.Q<VisualElement>("MainNav").Children().ToArray();
        //_settingsMenuOptions = _menu.Q<VisualElement>("SettingsNav");
        _widgets = root.Q<VisualElement>("Body").Children().ToList();

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
        if (!evt.stylePropertyNames.Contains("opacity")) { return; }

        //Main menu is done
        if (_mainPopupIndex == _mainMenuOptions.Length - 1)
        {
            _widgets.ForEach(x => x.style.translate = new StyleTranslate(new Translate(0, 0, 0)));
        }
        else
        {
            _mainPopupIndex++;

            _mainMenuOptions[_mainPopupIndex].ToggleInClassList(POPUP_ANIMATION);
        }

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
