using FairyGUI;

namespace PluginSet.FGuiToLua
{
#if FAIRYGUI_TMPRO
    public static class TextFormatHelper
    {
        public static float GetFaceDilate(this TextFormat self)
        {
            return self.faceDilate;
        }

        public static void SetFaceDilate(this TextFormat self, float value)
        {
            self.faceDilate = value;
        }

        public static float GetOutlineSoftness(this TextFormat self)
        {
            return self.outlineSoftness;
        }

        public static void SetOutlineSoftness(this TextFormat self, float value)
        {
            self.outlineSoftness = value;
        }

        public static float GetUnderlaySoftness(this TextFormat self)
        {
            return self.underlaySoftness;
        }

        public static void SetUnderlaySoftness(this TextFormat self, float value)
        {
            self.underlaySoftness = value;
        }
    }
#endif
}