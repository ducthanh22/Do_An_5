using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class BasedbDto
    {
        [Key]
        public Guid Id { get; set; }
        public int? ActiveFlag { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? Created { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime? Modified { get; set; }


    }
}
