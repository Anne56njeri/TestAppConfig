using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.Mvc;

namespace TestAppConfig.Controllers
{
     public class BetaController: Controller
    {
        private readonly IFeatureManager _featureManager;
        // Dependecy Injection
        public BetaController(IFeatureManagerSnapshot featureManager)
        {
            _featureManager = featureManager;
        }
        // use a feature gate attribute to control whether a whole controller 
        // class or specific action is enabled
        // this controller requires this feature to be on before any action the controller 
        // class contains is on otherwise if its off then actions in the controller cass will not be executed
        [FeatureGate(MyFeatureFlags.Beta)]
        public IActionResult Index()
        {
            return View();
        }
    }
}