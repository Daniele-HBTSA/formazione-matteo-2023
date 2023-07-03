using Models;
using Security;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using SicurezzaBackEnd.Security;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly AlphaShopDbContext alphaShopDbContext;
        private JwtSettings jwtSettings;

        public UserService(AlphaShopDbContext alphaShopDbContext, JwtSettings jwtSettings)
        {
            this.alphaShopDbContext = alphaShopDbContext;
            this.jwtSettings = jwtSettings;
        }

        public async Task<Utenti> GetUser(string UserId)
        {
            return await this.alphaShopDbContext.Utenti
                .Where(c => c.UserId == UserId)
                .Include(r => r.Profili)
                .FirstOrDefaultAsync();
        }

        public async Task<Utenti> GetUserToDelete(string UserId)
        {
            return await this.alphaShopDbContext.Utenti
                //.AsNoTracking()
                .Where(c => c.UserId == UserId)
                .FirstOrDefaultAsync();
        }

        public async Task<Utenti> GetUserByCodFid(string CodFid)
        {
            return await this.alphaShopDbContext.Utenti
                //.AsNoTracking()
                .Where(c => c.CodFidelity  == CodFid)
                .FirstOrDefaultAsync();
        }

        public async Task<ICollection<Utenti>> GetAll()
        {
            return await this.alphaShopDbContext.Utenti
                .Include(r => r.Profili)
                .OrderBy(c => c.UserId)
                .ToListAsync();
        }
        
        public async Task<bool> Authenticate(string username, string password)
        {
            bool retVal = false;

            PasswordHasher Hasher = new PasswordHasher();

            Utenti utente = await this.GetUser(username);

            if (utente != null)
            {
                string EncryptPwd = utente.Password;
                retVal = Hasher.Check(EncryptPwd, password).Verified;
            }
            
            return retVal; 
        }

        public async Task<bool> InsUtente(Utenti utente)
        {
            this.alphaShopDbContext.Add(utente);
            return await Salva();
        }

        public async Task<bool> UpdUtente(Utenti utente)
        {
            this.alphaShopDbContext.Update(utente);
            return await Salva();
        }

        public async Task<bool> DelUtente(Utenti utente)
        {
            this.alphaShopDbContext.Remove(utente);
            return await Salva();
        }

        private async Task<bool> Salva()
        {
            var saved = await this.alphaShopDbContext.SaveChangesAsync();
            return saved >= 0 ? true : false; 
        }

        public async Task<string> GetToken(string userId)
        {
            //Importiamo l'entità dell'utente
            Utenti utente = await this.GetUser(userId);

            //Importiamo la chiave segreta
            byte[] key = Encoding.ASCII.GetBytes(this.jwtSettings.Secret);

            //Payload
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, utente.UserId)); //settiamo l'elemento "name" del token al nome utente

            //Identity
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor() 
            { 
                Subject = new ClaimsIdentity(claims),

                //Scandenza del token, partendo dall'ora di creazione
                Expires = DateTime.UtcNow.AddSeconds(this.jwtSettings.Expiration),

                //Codifica del token al quale passiamo la chiave segreta e il tipo di codifica
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
