
namespace DTO
{
    public record GetOrderDTO(int Id, DateOnly Date,float Sum,int UserId, ICollection<GetOrderItemDTO> OrderItems);//, int ProductId);
    public record PostOrderDTO(int Id, DateTime Date, float Sum, int UserId);//, int[] Product_Id);



    // DTO - orderItem for order
    public record GetOrderItemDTO(int Id, int ProductId, int? Quantity);
}

