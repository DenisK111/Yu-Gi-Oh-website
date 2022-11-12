using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Yu_Gi_Oh_website.Web.Models.Contracts;

namespace Yu_Gi_Oh_website.Web.Extentension
{
	public static class ControllerExtensions
	{
		public static string? GetUserId(this ControllerBase controller)
		{
            return controller.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public static string? GetUserEmail(this ControllerBase controller)
        {
            return controller.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
        }

       
    }
}
