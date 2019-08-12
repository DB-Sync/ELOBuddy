using EloBuddy.SDK;

namespace Jinx
{
    using EloBuddy;
    using EloBuddy.SDK.Menu;
    using EloBuddy.SDK.Menu.Values;

    internal class Config
    {
        /// <summary>
        /// Initializes and Contains the Menu.
        /// </summary>
        public static Menu ConfigMenu;

        /// <summary>
        /// Initializes and Contains the Menu.
        /// </summary>
        public static Menu ComboMenu;

        /// <summary>
        /// Initializes and Contains the Menu.
        /// </summary>
        public static Menu LastHitMenu;

        /// <summary>
        /// Initializes and Contains the Menu.
        /// </summary>
        public static Menu HarassMenu;

        /// <summary>
        /// Initializes and Contains the Menu.
        /// </summary>
        public static Menu LaneClearMenu;

        /// <summary>
        /// Initializes and Contains the Menu.
        /// </summary>
        public static Menu KillStealMenu;

        /// <summary>
        /// Initializes and Contains the Menu.
        /// </summary>
        public static Menu JungleClearMenu;

        /// <summary>
        /// Initializes and Contains the Menu.
        /// </summary>
        public static Menu JungleStealMenu;

        /// <summary>
        /// Initializes and Contains the Menu.
        /// </summary>
        public static Menu FleeMenu;

        /// <summary>
        /// Initializes and Contains the Menu.
        /// </summary>
        public static Menu DrawingMenu;

        /// <summary>
        /// Initializes and Contains the Menu.
        /// </summary>
        public static Menu MiscMenu;

