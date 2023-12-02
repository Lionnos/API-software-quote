using appservicequotes.DataTransferObject.AdditionalObject;

namespace appservicequotes.Generic
{
    public class SoGeneric<Dto>
    {
        public SoGeneric()
        {
            mo = new DtoMessage();
        }

        public DtoMessage mo { get; set; }
        public Dto dto { get; set; }
        public List<Dto> listDto { get; set; }
    }
}
