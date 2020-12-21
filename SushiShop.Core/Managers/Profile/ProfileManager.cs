using SushiShop.Core.Data.Http;
using SushiShop.Core.Data.Models.Profile;
using SushiShop.Core.Providers;
using SushiShop.Core.Services.Http.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SushiShop.Core.Managers.Profile
{
    public class ProfileManager : IProfileManager
    {
        private readonly IProductsService productsService;
        private readonly IUserSession userSession;


        public ProfileManager(IProductsService productsService, IUserSession userSession)
        {
            this.productsService = productsService;
            this.userSession = userSession;
        }

        public async Task<Response<LoginProfile>> CheckLoginAsync(string login, bool? sendCode)
        {

        }

        public async Task<Response<AuthProfile>> AuthAsync(string login, string pass)
        {

        }

        public async Task<Response<RegistrationProfile>> RegistrationAsync(RegistrationData registrationData)
        {

        }

        public async Task<Response<PersonalData>> GetPersonalDataAsync()
        {

        }

        public async Task<Response<ProfileDiscount>> GetDiscountAsync()
        {

        }
    }
}
