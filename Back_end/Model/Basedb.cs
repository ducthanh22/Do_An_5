using System.ComponentModel.DataAnnotations;

namespace Back_end.Model
{
    public class Basedb
    {
        [Key]
        public int Id { get; set; }
        public int? ActiveFlag { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? Created { get; set; }
        public int? ModifiedBy { get; set; }
        

    }
}
