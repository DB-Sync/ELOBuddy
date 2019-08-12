using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using System;
using System.Drawing;

namespace AutoQSS
{
    class Program
    {
        public static Menu Menu, CC, Ult;
        public static Item QSS, Mercurial;
        public static bool keybind { get { return Menu["keybind"].Cast<KeyBind>().CurrentValue; } }
        public static bool drawk { get { return Menu["drawk"].Cast<CheckBox>().CurrentValue; } }

        public static bool Taunt { get { return CC["Taunt"].Cast<CheckBox>().CurrentValue; } }
        public static bool Stun { get { return CC["Stun"].Cast<CheckBox>().CurrentValue; } }
        public static bool Snare { get { return CC["Snare"].Cast<CheckBox>().CurrentValue; } }
        public static bool Polymorph { get { return CC["Polymorph"].Cast<CheckBox>().CurrentValue; } }
        public static bool Blind { get { return CC["Blind"].Cast<CheckBox>().CurrentValue; } }
        public static bool Fear { get { return CC["Fear"].Cast<CheckBox>().CurrentValue; } }
        public static bool Charm { get { return CC["Charm"].Cast<CheckBox>().CurrentValue; } }
        public static bool Suppression { get { return CC["Suppression"].Cast<CheckBox>().CurrentValue; } }
        public static bool Silence { get { return CC["Silence"].Cast<CheckBox>().CurrentValue; } }

        public static bool ZedUlt { get { return Ult["ZedUlt"].Cast<CheckBox>().CurrentValue; } }
        public static bool VladUlt { get { return Ult["VladUlt"].Cast<CheckBox>().CurrentValue; } }
        public static bool FizzUlt { get { return Ult["FizzUlt"].Cast<CheckBox>().CurrentValue; } }
        public static bool MordUlt { get { return Ult["MordUlt"].Cast<CheckBox>().CurrentValue; } }
        public static bool PoppyUlt { get { return Ult["PoppyUlt"].Cast<CheckBox>().CurrentValue; } }

        public static int MinBuff { get { return Menu["minbuff"].Cast<Slider>().CurrentValue; } }
        public static int MinDuration { get { return Menu["buffduration"].Cast<Slider>().CurrentValue; } }
        public static int DebuffCount;
        public static double increased;
        public static double decreased;

        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Game_OnStart;
        }
        private static void Game_OnStart(EventArgs args)
        {
            QSS = new Item(ItemId.Quicksilver_Sash);
            if (Game.MapId == GameMapId.CrystalScar) { Mercurial = new Item(ItemId.Dervish_Blade); }
            else { Mercurial = new Item(ItemId.Mercurial_Scimitar); }

            Menu = MainMenu.AddMenu("Auto QSS", "autoqss");
            CC = Menu.AddSubMenu("QSS Manager", "qsmg");
            Ult = Menu.AddSubMenu("Ults Manager", "ults");

            Menu.Add("keybind", new KeyBind("Auto QSS", false, KeyBind.BindTypes.PressToggle, 'L'));
            Menu.Add("drawk", new CheckBox("Draw Keybind"));
            Menu.Add("buffduration", new Slider("Min duration to QSS", 0, 0, 4));
            Menu.Add("minbuff", new Slider("Min buffs to QSS", 1, 1, 4));

            Menu.AddLabel("(You can increase / decrease the min buffs without pressing shift)");
            Menu.AddSeparator();
            var a = Menu.Add("increase", new KeyBind("Increase the min buffs to QSS", false, KeyBind.BindTypes.HoldActive, 'J'));
            var b = Menu.Add("decrease", new KeyBind("Decrease the min buffs to QSS", false, KeyBind.BindTypes.HoldActive, 'N'));
            a.OnValueChange += delegate
            (ValueBase<bool> sender, ValueBase<bool>.ValueChangeArgs Aargs)
            {
                if (Aargs.NewValue)
                {
                    Menu["minbuff"].Cast<Slider>().CurrentValue++;
                }
            };
            b.OnValueChange += delegate
            (ValueBase<bool> sender, ValueBase<bool>.ValueChangeArgs Aargs)
            {
                if (Aargs.NewValue)
                {
                    Menu["minbuff"].Cast<Slider>().CurrentValue--;
                }
            };
            CC.AddGroupLabel("Auto QSS if :");
            CC.Add("Taunt", new CheckBox("Taunt"));
            CC.Add("Stun", new CheckBox("Stun"));
            CC.Add("Snare", new CheckBox("Snare"));
            CC.Add("Polymorph", new CheckBox("Polymorph"));
            CC.Add("Blind", new CheckBox("Blind", false));
            CC.Add("Fear", new CheckBox("Fear"));
            CC.Add("Charm", new CheckBox("Charm"));
            CC.Add("Suppression", new CheckBox("Suppression"));
            CC.Add("Silence", new CheckBox("Silence", false));
            CC.Add("CCDelay", new Slider("Delay for CC", 400, 0, 2000));

            Ult.AddGroupLabel("Ults");
            Ult.Add("ZedUlt", new CheckBox("Zed Ult"));
            Ult.Add("VladUlt", new CheckBox("Vlad Ult"));
            Ult.Add("FizzUlt", new CheckBox("Fizz Ult"));
            Ult.Add("MordUlt", new CheckBox("Mordekaiser Ult"));
            Ult.Add("PoppyUlt", new CheckBox("Poppy Ult"));
            Ult.Add("UltDelay", new Slider("Delay for Ults", 1000, 0, 3000));


            Obj_AI_Base.OnBuffGain += OnBuffGain;
            Obj_AI_Base.OnBuffLose += OnBuffLose;
            Drawing.OnDraw += Game_OnDraw;
        }

