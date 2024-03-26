using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Note_Microservices.NoteEntity
{
    public class NoteEntity1
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NoteId { get; set; }
        [Required]
        public string NoteTitle { get; set; }
        public string? NoteDescription { get; set; }
        public string? Color { get; set; }
        public bool Archived { get; set; }=false;
        public int CreatedBy { get; set; }

        [NotMapped]
        public UserEntity1? User { get; set; }
    }
}
