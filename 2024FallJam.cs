using OWML.Common;
using OWML.ModHelper;

namespace FallJam;
public class FallJam : ModBehaviour
{
    public static FallJam Instance;

    public static INewHorizons newHorizons;
    private void Awake()
{
    // You won't be able to access OWML's mod helper in Awake.
    // So you probably don't want to do anything here.
    // Use Start() instead.
}

private void Start()
{
    // Starting here, you'll have access to OWML's mod helper.
    ModHelper.Console.WriteLine($"My mod {nameof(FallJam)} is loaded!", MessageType.Success);

    // Get the New Horizons API and load configs
    newHorizons = ModHelper.Interaction.TryGetModApi<INewHorizons>("xen.NewHorizons");
    newHorizons.LoadConfigs(this);

    // Example of accessing game code.
    LoadManager.OnCompleteSceneLoad += (scene, loadScene) =>
    {
        if (loadScene != OWScene.SolarSystem) return;
        ModHelper.Console.WriteLine("Loaded into solar system!", MessageType.Success);
    };
}
}

