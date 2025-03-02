using Entities;

namespace Services
{
    public interface IRatingServices
    {
        Task PostRating(Rating rating);
    }
}