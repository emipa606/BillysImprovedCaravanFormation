using System.Reflection;
using HarmonyLib;
using Mlie;
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
    private static string currentVersion;
    private readonly CaravanModSettings settings;

    public CaravanModInit(ModContentPack content)
        : base(content)
    {
        settings = GetSettings<CaravanModSettings>();
        // this should be executed when Rimworld loads our mod (because the class inherits from Verse.Mod). 
        // The code below tells Harmony to apply the patches attributed above.
        var harmony = new Harmony("bem.rimworld.mod.billy.smartcaravan");
        harmony.PatchAll(Assembly.GetExecutingAssembly());
        currentVersion =
            VersionFromManifest.GetVersionFromModMetaData(
                ModLister.GetActiveModWithIdentifier("Mlie.BillysImprovedCaravanFormation"));
    }

    public override void DoSettingsWindowContents(Rect inRect)
    {
        var listing = new Listing_Standard();
        listing.Begin(inRect);
        listing.CheckboxLabeled("BICF.SkipAnimals".Translate(), ref settings.fastAnimalCollection,
            "BICF.SkipAnimals.Tooltip".Translate());
        if (currentVersion != null)
        {
            listing.Gap();
            GUI.contentColor = Color.gray;
            listing.Label("BICF.ModVersion".Translate(currentVersion));
            GUI.contentColor = Color.white;
        }

        listing.End();
    }

    public override string SettingsCategory()
    {
        return "Billy's Improved Caravan Formation";
    }
}