switch (ObjectManager.Me.WowSpecialization())
{
	case WoWSpecialization.PaladinProtection:
	case WoWSpecialization.WarriorProtection:
	case WoWSpecialization.MonkBrewmaster:
	case WoWSpecialization.DruidGuardian:
	case WoWSpecialization.DeathknightBlood:
		return true;
	default:
		return false;
}

// Usage: <ScriptCondition>=IsTankScript.cs</ScriptCondition>