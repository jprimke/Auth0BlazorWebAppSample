using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BlazorApp4.Client
{
    public class PersistentAuthenticationStateProvider(PersistentComponentState persistentState) : AuthenticationStateProvider
    {
        private static readonly Task<AuthenticationState> _unauthenticatedTask =
            Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (!persistentState.TryTakeFromJson<UserInfo>(nameof(UserInfo), out var userInfo) || userInfo is null)
            {
                return _unauthenticatedTask;
            }

            List<Claim> claims =
            [
                new Claim(ClaimTypes.NameIdentifier, userInfo.UserId),
                new Claim(ClaimTypes.Name, userInfo.UserName),
                new Claim(ClaimTypes.Email, userInfo.Email)
            ];
            
            claims.AddRange(userInfo.Roles.Select(role => new Claim(ClaimTypes.Role, role)));

            return Task.FromResult
                (
                 new AuthenticationState
                     (
                      new ClaimsPrincipal
                          (
                           new ClaimsIdentity(claims, authenticationType: nameof(PersistentAuthenticationStateProvider))
                          )
                     )
                );
        }
    }
}