        private static void OnBuffGain(Obj_AI_Base sender, Obj_AI_BaseBuffGainEventArgs args)
        {
            if (!sender.IsMe) return;
            var type = args.Buff.Type;
            var duration = args.Buff.EndTime - Game.Time;
            var Name = args.Buff.Name.ToLower();

            if (type == BuffType.Taunt && Taunt)
            {
                DebuffCount++;
                if (duration >= MinDuration)
                {
                    DoQSS();
                }
            }
            if (type == BuffType.Stun && Stun)
            {
                DebuffCount++;
                if (duration >= MinDuration)
                {
                    DoQSS();
                }
            }
            if (type == BuffType.Snare && Snare)
            {
                DebuffCount++;
                if (duration >= MinDuration)
                {
                    DoQSS();
                }
            }
            if (type == BuffType.Polymorph && Polymorph)
            {
                DebuffCount++;
                if (duration >= MinDuration)
                {
                    DoQSS();
                }
            }
            if (type == BuffType.Blind && Blind)
            {
                DebuffCount++;
                if (duration >= MinDuration)
                {
                    DoQSS();
                }
            }
            if (type == BuffType.Flee && Fear)
            {
                DebuffCount++;
                if (duration >= MinDuration)
                {
                    DoQSS();
                }
            }
            if (type == BuffType.Charm && Charm)
            {
                DebuffCount++;
                if (duration >= MinDuration)
                {
                    DoQSS();
                }
            }
            if (type == BuffType.Suppression && Suppression)
            {
                DebuffCount++;
                if (duration >= MinDuration)
                {
                    DoQSS();
                }
            }
            if (type == BuffType.Silence && Silence)
            {
                DebuffCount++;
                if (duration >= MinDuration)
                {
                    DoQSS();
                }
            }
            if (Name == "zedrdeathmark" && ZedUlt)
            {
                UltQSS();
            }
            if (Name == "vladimirhemoplague" && VladUlt)
            {
                UltQSS();
            }
            if (Name == "fizzmarinerdoom" && FizzUlt)
            {
                UltQSS();
            }
            if (Name == "mordekaiserchildrenofthegrave" && MordUlt)
            {
                UltQSS();
            }
            if (Name == "poppydiplomaticimmunity" && PoppyUlt)
            {
                UltQSS();
            }
        }

        private static void OnBuffLose(Obj_AI_Base sender, Obj_AI_BaseBuffLoseEventArgs args)
        {
            if (!sender.IsMe) return;

            var type = args.Buff.Type;

            if (type == BuffType.Taunt && Taunt)
            {
                DebuffCount--;
            }
            if (type == BuffType.Stun && Stun)
            {
                DebuffCount--;
            }
            if (type == BuffType.Snare && Snare)
            {
                DebuffCount--;
            }
            if (type == BuffType.Polymorph && Polymorph)
            {
                DebuffCount--;
            }
            if (type == BuffType.Blind && Blind)
            {
                DebuffCount--;
            }
            if (type == BuffType.Flee && Fear)
            {
                DebuffCount--;
            }
            if (type == BuffType.Charm && Charm)
            {
                DebuffCount--;
            }
            if (type == BuffType.Suppression && Suppression)
            {
                DebuffCount--;
            }
            if (type == BuffType.Silence && Silence)
            {
                DebuffCount--;
            }
        }

        private static void Game_OnDraw(EventArgs args)
        {
            if (drawk)
            {
                var pos = Drawing.WorldToScreen(Player.Instance.Position);
                Drawing.DrawText(pos.X - 45, pos.Y + 30, keybind ? Color.White : Color.Red, keybind ? "Auto QSS ON" : "Auto QSS OFF");
            }
            Drawing.DrawText(Drawing.Width - 220, Drawing.Height / 13.5f, Color.White, "Min buffs to QSS: " + MinBuff);

        }
        private static void DoQSS()
        {
            if (DebuffCount < MinBuff || !keybind) return;

            if (QSS.IsOwned() && QSS.IsReady() && ObjectManager.Player.CountEnemiesInRange(1800) > 0)
            {
                Core.DelayAction(() => QSS.Cast(), CC["CCDelay"].Cast<Slider>().CurrentValue);
            }

            if (Mercurial.IsOwned() && Mercurial.IsReady() && ObjectManager.Player.CountEnemiesInRange(1800) > 0)
            {
                Core.DelayAction(() => Mercurial.Cast(), CC["CCDelay"].Cast<Slider>().CurrentValue);
            }
        }
        private static void UltQSS()
        {
            if (!keybind) return;

            if (QSS.IsOwned() && QSS.IsReady())
            {
                Core.DelayAction(() => QSS.Cast(), Ult["UltDelay"].Cast<Slider>().CurrentValue);
            }

            if (Mercurial.IsOwned() && Mercurial.IsReady())
            {
                Core.DelayAction(() => Mercurial.Cast(), Ult["UltDelay"].Cast<Slider>().CurrentValue);
            }
        }

    }
}