using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace appservicequotes.DataAccess.Entity
{
    [Table("tUser")]
    public class User
    {
        #region properties

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string idUser { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        
        #endregion

        #region parents
        #endregion

        #region childs
        #endregion
    }
}
