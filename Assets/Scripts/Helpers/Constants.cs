using System.Collections.Generic;

public static class ConstantsPlayerPref
{
    public static readonly string Volume = "Volume";
    public static readonly string Music = "Music";
}

public static class ConstantsScene
{
    public static readonly string MenuScene = "MenuScene";
    public static readonly string GameScene = "GameScene";
    public static readonly string GameOverScene = "SettingsScene";
}

public static class ConstantsTag
{
    public static readonly string Player = "Player";
    public static readonly string Target = "Target";
}

public static class BallPrefs
{
    public static KeyValuePair<string, BallProps>[] Options =
    {
        new("Ball2", new BallProps
        {
            TouchPoint = "TouchPoint2",
            LineRenderer = "TrajectoryLine2",
            Force = 900f
        }),
        new("Ball3", new BallProps
        {
            TouchPoint = "TouchPoint3",
            LineRenderer = "TrajectoryLine3",
            Force = 700f
        }),
    };
}

public class BallProps
{
    public string TouchPoint { get; set; }
    public string LineRenderer { get; set; }
    public float Force { get; set; }
}
