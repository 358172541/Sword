using System.ComponentModel.DataAnnotations;

namespace Application.Dtos
{
    public class PackageOpenCreateDto
    {
        [Required]
        public int MemberId { get; set; }

        [Required]
        public int RegionId { get; set; }

        [Required]
        public string Mark { get; set; }

        [Required]
        public int ItemCount { get; set; }

        public string ItemName { get; set; }

        public string SenderName { get; set; }

        public string DeliverPhoneNumber { get; set; }

        public string OrderNumber { get; set; }

        public string ExpressList { get; set; }
    }
}
