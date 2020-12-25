using SushiShop.Core.Data.Models.Common;

namespace SushiShop.Core.Data.Models.Orders
{
    public class PickupPoint
    {
        public PickupPoint(
            string? address,
            string[]? phones,
            string? worktimeState,
            string[]? worktime,
            Coordinates? coordinates)
        {
            Address = address;
            Phones = phones;
            WorktimeState = worktimeState;
            Worktime = worktime;
            Coordinates = coordinates;
        }

        public string? Address { get; }

        public string[]? Phones { get; }

        public string? WorktimeState { get; }

        public string[]? Worktime { get; }

        public Coordinates? Coordinates { get; }
    }
}