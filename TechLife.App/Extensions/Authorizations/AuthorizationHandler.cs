using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using TechLife.Common.Extension;
using TechLife.Service;

namespace TechLife.App.Extensions.Authorizations
{
    public class AuthorizationHandler : IAuthorizationHandler
    {
        private readonly IUserService _userService;
        private readonly ILogger<AuthorizationHandler> _logger;

        // Inject UserManager vào AppAuthorizationHandler
        public AuthorizationHandler(IUserService userService
            , ILogger<AuthorizationHandler> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            // lấy các requirement chưa được kiểm tra trong ngữ cảnh xác thực hiện tại
            var pendingRequirements = context.Requirements.ToList();
            foreach (var requirement in pendingRequirements)
            {
                if (requirement is RolesAuthorizationRequirement)
                {
                    if (IsInRoleRequirement(context.User.GetUserId(), context.Resource, requirement))
                    {
                        context.Succeed(requirement);
                    }
                    else
                    {
                        context.Fail();
                    }
                }
            }

            return Task.CompletedTask;
        }

        // Phương thức kiểm tra user có đáp ứng requirement GenZrequirement
        private bool IsInRoleRequirement(Guid userId, object resource, IAuthorizationRequirement requirement)
        {
            try
            {
                var require = requirement as RolesAuthorizationRequirement;

                var taskResult = _userService.IsContantRoles(userId, require.AllowedRoles);

                Task.WaitAll(taskResult);

                return taskResult.Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Đã có lỗi xãy ra khi xác thực vai trò");

                return false;
            }
        }
    }
}