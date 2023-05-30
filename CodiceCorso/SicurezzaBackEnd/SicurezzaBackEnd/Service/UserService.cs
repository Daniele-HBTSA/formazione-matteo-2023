
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Models;
using Security;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly AlphaShopDbContext alphaShopDbContext;
        public UserService(AlphaShopDbContext alphaShopDbContext)
        {
            this.alphaShopDbContext = alphaShopDbContext;
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
    }
}
