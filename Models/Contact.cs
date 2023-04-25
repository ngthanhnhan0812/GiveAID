using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GiveAID.Models
{
    [Table("contact")]
    [PrimaryKey("id")]
    public class Contact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string? email { get; set; }

        [Required]
        [StringLength(15)]
        public string? phone_number { get; set; }

        [Required]
        [StringLength(50)]
        public string? subject { get; set; }

        [Required]
        [MaxLength]
        public string? body { get; set; }

        public Int16 status { get; set; } = 2;

        [Required]
        [ForeignKey("mem_id")]
        public int mem_id { get; set; }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime created_at { get; set; }

        public Member? Member { get; set; }
    }
}
