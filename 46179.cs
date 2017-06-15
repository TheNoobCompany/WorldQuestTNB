       bool running = true;
        while (running)
        {
            List<WoWUnit> hammers = ObjectManager.GetWoWUnitByEntry(116614);
            if (hammers.Count > 0)
            {
                Logging.WriteDebug("Hammer found, ima go use it.");
                WoWUnit unit = ObjectManager.GetNearestWoWUnit(ObjectManager.GetWoWUnitByEntry(116614));
                uint baseAddress = MovementManager.FindTarget(unit);
                Thread.Sleep(2000);
                Interact.InteractWith(baseAddress);
                Thread.Sleep(2000);

                Logging.WriteDebug("Spell is usable, trying to find a fel curraptor!");
                unit = ObjectManager.GetNearestWoWUnit(ObjectManager.GetWoWUnitByEntry(118985));
                baseAddress = MovementManager.FindTarget(unit);
                Thread.Sleep(2000);
                Lua.RunMacroText("/click ExtraActionButton1");
                Thread.Sleep(7000);
            }
            else
            {
                WoWUnit unit = ObjectManager.GetNearestWoWUnit(ObjectManager.GetWoWUnitByEntry(116580));
                if (unit.IsValid)
                {
                    Logging.WriteDebug("Hammer not found, trying to find Felguard Sentry and get thier hammer.");
                    uint baseAddress = MovementManager.FindTarget(unit);
                    Thread.Sleep(2000);
                    Interact.InteractWith(baseAddress);
                    Thread.Sleep(10000);
                }
                else
                {

                }
            }
        }