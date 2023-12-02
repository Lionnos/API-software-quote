using appservicequotes.DataTransferObject.Object;
using appservicequotes.Generic;

namespace appservicequotes.ServiceObject
{
    public class SoUser : SoGeneric<DtoUser>
    {
        public string token { get; set; }
    }
}
