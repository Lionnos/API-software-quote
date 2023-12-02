using Microsoft.EntityFrameworkCore;

namespace appservicequotes.Generic
{
    public class DataBaseContextGeneric : DbContext
    {
        public DataBaseContextGeneric ()
        {
            InitAutoMapper.Star();
        }
    }
}
