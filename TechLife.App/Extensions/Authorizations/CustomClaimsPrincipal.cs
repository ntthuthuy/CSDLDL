using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using TechLife.Common.Extension;
using TechLife.Data.Entities;
using TechLife.Service;
namespace TechLife.App.Extensions.Authorizations
{
    public class CustomClaimsPrincipal : ClaimsPrincipal
    {
        private readonly ClaimsPrincipal _principal;
        private readonly IUserService _userService;

        public CustomClaimsPrincipal(IUserService userService, ClaimsPrincipal principal) : base(principal)
        {
            _userService = userService;
            _principal = principal;
        }

        public override bool IsInRole(string role)
        {
            try
            {
                // Lấy ngày sinh của User (Identity có cấu hình bảng User có trường)
                //var taskgetuser = _userManager.GetUserAsync(user);
                //Task.WaitAll(taskgetuser);
                //var appuser = taskgetuser.Result;

                //if (appuser.BirthDate == null) return false;

                var taskResult = _userService.IsInRole(_principal.GetUserId(), role);

                Task.WaitAll(taskResult);
                //int year = appuser.BirthDate.Value.Year;
                //return (year >= require.MinYear && year <= require.MaxYear);

                return taskResult.Result;
            }
            catch
            {
                return false;
            }
        }

        public bool IsInRoles(params Role[] roles)
        {
            try
            {
                // Lấy ngày sinh của User (Identity có cấu hình bảng User có trường)
                //var taskgetuser = _userManager.GetUserAsync(user);
                //Task.WaitAll(taskgetuser);
                //var appuser = taskgetuser.Result;

                //if (appuser.BirthDate == null) return false;

                var taskResult = _userService.IsIsInRoles(_principal.GetUserId(), roles.Select(x=>x.Name));

                Task.WaitAll(taskResult);
                //int year = appuser.BirthDate.Value.Year;
                //return (year >= require.MinYear && year <= require.MaxYear);

                return taskResult.Result;
            }
            catch
            {
                return false;
            }
        }
        public bool IsInRoles(params string[] roles)
        {
            try
            {
                // Lấy ngày sinh của User (Identity có cấu hình bảng User có trường)
                //var taskgetuser = _userManager.GetUserAsync(user);
                //Task.WaitAll(taskgetuser);
                //var appuser = taskgetuser.Result;

                //if (appuser.BirthDate == null) return false;

                var taskResult = _userService.IsIsInRoles(_principal.GetUserId(), roles);

                Task.WaitAll(taskResult);
                //int year = appuser.BirthDate.Value.Year;
                //return (year >= require.MinYear && year <= require.MaxYear);

                return taskResult.Result;
            }
            catch
            {
                return false;
            }
        }
    }
}