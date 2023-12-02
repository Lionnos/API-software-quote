using System.ComponentModel.DataAnnotations;

namespace appservicequotes.DataTransferObject.Object
{
    public class DtoAssignProject
    {
        #region properties

        public string idAssignProject { get; set; }

        public string idProjectTypeMechanism { get; set; }

        [Required(ErrorMessage = "El valor \"idProfessional\" es requerido.")]
        public string idProfessional { get; set; }

        [Range(1, 36, ErrorMessage = "El valor de la  \"Cantidad de meses requerido para el profesional\" tiene que ser un valor entre {1} y {2}")]
        public int professionalMonthQuantity { get; set; }

        public bool addProfessional { get; set; }

        [Range(0, 10, ErrorMessage = "El valor de la  \"Cantidad de meses de  desarrollo\" tiene que ser un valor entre {1} y {2}")]
        public double addProfessionalReducedMonth { get; set; }

        #endregion

        #region parents

        public DtoProfessional Professional { get; set; } = null!;

        public DtoProjectTypeMechanism ProjectTypeMechanism { get; set; } = null!;

        #endregion

        #region childs
        #endregion

        #region additional properties
        #endregion
    }
}
