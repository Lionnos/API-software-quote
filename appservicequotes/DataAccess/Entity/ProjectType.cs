using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace appservicequotes.DataAccess.Entity
{
    [Table("tProjectType")]
    public class ProjectType
    {
        #region properties

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string idProjectType { get; set; }
        public string name { get; set; }

        #endregion

        #region parents
        #endregion

        #region childs

        public ICollection<ProjectTypeMechanism> childProjectTypeMechanism { get; } = new List<ProjectTypeMechanism>();

        #endregion
    }
}
