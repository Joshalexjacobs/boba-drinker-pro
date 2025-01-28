using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public static class CommonUtilities
{

    public static void SetBackgroundPosition(this VisualElement visualElement, Vector2 position)
    {
        visualElement.style.backgroundPositionX = new StyleBackgroundPosition
        {
            keyword = StyleKeyword.Auto,
            value = new BackgroundPosition
            {
                keyword = BackgroundPositionKeyword.Left, offset = new Length(position.x, LengthUnit.Pixel)
            }
        };

        visualElement.style.backgroundPositionY = new StyleBackgroundPosition
        {
            keyword = StyleKeyword.Auto,
            value = new BackgroundPosition
            {
                keyword = BackgroundPositionKeyword.Left, offset = new Length(position.y, LengthUnit.Pixel)
            }
        };
    }

    public static void ChangeBackgroundImage(this VisualElement visualElement, Sprite sprite)
    {
        visualElement.style.backgroundImage = new StyleBackground(sprite);
    }

    private static readonly Dictionary<VisualElement, List<EventCallback<PointerDownEvent>>>
        PointerDownEventCallbackRegistry = new();

    public static void RegisterPointerDownCallback(this VisualElement visualElement,
        EventCallback<PointerDownEvent> callback)
    {
        visualElement.RegisterCallback(callback, TrickleDown.TrickleDown);

        if (!PointerDownEventCallbackRegistry.ContainsKey(visualElement))
        {
            PointerDownEventCallbackRegistry[visualElement] = new List<EventCallback<PointerDownEvent>>();
        }

        PointerDownEventCallbackRegistry[visualElement].Add(callback);
    }

    public static void UnregisterPointerDownCallback(this VisualElement visualElement)
    {
        if (!PointerDownEventCallbackRegistry.TryGetValue(visualElement, out var eventCallbacks))
        {
            return;
        }

        foreach (var eventCallback in eventCallbacks)
        {
            visualElement.UnregisterCallback(eventCallback);
        }
    }

    private static readonly Dictionary<VisualElement, List<EventCallback<PointerUpEvent>>>
        PointerUpEventCallbackRegistry = new();

    public static void RegisterPointerUpCallback(this VisualElement visualElement,
        EventCallback<PointerUpEvent> callback)
    {
        visualElement.RegisterCallback(callback, TrickleDown.TrickleDown);

        if (!PointerUpEventCallbackRegistry.ContainsKey(visualElement))
        {
            PointerUpEventCallbackRegistry[visualElement] = new List<EventCallback<PointerUpEvent>>();
        }

        PointerUpEventCallbackRegistry[visualElement].Add(callback);
    }

    public static void UnregisterPointerUpCallback(this VisualElement visualElement)
    {
        if (!PointerUpEventCallbackRegistry.TryGetValue(visualElement, out var eventCallbacks))
        {
            return;
        }

        foreach (var eventCallback in eventCallbacks)
        {
            visualElement.UnregisterCallback(eventCallback);
        }
    }

    public static void RegisterBackgroundToggleImageEvents(this VisualElement visualElement, Sprite up, Sprite down)
    {
        visualElement.RegisterPointerDownCallback(e => visualElement.ChangeBackgroundImage(down));
        visualElement.RegisterPointerUpCallback(e => visualElement.ChangeBackgroundImage(up));
    }

    public static void UnregisterBackgroundToggleImageEvents(this VisualElement visualElement)
    {
        visualElement.UnregisterPointerUpCallback();
    }

}
