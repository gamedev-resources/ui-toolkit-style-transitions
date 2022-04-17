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
    private List<VisualElement> _widgets;

    private const string POPUP_ANIMATION = "pop-animation-hide";
    private int _mainPopupIndex = -1;

    private void Awake()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        _menu = root.Q<VisualElement>("Menu");
        _mainMenuOptions = _menu.Q<VisualElement>("MainNav").Children().ToArray();
        _widgets = root.Q<VisualElement>("Body").Children().ToList();

        _menu.RegisterCallback<TransitionEndEvent>(Menu_TransitionEnd);

    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);

        _menu.ToggleInClassList(POPUP_ANIMATION);
    }

    private void Menu_TransitionEnd(TransitionEndEvent evt)
    {
        if (!evt.stylePropertyNames.Contains("opacity")) { return; }


    if (_mainPopupIndex < _mainMenuOptions.Length - 1)
    {
        _mainPopupIndex++;

        _mainMenuOptions[_mainPopupIndex].ToggleInClassList(POPUP_ANIMATION);
    }
    else
    {
        _widgets.ForEach(x => x.style.translate = new StyleTranslate(new Translate(0, 0, 0)));

    }

    }
}
