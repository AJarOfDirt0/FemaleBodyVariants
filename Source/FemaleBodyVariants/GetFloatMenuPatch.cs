using HarmonyLib;
using UnityEngine;
using Verse;

namespace FemaleBodyVariants
{
    [HarmonyPatch(typeof(PawnRenderNode_Body), "GraphicFor")]
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
    }

}
