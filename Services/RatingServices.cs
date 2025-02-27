using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resources;

namespace Services
{
    public class RatingServices : IRatingServices
    {
        IRatingRepository ratingRepository;
        public RatingServices(IRatingRepository _ratingRepository)
        {
            ratingRepository = _ratingRepository;
        }

        public void PostRating(Rating rating)
        {
            ratingRepository.PostRating(rating);
        }
    }
}
