
namespace DTO
{
    public record GetOrderDTO(int Id, DateOnly Date,float Sum,int UserId, ICollection<GetOrderItemDTO> OrderItems);//, int ProductId);
    public record PostOrderDTO(int UserId, ICollection<GetOrderItemDTO> OrderItems);//, int[] Product_Id);



    // DTO - orderItem for order
    public record GetOrderItemDTO(int ProductId, int? Quantity);
}

//חסר בלוגיקה של הזמנה סכום ותאריך
