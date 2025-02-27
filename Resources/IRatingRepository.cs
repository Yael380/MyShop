using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resources
{
    public interface IRatingRepository
    {
        Task PostRating(Rating rating);
    }
}
