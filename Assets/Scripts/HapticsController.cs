public static class HapticsController
{

    public static void TriggerHapticFeedback()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        UnityEngine.Application.ExternalEval("if (navigator.vibrate) { navigator.vibrate(100); }");
#endif
    }

}
