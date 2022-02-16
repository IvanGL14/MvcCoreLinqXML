using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreLinqXML.Filters
{
    public class AuthorizeUsuariosAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            string controller = context.RouteData.Values["controller"].ToString();
            string action = context.RouteData.Values["action"].ToString();
            string idcurso = "";
            if (context.RouteData.Values.ContainsKey("idcurso"))
            {
                idcurso = context.RouteData.Values["idcurso"].ToString();
                //inscripcion = context.HttpContext.Request.QueryString["inscripcion"].ToString();
            }

            context.HttpContext.Response.Cookies.Append("controller", controller);
            context.HttpContext.Response.Cookies.Append("action", action);
            context.HttpContext.Response.Cookies.Append("idcurso", idcurso);

            if (user.Identity.IsAuthenticated == false)
            {
                context.Result = this.GetRouteRedirect("Manage", "Login");
            }


        }

        private RedirectToRouteResult GetRouteRedirect(string controller, string action)
        {
            RouteValueDictionary ruta = new RouteValueDictionary(new
            {
                controller = controller,
                action = action,
            });
            RedirectToRouteResult result = new RedirectToRouteResult(ruta);
            return result;
        }

       
    }
}


