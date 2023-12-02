using appservicequotes.DataAccess.Query;
using appservicequotes.Generic;
using appservicequotes.ServiceObject;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace appservicequotes.Controllers
{
    public class ProfessionalController : ControllerGeneric<SoProfessional>
    {
        [HttpPost]
        [Route("[action]")]
        public ActionResult<SoProfessional> Insert([FromForm] SoProfessional so)
        {
            try
            {
                using(TransactionScope ts = new TransactionScope())
                {
                    so.dto.idProfessional = Guid.NewGuid().ToString();

                    //  validacion de datos
                    //  ===================
                    _so.mo = ValidateDto(so.dto, new List<string>() 
                    { 
                        nameof(so.dto.name) 
                    });

                    if (_so.mo.ExistsMessage())
                    {
                        return _so;
                    }

                    QProfessional qProfessional = new QProfessional();

                    if (qProfessional.ExistsByName(so.dto.name))
                    {
                        _so.mo.listMessage.Add("Este profesional ya se encuentra registrado en el sistema.");
                        _so.mo.Error();

                        return _so;
                    }
                    //  ======================

                    qProfessional.Insert(so.dto);

                    _so.mo.listMessage.Add("Operación realizada correctamente.");
                    _so.mo.Success();

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                _so.mo.listMessage.Add("Ocurrió un error inesperado. Estamos trabajando para resolverlo.");
                _so.mo.listMessage.Add("ERROR_EXCEPTION_-_-_" + ex.Message);
                _so.mo.Error();
            }

            return _so;
        }

        [HttpGet]
        [Route("[action]")]
        public ActionResult<SoProfessional> GetAll()
        {
            try
            {
                _so.listDto = new QProfessional().GetAll();
            }
            catch (Exception ex)
            {
                _so.mo.listMessage.Add("Ocurrió un error inesperado. Estamos trabajando para resolverlo.");
                _so.mo.listMessage.Add("ERROR_EXCEPTION_-_-_" + ex.Message);
                _so.mo.Error();
            }

            return _so;
        }

        [HttpGet]
        [Route("[action]/{idProfessional}")]
        public ActionResult<SoProfessional> GetById(string idProfessional)
        {
            try
            {
                _so.dto = new QProfessional().GetById(idProfessional);
            }
            catch (Exception ex)
            {
                _so.mo.listMessage.Add("Ocurrió un error inesperado. Estamos trabajando para resolverlo.");
                _so.mo.listMessage.Add("ERROR_EXCEPTION_-_-_" + ex.Message);
                _so.mo.Error();
            }

            return _so;
        }

        [HttpPut]
        [Route("[action]")]
        public ActionResult<SoProfessional> Update([FromForm] SoProfessional so)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    _so.mo = ValidateDto(so.dto, new List<string>()
                    {
                        nameof(so.dto.name)
                    });

                    if (_so.mo.ExistsMessage())
                    {
                        return _so;
                    }

                    QProfessional qProfessional = new QProfessional();

                    if (qProfessional.ExistsByNameDiffId(so.dto.name, so.dto.idProfessional))
                    {
                        _so.mo.listMessage.Add("Este profesional ya se encuentra registrado en el sistema.");
                        _so.mo.Error();

                        return _so;
                    }

                    qProfessional.Update(so.dto);

                    _so.mo.listMessage.Add("Operación realizada correctamente.");
                    _so.mo.Success();

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                _so.mo.listMessage.Add("Ocurrió un error inesperado. Estamos trabajando para resolverlo.");
                _so.mo.listMessage.Add("ERROR_EXCEPTION_-_-_" + ex.Message);
                _so.mo.Error();
            }

            return _so;
        }

        [HttpDelete]
        [Route("[action]/{idProfessional}")]
        public ActionResult<SoProfessional> Delete(string idProfessional)
        {
            try
            {
                new QProfessional().Delete(idProfessional);

                _so.mo.listMessage.Add("Operación realizada correctamente.");
                _so.mo.Success();
            }
            catch (Exception ex)
            {
                _so.mo.listMessage.Add("Ocurrió un error inesperado. Estamos trabajando para resolverlo.");
                _so.mo.listMessage.Add("ERROR_EXCEPTION_-_-_" + ex.Message);
                _so.mo.Error();
            }

            return _so;
        }
    }
}