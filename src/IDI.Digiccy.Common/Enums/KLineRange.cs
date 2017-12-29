using System.ComponentModel;

namespace IDI.Digiccy.Common.Enums
{
    public enum KLineRange : uint
    {
        [Description("30Day")]
        D30 = 2592000000,
        [Description("7Day")]
        D7 = 604800000,
        [Description("1Day")]
        D1 = 86400000,
        [Description("60Minute")]
        H1 = 3600000,
        [Description("30Minute")]
        M30 = 1800000,
        [Description("15Minute")]
        M15 = 900000,
        [Description("5Minute")]
        M5 = 300000,
        [Description("1Minute")]
        M1 = 60000
    }
}
