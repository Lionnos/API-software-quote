using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace appservicequotes.DataAccess.Entity
{
    [Table("tAssignProject ")]
    public class AssignProject
    {
        #region properties

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string idAssignProject { get; set; }
        public string idProjectTypeMechanism { get; set; }
        public string idProfessional { get; set; }
        public int professionalMonthQuantity { get; set; }
        public bool addProfessional { get; set; }
        public double addProfessionalReducedMonth { get; set; }

        #endregion

        #region parents

        public Professional Professional { get; set; } = null!;

        public ProjectTypeMechanism ProjectTypeMechanism { get; set; } = null!;

        #endregion

        #region childs
        #endregion
    }
}

