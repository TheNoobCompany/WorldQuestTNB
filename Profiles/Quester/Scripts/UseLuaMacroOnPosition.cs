/*******************************************************
 * USAGE: 
 * *****************************************************
    - OPTIONS:
    WaitMs: Will wait xxxx millisecond after having done the actions
    Range: Distance to be to perform actions

    - On player position:
    <Objective>CSharpScript</Objective>
    <Script>=UseLuaMacroOnPosition.cs</Script>
    <LuaMacro>/click TheButtonYouWantToClick</LuaMacro>
    
    - On given position
    <Objective>CSharpScript</Objective>
    <Script>=UseLuaMacroOnPosition.cs</Script>
    <LuaMacro>/click TheButtonYouWantToClick</LuaMacro>
    <Position>
        <X></X>
        <Y></Y>
        <Z></Z>
    </Position>

    - On previously killed mob or an NPC, will target the mob first if the action is on target
    <Objective>CSharpScript</Objective>
    <Script>=UseLuaMacroOnPosition.cs</Script>
    <LuaMacro>/click TheButtonYouWantToClick</LuaMacro>
    <Entry>
        <int>xxx</int>
    </Entry>
    <Hotspots>
        <Point>
            <X></X>
            <Y></Y>
            <Z></Z>
        </Point>
    </Hotspots>
 * 
 */ 

if (!MovementManager.InMovement)
{
    if (questObjective.LuaMacro != null && questObjective.LuaMacro.Length > 0) 
    {
        Point position = null;
        float range = (questObjective.Range > 0) ? questObjective.Range : 5f;
        if (questObjective.Position != null && questObjective.Position.IsValid)
        {
            position = questObjective.Position;
            if(position.DistanceTo(ObjectManager.Me.Position) > range){
                Npc target = new Npc();
                target.Name = "Objective position";
                target.Position = questObjective.Position;
                MovementManager.FindTarget(ref target, range);
            }
        } 
        else if (questObjective.Entry.Count > 0)
        {
            /**
             * need access to have access to:
             * - nManagerSetting (line 23,48)
             * - lockedTarget (line 34)
             * - Statistics^(line 53)
             * 
            WoWUnit wowUnit = ObjectManager.GetNearestWoWUnit(ObjectManager.GetWoWUnitByEntry(questObjective.Entry, questObjective.IsDead), questObjective.IgnoreBlackList);   
            if (!IsInAvoidMobsList(wowUnit) && !nManagerSetting.IsBlackListedZone(wowUnit.Position) &&
                !nManagerSetting.IsBlackListed(wowUnit.Guid) && wowUnit.IsValid)
            {
                if(!wowUnit.isHostile || (!wowUnit.IsAlive && (!wowUnit.IsTapped || (wowUnit.IsTapped && wowUnit.IsTappedByMe))))
                {
                    position = wowUnit.Position;
                    Interact.InteractWith(wowUnit.GetBaseAddress);
                }
                else if (questObjective.CanPullUnitsAlreadyInFight || !wowUnit.InCombat)
                {
                    if (lockedTarget == null)
                    {
                        lockedTarget = wowUnit;
                        MovementManager.FindTarget(wowUnit, CombatClass.GetAggroRange);
                        Thread.Sleep(100);
                        if (MovementManager.InMovement)
                        {
                            return;
                        }
                    }
                    Logging.Write("Attacking Lvl " + wowUnit.Level + " " + wowUnit.Name);
                    UInt128 Unkillable = Fight.StartFight(wowUnit.Guid);
                    if (!wowUnit.IsDead && Unkillable != 0 && wowUnit.HealthPercent == 100.0f)
                    {
                        nManagerSetting.AddBlackList(Unkillable, 3*60*1000);
                        Logging.Write("Can't reach " + wowUnit.Name + ", blacklisting it.");
                    }
                    else if (wowUnit.IsDead)
                    {
                        Statistics.Kills++;
                        if (!wowUnit.IsTapped || (wowUnit.IsTapped && wowUnit.IsTappedByMe))
                        {
                            position = wowUnit.Position;
                            Interact.InteractWith(wowUnit.GetBaseAddress);  
                        }
                        Thread.Sleep(50 + Usefuls.Latency);
                        while (!ObjectManager.Me.IsMounted && ObjectManager.Me.InCombat &&
                                ObjectManager.GetNumberAttackPlayer() <= 0)
                        {
                            Thread.Sleep(50);
                        }
                        Fight.StopFight();
                    }
                   lockedTarget = null;
                }
            }
            else if (!MovementManager.InMovement && questObjective.Hotspots.Count > 0)
            {
                MountTask.Mount();
                if (questObjective.Hotspots[nManager.Helpful.Math.NearestPointOfListPoints(questObjective.Hotspots, ObjectManager.Me.Position)].DistanceTo(ObjectManager.Me.Position) > 5)
                {
                    MovementManager.Go(PathFinder.FindPath(questObjective.Hotspots[nManager.Helpful.Math.NearestPointOfListPoints(questObjective.Hotspots, ObjectManager.Me.Position)]));
                }
                else
                {
                    MovementManager.GoLoop(questObjective.Hotspots);
                }
            }
            **/
            Logging.Write("UseLuaMacroOnPosition not yet implement for mobs.");
        } else {
            position = ObjectManager.Me.Position;
        }
        if (position != null && position.IsValid && position.DistanceTo(ObjectManager.Me.Position) <= range)
        {
            Lua.RunMacroText(questObjective.LuaMacro);
            Thread.Sleep(100);
            ClickOnTerrain.ClickOnly(position);
            if (questObjective.WaitMs > 0)
                Thread.Sleep(questObjective.WaitMs);
            questObjective.IsObjectiveCompleted = true;
        }
        Thread.Sleep(50);
    } 
    else 
    {
        Logging.Write("UseLuaMacroOnPosition require 'LuaMacro'.");
        questObjective.IsObjectiveCompleted = true;
    }
}

