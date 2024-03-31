using System;
using HarmonyLib;
using Verse;

namespace FemaleBodyVariants
{
	// Token: 0x02000008 RID: 8
	[StaticConstructorOnStartup]
	public static class HarmonyInit
	{
		// Token: 0x06000017 RID: 23 RVA: 0x00002095 File Offset: 0x00000295
		static HarmonyInit()
		{
			new Harmony("FemaleBodyVariants").PatchAll();
		}
	}
}
