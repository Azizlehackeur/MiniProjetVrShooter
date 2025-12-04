public enum PlatformType
{
    PCVR,
    Quest
}

public static class PlatformDetector
{
    public static PlatformType GetPlatform()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        return PlatformType.Quest;
#else
        return PlatformType.PCVR;
#endif
    }
}
