using System.ComponentModel.DataAnnotations;

namespace appservicequotes.DataTransferObject.Object
{
    public class DtoProjectTypeMechanism
    {
        #region properties

        public string idProjectTypeMechanism { get; set; }

        public string idProjectType { get; set; }

        [Required(ErrorMessage = "El campo \"Nombre de mecanismo\" es requerido.")]
        public string name { get; set; }

        [Range(1,36, ErrorMessage = "El valor de la  \"Cantidad de meses de  desarrollo\" tiene que ser un valor entre {1} y {2}")]
        public double developerMonthsQuantity { get; set; }

        #endregion

        #region parents

        public DtoProjectType ProjectType { get; set; } = null!;

        #endregion

        #region childs

        public ICollection<DtoAssignProject> childAssignProject { get; } = new List<DtoAssignProject>();

        #endregion

        #region additional properties
        #endregion
    }
}
