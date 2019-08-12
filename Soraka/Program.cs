namespace SorakaBuddy
{
    using System;
    using System.Drawing;
    using System.Linq;

    using EloBuddy;
    using EloBuddy.SDK;
    using EloBuddy.SDK.Enumerations;
    using EloBuddy.SDK.Events;
    using EloBuddy.SDK.Menu;
    using EloBuddy.SDK.Menu.Values;
    using EloBuddy.SDK.Rendering;

    internal class Program
    {
        public const string ChampionName = "Soraka";

        public static Spell.Skillshot Q;

        public static Spell.Targeted W;

        public static Spell.Skillshot E;

        public static Spell.Active R;

        public static Menu SorakaBuddy, ComboMenu, HarassMenu, HealMenu, InterruptMenu, GapcloserMenu, DrawingMenu, MiscMenu;

        public static AIHeroClient PlayerInstance
        {
            get { return Player.Instance; }
        }

        public static HitChance GetHitChance()
        {
            switch (MiscMenu["Slider"].Cast<Slider>().DisplayName)
            {
                case "High":
                    return HitChance.High;
                case "Medium":
                    return HitChance.Medium;
                case "Low":
                    return HitChance.Low;
                default:
                    return HitChance.High;
            }
        }

        private static void Main()
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            try
            {
                if (ChampionName != PlayerInstance.BaseSkinName)
                {
                    return;
                }

                Bootstrap.Init(null);

                Q = new Spell.Skillshot(SpellSlot.Q, 800, SkillShotType.Circular, 283, 1100, 210);
                W = new Spell.Targeted(SpellSlot.W, 550);
                E = new Spell.Skillshot(SpellSlot.E, 925, SkillShotType.Circular, 500, 1750, 70);
                R = new Spell.Active(SpellSlot.R, int.MaxValue);

                SorakaBuddy = MainMenu.AddMenu("SorakaBuddy", "SorakaBuddy");

                // Combo Menu
                ComboMenu = SorakaBuddy.AddSubMenu("Combo", "Combo");
                ComboMenu.AddGroupLabel("Combo Setting");
                ComboMenu.Add("useQ", new CheckBox("Use Q"));
                ComboMenu.Add("useE", new CheckBox("Use E"));
                ComboMenu.AddSeparator();
                ComboMenu.AddGroupLabel("ManaManager");
                ComboMenu.Add("manaQ", new Slider("Min Mana % before Q", 25));
                ComboMenu.Add("manaE", new Slider("Min Mana % before E", 25));

                // Harass Menu
                HarassMenu = SorakaBuddy.AddSubMenu("Harass", "Harass");
                HarassMenu.AddGroupLabel("Harass Setting");
                HarassMenu.Add("useQ", new CheckBox("Use Q"));
                HarassMenu.Add("useE", new CheckBox("Use E"));
                HarassMenu.AddSeparator();
                HarassMenu.AddGroupLabel("ManaManager");
                HarassMenu.Add("manaQ", new Slider("Min Mana % before Q", 25));
                HarassMenu.Add("manaE", new Slider("Min Mana % before E", 25));

                // Heal Menu
                var allies = EntityManager.Heroes.Allies.Where(a => !a.IsMe).ToArray();
                HealMenu = SorakaBuddy.AddSubMenu("Auto Heal", "Heal");
                HealMenu.AddGroupLabel("Auto W Setting");
                HealMenu.Add("autoW", new CheckBox("Auto W Allies and Me"));
                HealMenu.Add("autoWHP_self", new Slider("Own HP % before using W", 50));
                HealMenu.Add("autoWHP_other", new Slider("Ally HP % before W", 50));
                HealMenu.AddSeparator();
                HealMenu.AddGroupLabel("Auto R Setting");
                HealMenu.Add("useR", new CheckBox("Auto R on HP %"));
                HealMenu.AddSeparator();
                HealMenu.Add("hpR", new Slider("HP % before using R", 25));
                HealMenu.AddSeparator();
                HealMenu.AddLabel("Which Champion to Heal? Using W?");
                foreach (var a in allies)
                {
                    HealMenu.Add("autoHeal_" + a.BaseSkinName, new CheckBox("Auto Heal with W " + a.ChampionName));
                }
                HealMenu.AddSeparator();
                HealMenu.AddLabel("Which Champion to Heal? Using R?");
                foreach (var a in allies)
                {
                    HealMenu.Add("autoHealR_" + a.BaseSkinName, new CheckBox("Auto Heal with R " + a.ChampionName));
                }
                HealMenu.Add("autoHealR_" + PlayerInstance.BaseSkinName, new CheckBox("Auto Heal Self with R"));
                HealMenu.AddSeparator();

                // Interrupt Menu
                InterruptMenu = SorakaBuddy.AddSubMenu("Interrupter", "Interrupter");
                InterruptMenu.AddGroupLabel("Interrupter Setting");
                InterruptMenu.Add("useE", new CheckBox("Use E on Interrupt"));

                // Gapcloser Menu
                GapcloserMenu = SorakaBuddy.AddSubMenu("Gapcloser", "Gapcloser");
                GapcloserMenu.AddGroupLabel("Gapcloser Setting");
                GapcloserMenu.Add("useQ", new CheckBox("Use Q on Gapcloser"));
                GapcloserMenu.Add("useE", new CheckBox("Use E on Gapcloser"));

                // Drawing Menu
                DrawingMenu = SorakaBuddy.AddSubMenu("Drawing", "Drawing");
                DrawingMenu.AddGroupLabel("Drawing Setting");
                DrawingMenu.Add("drawQ", new CheckBox("Draw Q Range"));
                DrawingMenu.Add("drawW", new CheckBox("Draw W Range"));
                DrawingMenu.Add("drawE", new CheckBox("Draw E Range"));

                // Misc Menu
                MiscMenu = SorakaBuddy.AddSubMenu("Misc", "Misc");
                MiscMenu.AddGroupLabel("Miscellaneous Setting");
                MiscMenu.Add("disableMAA", new CheckBox("Disable Minion AA"));
                MiscMenu.Add("disableCAA", new CheckBox("Disable Champion AA"));
                MiscMenu.AddLabel("Prediction Settings");
                var predictionSlider = MiscMenu.Add("Slider", new Slider("mode", 0, 0, 2));
                var predictionArray = new[] { "High", "Medium", "Low" };
                predictionSlider.DisplayName = predictionArray[predictionSlider.CurrentValue];
                predictionSlider.OnValueChange += delegate(ValueBase<int> sender, ValueBase<int>.ValueChangeArgs changeArgs)
                {
                    sender.DisplayName = predictionArray[changeArgs.NewValue];
                };

                Chat.Print("SorakaBuddy: Initialized", Color.LightGreen);

                Orbwalker.OnPreAttack += Orbwalker_OnPreAttack;
                Game.OnTick += Game_OnTick;
                Drawing.OnDraw += Drawing_OnDraw;

                Interrupter.OnInterruptableSpell += Interrupter_OnInterruptableSpell;
                Gapcloser.OnGapcloser += Gapcloser_OnGapcloser;
            }
            catch (Exception e)
            {
                Chat.Print("SorakaBuddy: Exception occured while Initializing Addon. Error: " + e.Message);
            }
        }

        private static void Combo()
        {
            var hitChance = GetHitChance();

            if (ComboMenu["useQ"].Cast<CheckBox>().CurrentValue)
            {
                var target = TargetSelector.GetTarget(Q.Range, DamageType.Magical, PlayerInstance.ServerPosition);

                if (target != null)
                {
                    if (PlayerInstance.ManaPercent >= ComboMenu["manaQ"].Cast<Slider>().CurrentValue
                        && (Q.IsReady() && Q.IsInRange(target)))
                    {
                        var pred = Q.GetPrediction(target);

                        if (pred.HitChance >= hitChance)
                        {
                            Q.Cast(pred.CastPosition);
                        }
                    }
                }
            }

            if (!ComboMenu["useE"].Cast<CheckBox>().CurrentValue)
            {
                return;
            }

            var etarget = TargetSelector.GetTarget(E.Range, DamageType.Magical, PlayerInstance.ServerPosition);

            if (etarget == null
                || (!(PlayerInstance.ManaPercent >= ComboMenu["manaE"].Cast<Slider>().CurrentValue)
                    || (!E.IsReady() || !E.IsInRange(etarget))))
            {
                return;
            }

            var ePrediction = E.GetPrediction(etarget);

            if (ePrediction.HitChance >= hitChance)
            {
                E.Cast(ePrediction.CastPosition);
            }
        }

        private static void Harass()
        {
            var hitChance = GetHitChance();

            if (HarassMenu["useQ"].Cast<CheckBox>().CurrentValue)
            {
                var target = TargetSelector.GetTarget(Q.Range, DamageType.Magical, PlayerInstance.ServerPosition);

                if (target != null)
                {
                    if (PlayerInstance.ManaPercent >= HarassMenu["manaQ"].Cast<Slider>().CurrentValue)
                    {
                        if (Q.IsInRange(target) && Q.IsReady())
                        {
                            var pred = Q.GetPrediction(target);

                            if (pred.HitChance >= hitChance)
                            {
                                Q.Cast(pred.CastPosition);
                            }
                        }
                    }
                }
            }
            if (!HarassMenu["useE"].Cast<CheckBox>().CurrentValue)
            {
                return;
            }
            var eTarget = TargetSelector.GetTarget(E.Range, DamageType.Magical, PlayerInstance.ServerPosition);

            if (eTarget == null)
            {
                return;
            }
            if (!(PlayerInstance.ManaPercent >= HarassMenu["manaE"].Cast<Slider>().CurrentValue))
            {
                return;
            }
            if (!E.IsInRange(eTarget) || !E.IsReady())
            {
                return;
            }
            var ePrediction = E.GetPrediction(eTarget);

            if (ePrediction.HitChance >= hitChance)
            {
                E.Cast(ePrediction.CastPosition);
            }
        }

        private static void AutoHeal()
        {
            var autoWhpOther = HealMenu["autoWHP_other"].Cast<Slider>().CurrentValue;
            var autoWhpSelf = HealMenu["autoWHP_self"].Cast<Slider>().CurrentValue;

            if (HealMenu["autoW"].Cast<CheckBox>().CurrentValue && W.IsReady())
            {
                var lowestHealthAlly = EntityManager.Heroes.Allies.OrderBy(a => a.Health).FirstOrDefault(a => W.IsInRange(a) && !a.IsMe);

                if (lowestHealthAlly != null)
                {
                    if (lowestHealthAlly.HealthPercent <= autoWhpOther
                        && PlayerInstance.HealthPercent >= autoWhpSelf)
                    {
                        if (HealMenu["autoHeal_" + lowestHealthAlly.BaseSkinName].Cast<CheckBox>().CurrentValue)
                        {
                            W.Cast(lowestHealthAlly);
                        }
                    }
                }
            }

            if (!HealMenu["useR"].Cast<CheckBox>().CurrentValue || !R.IsReady())
            {
                return;
            }

            var lowestHealthAllyOor = EntityManager.Heroes.Allies.OrderByDescending(a => a.Health).FirstOrDefault();

            if (lowestHealthAllyOor == null || !(lowestHealthAllyOor.HealthPercent <= HealMenu["hpR"].Cast<Slider>().CurrentValue))
            {
                return;
            }

            if (HealMenu["autoHealR_" + lowestHealthAllyOor.BaseSkinName].Cast<CheckBox>().CurrentValue)
            {
                R.Cast();
            }
        }
        private static void Interrupter_OnInterruptableSpell(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs e)
        {
            if (sender == null || sender.IsAlly)
            {
                return;
            }

            if (!InterruptMenu["useE"].Cast<CheckBox>().CurrentValue && e.DangerLevel != DangerLevel.High)
            {
                return;
            }

            if (!E.IsInRange(sender) || !E.IsReady())
            {
                return;
            }

            var pred = E.GetPrediction(sender);

            if (pred.HitChance >= GetHitChance())
            {
                E.Cast(pred.CastPosition);
            }
        }

        private static void Gapcloser_OnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
        {
            if (sender == null || sender.IsAlly)
            {
                return;
            }

            if (GapcloserMenu["useQ"].Cast<CheckBox>().CurrentValue)
            {
                if (Q.IsInRange(sender) && Q.IsReady())
                {
                    var pred = Q.GetPrediction(sender);

                    if (pred.HitChance >= GetHitChance())
                    {
                        Q.Cast(pred.CastPosition);
                    }
                }
            }

            if (!GapcloserMenu["useE"].Cast<CheckBox>().CurrentValue)
            {
                return;
            }

            if (!E.IsInRange(sender) || !E.IsReady())
            {
                return;
            }
            var ePrediction = E.GetPrediction(sender);

            if (ePrediction.HitChance >= GetHitChance())
            {
                E.Cast(ePrediction.CastPosition);
            }
        }

        private static void Orbwalker_OnPreAttack(AttackableUnit target, Orbwalker.PreAttackArgs args)
        {
            var t = target as AIHeroClient;
            var m = target as Obj_AI_Minion;
            var alliesNearPlayer = EntityManager.Heroes.Allies.Count(a => PlayerInstance.Distance(a) <= PlayerInstance.AttackRange);

            if (t != null)
            {
                if (alliesNearPlayer < 1)
                {
                    return;
                }
                if (MiscMenu["disableCAA"].Cast<CheckBox>().CurrentValue)
                {
                    args.Process = false;
                }
            }

            if (m == null)
            {
                return;
            }

            if (alliesNearPlayer < 1)
            {
                return;
            }

            if (MiscMenu["disableMAA"].Cast<CheckBox>().CurrentValue)
            {
                args.Process = false;
            }
        }

        private static void Game_OnTick(EventArgs args)
        {
            AutoHeal();
            
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                Combo();
            }

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass))
            {
                Harass();
            }
        }
        
        private static void Drawing_OnDraw(EventArgs args)
        {
            var playerPosition = PlayerInstance.Position;

            if (DrawingMenu["drawQ"].Cast<CheckBox>().CurrentValue)
            {
                Circle.Draw(Q.IsReady() ? SharpDX.Color.Green : SharpDX.Color.Red, Q.Range, playerPosition);
            }
            if (DrawingMenu["drawW"].Cast<CheckBox>().CurrentValue)
            {
                Circle.Draw(W.IsReady() ? SharpDX.Color.Green : SharpDX.Color.Red, W.Range, playerPosition);
            }
            if (DrawingMenu["drawE"].Cast<CheckBox>().CurrentValue)
            {
                Circle.Draw(E.IsReady() ? SharpDX.Color.Green : SharpDX.Color.Red, E.Range, playerPosition);
            }
        }
    }
}
