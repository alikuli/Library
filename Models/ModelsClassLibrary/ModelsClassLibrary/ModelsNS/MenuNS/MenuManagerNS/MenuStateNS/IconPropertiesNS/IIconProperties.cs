using System;
namespace ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS.MenuStateNS.IconPropertiesNS
{
    public interface IIconProperties
    {
        string AnchorButtonClassWithIcon { get; }
        string BadgeClass { get; }
        string ButtonClass { get; }
        string ButtonClassWithIcon { get; }
        string ButtonColorClass { get; set; }
        string ButtonDisableConditionally { get; }
        int CountIcon { get; set; }
        bool HasPressed { get; set; }
        string IconPrefix { get; set; }
        string Id { get; }
        string IdCount { get; }
        void InitEachTime(int count);
        string OnClick { get; }
        string Url { get; set; }
        string UrlCount { get; set; }
    }
}
