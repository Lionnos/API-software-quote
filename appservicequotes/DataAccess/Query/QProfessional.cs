using appservicequotes.DataAccess.Connection;
using appservicequotes.DataAccess.Entity;
using appservicequotes.DataTransferObject.Object;

namespace appservicequotes.DataAccess.Query
{
    public class QProfessional
    {
        public int Insert(DtoProfessional dtoProfessional)
        {
            using (DataBaseContext dbc = new DataBaseContext())
            {
                dbc.Professionals.Add(InitAutoMapper.mapper.Map<Professional>(dtoProfessional));

                return dbc.SaveChanges();
            }
        }

        public List<DtoProfessional> GetAll()
        {
            using (DataBaseContext dbc = new DataBaseContext())
            {
                return InitAutoMapper.mapper.Map<List<DtoProfessional>>(dbc.Professionals.OrderBy(ob => ob.name).ToList());
            }
        }

        public DtoProfessional GetById(string idProfessional)
        {
            using (DataBaseContext dbc = new DataBaseContext())
            {
                return InitAutoMapper.mapper.Map<DtoProfessional>(dbc.Professionals.Find(idProfessional));
            }
        }

        public int Update(DtoProfessional dtoProfessional)
        {
            using (DataBaseContext dbc = new DataBaseContext())
            {
                Professional professional = dbc.Professionals.Find(dtoProfessional.idProfessional);

                professional.name = dtoProfessional.name;
                professional.monthPay = dtoProfessional.monthPay;

                return dbc.SaveChanges();
            }
        }

        public int Delete(string idProfessional)
        {
            using (DataBaseContext dbc = new DataBaseContext())
            {
                Professional professional = dbc.Professionals.Find(idProfessional);

                if (professional != null)
                {
                    dbc.Professionals.Remove(professional);
                    return dbc.SaveChanges();
                }

                return 0;
            }
        }

        public bool ExistsByName(string name)
        {
            using (DataBaseContext dbc = new DataBaseContext())
            {
                return dbc.Professionals.Where(w => w.name.Replace(" ", string.Empty) == name.Replace(" ", string.Empty)).Any();
            };
        }

        public bool ExistsByNameDiffId(string name, string idProfessional)
        {
            using (DataBaseContext dbc = new DataBaseContext())
            {
                return dbc.Professionals.Where(w => w.name.Replace(" ", string.Empty) == name.Replace(" ", string.Empty) && w.idProfessional != idProfessional).Any();
            };
        }

    }
}
