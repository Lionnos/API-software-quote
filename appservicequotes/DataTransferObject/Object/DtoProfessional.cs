using System.ComponentModel.DataAnnotations;

namespace appservicequotes.DataTransferObject.Object
{
    public class DtoProfessional
    {
        #region properties

        public string idProfessional { get; set; }

        [Required(ErrorMessage = "El campo \"Nombre de profesión\" es requerido.")]
        public string name { get; set; }

        public decimal monthPay { get; set; }

        #endregion

        #region parents
        #endregion

        #region childs

        public ICollection<DtoAssignProject> chilAssignProject { get; } = new List<DtoAssignProject>();

        #endregion

        #region additional properties
        #endregion
    }
}
