namespace IRunes.Controllers
{
    using SIS.HTTP.Requests.Contracts;
    using SIS.HTTP.Responses.Contracts;

    public class HomeController : BaseController
    {
        public IHttpResponse Index(IHttpRequest request)
        {
            if (base.IsAuthenticated(request))
            {
                string username = request.Session.GetParameter("username").ToString();
                base.ViewBag["username"] = username;

                return base.View("IndexLoggedIn");
            }

            return base.View();
        }
    }
}
