using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atom.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace QtechAuthDemo.Controllers
{
    public class Ids4Controller : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            Console.WriteLine("************************************************");

            //string id_token = base.HttpContext.Request.Cookies["id_token"];
            //var token_parts = id_token.Split('.');
            //var header = Encoding.UTF8.GetString(Base64Url.Decode(token_parts[0]));
            //var claims = Encoding.UTF8.GetString(Base64Url.Decode(token_parts[1]));
            //var sign = Encoding.UTF8.GetString(Base64Url.Decode(token_parts[2]));
            //Console.WriteLine(header);
            //Console.WriteLine(claims);
            //Console.WriteLine(sign);


            //打印 基本信息
            foreach (var item in base.HttpContext.User.Identities.First().Claims)
            {
                Console.WriteLine($"{item.Type}:{item.Value}");
            }
            Console.WriteLine("************************************************");
            return View();
        }


        [Authorize(Roles = "Admin")]
        public IActionResult IndexRole()
        {
            return View();
        }


        [Authorize(Policy = "eMailPolicy")]
        public IActionResult IndexPolicy()
        {
            return View();
        }

        [Authorize(Policy = "DoubleEmail")]
        public IActionResult IndexPolicyDouble()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult IndexToken()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult IndexCodeToken()
        {
            return View();
        }
    }
}
