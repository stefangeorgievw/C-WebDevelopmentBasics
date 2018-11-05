namespace IRunes.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Runtime.CompilerServices;

    using Data;
    using Services;
    using Services.Contracts;
    using SIS.HTTP.Cookies;
    using SIS.HTTP.Requests.Contracts;
    using SIS.HTTP.Responses.Contracts;
    using SIS.WebServer.Results;

    public abstract class BaseController
    {
        private const string RootDirectoryRelativePath = "../../../";

        private const string ControllerDefaultName = "Controller";

        private const string DirectorySeparator = "/";

        private const string ViewsFolderName = "Views";

        private const string HtmlFileExtension = ".html";

        private readonly IUserCookieService userCookieService;

        public BaseController()
        {
            this.userCookieService = new UserCookieService();
            this.DbContext = new IRunesDbContext();
            this.ViewBag = new Dictionary<string, string>();
        }

        protected IRunesDbContext DbContext { get; }

        protected IDictionary<string, string> ViewBag { get; set; }

        protected IHttpResponse View([CallerMemberName]string viewName = "")
        {
            // ../../../Views/ControllerName/ActionName.html
            string filePath = RootDirectoryRelativePath + ViewsFolderName + DirectorySeparator +
                this.GetCurrentControllerName() + DirectorySeparator + viewName + HtmlFileExtension;

            if (!File.Exists(filePath))
                return new HtmlResult($"View {viewName} not found.", HttpStatusCode.NotFound);

            string fileContent = File.ReadAllText(filePath);

            foreach (string viewBagKey in this.ViewBag.Keys)
            {
                string dynamicDataPlaceholder = $"{{{{{viewBagKey}}}}}";
                if (fileContent.Contains(dynamicDataPlaceholder))
                {
                    fileContent = fileContent.Replace(dynamicDataPlaceholder, this.ViewBag[viewBagKey]);
                }
            }

            var response = new HtmlResult(fileContent, HttpStatusCode.OK);
            return response;
        }

        private string GetCurrentControllerName()
        {
            string result = this.GetType().Name.Replace(ControllerDefaultName, String.Empty);
            return result;
        }

        protected void SignInUser(string username, IHttpRequest request, IHttpResponse response)
        {
            request.Session.AddParameter("username", username);

            string userCookieValue = this.userCookieService.GetUserCookie(username);
            response.Cookies.Add(new HttpCookie("IRunes_auth", userCookieValue));
        }

        protected bool IsAuthenticated(IHttpRequest request)
        {
            return request.Session.ContainsParameter("username");
        }
    }
}
