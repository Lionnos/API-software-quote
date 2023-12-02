using System.ComponentModel.DataAnnotations;

namespace appservicequotes.DataTransferObject.Object
{
    public class DtoProjectType
    {
        #region properties

        public string idProjectType { get; set; }

        [Required(ErrorMessage = "El campo \"Nombre de tipo de proyecto\" es requerido.")]
        public string name { get; set; }

        #endregion

        #region parents
        #endregion

        #region childs

        public ICollection<DtoProjectTypeMechanism> childProjectTypeMechanism { get; } = new List<DtoProjectTypeMechanism>();

        #endregion

        #region additional properties
        #endregion
    }
}