        /// <summary>
        /// Creates the Menu
        /// </summary>
        public static void Initialize()
        {
            ConfigMenu = MainMenu.AddMenu("Jin-XXX", "Jin-XXX");
            ConfigMenu.AddGroupLabel("This addon is made by DBSync and should not be redistributed in any way.");
            ConfigMenu.AddGroupLabel("Any unauthorized redistribution without credits will result in severe consequences.");
            ConfigMenu.AddGroupLabel("Thank you for using this addon and have a fun time!");

            ComboMenu = ConfigMenu.AddSubMenu("Combo", "Combo");
            ComboMenu.AddLabel("Combo Settings");
            ComboMenu.Add("useQ", new CheckBox("Use Q"));
            ComboMenu.Add("useW", new CheckBox("Use W"));
            ComboMenu.Add("useE", new CheckBox("Use E"));
            ComboMenu.Add("useR", new CheckBox("Use R"));
            ComboMenu.AddLabel("ManaManager");
            ComboMenu.Add("manaQ", new Slider("ManaManager for Q", 25));
            ComboMenu.Add("manaW", new Slider("ManaManager for W", 25));
            ComboMenu.Add("manaE", new Slider("ManaManager for E", 25));
            ComboMenu.Add("manaR", new Slider("ManaManager for R", 25));
            ComboMenu.AddLabel("Hit on Champion is Prioritized first over Minion");
            ComboMenu.Add("qCountC", new Slider("Use Q if Hit {0} Champion(s)", 3, 1, 5));
            ComboMenu.Add("rCountC", new Slider("Use R if Hit {0} Champion(s)", 5, 1, 5));
            ComboMenu.AddLabel("Prediction Settings");
            ComboMenu.Add("wSlider", new Slider("Use W if HitChance % is {0}", 80));
            ComboMenu.Add("eSlider", new Slider("Use E if HitChance % is {0}", 80));
            ComboMenu.Add("rSlider", new Slider("Use R if HitChance % is {0}", 80));
            ComboMenu.AddLabel("Extra Settings");
            ComboMenu.Add("wRange2", new Slider("Only Use W if Player Range from Target is more than {0}", 150, 0, 1500));
            ComboMenu.Add("eRange", new Slider("Only Use E if Player Range from Target is more than {0}", 150, 0, 900));
            ComboMenu.Add("eRange2", new Slider("Max E Range", 900, 0, 900));
            ComboMenu.Add("rRange2", new Slider("Max R Range", 3000, 0, 3000));

            LastHitMenu = ConfigMenu.AddSubMenu("LastHit", "LastHit");
            LastHitMenu.AddGroupLabel("LastHit Settings");
            LastHitMenu.Add("useQ", new CheckBox("Use Q"));
            LastHitMenu.Add("qCountM", new Slider("Use Q if Hit {0} Minions", 3, 1, 7));
            LastHitMenu.AddLabel("ManaManager");
            LastHitMenu.Add("manaQ", new Slider("Mana Manager for Q", 25));

            HarassMenu = ConfigMenu.AddSubMenu("Harass", "Harass");
            HarassMenu.AddLabel("Harass Settings");
            HarassMenu.Add("useQ", new CheckBox("Use Q"));
            HarassMenu.Add("useW", new CheckBox("Use W"));
            HarassMenu.AddLabel("ManaManager");
            HarassMenu.Add("manaQ", new Slider("Mana Manager for Q", 15));
            HarassMenu.Add("manaW", new Slider("Mana Manager for W", 35));
            HarassMenu.AddLabel("Hit on Champion is Prioritized first over Minion");
            HarassMenu.Add("qCountC", new Slider("Use Q if Hit {0} Champion(s)", 3, 1, 5));
            HarassMenu.Add("qCountM", new Slider("Use Q if Hit {0} Minion(s)", 3, 1, 7));
            HarassMenu.AddLabel("Prediction Settings");
            HarassMenu.Add("wSlider", new Slider("Use W if HitChance % is {0}", 95));
            HarassMenu.AddLabel("Extra Settings");
            HarassMenu.Add("wRange2", new Slider("Don't Use W if Player Range from Target is {0}", 0, 0, 1450));

            LaneClearMenu = ConfigMenu.AddSubMenu("Lane Clear", "LaneClear");
            LaneClearMenu.AddLabel("Lane Clear Settings");
            LaneClearMenu.Add("useQ", new CheckBox("Use Q"));
            LaneClearMenu.Add("lastHit", new CheckBox("Last Hit Minion Out of Range", false));
            LaneClearMenu.Add("killQ", new CheckBox("Only use Q if minion is killable"));
            LaneClearMenu.Add("manaQ", new Slider("Mana Manager for Q", 25));
            LaneClearMenu.Add("qCountM", new Slider("Use Q if Hit {0} Minion(s)", 3, 1, 7));

            KillStealMenu = ConfigMenu.AddSubMenu("Kill Steal", "KillSteal");
            KillStealMenu.Add("toggle", new CheckBox("Use Kill Steal"));
            KillStealMenu.Add("useW", new CheckBox("Use W to KS"));
            KillStealMenu.Add("useR", new CheckBox("Use R to KS"));
            KillStealMenu.AddLabel("ManaManager");
            KillStealMenu.Add("manaW", new Slider("Mana Manager for W", 25));
            KillStealMenu.Add("manaR", new Slider("Mana Manager for R", 25));
            KillStealMenu.AddLabel("Prediction Settings");
            KillStealMenu.Add("wSlider", new Slider("Use W if HitChance % is {0}", 80));
            KillStealMenu.Add("rSlider", new Slider("Use R if HitChance % is {0}", 80));
            KillStealMenu.AddLabel("Spell Settings");
            KillStealMenu.Add("wRange", new Slider("Minimum Distance for W", 450, 0, 1500));
            KillStealMenu.Add("rRange", new Slider("Max Distance for R", 3000, 0, 3000));

            JungleClearMenu = ConfigMenu.AddSubMenu("Jungle Clear", "JungleClear");
            JungleClearMenu.AddLabel("Jungle Clear Settings");
            JungleClearMenu.Add("useQ", new CheckBox("Use Q"));
            JungleClearMenu.Add("useW", new CheckBox("Use W", false));
            JungleClearMenu.AddLabel("ManaManager");
            JungleClearMenu.Add("manaQ", new Slider("Mana Manager for Q", 25));
            JungleClearMenu.Add("manaW", new Slider("Mana Manager for W", 25));
            JungleClearMenu.AddLabel("Misc Settings");
            JungleClearMenu.Add("wSlider", new Slider("Use W if HitChance % is {0}", 85));

            JungleStealMenu = ConfigMenu.AddSubMenu("Jungle Steal", "JungleSteal");
            JungleStealMenu.AddLabel("Jungle Steal Settings");
            JungleStealMenu.Add("toggle", new CheckBox("Use Jungle Steal", false));
            JungleStealMenu.Add("manaR", new Slider("Mana Manager for R", 25));
            JungleStealMenu.Add("rRange", new Slider("Range from mob before using R", 3000, 0, 3000));
            if (Game.MapId == GameMapId.SummonersRift)
            {
                JungleStealMenu.AddLabel("Epics");
                JungleStealMenu.Add("SRU_Baron", new CheckBox("Baron"));
                JungleStealMenu.Add("SRU_Dragon", new CheckBox("Dragon"));
                JungleStealMenu.AddLabel("Buffs");
                JungleStealMenu.Add("SRU_Blue", new CheckBox("Blue", false));
                JungleStealMenu.Add("SRU_Red", new CheckBox("Red", false));
                JungleStealMenu.AddLabel("Small Camps");
                JungleStealMenu.Add("SRU_Gromp", new CheckBox("Gromp", false));
                JungleStealMenu.Add("SRU_Murkwolf", new CheckBox("Murkwolf", false));
                JungleStealMenu.Add("SRU_Krug", new CheckBox("Krug", false));
                JungleStealMenu.Add("SRU_Razorbeak", new CheckBox("Razerbeak", false));
                JungleStealMenu.Add("Sru_Crab", new CheckBox("Skuttles", false));
            }

            if (Game.MapId == GameMapId.TwistedTreeline)
            {
                JungleStealMenu.AddLabel("Epics");
                JungleStealMenu.Add("TT_Spiderboss8.1", new CheckBox("Vilemaw"));
                JungleStealMenu.AddLabel("Camps");
                JungleStealMenu.Add("TT_NWraith1.1", new CheckBox("Wraith", false));
                JungleStealMenu.Add("TT_NWraith4.1", new CheckBox("Wraith", false));
                JungleStealMenu.Add("TT_NGolem2.1", new CheckBox("Golem", false));
                JungleStealMenu.Add("TT_NGolem5.1", new CheckBox("Golem", false));
                JungleStealMenu.Add("TT_NWolf3.1", new CheckBox("Wolf", false));
                JungleStealMenu.Add("TT_NWolf6.1", new CheckBox("Wolf", false));
            }

            FleeMenu = ConfigMenu.AddSubMenu("Flee", "Flee");
            FleeMenu.AddLabel("Flee Settings");
            FleeMenu.Add("useW", new CheckBox("Use W while Fleeing"));
            FleeMenu.Add("useE", new CheckBox("Use E while Fleeing"));
            FleeMenu.Add("wSlider", new Slider("Use W if HitChance is {0}", 75));
            FleeMenu.Add("eSlider", new Slider("Use E if HitChance is {0}", 75));

            DrawingMenu = ConfigMenu.AddSubMenu("Drawing", "Drawing");
            DrawingMenu.AddLabel("Drawing Settings");
            DrawingMenu.Add("drawQ", new CheckBox("Draw Q Range"));
            DrawingMenu.Add("drawW", new CheckBox("Draw W Range"));
            DrawingMenu.Add("drawE", new CheckBox("Draw E Range", false));
            DrawingMenu.AddLabel("Prediction Drawings");
            DrawingMenu.Add("predW", new CheckBox("Draw W Prediction"));
            DrawingMenu.Add("predR", new CheckBox("Draw R Prediction (In consideration of Range before R)", false));
            DrawingMenu.AddLabel("DamageIndicator");
            DrawingMenu.Add("draw.Damage", new CheckBox("Draw Damage"));
            DrawingMenu.Add("draw.Q", new CheckBox("Calculate Q Damage", false));
            DrawingMenu.Add("draw.W", new CheckBox("Calculate W Damage"));
            DrawingMenu.Add("draw.E", new CheckBox("Calculate E Damage", false));
            DrawingMenu.Add("draw.R", new CheckBox("Calculate R Damage"));
            DrawingMenu.AddLabel("Color Settings for Damage Indicator");
            DrawingMenu.Add("draw_Alpha", new Slider("Alpha: ", 255, 0, 255));
            DrawingMenu.Add("draw_Red", new Slider("Red: ", 255, 0, 255));
            DrawingMenu.Add("draw_Green", new Slider("Green: ", 0, 0, 255));
            DrawingMenu.Add("draw_Blue", new Slider("Blue: ", 0, 0, 255));

            MiscMenu = ConfigMenu.AddSubMenu("Misc Menu", "Misc");
            MiscMenu.AddLabel("Interrupter");
            MiscMenu.Add("interruptE", new CheckBox("Use E to Interrupt"));
            MiscMenu.Add("interruptmanaE", new Slider("Mana % before using E to Interrupt", 25));
            MiscMenu.AddLabel("Gapcloser");
            MiscMenu.Add("gapcloserE", new CheckBox("Use E to Gapcloser"));
            MiscMenu.Add("gapclosermanaE", new Slider("Mana % before using E to Gapclose", 25));
            MiscMenu.AddLabel("Spell Settings");
            MiscMenu.Add("autoW", new CheckBox("Automatically use W in certain situations"));
            MiscMenu.Add("autoE", new CheckBox("Automatically uses E in certain situations"));
            MiscMenu.Add("qRange", new Slider("Extra Q Activation Range", 25, 0, 100));
            MiscMenu.Add("wRange", new CheckBox("Use W only if target is in AA range", false));
            MiscMenu.Add("rRange", new Slider("Don't Use R if Player Range from target is {0}", 500, 0, 3000));
            MiscMenu.AddLabel("Auto W Settings (You must have Auto W on)");
            MiscMenu.Add("stunW", new CheckBox("Use W on Stunned Enemy", false));
            MiscMenu.Add("charmW", new CheckBox("Use W on Charmed Enemy", false));
            //MiscMenu.Add("tauntW", new CheckBox("Use W on Taunted Enemy", false));
            MiscMenu.Add("fearW", new CheckBox("Use W on Feared Enemy", false));
            MiscMenu.Add("snareW", new CheckBox("Use W on Snared Enemy", false));
            MiscMenu.Add("wRange2", new Slider("Only Use W if Player Range from Target is more than {0}", 450, 0, 1500));
            MiscMenu.AddLabel("Prediction Settings");
            MiscMenu.Add("wSlider", new Slider("Use W if HitChance % is {0}", 75));
            MiscMenu.Add("eSlider", new Slider("Use E if HitChance % is {0}", 75));
            /*MiscMenu.AddLabel("Allah Akbar");
            MiscMenu.Add("allahAkbarT", new CheckBox("Play Allah Akbar after casting R", false));*/
        }
    }
}
