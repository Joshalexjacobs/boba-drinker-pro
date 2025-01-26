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

}
