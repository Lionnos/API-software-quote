using appservicequotes.DataAccess.Query;
using appservicequotes.DataTransferObject.Object;
using appservicequotes.Generic;
using appservicequotes.ServiceObject;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace appservicequotes.Controllers
{
    public class ProjectTypeController : ControllerGeneric<SoProjectType>
    {
        [HttpPost]
        [Route("[action]")]
        public ActionResult<SoProjectType> Insert([FromForm] SoProjectType so)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    so.dto.idProjectType = Guid.NewGuid().ToString();

                    // ====================== OpenValidation
                    if (so.dto.childProjectTypeMechanism == null || (so.dto.childProjectTypeMechanism != null && so.dto.childProjectTypeMechanism.Count == 0)) 
                    {
                        _so.mo.listMessage.Add("El tipo de proyecto debe tener por lo menos un mecanismo");
                        _so.mo.Error();

                        return _so;
                    }

                    _so.mo = ValidateDto(so.dto, new List<string>()
                        {
                            nameof(so.dto.name)
                        });

                    //  =================== EndValidation


                    // ========= Open business logic
                    List<string> listNameMechanismTemp = new List<string>(); 
                    // ========= end business logic

                    foreach (DtoProjectTypeMechanism item in so.dto.childProjectTypeMechanism)
                    {
                        item.idProjectTypeMechanism = Guid.NewGuid().ToString();

                        // ====================== OpenValidation
                        if (item.childAssignProject == null || (item.childAssignProject != null && item.childAssignProject.Count == 0))
                        {
                            _so.mo.listMessage.Add("El mecanismo debe tener por lo menos un profesional asignado");
                            _so.mo.Error();

                            return _so;
                        }

                        _so.mo.listMessage.AddRange(ValidateDto(item, new List<string>()
                            {
                                nameof(item.name),
                                nameof(item.developerMonthsQuantity)
                            }).listMessage);
                        //  =================== EndValidation

                        // ========= Open business logic
                        if (listNameMechanismTemp.Where(w => w.Replace(" ", string.Empty) == item.name.Replace(" ", string.Empty)).Any())
                        {
                            _so.mo.listMessage.Add("No se puede repetir el mismo nombre de mecanismo en un tipo de proyecto.");
                            _so.mo.Error();

                            return _so;
                        }

                        listNameMechanismTemp.Add(item.name);
                        // ========= end business logic

                        foreach (DtoAssignProject value in item.childAssignProject)
                        {
                            value.idAssignProject = Guid.NewGuid().ToString();

                            // ====================== OpenValidation
                            _so.mo.listMessage.AddRange(ValidateDto(value, new List<string>()
                            {
                                nameof(value.idProfessional),
                                nameof(value.professionalMonthQuantity),
                                nameof(value.addProfessionalReducedMonth)
                            }).listMessage);
                            //  =================== EndValidation
                        }
                    }

                    if (_so.mo.ExistsMessage())
                    {
                        return _so;
                    }

                    QProjectType qProjectType = new QProjectType();

                    // ========= Open business logic
                    if (qProjectType.ExistsByName(so.dto.name))
                    {
                        _so.mo.listMessage.Add("Este tipo de proyecto ya se encuentra registrado en el sistema.");
                        _so.mo.Error();

                        return _so;
                    }
                    // ========= end business logic

                    qProjectType.Insert(so.dto);

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
        public ActionResult<SoProjectType> GetAll()
        {
            try
            {
                _so.listDto = new QProjectType().GetAll();
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
        [Route("[action]/{idProjectType}")]
        public ActionResult<SoProjectType> GetById(string idProjectType)
        {
            try
            {
                _so.dto = new QProjectType().GetById(idProjectType); 

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

        [HttpPut]
        [Route("[action]")]
        public ActionResult<SoProjectType> UpdateOnCascade([FromForm] SoProjectType so)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    if (so.dto.childProjectTypeMechanism == null || (so.dto.childProjectTypeMechanism != null && so.dto.childProjectTypeMechanism.Count == 0))
                    {
                        _so.mo.listMessage.Add("El tipo de proyecto debe tener por lo menos un mecanismo");
                        _so.mo.Error();

                        return _so;
                    }

                    _so.mo = ValidateDto(so.dto, new List<string>()
                    {
                        nameof(so.dto.name)
                    });

                    if (_so.mo.ExistsMessage())
                    {
                        return _so;
                    }

                    QProjectType qProjectType = new QProjectType();
                    QProjectTypeMechanism qProjectTypeMechanism = new QProjectTypeMechanism();
                    QAssignProject qAssignProject = new QAssignProject();

                    if ( qProjectType.ExistsByNameDiffId(so.dto.name, so.dto.idProjectType) )
                    {
                        _so.mo.listMessage.Add("Este tipo de proyecto ya se encuentra registrado.");
                        _so.mo.Error();

                        return _so;
                    }

                    qProjectType.Update(so.dto);

                    List<string> listIdProjectTypeMechanismTemp = new List<string>();
                            
                    foreach (DtoProjectTypeMechanism item in so.dto.childProjectTypeMechanism)
                    {
                        if (item.idProjectTypeMechanism != null || item.idProjectTypeMechanism != string.Empty)
                        {
                            listIdProjectTypeMechanismTemp.Add(item.idProjectTypeMechanism);
                        }
                    }

                    qProjectTypeMechanism.DeleteByIdProjectTypeNotListIdProjectTypeMechanism(so.dto.idProjectType, listIdProjectTypeMechanismTemp);

                    foreach (DtoProjectTypeMechanism item in so.dto.childProjectTypeMechanism)
                    {
                        if (item.childAssignProject == null || (item.childAssignProject != null && item.childAssignProject.Count == 0))
                        {
                            _so.mo.listMessage.Add("El mecanismo debe tener por lo menos un profesional asignado");
                            _so.mo.Error();

                            return _so;
                        }

                        _so.mo.listMessage.AddRange(ValidateDto(item, new List<string>()
                        {
                            nameof(item.name),
                            nameof(item.developerMonthsQuantity)
                        }).listMessage);

                        if (_so.mo.ExistsMessage())
                        {
                            return _so;
                        }

                        if (
                            (item.idProjectTypeMechanism != null && item.idProjectTypeMechanism != string.Empty && qProjectTypeMechanism.ExistsByIdProjectTypeAndNameDiffId(so.dto.idProjectType, item.name, item.idProjectTypeMechanism))
                            || ((item.idProjectTypeMechanism == null && item.idProjectTypeMechanism == string.Empty) && qProjectTypeMechanism.ExistsByIdProjectTypeAndName(so.dto.idProjectType, item.name))
                            )
                        {
                            _so.mo.listMessage.Add("No se puede repetir el mismo nombre de mecanismo en un tipo de proyecto.");
                            _so.mo.Error();

                            return _so;
                        }

                        if (item.idProjectTypeMechanism == null || item.idProjectTypeMechanism == string.Empty)
                        {
                            item.idProjectTypeMechanism = Guid.NewGuid().ToString();
                            item.idProjectType = so.dto.idProjectType;

                            qProjectTypeMechanism.Insert(item);
                        }
                        else
                        {
                            qProjectTypeMechanism.Update(item);
                        }

                        List<string> listIdAssignProjectTemp = new List<string>();

                        foreach (DtoAssignProject value in item.childAssignProject)
                        {
                            if (value.idAssignProject == null || value.idAssignProject == string.Empty)
                            {
                                listIdAssignProjectTemp.Add(value.idAssignProject);
                            }
                        }

                        qAssignProject.DeleteByIdProjectTypeMechanismNotListIdAssignProject(item.idProjectType, listIdAssignProjectTemp);

                        foreach (DtoAssignProject value in item.childAssignProject)
                        {
                            if (value.idAssignProject == null || value.idAssignProject == string.Empty)
                            {
                                value.idAssignProject = Guid.NewGuid().ToString();
                                value.idProjectTypeMechanism = item.idProjectTypeMechanism;

                                _so.mo.listMessage.AddRange(ValidateDto(value, new List<string>()
                                {
                                    nameof(value.idProfessional),
                                    nameof(value.professionalMonthQuantity),
                                    nameof(value.addProfessionalReducedMonth)
                                }).listMessage);

                                if (_so.mo.ExistsMessage())
                                {
                                    return _so;
                                }

                                qAssignProject.Insert(value);
                            }
                            else
                            {
                                _so.mo.listMessage.AddRange(ValidateDto(value, new List<string>()
                                {
                                    nameof(value.professionalMonthQuantity),
                                    nameof(value.addProfessionalReducedMonth)
                                }).listMessage);

                                if (_so.mo.ExistsMessage())
                                {
                                    return _so;
                                }
                                qAssignProject.Update(value);
                            }
                        }
                    }

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
        [Route("[action]/{idProjectType}")]
        public ActionResult<SoProjectType> Delete(string idProjectType)
        {
            try
            {
                QProjectType qprojectType = new QProjectType();

                DtoProjectType dtoProjectType = qprojectType.GetByIdForDelete(idProjectType);

                qprojectType.DeleteOnCascade(dtoProjectType);

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

        [HttpGet]
        [Route("[action]")]
        public ActionResult<SoProjectType> GetForQuote()
        {
            try
            {
                _so.listDto = new QProjectType().getForQuote();

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
