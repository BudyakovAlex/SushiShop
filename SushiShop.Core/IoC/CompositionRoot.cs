using Acr.UserDialogs;
using Plugin.Media;
using SushiShop.Core.Factories.Cart;
using SushiShop.Core.Managers.Cart;
using SushiShop.Core.Managers.Cities;
using SushiShop.Core.Managers.CommonInfo;
using SushiShop.Core.Managers.Feedback;
using SushiShop.Core.Managers.Menu;
using SushiShop.Core.Managers.Orders;
using SushiShop.Core.Managers.Products;
using SushiShop.Core.Managers.Profile;
using SushiShop.Core.Managers.Promotions;
using SushiShop.Core.Providers;
using SushiShop.Core.Services.Http;
using SushiShop.Core.Services.Http.Cart;
using SushiShop.Core.Services.Http.Cities;
using SushiShop.Core.Services.Http.CommonInfo;
using SushiShop.Core.Services.Http.Feedback;
using SushiShop.Core.Services.Http.Menu;
using SushiShop.Core.Services.Http.Orders;
using SushiShop.Core.Services.Http.Products;
using SushiShop.Core.Services.Http.Profile;
using SushiShop.Core.Services.Http.Promotions;

namespace SushiShop.Core.IoC
{
    public class CompositionRoot : BuildApps.Core.Mobile.MvvmCross.IoC.CompositionRoot
    {
        protected override void RegisterManagers()
        {
            Container.RegisterSingleton<ICitiesManager, CitiesManager>();
            Container.RegisterSingleton<IMenuManager, MenuManager>();
            Container.RegisterSingleton<ICommonInfoManager, CommonInfoManager>();
            Container.RegisterSingleton<IPromotionsManager, PromotionsManager>();
            Container.RegisterSingleton<IProductsManager, ProductsManager>();
            Container.RegisterSingleton<ICartManager, CartManager>();
            Container.RegisterSingleton<IOrdersManager, OrdersManager>();
            Container.RegisterSingleton<IFeedbackManager, FeedbackManager>();
            Container.RegisterSingleton<IProfileManager, ProfileManager>();
        }

        protected override void RegisterServices()
        {
            Container.RegisterSingleton<IHttpService, HttpService>();
            Container.RegisterSingleton<IMenuService, MenuService>();
            Container.RegisterSingleton<ICitiesService, CitiesService>();
            Container.RegisterSingleton<ICommonInfoService, CommonInfoService>();
            Container.RegisterSingleton<IPromotionsService, PromotionsService>();
            Container.RegisterSingleton<IProductsService, ProductsService>();
            Container.RegisterSingleton<ICartService, CartService>();
            Container.RegisterSingleton<IOrdersService, OrdersService>();
            Container.RegisterSingleton<IFeedbackService, FeedbackService>();
            Container.RegisterSingleton<IProfileService, ProfileService>();
        }

        protected override void RegisterFactories()
        {
            base.RegisterFactories();
            Container.RegisterSingleton<ICartItemsViewModelsFactory, CartItemsViewModelsFactory>();
        }

        protected override void RegisterDependencies()
        {
            base.RegisterDependencies();

            Container.RegisterSingleton<IUserSession, UserSession>();
            Container.RegisterSingleton(() => CrossMedia.Current);
            Container.RegisterSingleton(() => UserDialogs.Instance);
        }
    }
}