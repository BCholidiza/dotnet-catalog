using System;

namespace Catalog.Dtos
{
    // This is how the data that will be a response to a user request will look like
    // This is a request
    public record ItemDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public decimal Price { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
    }
}