using HarmonyLib;
using UnityEngine;
using Verse;
using System;
using System.Collections.Generic;
using RimWorld;

namespace FemaleBodyVariants
{
    [HarmonyPatch(typeof(PawnGraphicSet), "ResolveAllGraphics")]
    public static class PawnGraphicSet_ResolveAllGraphics_Patch
    {
        [HarmonyPostfix]
        public static void Postfix(PawnGraphicSet __instance)
        {
            if (__instance.pawn == null
                || __instance.pawn.story == null
                || __instance.pawn.story.bodyType == null
                || __instance.pawn.story.bodyType.bodyNakedGraphicPath == null)
                return;

            string graphicPath = __instance.pawn.story.bodyType.bodyNakedGraphicPath;
            if (__instance.pawn.gender != Gender.Male && graphicPath != null && !graphicPath.Contains("_Female") && (graphicPath.Contains("_Thin") || graphicPath.Contains("_Fat") || graphicPath.Contains("_Hulk")))
            {
                graphicPath += "_Female";
                __instance.nakedGraphic = GraphicDatabase.Get<Graphic_Multi>(graphicPath, ShaderUtility.GetSkinShader(__instance.pawn.story.SkinColorOverriden), Vector2.one, __instance.pawn.story.SkinColor);
            }
        }
    }

    /*[HarmonyPatch(typeof(PawnRenderNode_Body), "GraphicFor")]
    public static class PawnRenderNode_Body_GraphicFor_Patch
    {
        [HarmonyPostfix]
        public static void Postfix(PawnRenderNode_Body __instance, ref Pawn pawn, ref Graphic __result)
        {
            if ((__result == null)
               || (pawn.Drawer.renderer.CurRotDrawMode == RotDrawMode.Dessicated)
               || (ModsConfig.AnomalyActive && pawn.IsMutant && !pawn.mutant.Def.bodyTypeGraphicPaths.NullOrEmpty())
               || (ModsConfig.AnomalyActive && pawn.IsCreepJoiner && pawn.story.bodyType != null && !pawn.creepjoiner.form.bodyTypeGraphicPaths.NullOrEmpty())
               || (pawn.story?.bodyType?.bodyNakedGraphicPath == null)
               )
            {
                return;
            }

            string graphicPath = pawn.story.bodyType.bodyNakedGraphicPath;
            if (pawn.gender != Gender.Male && graphicPath != null && !graphicPath.Contains("_Female") && (graphicPath.Contains("_Thin") || graphicPath.Contains("_Fat") || graphicPath.Contains("_Hulk")))
            {
                graphicPath += "_Female";
                Shader shader = __instance.ShaderFor(pawn);
                __result = GraphicDatabase.Get<Graphic_Multi>(graphicPath, shader, Vector2.one, __instance.ColorFor(pawn));
            }
        }
    }*/

}
