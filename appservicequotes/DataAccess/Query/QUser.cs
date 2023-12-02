using appservicequotes.DataAccess.Connection;
using appservicequotes.DataTransferObject.Object;

namespace appservicequotes.DataAccess.Query
{
    public class QUser
    {
        public DtoUser GetByEmail(string email)
        {
            using (DataBaseContext dbc = new DataBaseContext())
            {
                /*
                User user = dbc.Users.Where(w => w.email == email).FirstOrDefault();

                DtoUser dtoUser = InitAutoMapper.mapper.Map<DtoUser>(user);

                return dtoUser;
                */
                return InitAutoMapper.mapper.Map<DtoUser>(dbc.Users.Where(w => w.email == email).FirstOrDefault());
            }
        }
    }
}
