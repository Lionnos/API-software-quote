using appservicequotes.DataAccess.Connection;
using appservicequotes.DataAccess.Entity;
using appservicequotes.DataTransferObject.Object;

namespace appservicequotes.DataAccess.Query
{
    public class QProjectTypeMechanism
    {
        public int Insert(DtoProjectTypeMechanism dtoProjectTypeMechanism)
        {
            using (DataBaseContext dbc = new DataBaseContext())
            {
                dbc.ProjectTypeMechanisms.Add(InitAutoMapper.mapper.Map<ProjectTypeMechanism>(dtoProjectTypeMechanism));

                return dbc.SaveChanges();
            }
        }

        public int Update(DtoProjectTypeMechanism dtoProjectTypeMechanism)
        {
            using (DataBaseContext dbc = new DataBaseContext())
            {
                ProjectTypeMechanism projectTypeMechanism = dbc.ProjectTypeMechanisms.Find(dtoProjectTypeMechanism.idProjectTypeMechanism);

                projectTypeMechanism.name = dtoProjectTypeMechanism.name;
                projectTypeMechanism.developerMonthsQuantity = dtoProjectTypeMechanism.developerMonthsQuantity;

                return dbc.SaveChanges();
            }
        }

        public bool ExistsByIdProjectTypeAndNameDiffId(string idProjectType, string name, string idProjectTypeMechanism)
        {
            using (DataBaseContext dbc = new DataBaseContext())
            {
                return dbc.ProjectTypeMechanisms.Where(w => w.idProjectType == idProjectType && w.name.Replace(" ", string.Empty) == name.Replace(" ", string.Empty) && w.idProjectTypeMechanism != idProjectTypeMechanism).Any();
            }
        }

        public bool ExistsByIdProjectTypeAndName(string idProjectType, string name)
        {
            using (DataBaseContext dbc = new DataBaseContext())
            {
                return dbc.ProjectTypeMechanisms.Where( w => w.idProjectType == idProjectType && w.name.Replace(" ", string.Empty) == name.Replace(" ", string.Empty) ).Any();
            }
        }

        public int DeleteByIdProjectTypeNotListIdProjectTypeMechanism(string idProjectType, List<string> listIdProjectTypeMechanism) 
        {
            using (DataBaseContext dbc = new DataBaseContext())
            {
                List<ProjectTypeMechanism> listProjectTypeMechanisms = dbc.ProjectTypeMechanisms.Where(w => w.idProjectType == idProjectType && !listIdProjectTypeMechanism.Contains(w.idProjectTypeMechanism)).ToList();

                if (listIdProjectTypeMechanism.Count > 0)
                {
                    dbc.ProjectTypeMechanisms.RemoveRange(listProjectTypeMechanisms);

                    return dbc.SaveChanges();
                }

                return 0;
            }
        }
    }
}
