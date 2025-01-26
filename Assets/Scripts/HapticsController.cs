using UnityEngine;

public static class HapticsController
{

    public static void TriggerHapticFeedback()
    {
#if UNITY_WEBGL
        Application.ExternalEval("if (navigator.vibrate) { navigator.vibrate(100); }");
#endif
    }

}
