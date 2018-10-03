using System;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace SampleApp.Api.Controllers
{
    public class BaseApiController : ApiController
    {
        private int _userId;
        private string _userName;

        protected int UserId
        {
            get
            {
                if (_userId == 0)
                {
                    _userId = User.Identity.GetUserId<int>();
                }
                return _userId;
            }
        }

        protected string UserName
        {
            get
            {
                if (String.IsNullOrEmpty(_userName))
                {
                    _userName = User.Identity.Name;
                }
                return _userName;
            }
        }
    }
}
