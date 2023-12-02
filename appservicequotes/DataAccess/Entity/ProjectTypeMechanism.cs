using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace appservicequotes.DataAccess.Entity
{
    [Table("tProjectTypeMechanism")]
    public class ProjectTypeMechanism
    {
        #region properties

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string idProjectTypeMechanism { get; set; }
        public string idProjectType { get; set; }
        public string name { get; set; }
        public double developerMonthsQuantity { get; set; }

        #endregion

        #region parents

        public ProjectType ProjectType { get; set; } = null!;

        #endregion

        #region childs

        public ICollection<AssignProject> childAssignProject { get; } = new List<AssignProject>();

        #endregion
    }
}
