using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Resources
{
    public class RatingRepository : IRatingRepository
    {
        ApiManageContext context;

        public RatingRepository(ApiManageContext apiManageContext)
        {
            context = apiManageContext;
        }
       
        public async Task PostRating(Rating rating)
        {
            await context.Ratings.AddAsync(rating);
            await context.SaveChangesAsync();
            return;
        }
    }
}
