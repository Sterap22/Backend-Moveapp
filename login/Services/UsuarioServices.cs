using login.auth;
using login.Connections;
using login.DTOs;
using login.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace login.Services
{
    public class UsuarioServices : IUsaurioServices
    {
        ConectionsModels context;

        public UsuarioServices(ConectionsModels dbcontext)
        {
            context = dbcontext;
        }
        public async Task<RespuestaDTO> Save(Usuario uss)
        {
            var respuestaVi = new RespuestaDTO();
            try
            {
                uss.vigente = true;
                uss.fechaCreacion = DateTime.Now;
                context.usuario.Add(uss);
                await context.SaveChangesAsync();
                respuestaVi.mensaje = "Se creo el usuario correctamente";
                return respuestaVi;
            }
            catch (Exception ex)
            {
                respuestaVi.mensaje = ex.Message;
                return respuestaVi;
            }
        }
        public async Task<RespuestaDTO> Update(Usuario uss)
        {
            var respuestaVi = new RespuestaDTO();
            try
            {
                var usuarioAc = context.usuario.Find(uss.Id);

                if (usuarioAc != null)
                {
                    usuarioAc.clave = uss.clave;
                    usuarioAc.correo = uss.correo;

                    await context.SaveChangesAsync();
                    respuestaVi.mensaje = "Se actualizo el usuario correctamente";
                    return respuestaVi;
                }
                else
                {
                    respuestaVi.mensaje = "No se actualizo el usuario";
                    return respuestaVi;
                }
            }
            catch (Exception ex)
            {
                respuestaVi.mensaje = ex.Message;
                return respuestaVi;
            }

        }
        public async Task<RespuestaDTO> Delete(int uss)
        {
            var respuestaVi = new RespuestaDTO();
            try
            {
                var usuarioAc = context.usuario.Find(uss);

                if (usuarioAc != null)
                {
                    context.Remove(usuarioAc);
                    //await context.SaveChangesAsync();
                    respuestaVi.mensaje = "Se elimino el usuario correctamente";
                    return respuestaVi;
                }
                else
                {
                    respuestaVi.mensaje = "No se encontro el usuario";
                    return respuestaVi;
                }
            }
            catch (Exception ex)
            {
                respuestaVi.mensaje = ex.Message;
                return respuestaVi;
            }
        }
        public async Task<RespuestaDTO> LoginSession(LoginDTO uss, IConfiguration configuration)
        {
            var respuestaVi = new RespuestaDTO();
            try
            {
                var usuarioAc = await context.usuario.FirstOrDefaultAsync(x => x.correo == uss.correo && x.clave == uss.clave && x.vigente == true);
                if (usuarioAc != null)
                {
                    respuestaVi.token = GeneratiToken(uss, configuration);
                    respuestaVi.mensaje = "Se inicio sesion exitosamente";
                    return respuestaVi;
                }
                else
                {
                    respuestaVi.mensaje = "No se encontro el usuario";
                    return respuestaVi;
                }
            }
            catch (Exception ex)
            {
                respuestaVi.mensaje = ex.Message;
                return respuestaVi;
            }
        }
        public string GeneratiToken(LoginDTO uss, IConfiguration configuration)
        {
            var token = configuration.GetSection("JWT:Key");
            JWTAutentication _jwt = new JWTAutentication();
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,uss.correo),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email,uss.correo),
            };

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(token.Value));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issure,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwt.DurationInMinutes),
                signingCredentials: signingCredentials
                );
            string tokenKey = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return tokenKey;
        }
    }
    public interface IUsaurioServices
    {
        Task<RespuestaDTO> Save(Usuario uss);
        Task<RespuestaDTO> Update(Usuario uss);
        Task<RespuestaDTO> Delete(int uss);
        Task<RespuestaDTO> LoginSession(LoginDTO uss, IConfiguration configuration);
    }
}
