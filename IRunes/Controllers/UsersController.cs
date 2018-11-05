namespace IRunes.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    
    using Models;
    using Services;
    using Services.Contracts;
    using SIS.HTTP.Requests.Contracts;
    using SIS.HTTP.Responses.Contracts;
    using SIS.WebServer.Results;

    public class UsersController : BaseController
    {
        private IHashService hashService;

        public UsersController()
        {
            this.hashService = new HashService();
        }

        public IHttpResponse Login(IHttpRequest request)
        {
            return base.View();
        }

        public IHttpResponse DoLogin(IHttpRequest request)
        {
            string usernameOrEmail = request.FormData["usernameOrEmail"].ToString().Trim();
            string password = request.FormData["password"].ToString();

            string hashedPassword = this.hashService.Hash(password);

            User user = base.DbContext.Users.FirstOrDefault(
                x => (x.Username == usernameOrEmail || x.Email == usernameOrEmail) && x.Password == hashedPassword);

            if (user == null)
                return new RedirectResult("/Users/Login");

            var response = new RedirectResult("/");
            base.SignInUser(user.Username, request, response);

            return response;
        }

        public IHttpResponse Logout(IHttpRequest request)
        {
            if (!base.IsAuthenticated(request))
                return new RedirectResult("/Users/Login");

            request.Session.CleanParameters();

            return new RedirectResult("/");
        }

        public IHttpResponse Register(IHttpRequest request)
        {
            return base.View();
        }

        public IHttpResponse DoRegister(IHttpRequest request)
        {
            string username = request.FormData["username"].ToString().Trim();
            string password = request.FormData["password"].ToString();
            string confirmPassword = request.FormData["confirmPassword"].ToString();
            string email = request.FormData["email"].ToString().Trim();

            if (String.IsNullOrWhiteSpace(username))
                return new RedirectResult("/Users/Register");

            if (base.DbContext.Users.Any(x => x.Username == username))
                return new RedirectResult("/Users/Register");

            if (String.IsNullOrEmpty(password))
                return new RedirectResult("/Users/Register");

            if (password != confirmPassword)
                return new RedirectResult("/Users/Register");

            if (String.IsNullOrWhiteSpace(email))
                return new RedirectResult("/Users/Register");

            if (base.DbContext.Users.Any(x => x.Email == email))
                return new RedirectResult("/Users/Register");

            string hashedPassword = this.hashService.Hash(password);

            var user = new User
            {
                Username = username,
                Password = hashedPassword,
                Email = email
            };

            base.DbContext.Users.Add(user);

            try
            {
                base.DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return new HtmlResult(ex.Message, HttpStatusCode.InternalServerError);
            }

            var response = new RedirectResult("/");
            base.SignInUser(username, request, response);

            return response;
        }
    }
}