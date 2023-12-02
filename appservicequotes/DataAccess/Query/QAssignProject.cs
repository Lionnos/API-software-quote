using appservicequotes.DataAccess.Connection;
using appservicequotes.DataAccess.Entity;
using appservicequotes.DataTransferObject.Object;

namespace appservicequotes.DataAccess.Query
{
    public class QAssignProject
    {
        public int Insert(DtoAssignProject dtoAssignProject)
        {
            using (DataBaseContext dbc = new DataBaseContext())
            {
                dbc.AssignProjects.Add(InitAutoMapper.mapper.Map<AssignProject>(dtoAssignProject));

                return dbc.SaveChanges();
            }
        }

        public int Update(DtoAssignProject dtoAssignProject)
        {
            using (DataBaseContext dbc = new DataBaseContext())
            {
                AssignProject assignProject = dbc.AssignProjects.Find(dtoAssignProject.idAssignProject);

                assignProject.professionalMonthQuantity = dtoAssignProject.professionalMonthQuantity;
                assignProject.addProfessional = dtoAssignProject.addProfessional;
                assignProject.addProfessionalReducedMonth = dtoAssignProject.addProfessionalReducedMonth;

                return dbc.SaveChanges();
            }
        }

        public int DeleteByIdProjectTypeMechanismNotListIdAssignProject(string idProjectTypeMechanism, List<string> listIdAssignProject)
        {
            using (DataBaseContext dbc = new DataBaseContext())
            {
                List<AssignProject> listAssignProjects = dbc.AssignProjects.Where(w => w.idProjectTypeMechanism == idProjectTypeMechanism && !listIdAssignProject.Contains(w.idAssignProject)).ToList();

                if (listIdAssignProject.Count > 0)
                {
                    dbc.AssignProjects.RemoveRange(listAssignProjects);

                    return dbc.SaveChanges();
                }

                return 0;
            }
        }
    }
}
