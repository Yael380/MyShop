using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;

namespace Services
{
    public class RatingServices : IRatingServices
    {
        IRatingRepository ratingRepository;
        public RatingServices(IRatingRepository _ratingRepository)
        {
            ratingRepository = _ratingRepository;
        }

        public async Task PostRating(Rating rating)
        {
            await ratingRepository.PostRating(rating);
        }
    }
}
