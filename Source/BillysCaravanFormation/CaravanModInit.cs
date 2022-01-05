using System.Reflection;
using HarmonyLib;
using UnityEngine;
using Verse;

namespace BillysCaravanFormation;

public class CaravanModSettings : ModSettings
{
    public bool fastAnimalCollection;

    public override void ExposeData()
    {
        Scribe_Values.Look(ref fastAnimalCollection, "fastCaravanAnimalCollection");
        base.ExposeData();
    }
}

public class CaravanModInit : Mod
{
    private readonly CaravanModSettings settings;

    public CaravanModInit(ModContentPack content)
        : base(content)
    {
        settings = GetSettings<CaravanModSettings>();
        // this should be executed when Rimworld loads our mod (because the class inherits from Verse.Mod). 
        // The code below tells Harmony to apply the patches attributed above.
        var harmony = new Harmony("bem.rimworld.mod.billy.smartcaravan");
        harmony.PatchAll(Assembly.GetExecutingAssembly());
    }

    public override void DoSettingsWindowContents(Rect inRect)
    {
        var listing = new Listing_Standard();
        listing.Begin(inRect);
        listing.CheckboxLabeled("Skip gathering of caravan animals by colonists", ref settings.fastAnimalCollection,
            "Animals will move to the meeting spot on their own, speeding up caravan formation");
        listing.End();
        base.DoSettingsWindowContents(inRect);
    }

    public override string SettingsCategory()
    {
        return "Billy's Improved Caravan Formation";
    }
}