using System.ComponentModel.DataAnnotations;

namespace SampleAPI.Entities
{
    public class Order
    {

        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        //[MinLength(10, ErrorMessage = "The {0} must be at least {1} characters long.")]
        public string?  Description { get; set; }
        public bool OrderInvoiced { get; set; } = true;
        public bool OrderDeleted { get; set; } = false;
    }
}
