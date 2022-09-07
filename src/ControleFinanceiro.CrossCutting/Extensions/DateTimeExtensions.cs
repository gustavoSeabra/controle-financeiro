using System.Runtime.InteropServices;

namespace ControleFinanceiro.CrossCutting.Extensions
{
    public static class DateTimeExtensions
    {
        private static readonly string SouthAmericaZoneId = "E. South America Standard Time";
        private static readonly string SaoPauloZoneId = "America/Sao_Paulo";

        public static DateTime ToBrazilianTimeZone(this DateTime dateTime)
        {
            var zoneId = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? SouthAmericaZoneId : SaoPauloZoneId;
            var targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById(zoneId);
            return TimeZoneInfo.ConvertTime(dateTime, targetTimeZone);
        }

        public static DateTime TruncateMilliseconds(this DateTime dateTime)
            => dateTime.AddTicks(-(dateTime.Ticks % TimeSpan.TicksPerSecond));
    }
}
