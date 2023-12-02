using appservicequotes.DataAccess.Connection;
using appservicequotes.DataAccess.Entity;
using appservicequotes.DataTransferObject.Object;
using Microsoft.EntityFrameworkCore;

namespace appservicequotes.DataAccess.Query
{
    public class QProjectType
    {
        public int Insert(DtoProjectType dtoProjectType)
        {
            using (DataBaseContext dbc = new DataBaseContext())
            {
                dbc.ProjectTypes.Add(InitAutoMapper.mapper.Map<ProjectType>(dtoProjectType));

                return dbc.SaveChanges();
            }
        }

        public bool ExistsByName(string name)
        {
            using (DataBaseContext dbc = new DataBaseContext())
            {
                return dbc.ProjectTypes.Where(w => w.name.Replace(" ", string.Empty) == name.Replace(" ", string.Empty)).Any();
            };
        }

        public List<DtoProjectType> GetAll()
        {
            using (DataBaseContext dbc = new DataBaseContext())
            {
                List<ProjectType> x = dbc.ProjectTypes.Include(i => i.childProjectTypeMechanism).OrderBy(ob => ob.name).ToList();
                //List<ProjectType> y = dbc.ProjectTypes.Include(i => i.childProjectTypeMechanism).ThenInclude(i => i.childAssignProject).OrderBy(ob => ob.name).ToList();

                return InitAutoMapper.mapper.Map<List<DtoProjectType>>(x);
            }
        }

        public int Update(DtoProjectType dtoProjectType)
        {
            using (DataBaseContext dbc = new DataBaseContext())
            {
                ProjectType projectType = dbc.ProjectTypes.Find(dtoProjectType.idProjectType);

                projectType.name = dtoProjectType.name;

                return dbc.SaveChanges();
            }
        }

        public int DeleteOnCascade(DtoProjectType dtoProjectType)
        {
            using (DataBaseContext dbc = new DataBaseContext())
            {
                ProjectType projectType = InitAutoMapper.mapper.Map<ProjectType>(dtoProjectType);

                foreach (ProjectTypeMechanism item in projectType.childProjectTypeMechanism)
                {
                    dbc.AssignProjects.RemoveRange(item.childAssignProject);
                }

                dbc.ProjectTypeMechanisms.RemoveRange(projectType.childProjectTypeMechanism);

                dbc.ProjectTypes.Remove(projectType);

                return dbc.SaveChanges();
            }
        }

        public DtoProjectType GetById(string idProjectType)
        {
            using (DataBaseContext dbc = new DataBaseContext())
            {
                ProjectType x = dbc.ProjectTypes.Include(i => i.childProjectTypeMechanism).ThenInclude(i => i.childAssignProject).Where(w => w.idProjectType == idProjectType).FirstOrDefault();

                return InitAutoMapper.mapper.Map<DtoProjectType>(x);
            }
        }

        public bool ExistsByNameDiffId(string name, string idProjectType)
        {
            using (DataBaseContext dbc = new DataBaseContext())
            {
                return dbc.ProjectTypes.Where(w => w.name.Replace(" ", string.Empty) == name.Replace(" ", string.Empty) && w.idProjectType != idProjectType).Any();
            };
        }

        public DtoProjectType GetByIdForUpdate(string idProjectType)
        {
            using (DataBaseContext dbc = new DataBaseContext())
            {
                ProjectType x = dbc.ProjectTypes.Include(i => i.childProjectTypeMechanism).ThenInclude(i => i.childAssignProject).Where(w => w.idProjectType == idProjectType).FirstOrDefault();

                return InitAutoMapper.mapper.Map<DtoProjectType>(x);
            }
        }

        public DtoProjectType GetByIdForDelete(string idProjectType)
        {
            using (DataBaseContext dbc = new DataBaseContext())
            {
                ProjectType x = dbc.ProjectTypes.Include(i => i.childProjectTypeMechanism).ThenInclude(i => i.childAssignProject).Where(w => w.idProjectType == idProjectType).FirstOrDefault();
                
                return InitAutoMapper.mapper.Map<DtoProjectType>(x);
            }
        }

        public List<DtoProjectType> getForQuote()
        {
            using (DataBaseContext dbc = new DataBaseContext())
            {
                List<ProjectType> x = dbc.ProjectTypes.Include(i => i.childProjectTypeMechanism).ThenInclude(i => i.childAssignProject).ToList();

                return InitAutoMapper.mapper.Map<List<DtoProjectType>>(x);
            }
        }

    }
}
