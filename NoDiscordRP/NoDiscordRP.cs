using HarmonyLib;
using ResoniteModLoader;
using FrooxEngine.Interfacing;

namespace NoDiscordRP;

public class NoDiscordRP : ResoniteMod
{
    public override string Name => "NoDiscordRP";
    public override string Author => "Raidriar796";
    public override string Version => "1.0.0";
    public override string Link => "https://github.com/Raidriar796/NoDiscordRP";
    public static ModConfiguration? Config;

    [AutoRegisterConfigKey] public static readonly ModConfigurationKey<bool> Enabled =
        new ModConfigurationKey<bool>(
            "Enabled",
            "Allow session info to display in Discord.",
            () => false);

    public override void OnEngineInit()
    {
        Harmony harmony = new("net.raidriar796.NoDiscordRP");
        Config = GetConfiguration();
        Config?.Save(true);
        harmony.PatchAll();
    }

    [HarmonyPatch(typeof(DiscordConnector), "SetCurrentStatus")]
    class RPPatch
    {
        public static bool Prefix()
        {
            if (Config.GetValue(Enabled)) return true;

            return false;
        }
    }
}
