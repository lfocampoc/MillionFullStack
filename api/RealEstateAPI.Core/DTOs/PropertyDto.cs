namespace RealEstateAPI.Core.DTOs
{
    public class PropertyDto
    {
        public string Id { get; set; } = string.Empty;
        public string IdOwner { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string CodeInternal { get; set; } = string.Empty;
        public int Year { get; set; }
        public string? Image { get; set; }
        public OwnerDto? Owner { get; set; }
    }
}
