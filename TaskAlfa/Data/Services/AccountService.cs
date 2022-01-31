using Task.Mail;
using TaskAlfa.Data.Models;
using TaskAlfa.Data.Services.MailingServices;
using TaskAlfa.PageModels.Account;
using Microsoft.AspNetCore.Components;
using System;
using System.Linq;
using System.Threading.Tasks;
using AlfaAmsDb;
using AlfaAmsDb.Models;
using DbRepository;

namespace TaskAlfa.Data.Services
{
    public class AccountService : MailingService
    {
        private AlfaAmsDbContext _dbContext;
        private EFRepository<BenutzerModel> repo;

        private MailingService mMailingService;
        private readonly NavigationManager NavigationManager;

        public AccountService(AlfaAmsDbContext context, MailingService mailingService, NavigationManager navigationManager, MailSettings mailSettings) : base(mailSettings)
        {
            _dbContext = context;
            repo = new EFRepository<BenutzerModel>(context);
            mMailingService = mailingService;
            NavigationManager = navigationManager;
        }

        public async Task<string> ResetPassword(string login)
        {
            var benutzerId = repo.Get().Where(x => x.Login == login)?.SingleOrDefault()?.BenutzerId;
            if (benutzerId == null)
            {
                return await System.Threading.Tasks.Task.FromResult("Ungültiger Benutzername");
            }

            var user = repo.FindById(benutzerId.GetValueOrDefault());
            user.ResetToken = Guid.NewGuid();
            user = repo.Update(user);

            var message = new MailItem()
            {
                ToEmail = user.Login,
                Subject = "AMS: Passwort wiederherstellen",
                Text = $"Bitte folgen Sie dem Link, um das neue Passwort festzulegen: <a href='{GetUrl(user.ResetToken.ToString())}'>Anmeldepasswort ändern</a>"
            };

            SendMessage(message);

            return null;
        }

        private string GetUrl(string guid)
        {
            var urlBase = new Uri(NavigationManager.BaseUri);
            return $"{urlBase}resetPassword/{guid}";
        }

        public async Task<string> SaveNewPassword(string guid, PasswordItemViewModel model)
        {
            var benutzerId = repo.Get().Where(x => x.ResetToken.ToString() == guid)?.SingleOrDefault()?.BenutzerId;
            if (benutzerId == null)
            {
                return await System.Threading.Tasks.Task.FromResult("Ungültiger Benutzername");
            }

            var user = repo.FindById(benutzerId.GetValueOrDefault());
            user.Passwort = model.Password;
            user.ResetToken = null;
            user = repo.Update(user);

            return null;
        }

        public async Task<bool> CheckToken(string guid)
        {
            var benutzerId = repo.Get().Where(x => x.ResetToken.ToString() == guid)?.FirstOrDefault()?.BenutzerId;
            if (benutzerId == null)
            {
                return await System.Threading.Tasks.Task.FromResult(false);
            }

            return await System.Threading.Tasks.Task.FromResult(true);
        }
    }
}