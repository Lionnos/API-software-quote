using appservicequotes.DataAccess.Query;
using appservicequotes.Generic;
using appservicequotes.Helper;
using appservicequotes.ServiceObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace appservicequotes.Controllers
{
    public class UserController : ControllerGeneric<SoUser>
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("[action]")]
        public ActionResult<SoUser> Login([FromForm] SoUser so)
        {
            try
            {
                _so.dto = new QUser().GetByEmail(so.dto.email);

                if ( _so.dto != null && _so.dto.password == so.dto.password)
                {
                    _so.dto.password = null;

                    //  ================= PROTECCION DE JWT
                    Claim[] claim = new[]
                    {
                        new Claim("UserTokenData", JsonConvert.SerializeObject(_so.dto))
                    };

                    SecurityToken token = new JwtSecurityToken
                    (
                        audience: "API_Rest",
                        claims: claim,
                        expires: DateTime.Now.AddMinutes(30),
                        notBefore: DateTime.Now,
                        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettings.GetJwtSecret())), SecurityAlgorithms.HmacSha256)
                    );

                    _so.token = new JwtSecurityTokenHandler().WriteToken(token);
                    //  =================

                    _so.mo.listMessage.Add("Bienvenido al sistema.");
                    _so.mo.Success();
                }
                else
                {
                    _so.dto = null;
                    _so.mo.listMessage.Add("Usuario o contraseña incorrecta.");
                    _so.mo.Error();
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
    }
}
