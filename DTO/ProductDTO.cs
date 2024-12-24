namespace DTO
{
    public record GetProductDTO(int Id, int CategoryId, string? CategoryName, string? Name, string? Description, double? Price, string? Image);
    //public record GetProductIdDTO(int Id, int CategoryId, string Name, string Description, double Price, string Image);

}
