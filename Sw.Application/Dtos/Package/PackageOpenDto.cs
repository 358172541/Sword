namespace Application.Dtos
{
    public class PackageOpenDto
    {
        public int Id { get; set; }

        public string ExpressNumber { get; set; }

        public string Mark { get; set; }

        public int MemberId { get; set; }

        public string MemberAccount { get; set; }

        public int ItemCount { get; set; }

        public decimal Weight { get; set; }

        public int RegionId { get; set; }

        public string RegionName { get; set; }

        public decimal Volume { get; set; }

        public string ItemName { get; set; }

        public int UserId { get; set; }

        public int UserAccount { get; set; }
    }
}
