using FinanceApp.Models;
using FinanceApp.Repository;
using FinanceApp.Utils.Security;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FinanceApp.Services.InterfacesImpl
{
    public class AuthenticationService : IAuthenticationService
    {
        public IAziendeRepository aziendeRepository { get; set; }
        JwtSettings jwtSettings { get; set; }

        public AuthenticationService(IAziendeRepository repository, IOptions<JwtSettings> jwtSettings) 
        {
            this.aziendeRepository = repository;
            this.jwtSettings = jwtSettings.Value;
        }

        //Selezionatori entità:

        public async Task<List<AziendaDTO>> SelezionaElencoEntitaAziende()
        {
            List<AziendaDTO>? listaAziende = await aziendeRepository.SelezionaAziende();

            if(listaAziende == null)
            {
                throw new Exception("Lista non trovata");
            }

            return listaAziende;
        }

        /*================================================================*/

        public async Task<AziendaDTO> Autenticazione(string username, string password)
        {
            List<AziendaDTO> listaAziende = await this.SelezionaElencoEntitaAziende();
            AziendaDTO? aziendaCorrente = listaAziende.FirstOrDefault(element => element.AccountAzienda.Equals(username) && element.PswAzienda.Equals(password));
            
            if(aziendaCorrente == null)
            {
                throw new Exception();
            }

            return aziendaCorrente;

        }

        public async Task<bool> Registrazione(AziendaDTO nuovaAzienda)
        {
            List<AziendaDTO> listaAziende = await this.SelezionaElencoEntitaAziende();

            if (!listaAziende.Exists(element => element.AccountAzienda.Equals(nuovaAzienda.AccountAzienda)))
            {
                await aziendeRepository.AggiungiAzienda(nuovaAzienda);
                return true;

            } else
            {
                await Console.Out.WriteLineAsync("Account già esistente");
                return false;

            }
        }
        public async Task<Dictionary<string, string>> GetToken(int IdAzienda)
        {
            AziendaDTO aziendaCorrente = await aziendeRepository.SelezionaAziendaPerID(IdAzienda);
            byte[] chiaveSegreta = Encoding.ASCII.GetBytes(this.jwtSettings.Secret);

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("Id", aziendaCorrente.IdAzienda.ToString()));
            claims.Add(new Claim("Account", aziendaCorrente.AccountAzienda));

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            SecurityToken accessToken = tokenHandler.CreateToken(AccessToken(claims, chiaveSegreta));
            SecurityToken refreshToken = tokenHandler.CreateToken(RefreshToken(claims, chiaveSegreta));

            Dictionary<string, string> tokenPair = new Dictionary<string, string>();
            tokenPair.Add("AccessToken", tokenHandler.WriteToken(accessToken));
            tokenPair.Add("RefreshToken", tokenHandler.WriteToken(refreshToken));

            return tokenPair;
        }

        public SecurityTokenDescriptor AccessToken(List<Claim> claims, byte[] chiaveSegreta)
        {
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddSeconds(this.jwtSettings.Expire),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(chiaveSegreta), SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenDescriptor;
        }

        public SecurityTokenDescriptor RefreshToken(List<Claim> claims, byte[] chiaveSegreta)
        {
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(this.jwtSettings.Refresh),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(chiaveSegreta), SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenDescriptor;
        }
    }
}
