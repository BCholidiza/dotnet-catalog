using System.ComponentModel.DataAnnotations;

namespace Catalgo.Dtos
{
    // This is how the data that will be a populated by the user will look like
    // This is a response
    // It could have been the same as ItemDto but user but it would not make sense since GUID and CreatedDate are auto-generated
    public record CreateItemDto
    {
        [Required]
        public string Name { get; init; }
        
        [Required]
        [Range(1,1000)]
        public decimal Price { get; init; }
    }
}