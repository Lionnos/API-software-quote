using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace appservicequotes.DataAccess.Entity
{
    [Table("tProfessional")]
    public class Professional
    {
        #region properties

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string idProfessional { get; set; }
        public string name { get; set; }
        public decimal monthPay { get; set; }


        #endregion

        #region parents
        #endregion

        #region childs

        public ICollection<AssignProject> childAssignProject { get; } = new List<AssignProject>();

        #endregion
    }
}
